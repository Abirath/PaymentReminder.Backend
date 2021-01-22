using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentReminder.Backend.Payments.Data;
using PaymentReminder.Backend.Payments.Mappers;
using PaymentReminder.Backend.Payments.Model;
using PaymentReminder.Backend.Payments.ViewModel.Request;
using PaymentReminder.Backend.Payments.ViewModel.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentReminder.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PaymentController : ControllerBase
    {
        private IPaymentRepository _repository;
        PaymentMapper mapPayment = new PaymentMapper();

        public PaymentController(IPaymentRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<PaymentController>
        [HttpGet]
        public IEnumerable<GetPaymentResponse> GetAllPayments()
        {
            Task<IEnumerable<Payment>> paymentList = null;
            
            try
            {
                paymentList = _repository.GetPayments();
                var orderedPayment = mapPayment.MapPayments(paymentList);

                return orderedPayment;

            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // PUT api/<PaymentController>
        [HttpPut]
        public IEnumerable<UpdatePaymentResponse> UpdatePayments([FromBody] UpdatePaymentRequest updatePaymentRequest)
        {
            try
            {
                var requestValues = mapPayment.UpdateRequestMapper(updatePaymentRequest);
                    
                //decimal amount = requestValues.Amount;
                    
                if (requestValues.Amount > 100000)
                {
                    throw new ArgumentException($"You don't have enough funds to make this payment");
                }

                var updatePayment = _repository.UpdatePayment(requestValues);
                var updatedPayments = mapPayment.UpdatePaymentResponseMapper(updatePayment);                  

                return updatedPayments;                               
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}
