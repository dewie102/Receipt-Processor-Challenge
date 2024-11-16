using Microsoft.AspNetCore.Mvc;

namespace ReceiptProcessorChallenge_CSharp.Controllers
{
    public class HomeController : Controller
    {
        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return Redirect("/swagger/");
        }
    }
}
