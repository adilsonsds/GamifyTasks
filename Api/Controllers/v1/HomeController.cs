using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Gamify Tasks API - version 1.0");
        }
    }
}
