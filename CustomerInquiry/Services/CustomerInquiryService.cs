using CustomerInquiry.Models;
using CustomerInquiry.Requests;
using CustomerInquiry.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
                throw new Exception("No inquiry criteria");
            }
            var invalidMessages = new List<string>();
            if (request.customerID != null && request.customerID < 1)
            {
                invalidMessages.Add("Invalid Customer ID");
            }

            var emailAttribute = new EmailAddressAttribute();
            if (!string.IsNullOrEmpty(request.email) && !emailAttribute.IsValid(request.email))
            {
                invalidMessages.Add("Invalid Email");
            }

            if (invalidMessages.Any())
            {
                var message = string.Join(", ", invalidMessages);
                throw new Exception(message);
            }
        }
    }
}
