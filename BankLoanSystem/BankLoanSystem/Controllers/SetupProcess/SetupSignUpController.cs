using System.Web.Mvc;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.SetupProcess
{
    public class SetupSignUpController : Controller
    {
        // GET: SetupSignUp
        public ActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
            user.Email = user.NewEmail;
            user.RoleId = 1;
            user.Status = true;
            user.step_status = 0;

            UserAccess ua = new UserAccess();
            if (ua.InsertUser(user) >= 1)
            {
                ViewBag.SuccessMsg = "Your profile Successfully created.";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                if (sa.updateStepNumberByUserId(ua.getUserId(user.Email), 1))
                    return RedirectToAction("UserLogin", "Login");
            }
            ViewBag.ErrorMsg = "Failed to Sign up try again!";
            return View();
        }
    }
}