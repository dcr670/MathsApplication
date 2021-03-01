using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Web;

namespace MathsAppClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index([FromForm] string calculation, [FromForm] bool useBODMAS)
        {
            calculation = HttpUtility.UrlEncode(calculation);

            if (!string.IsNullOrEmpty(calculation))
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = client.GetAsync($"http://localhost:5000/api/Maths?expr={calculation}&useBODMAS=" + useBODMAS))
                    {
                        ViewData["StatusCode"] = response.Result.StatusCode;
                        ViewData["Result"] = response.Result.Content.ReadAsStringAsync().Result;
                    }
                }
            }

            return View();
        }
    }
}
