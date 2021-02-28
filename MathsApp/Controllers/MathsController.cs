using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MathsApp.Controllers
{
    [Route("api/[controller]")]
    public class MathsController : ControllerBase
    {
        private readonly ILogger<MathsController> _logger;

        public MathsController(ILogger<MathsController> logger)
        {
            _logger = logger;

            _logger.LogInformation("Logging test");
        }

        public IActionResult Calculate(string expr, bool useBODMAS = false)
        {
            return BadRequest("Bad Request");
        }
    }
}
