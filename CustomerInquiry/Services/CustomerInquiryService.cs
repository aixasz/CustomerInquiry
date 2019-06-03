using CustomerInquiry.Models;
using CustomerInquiry.Requests;
using CustomerInquiry.Responses;
using Microsoft.EntityFrameworkCore;
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
                return _dbContext.Customers
                    .Include(x => x.Transactions)
                    .Where(customer => customer.CustomerID == request.customerID && customer.Email == request.email)
                    .Select(customer => new CustomerInquiryResponse
                    {
                        customerID = customer.CustomerID,
                        email = customer.Email,
                        mobile = customer.Mobile,
                        name = customer.Name,
                        transactions = customer.Transactions.Select(transaction => ToTransactionResponse(transaction)).ToArray()
                    }).FirstOrDefault();
            }
            else if (request.customerID > 0)
            {
                return _dbContext.Customers
                    .Where(customer => customer.CustomerID == request.customerID)
                    .Select(customer => new CustomerInquiryResponse
                    {
                        customerID = customer.CustomerID,
                        email = customer.Email,
                        mobile = customer.Mobile,
                        name = customer.Name,
                        transactions = Array.Empty<TransactionResponse>()
                    }).FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(request.email))
            {
                return _dbContext.Customers
                    .Include(x => x.Transactions)
                    .Where(customer => customer.Email == request.email)
                    .Select(customer => new CustomerInquiryResponse
                    {
                        customerID = customer.CustomerID,
                        email = customer.Email,
                        mobile = customer.Mobile,
                        name = customer.Name,
                        transactions = GetFirstSuccessTransaction(customer)
                    }).FirstOrDefault();
            }

            return null;
        }

        private static TransactionResponse[] GetFirstSuccessTransaction(Customer customer)
        {
            var result = customer.Transactions
                                .Select(transaction => ToTransactionResponse(transaction))
                                .FirstOrDefault(transaction => transaction.status == TransactionStatus.Success);
            return result != null
                ? new[] { result }
                : Array.Empty<TransactionResponse>();
        }

        private static TransactionResponse ToTransactionResponse(Transaction transaction)
        {
            return new TransactionResponse
            {
                amount = transaction.Amount,
                currency = transaction.Currency,
                date = transaction.Date.ToString("dd/MM/yyyy hh:mm"),
                id = transaction.Id,
                status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), transaction.Status)
            };
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
