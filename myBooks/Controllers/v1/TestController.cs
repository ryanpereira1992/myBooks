using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myBooks.Controllers.v1
{
    [ApiVersion("1.0")] // Using Headers
    [ApiVersion("1.5")] // Using Headers
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")] // Using Route Based Versioning

    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data"), MapToApiVersion("1.0")] // Mapping to Header based Versioning
        public IActionResult GetV10()
        {
            return Ok("This is Test Controller V1.0");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.5")]
        public IActionResult GetV15()
        {
            return Ok("This is Test Controller V1.5");
        }
    }


}
