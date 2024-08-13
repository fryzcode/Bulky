using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(int id)
        {
            List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser").ToList();
            return Json(new { data = objOrderHeaders });
        }
        
        #endregion
    }
}
