
using KolokwiumCF.Models_.DTOs;

public class RepositoryService : IRepositoryService
{
    private IClientRepository _tripRepository = new ClientRepository();
    public ClientDTO GetClients(int id)
    {
        return _tripRepository.GetClients(id);
    }

    public void PostPayment(PaymentDTO payment)
    {
        _tripRepository.PostPayment(payment);
    }
}