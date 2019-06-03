using CustomerInquiry.Requests;
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
    }
}
