using PaymentReminder.Backend.Payments.Mappers;
using PaymentReminder.Backend.Payments.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentReminder.Backend.Payments.Data
{
    public class PaymentRepository : IPaymentRepository
    {
        /// <summary>
        /// Get All payments
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            try
            {               
                return await JsonSerializer
                .DeserializeAsync<IEnumerable<Payment>>(File.OpenRead("Payments/Data/payments.json"));

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Update Payment Status
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Payment>> UpdatePayment(UpdatePayment request)
        {
            PaymentMapper mapPayment = new PaymentMapper();

            try
            { 
                string readJsonString = File.ReadAllText("Payments/Data/payments.json");
                var paymentList = JsonSerializer.Deserialize<IEnumerable<Payment>>(readJsonString);

                GC.Collect();
                GC.WaitForPendingFinalizers();                

                var paymentValues = mapPayment.MapPaymentsForUpdate(paymentList);

                foreach (var item in paymentValues.Where(data => data.Id == request.Id)) { item.Status = "Paid"; }

                string jsonString = JsonSerializer.Serialize(paymentValues, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                File.WriteAllText("Payments/Data/payments.json", jsonString);

                return await JsonSerializer
                .DeserializeAsync<IEnumerable<Payment>>(File.OpenRead("Payments/Data/payments.json"));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally 
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
