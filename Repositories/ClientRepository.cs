
using System.Linq;
using System.Transactions;
using KolokwiumCF.Models;
using KolokwiumCF.Models_.DTOs;
using Microsoft.EntityFrameworkCore;

public class ClientRepository : IClientRepository
{
    private S26225Context _context = new S26225Context();
    public ClientRepository()
    {
    }

    public ClientDTO GetClients(int id)
    {
        var client = _context.Clients.Where(c => c.IdClient==id).FirstOrDefault();

        ClientDTO clientDTO = new ClientDTO();

        clientDTO.firstName = client.FirstName;
        clientDTO.lastName = client.LastName;
        clientDTO.email = client.Email;
        clientDTO.phone = client.Phone;
        
        var discount = _context.Discounts.Where(d => d.IdClient==id).FirstOrDefault();

        clientDTO.discount = null;

        if (discount != null)
        {
            clientDTO.discount = discount.Value;
        }

        var sales = _context.Sales.Where(s => s.IdClient == id).Select(s => s.IdSubscription);

        var subscriptions = _context.Subscriptions.Where(s => sales.Contains(s.IdSubscription)).ToArray();

        List<SubscriptionDTO> toAdd = new List<SubscriptionDTO>();

        foreach (var sub in subscriptions)
        {
            SubscriptionDTO subscriptionDTO = new SubscriptionDTO();

            subscriptionDTO.IdSubscription = sub.IdSubscription;
            subscriptionDTO.Name = sub.Name;
            subscriptionDTO.RenewalPeriod = sub.RenewalPeriod;
            subscriptionDTO.TotalPaidAmount = (int)_context.Payments.Where(p => p.IdSubscription == sub.IdSubscription).Sum(p => p.Value);

            toAdd.Add(subscriptionDTO);
        }

        clientDTO.subscriptions = toAdd;

        return clientDTO;
    }

    public void PostPayment(PaymentDTO paymentDTO)
    {
        var client = _context.Clients.Where(c => c.IdClient == paymentDTO.IdClient).FirstOrDefault();

        //1
        if (client == null) {
            throw new Exception("Klient nie istnieje");
        }

        var sub = _context.Subscriptions.Where(s => s.IdSubscription == paymentDTO.IdSubscription).FirstOrDefault();
        //2
        if (sub == null)
        {
            throw new Exception("Subskrypcja nie istnieje");
        }
        //3
        if (sub.EndTime >  DateTime.UtcNow)
        {
            throw new Exception("Subskrypcja nie aktywna");
        }

        //4.
        var Period = sub.RenewalPeriod;
        var CreatedAt = _context.Sales.Where(s => s.IdSubscription == sub.IdSubscription).FirstOrDefault().CreatedAt;

        if (DateTime.UtcNow < CreatedAt.AddMonths(Period)) {
            throw new Exception("Subskrypcj jest już opłacona za ten okers. Następną opłatę należy dokonać po: " + CreatedAt.AddMonths(Period).ToString());
        }


        //5.
        if (sub.Price != paymentDTO.Payment)
        {
            throw new Exception("Zła kwota płatności. Poprawna kwota za subskrypcję to: " + sub.Price);
        }

        //6.
        int discount = _context.Discounts.Where(d => d.IdClient == paymentDTO.IdClient).Sum(d => d.Value);

        if (discount > 0)
        {
            if (discount > 50)
            {
                discount = 50;
            }

            paymentDTO.Payment = paymentDTO.Payment * (discount) / 100;
        }

        Payment payment = new Payment();

        payment.IdClient = paymentDTO.IdClient;
        payment.IdSubscription = paymentDTO.IdSubscription;
        payment.Value = paymentDTO.Payment;
        payment.Date = DateTime.UtcNow;

        payment.IdPayment = _context.Payments.Max(d => d.IdPayment) + 1;

        _context.Payments.Add(payment);

        _context.SaveChanges();
    }
}