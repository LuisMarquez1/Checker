using Checker.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Checker.API.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestController : ControllerBase
    {
        private readonly TestCoordinator _coordinator;

        public TestController(TestCoordinator coordinator)
        {
             _coordinator = coordinator;
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start()
        {
            return Ok("Pending implementation");
        }
    }
}
