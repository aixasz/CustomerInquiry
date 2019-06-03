using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.Responses
{
    public class CustomerInquiryResponse
    {
        public int customerID { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public TransactionResponse[] transactions { get; set; }
    }
}
