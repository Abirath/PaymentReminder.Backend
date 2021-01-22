using PaymentReminder.Backend.Payments.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentReminder.Backend.Payments.Data
{
    public interface IPaymentRepository
    {
        /// <summary>
        /// Gets all payments
        /// </summary>
        /// <returns>List of ordered payments</returns>
        Task<IEnumerable<Payment>> GetPayments();

        /// <summary>
        /// Updates the payment status
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of Updated payments</returns>
        Task<IEnumerable<Payment>> UpdatePayment(UpdatePayment request);
    }
}
