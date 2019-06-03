using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInquiry.Requests;
using CustomerInquiry.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CustomerInquiry.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public ActionResult<CustomerInquiryResponse> CustomerInquiry([FromBody]CustomerInquiryRequest request)
        {
            return null;
        }
    }
}
