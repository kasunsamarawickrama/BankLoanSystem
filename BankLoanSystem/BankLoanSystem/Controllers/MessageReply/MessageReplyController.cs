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
            return View();
        }

    }
}