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
            result = userreques.SelectDatalistForAnswer();
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
                string body = "Title         :" + userrequest.topic+ "< br />" +
                              "Message       :" + userrequest.message + "< br />" +
                              "Answer        :" + userrequest.answer + "< br />";

                Email email = new Email(userrequest.email);
                email.SendMail(body, "Reply By Dealer Floor Plan Management Software team");

                ViewBag.SuccessMsg = "Response will be delivered to your program inbox";

            }
            else
            {
                ViewBag.SuccessMsg = "Error Occured";
            }
            //retrieve list from db
            List<Models.UserRequest> result = new List<Models.UserRequest>();
            UserRequest ob = new UserRequest();
            UserRequestAccess userreques = new UserRequestAccess();
            result = userreques.SelectDatalistForAnswer();
            ob.RequestList = result;
            return View(ob);
        }

    }
}