using Microsoft.AspNetCore.Mvc;

namespace PaymentServiceApi.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
