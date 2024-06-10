using KolokwiumCF.Models_.DTOs;

public interface IRepositoryService
{
    public ClientDTO GetClients(int id);
    public void PostPayment(PaymentDTO payment);
}