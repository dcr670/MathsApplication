using MathsApp.Classes;
using MathsApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace MathsApp.Tests
{
    public class MathsControllerTests
    {
        MathsController mathsController;

        public class MathsOptionsList : IOptions<MathsOptions>
        {
            public MathsOptions Value
            {
                get
                {
                    return new MathsOptions()
                    {
                        SplitRegExPattern = "(\\+|-|\\*|\\/)",
                        SplitBODMASRegExPattern = "(\\+|-|\\*|\\/)",
                        UseFallBackService = false,
                        FallBackServiceURL = "http://api.mathjs.org/v4/",
                        FallBackServiceURLParameter = "expr={0}"
                    };
                }
            }
        }


        public MathsControllerTests()
        {
            var mathsOptions = new MathsOptionsList();

            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<MathsController>();

            mathsController = new MathsController(mathsOptions, logger);
        }

        [Fact]
        public void CalculateAddTwoNumbers()
        {
            // Act
            IActionResult result = mathsController.Calculate("2+2");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal(4.0m, ((OkObjectResult)result).Value);
        }

        [Fact]
        public void CalculateSubtractTwoNumbers()
        {
            // Act
            IActionResult result = mathsController.Calculate("2-2");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal(0.0m, ((OkObjectResult)result).Value);
        }

        [Fact]
        public void CalculateDivideTwoNumbers()
        {
            // Act
            IActionResult result = mathsController.Calculate("10/2");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal(5.0m, ((OkObjectResult)result).Value);
        }

        [Fact]
        public void CalculateMultiplyTwoNumbers()
        {
            // Act
            IActionResult result = mathsController.Calculate("10*2");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal(20.0m, ((OkObjectResult)result).Value);
        }

        [Fact]
        public void CalculateAddTwoDecimalNumbers()
        {
            // Act
            IActionResult result = mathsController.Calculate("10.2+5.3");

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ((OkObjectResult)result).StatusCode);
            Assert.Equal(15.5m, ((OkObjectResult)result).Value);
        }

        [Fact]
        public void CalculatInvalidOperator()
        {
            // Act
            IActionResult result = mathsController.Calculate("10&2");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((BadRequestObjectResult)result).StatusCode);
            Assert.Equal("Calculation Error: value not a valid decimal number: 10&2", ((BadRequestObjectResult)result).Value);
        }
    }
}