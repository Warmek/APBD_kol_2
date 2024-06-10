using Microsoft.AspNetCore.Mvc;
using KolokwiumCF.Models;
using KolokwiumCF.Models_.DTOs;

namespace Zadanie7.Controllers
{
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private IRepositoryService _repositoryService = new RepositoryService();

        public SubscriptionController()
        {
        }


        [HttpGet]
        [Route("api/clients")]
        public ActionResult<IEnumerable<ClientDTO>> Get(int idKilenta)
        {
            try
            {
                return Ok(_repositoryService.GetClients(idKilenta));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/payments")]
        public ActionResult SavePayment(PaymentDTO paymentDTO)
        {
            try
            {
                _repositoryService.PostPayment(paymentDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}