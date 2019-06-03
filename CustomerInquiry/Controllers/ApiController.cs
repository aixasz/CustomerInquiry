using System;
using CustomerInquiry.Requests;
using CustomerInquiry.Responses;
using CustomerInquiry.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInquiry.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ICustomerInquiryService _customerInquiryService;

        public ApiController(ICustomerInquiryService customerInquiryService)
        {
            _customerInquiryService = customerInquiryService;
        }

        /// <summary>
        /// The customer inquiry api
        /// </summary>
        /// <remarks>
        /// Available criteria:
        ///
        ///     POST api/CustomerInquiry
        ///     {
        ///        "customerID": 1,
        ///        "email": "test@test.com"
        ///     }
        ///     
        ///     POST api/CustomerInquiry
        ///     {
        ///        "customerID": 1
        ///     }
        ///     
        ///     POST api/CustomerInquiry
        ///     {
        ///        "email": "test@test.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Inquiry criteria</param>
        /// <returns>Customer transactions</returns>
        /// <response code="200">Returns the customer transaction</response>
        /// <response code="204">Not found</response>
        /// <response code="400">Invalid criteria</response> 
        [HttpPost("CustomerInquiry")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public ActionResult<CustomerInquiryResponse> CustomerInquiry([FromBody]CustomerInquiryRequest request)
        {
            try
            {
                var result = _customerInquiryService.GetCustomerInquiry(request);

                if (result == null)
                {
                    // This will return HTTP Status code 204 for no content 
                    return NoContent();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
