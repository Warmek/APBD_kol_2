using KolokwiumCF.Models_.DTOs;

public interface IClientRepository
{
    public ClientDTO GetClients(int id);
    public void PostPayment(PaymentDTO paymentDTO);
}