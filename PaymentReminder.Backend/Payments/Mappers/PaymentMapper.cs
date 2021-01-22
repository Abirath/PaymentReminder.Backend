using PaymentReminder.Backend.Payments.ViewModel.Response;
using PaymentReminder.Backend.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using PaymentReminder.Backend.Payments.ViewModel.Request;
using System.Text.RegularExpressions;

namespace PaymentReminder.Backend.Payments.Mappers
{
    public class PaymentMapper
    {
        /// <summary>
        /// Maps payment list to payment response - Get
        /// </summary>
        /// <param name="paymentList"></param>
        /// <returns>Ordered list of Payments</returns>
        public IEnumerable<GetPaymentResponse> MapPayments(Task<IEnumerable<Payment>> paymentList)
        {
            List<GetPaymentResponse> listPayment = new List<GetPaymentResponse>();
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            foreach (var item in paymentList.Result.ToList())
            {
                GetPaymentResponse payment = new GetPaymentResponse();

                payment.Id = item.Id.ToString();
                payment.DueDate = item.DueDate;
                payment.Status = item.Status;
                payment.Category = item.Category;
                payment.Description = item.Description;
                payment.Amount = Math.Round(item.Amount/100,2).ToString("C2",CultureInfo.CurrentCulture);

                listPayment.Add(payment);
            }

            return listPayment.OrderBy(date => date.DueDate.Year)
                    .ThenBy(date => date.DueDate.Month)
                    .ThenBy(date => date.DueDate.Day);
        }

        /// <summary>
        /// Maps the json data to class objects for Status Update
        /// </summary>
        /// <param name="paymentList"></param>
        /// <returns>listPayment</returns>
        public IEnumerable<UpdatePaymentResponseModel> MapPaymentsForUpdate(IEnumerable<Payment> paymentList)
        {
            List<UpdatePaymentResponseModel> listPayment = new List<UpdatePaymentResponseModel>();

            foreach (var item in paymentList.ToList())
            {
                UpdatePaymentResponseModel payment = new UpdatePaymentResponseModel();

                payment.Id = item.Id.ToString();
                payment.DueDate = item.DueDate.ToString("yyyy-MM-dd");
                payment.Status = item.Status;
                payment.Category = item.Category;
                payment.Description = item.Description;
                payment.Amount = item.Amount;

                listPayment.Add(payment);
            }

            return listPayment;
        }

        /// <summary>
        /// Maps Input parameters for Update Payments
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpdatePayment UpdateRequestMapper(UpdatePaymentRequest request) 
        {
            UpdatePayment updatePayment = new UpdatePayment();
            int formatMoney;

            if (request.Id.Contains("\""))
            {
                request.Id = Regex.Replace(request.Id, "\"", "");
                request.Amount = Regex.Replace(request.Amount, "\"", "");
            }            

            formatMoney = (int)(Convert.ToDecimal((request.Amount.Substring(1, request.Amount.Length - 1)).Replace(",", "").Trim()) * 100);             

            updatePayment.Amount = formatMoney;
            updatePayment.Id = request.Id;

            return updatePayment;
        }

        /// <summary>
        /// Maps updated payment list to response model class 
        /// </summary>
        /// <param name="updatePayment"></param>
        /// <returns></returns>
        public IEnumerable<UpdatePaymentResponse> UpdatePaymentResponseMapper(Task<IEnumerable<Payment>> updatePayment) 
        {
            List<UpdatePaymentResponse> updatedPayments = new List<UpdatePaymentResponse>();
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            foreach (var item in updatePayment.Result.ToList())
            {
                UpdatePaymentResponse payment = new UpdatePaymentResponse();

                payment.Id = item.Id.ToString();
                payment.DueDate = item.DueDate;
                payment.Status = item.Status;
                payment.Category = item.Category;
                payment.Description = item.Description;
                payment.Amount = Math.Round(item.Amount / 100, 2).ToString("C2", CultureInfo.CurrentCulture);

                updatedPayments.Add(payment);
            }

            return updatedPayments.OrderBy(date => date.DueDate.Year)
                    .ThenBy(date => date.DueDate.Month)
                    .ThenBy(date => date.DueDate.Day);

        }
    }
}
