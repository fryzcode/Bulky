using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }

    }
}
