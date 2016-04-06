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

            UserRequestAccess userreques = new UserRequestAccess();
            result = userreques.SelectDatalistForAnswer(154);
            return View(result);
        }

        [HttpPost]
        public ActionResult MessageReply(UserRequest userrequest)
        {
            return View();
        }

    }
}