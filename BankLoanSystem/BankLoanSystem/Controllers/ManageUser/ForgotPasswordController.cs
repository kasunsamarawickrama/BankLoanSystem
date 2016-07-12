using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.ManageUser
{
    public class ForgotPasswordController : Controller
    {

        /*

   Frontend page: Account help page
   Title: Allowing user to enter email
   Designed: Irfan Mam
   User story: 
   Developed: Irfan MAM
   Date created: 1/13/2016

*/

        public ActionResult Index()
        {
            // returning the page
            return View();
        }


        /*

  Frontend page: Account help page
  Title: Sending password reset details to user
  Designed: Irfan Mam
  User story: 
  Developed: Irfan MAM
  Date created: 1/17/2016

*/

        [HttpPost]
        public ActionResult Index(ForgotPassword forgotPassword)
        {
            int userId = (new UserAccess()).getUserId(forgotPassword.Email);

            // if user doesn't exist
            if (userId == 0)
            {

                // pass error message and return the page
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

                // if error exist, when save the detail pass the error message
                if (!isUpdate)
                {
                    ViewBag.ErrorMsg = "Updating Failed";
                    return View();
                }

                // send it to the user's email account

                Email email = new Email(forgotPassword.Email);


                int isSuccess = email.SendMail("Hi " + userName + " ! <br><br>We recieved a request to reset your password.<br><br> Click here to Reset Your password : <a href='"+ string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "ForgotPassword/ConfirmAccount?userId=" + userId + "&token=" + forgotPassword.token + "'>Link</a><br> If you don't want to change your password, you can ignore this email.<br><br> Thanks,<br> The Futunet Net Team", "Account - Help (Reset Your Password)");

                // if sending failed -- return the page with error
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

            // return the page with success
            ViewBag.Message = "Instructions on how to reset Your Password have been sent to your inbox";
            return View();
        }



        /*

 Frontend page: Reset Password page
 Title:  Verifying the user account using usedId and token
 Designed: Irfan Mam
 User story: 
 Developed: Irfan MAM
 Date created: 1/17/2016

*/


        public ActionResult ConfirmAccount(int userId, string token)
        {

            // verify the user id and token
            bool isSuccuss = (new forgotPasswordTokenAccess()).verifyAccount(userId, token);

            // if user is a correct person
            if (isSuccuss)
            {

                Session["forgotId"] = userId; // pass user id on Session

                // return reset password page
                return RedirectToAction("ResetPassword");
            }
            return new HttpStatusCodeResult(404);
        }



        /*

Frontend page: Reset Password page
Title:  Reset Password Page view
Designed: Irfan Mam
User story: 
Developed: Irfan MAM
Date created: 1/17/2016

*/
        public ActionResult ResetPassword(string message)
        {

            // if message exists
            if (message != null)
            {
                ViewBag.message = message; // pass the message to the view
                // clear the session
                Session.Abandon();
            }

            // return the page
            return View();
        }


        /*

  Frontend page: Reset Password page
  Title:  Resetting Password
  Designed: Irfan Mam
  User story: 
  Developed: Irfan MAM
  Date created: 1/17/2016

  */

        [HttpPost]
        public ActionResult ResetPassword(ResetPassword resetPasswordModel)
        {

            // check the session, is exists allow otherwise don't
            int userId;
            try
            {
                userId = int.Parse(Session["forgotId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }

            // reset the password
            bool isSuccess = (new forgotPasswordTokenAccess()).resetPassword(userId, resetPasswordModel);


            // if failed to reset, return error
            if (!isSuccess)
            {
                return new HttpStatusCodeResult(404);
            }

            
            ViewBag.message = "Your Password Successfully Updated"; // pass success message

            // return the same page
            return RedirectToAction("ResetPassword", new { message = ViewBag.message });
        }


    }
}