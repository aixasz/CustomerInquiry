using CustomerInquiry.Models;
using CustomerInquiry.Requests;
using CustomerInquiry.Responses;

namespace CustomerInquiry.Services
{
    public class CustomerInquiryService : ICustomerInquiryService
    {
        private readonly CustomerInquiryContext _dbContext;

        public CustomerInquiryService(CustomerInquiryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerInquiryResponse GetCustomerInquiry(CustomerInquiryRequest request)
        {
            ValidateInquiry(request);

            if (request.customerID > 0 && !string.IsNullOrEmpty(request.email))
            {
                return null;
            }
            else if (request.customerID > 0)
            {
                return null;
            }
            else if (!string.IsNullOrEmpty(request.email))
            {
                return null;
            }

            return null;
        }

        private static void ValidateInquiry(CustomerInquiryRequest request)
        {
            if (string.IsNullOrEmpty(request.email) && request.customerID == null)
            {
                // No criteria
            }

            if (request.customerID != null && request.customerID < 1)
            {
               // invalid customer id        
            }

            if (!string.IsNullOrEmpty(request.email))
            {
                // invalid email
            }
        }
    }
}
