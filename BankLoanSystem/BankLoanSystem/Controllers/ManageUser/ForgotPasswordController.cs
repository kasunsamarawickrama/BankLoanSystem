using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.ManageUser
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing Enter email page
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Index()
        {

            return View();
        }


        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// Check the email, if it is correct generate a token and save it to database.
        /// And send email with token to client
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(ForgotPassword forgotPassword)
        {
            int userId = (new UserAccess()).getUserId(forgotPassword.Email);
            if (userId == 0)
            {
                ViewBag.ErrorMsg = "User Account not exists";
                return View();
            }

            string userName = (new UserAccess()).retreiveUserByUserId(userId).UserName;

            try
            {
                // generate token
                forgotPassword.token = forgotPassword.GenerateRandomString(30);





                // save it to db
                bool isUpdate = (new forgotPasswordTokenAccess()).updateToken(userId, forgotPassword.token);

                if (!isUpdate)
                {
                    ViewBag.ErrorMsg = "Updating Failed";
                    return View();
                }
                // sendit to the user

                Email email = new Email(forgotPassword.Email);


                int isSuccess = email.SendMail("Hi " + userName + "! <br><br>We recieved a request to reset your password.<br><br> Click here to Reset Your password : <a href='http://localhost:57318/ForgotPassword/ConfirmAccount?userId=" + userId + "&token=" + forgotPassword.token + "'>Link</a><br> If you don't want to change your password, you can ignore this email.<br><br> Thanks,<br> The Futunet Net Team", "Account - Help (Reset Your Password)");
                if (isSuccess == 0)
                {
                    ViewBag.errorMsg = "Sending Mail Failed";
                    return View();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            ViewBag.Message = "Instructions on how to reset Your Password have been sent to your inbox";
            return View();
        }


        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// Confirm the the user account using usedId and token
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult ConfirmAccount(int userId, string token)
        {


            bool isSuccuss = (new forgotPasswordTokenAccess()).verifyAccount(userId, token);
            if (isSuccuss)
            {

                Session["editId"] = userId;
                return RedirectToAction("ResetPassword");
            }
            return new HttpStatusCodeResult(404);
        }

        // <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// Reset Password Page view
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult ResetPassword(string message)
        {
            if (message != null)
            {
                ViewBag.message = message;
                Session.Abandon();
            }

            return View();
        }


        // <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// Reset Password Page view
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ResetPassword(ResetPassword resetPasswordModel)
        {

            // check the session, is exists allow otherwise don't
            int userId;
            try
            {
                userId = int.Parse(Session["editId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }


            bool isSuccess = (new forgotPasswordTokenAccess()).resetPassword(userId, resetPasswordModel);

            if (!isSuccess)
            {
                return new HttpStatusCodeResult(404);
            }

            ViewBag.message = "Your Password Sucessfully Updated";

            return RedirectToAction("ResetPassword", new { message = ViewBag.message });
        }


    }
}