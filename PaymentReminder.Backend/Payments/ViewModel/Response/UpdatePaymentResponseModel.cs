using System;
namespace PaymentReminder.Backend.Payments.ViewModel.Response
{
    public class UpdatePaymentResponseModel
    {
        public string Id { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
