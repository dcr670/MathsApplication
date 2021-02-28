using MathsApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MathsApp.Controllers
{
    [Route("api/[controller]")]
    public class MathsController : ControllerBase
    {
        private readonly MathsOptions _config;
        private readonly ILogger<MathsController> _logger;

        public MathsController(IOptions<MathsOptions> config, ILogger<MathsController> logger)
        {
            _logger = logger;

            try
            {
                _config = config.Value;
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogError(ex, "Configuration Error");
            }
        }

        public IActionResult Calculate(string expr, bool useBODMAS = false)
        {
            return BadRequest("Bad Request");
        }
    }
}
