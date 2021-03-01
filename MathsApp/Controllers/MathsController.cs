using MathsApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

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
            decimal value;
            decimal total;
            string operation;

            string[] calculationParts = Regex.Split(expr, _config.SplitRegExPattern);

            decimal.TryParse(calculationParts[0], out total);

            for (int position = 1; position < calculationParts.Length - 1; position += 2)
            {
                operation = calculationParts[position];

                decimal.TryParse(calculationParts[position + 1], out value);

                switch (operation)
                {
                    case "+":
                        total += value;
                        break;
                    case "-":
                        total -= value;
                        break;
                    case "*":
                        total *= value;
                        break;
                    case "/":
                        total /= value;
                        break;
                    default:
                        break;
                }
            }

            return Ok(total);
        }
    }
}
