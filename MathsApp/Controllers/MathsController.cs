using Microsoft.AspNetCore.Mvc;

namespace MathsApp.Controllers
{
    [Route("api/[controller]")]
    public class MathsController : ControllerBase
    {
        public IActionResult Calculate(string expr, bool useBODMAS = false)
        {
            return BadRequest(string.Empty);
        }
    }
}
