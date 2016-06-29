using System;
using System.Web.Mvc;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

using SRVTextToImage;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace BankLoanSystem.Controllers.SetupProcess
{
    public class SetupSignUpController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["employeeId"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Login/EmpLogin");
                }
            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectResult("~/Login/EmpLogin");
            }
        }


        // GET: SetupSignUp
        public ActionResult SignUp()
        {
            if (TempData["status"] != null) {
                if(TempData["status"].ToString() == "success")
                    ViewBag.SuccessMsg = "Your profile Successfully created.";
                else if(TempData["status"].ToString() == "fail")
                    ViewBag.ErrorMsg = "Failed to Sign up try again!";
                else if(TempData["status"].ToString() == "captchaFail")
                    ViewBag.ErrorMsg = "Entered Security Code is Not Correct!";
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (this.Session["CaptchaImageText"].ToString() == user.SecurityCode)
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
                    //ViewBag.SuccessMsg = "Your profile Successfully created.";
                    TempData["status"] = "success";
                    //If succeed update step table to step2 
                    StepAccess sa = new StepAccess();
                    //if (sa.updateStepNumberByUserId(ua.getUserId(user.Email), 1))
                    return RedirectToAction("UserLogin", "Login");
                }
                TempData["status"] = "fail";
                //ViewBag.ErrorMsg = "Failed to Sign up try again!";
            }
            else
            {
                TempData["status"] = "captchaFail";
                //ViewBag.ErrorMsg = "Entered Security Code is Not Correct!";
            }


            //return View();
            return RedirectToAction("SignUp");
        }

        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/06/27
        /// 
        /// This action for get Captcha Image
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        // This is for output cache false
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public FileResult GetCaptchaImage()
        {
            CaptchaRandomImage CI = new CaptchaRandomImage();
            // here 5 means I want to get 5 char long captcha
            this.Session["CaptchaImageText"] = CI.GetRandomString(5); 

            CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 200, 50, Color.Black, Color.LightBlue);
            MemoryStream stream = new MemoryStream();
            CI.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }
    }
}