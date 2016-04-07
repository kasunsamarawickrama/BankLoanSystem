using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;

namespace BankLoanSystem.Controllers.MessageReply
{
    public class MessageReplyController : Controller
    {
        // GET: MessageReply
        public ActionResult MessageReply()
        {
            List<Models.UserRequest> result = new List<Models.UserRequest>();
            UserRequest ob = new UserRequest();
            UserRequestAccess userreques = new UserRequestAccess();
            result = userreques.SelectDatalistForAnswer(154);
            ob.RequestList = result;
            return View(ob);
        }

        [HttpPost]
        public ActionResult MessageReply(UserRequest userrequest)
        {
            userrequest.request_id = userrequest.request_id;
            userrequest.answer = userrequest.answer;
            userrequest.answer_user_id = 0;

            UserRequestAccess userreqAccsss = new UserRequestAccess();
            int reslt = userreqAccsss.UpdateUserRequestAnswer(userrequest);
            if (reslt >= 0)
            {
                //string body = "User Name      " + userData.UserName + "< br />" +
                //              "Position       " + (string)Session["searchType"] + "< br />" +
                //              "Company        " + userData.CompanyName + "< br />" +
                //              "Branch         " + userData.BranchName + "< br />" +
                //              "Loan           " + "< br />" +
                //              "Date and Time  " + DateTime.Now + "< br />" +
                //              "Title          " + "< br />" +
                //              "Message        " + userReq.message + "< br />" +
                //              "Page           " + "< br />";

                //Email email = new Email("asanka@thefuturenet.com");
                //email.SendMail(body, "Account details");

                ViewBag.SuccessMsg = "Response will be delivered to your program inbox";

            }
            else
            {
                ViewBag.SuccessMsg = "Error Occured";
            }
                return View();
        }

    }
}