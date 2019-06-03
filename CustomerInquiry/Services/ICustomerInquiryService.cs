using CustomerInquiry.Requests;
using CustomerInquiry.Responses;

namespace CustomerInquiry.Services
{
    public interface ICustomerInquiryService
    {
        CustomerInquiryResponse GetCustomerInquiry(CustomerInquiryRequest request);
    }
}
