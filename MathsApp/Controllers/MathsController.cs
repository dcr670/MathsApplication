using MathsApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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

            try
            {
                string[] calculationParts = Regex.Split(expr, _config.SplitRegExPattern);

                if (!decimal.TryParse(calculationParts[0], out total))
                {
                    throw new InvalidCastException(calculationParts[0]);
                }

                for (int position = 1; position < calculationParts.Length - 1; position += 2)
                {
                    operation = calculationParts[position];

                    if (!decimal.TryParse(calculationParts[position + 1], out value))
                    {
                        throw new InvalidCastException(calculationParts[position + 1]);
                    }

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
            catch (InvalidCastException ex)
            {
                _logger.LogError(ex, $"Calculation Error: value not a valid decimal number: {ex.Message}");
                return BadRequest($"Calculation Error: value not a valid decimal number: {ex.Message}");
            }
            catch (OverflowException ex)
            {
                _logger.LogError(ex, $"Calculation Error: decimal number too large: {ex.Message}");
                return BadRequest($"Calculation Error: decimal number too large: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Error");
                return BadRequest("Unexpected Error");
            }
        }
    }
}
