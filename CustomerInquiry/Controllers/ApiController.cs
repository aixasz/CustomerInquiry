using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
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
