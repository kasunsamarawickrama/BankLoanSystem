using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;

namespace BankLoanSystem.Controllers.MessageReply
{
    public class MessageReplyController : Controller
    {
        ///<summary>
        /// Frontend page: Message Reply
        /// Title: create view and get messages send by users( Page bottom message section) 
        /// Designed : Asanka Senarathna
        /// User story: 
        /// Developed: Asanka Senarathna
        /// Date created: 3/30/2016
        ///</summary>
        /// <returns></returns>
        public ActionResult MessageReply()
        {
            List<Models.UserRequest> result = new List<Models.UserRequest>();
            UserRequest ob = new UserRequest();
            UserRequestAccess userreques = new UserRequestAccess();
            //get datalist for need to be answer and loan in view
            result = userreques.SelectDatalistForAnswer();
            ob.RequestList = result;
            return View(ob);
        }
        /// <summary>
        /// Frontend page: Message Reply Post method
        /// Title: create view and get messages send by users( Page bottom message section) 
        /// Designed : Asanka Senarathna
        /// User story: 
        /// Developed: Asanka Senarathna
        /// Date created: 3/30/2016
        /// </summary>
        /// <param name="userrequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MessageReply(UserRequest userrequest)
        {
            userrequest.request_id = userrequest.request_id;
            userrequest.answer = userrequest.answer;
            userrequest.answer_user_id = 0;

            UserRequestAccess userreqAccsss = new UserRequestAccess();
            //if autherizer person answered then reply send to relavent user via nortification and email
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
            //retrieve not answersd list from database
            List<Models.UserRequest> result = new List<Models.UserRequest>();
            UserRequest ob = new UserRequest();
            UserRequestAccess userreques = new UserRequestAccess();
            result = userreques.SelectDatalistForAnswer();
            ob.RequestList = result;
            return View(ob);
        }

    }
}