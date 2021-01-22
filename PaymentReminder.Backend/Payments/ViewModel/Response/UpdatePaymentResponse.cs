using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentReminder.Backend.Payments.ViewModel.Response
{
    public class UpdatePaymentResponse
    {
         public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
    }
}
