using CustomerInquiry.Controllers;
using CustomerInquiry.Requests;
using CustomerInquiry.Responses;
using CustomerInquiry.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace CustomerInquiry.Tests.Controllers
{
    public class ApiControllerTest
    {
        private readonly ICustomerInquiryService _customerInquiryService;
        private readonly ApiController controller;

        public ApiControllerTest()
        {
            _customerInquiryService = Substitute.For<ICustomerInquiryService>();
            controller = new ApiController(_customerInquiryService);
        }

        [Fact]
        public void CustomerInquiry_WithNoContent_ReturnedNoContentResult()
        {
            CustomerInquiryResponse response = null;
            _customerInquiryService.GetCustomerInquiry(Arg.Any<CustomerInquiryRequest>()).Returns(response);

            var actionResult = controller.CustomerInquiry(new CustomerInquiryRequest());

            Assert.IsType<NoContentResult>(actionResult.Result);

            var noContentResult = actionResult.Result as NoContentResult;
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void CustomerInquiry_WithBadRequest_ReturnedBadRequestObjectResult()
        {
            _customerInquiryService.GetCustomerInquiry(Arg.Any<CustomerInquiryRequest>())
                .Returns(x => { throw new Exception("Something went wrong"); });

            var actionResult = controller.CustomerInquiry(new CustomerInquiryRequest());

            Assert.IsType<BadRequestObjectResult>(actionResult.Result);

            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public void CustomerInquiry_WithResult_ReturnedOkResult()
        {

            CustomerInquiryResponse response = new CustomerInquiryResponse();
            _customerInquiryService.GetCustomerInquiry(Arg.Any<CustomerInquiryRequest>()).Returns(response);

            var actionResult = controller.CustomerInquiry(new CustomerInquiryRequest());

            Assert.IsType<OkObjectResult>(actionResult.Result);

            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.Equal(200, okObjectResult.StatusCode);

            Assert.IsType<CustomerInquiryResponse>(okObjectResult.Value);
        }
    }
}
