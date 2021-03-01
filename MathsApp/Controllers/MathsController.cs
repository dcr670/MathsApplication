using MathsApp.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

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
                if (useBODMAS)
                {
                    expr = CalculateBODMAS(expr);
                }

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

        public string CalculateBODMAS(string expr)
        {
            decimal leftValue;
            decimal rightValue;
            string oper;
            string bodmasFormula = string.Empty;

            string pattern = _config.SplitBODMASRegExPattern;
            string[] calculationParts = Regex.Split(expr, pattern);

            if (!decimal.TryParse(calculationParts[0], out leftValue))
            {
                throw new InvalidCastException(calculationParts[0]);
            }

            for (int position = 1; position < calculationParts.Length - 1; position += 2)
            {
                oper = calculationParts[position];

                if (!decimal.TryParse(calculationParts[position + 1], out rightValue))
                {
                    throw new InvalidCastException(calculationParts[position + 1]);
                }

                switch (oper)
                {
                    case "+":
                    case "-":
                        bodmasFormula += leftValue + oper;
                        leftValue = rightValue;
                        break;
                    case "*":
                        leftValue = leftValue * rightValue;
                        break;
                    case "/":
                        leftValue = leftValue / rightValue;
                        break;
                    default:
                        break;
                }
            }

            bodmasFormula += leftValue;

            return bodmasFormula;
        }
    }
}
