using System.ComponentModel.DataAnnotations;

namespace CustomerInquiry.Requests
{
    public class CustomerInquiryRequest
    {
        public int? customerID { get; set; }
        public string email { get; set; }
    }
}
