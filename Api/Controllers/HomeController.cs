using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Gamify Tasks API");
        }
    }
}
