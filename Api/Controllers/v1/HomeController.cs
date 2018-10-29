using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Gamify Tasks API - version 1.0");
        }
    }
}
