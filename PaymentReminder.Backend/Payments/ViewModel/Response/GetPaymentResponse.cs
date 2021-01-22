using System;

namespace PaymentReminder.Backend.Payments.ViewModel.Response
{
    public class GetPaymentResponse
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
    }
}
