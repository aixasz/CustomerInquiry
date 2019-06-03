using CustomerInquiry.Requests;
using CustomerInquiry.Responses;
using CustomerInquiry.Services;
using System;
using Xunit;

namespace CustomerInquiry.Tests.Services
{
    public class CustomerInquiryServiceTest : CustomerInquiryTestBase
    {
        private readonly CustomerInquiryService _service;

        public CustomerInquiryServiceTest()
        {
            _service = new CustomerInquiryService(DbContext);
        }

        [Fact]
        public void GetCustomerInquiry_WithNoCriteriaInquiry_ThrowAnExceptionWithMessage()
        {
            var request = new CustomerInquiryRequest
            {
                customerID = null,
                email = null
            };

            var exception = Assert.Throws<Exception>(() =>
            {
                _ = _service.GetCustomerInquiry(request);
            });

            Assert.Equal("No inquiry criteria", exception.Message);
        }

        [Fact]
        public void GetCustomerInquiry_WithInvalidCustomerId_ThrowAnExceptionWithMessage()
        {
            var request = new CustomerInquiryRequest
            {
                customerID = 0
            };

            var exception = Assert.Throws<Exception>(() =>
            {
                _ = _service.GetCustomerInquiry(request);
            });

            Assert.Equal("Invalid Customer ID", exception.Message);
        }

        [Fact]
        public void GetCustomerInquiry_WithInvalidEmail_ThrowAnExceptionWithMessage()
        {
            var request = new CustomerInquiryRequest
            {
                email = "abc1234"
            };

            var exception = Assert.Throws<Exception>(() =>
            {
                _ = _service.GetCustomerInquiry(request);
            });

            Assert.Equal("Invalid Email", exception.Message);
        }

        [Fact]
        public void GetCustomerInquiry_WithInvalidEmailAndCustomerId_ThrowAnExceptionWithMessage()
        {
            var request = new CustomerInquiryRequest
            {
                email = "abc1234",
                customerID = -1
            };

            var exception = Assert.Throws<Exception>(() =>
            {
                _ = _service.GetCustomerInquiry(request);
            });

            Assert.Equal("Invalid Customer ID, Invalid Email", exception.Message);
        }

        [Fact]
        public void GetCustomerInquiry_WithCustomerId_ReturnedOnlyCustomerData()
        {
            var request = new CustomerInquiryRequest
            {
                customerID = 1
            };

            var actual = _service.GetCustomerInquiry(request);

            Assert.Equal(1, actual.customerID);
            Assert.Equal("fullname lastname", actual.name);
            Assert.Equal("0987654321", actual.mobile);
            Assert.Equal("test@test.com", actual.email);
            Assert.Empty(actual.transactions);
        }

        [Fact]
        public void GetCustomerInquiry_WithEmail_ReturnedCustomerDataWithFirstSuccessTransaction()
        {
            var request = new CustomerInquiryRequest
            {
                email = "test@test.com"
            };

            var actual = _service.GetCustomerInquiry(request);

            Assert.Single(actual.transactions);

            var transaction = actual.transactions[0];
            Assert.Equal(TransactionStatus.Success, actual.transactions[0].status);
            Assert.Equal(1500.00m, transaction.amount);
            Assert.Equal("THB", transaction.currency);
        }

        [Fact]
        public void GetCustomerInquiry_WithCustomerIdAndEmail_ReturnedCustomerDataWithAllTransaction()
        {
            var request = new CustomerInquiryRequest
            {
                customerID = 1,
                email = "test@test.com"
            };

            var actual = _service.GetCustomerInquiry(request);
            
            Assert.Equal(2, actual.transactions.Length);
        }

        [Fact]
        public void GetCustomerInquiry_WithCritertiaNotMatched_ReturnedNull()
        {
            var request = new CustomerInquiryRequest
            {
                customerID = 2,
                email = "abc@xyz.com"
            };

            var actual = _service.GetCustomerInquiry(request);

            Assert.Null(actual);
        }
    }
}
