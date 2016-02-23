using System.Web.Mvc;

namespace BankLoanSystem.Controllers.AddUnit
{
    public class AddUnitController : Controller
    {
        // GET: AddUnit
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Unit()
        {
            Session["UserId"] = 64;

            if (string.IsNullOrEmpty(Session["UserId"].ToString()))
                RedirectToAction("UserLogin", "Login");



            return View();
        }
    }
}