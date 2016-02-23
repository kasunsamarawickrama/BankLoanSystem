using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Unit
{
    public class UnitController : Controller
    {
        // GET: AddUnit
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddUnit()
        {
            Session["UserId"] = 64;

            if (string.IsNullOrEmpty(Session["UserId"].ToString()))
                RedirectToAction("UserLogin", "Login");



            return View();
        }

        [HttpPost]
        [ActionName("AddUnit")]
        public ActionResult AddUnitPost()
        {
            return View();
        }
    }
}