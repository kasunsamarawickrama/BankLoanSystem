using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Helpers;

namespace BankLoanSystem.Controllers.Curtailments
{
    public class CurtailmentsController : Controller
    {
        string lCode=string.Empty;
        // private static LoanSetupStep1 loan;
        User userData = new User();

        // Check session in page initial stage
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["AuthenticatedUser"] != null)
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// set loan code to session
        /// </summary>
        /// <param name="loanCode"></param>
        /// <returns></returns>
        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;
            lCode = loanCode;
            return RedirectToAction("PayCurtailments");
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// GET: Curtailments by today date
        /// </summary>
        /// <returns></returns>
        public ActionResult PayCurtailments()
        {
            try
            {
                lCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
            if (userData.RoleId == 3)
            {
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    string rgts = "";
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList = null;
                    if (rgts != "")
                    {
                        rgtList = rgts.Split(',');
                    }
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            if (x == "U005")
                            {
                                checkPermission = true;
                            }
                        }
                        if (checkPermission == false)
                        {
                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                    }
                    else {
                        return RedirectToAction("UserDetails", "UserManagement");
                    }

                }
            }
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;

            return View();
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// GET: Curtailments by selected due date
        /// </summary>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        public ActionResult PayCurtailmentForSelectedDueDate(DateTime dueDate)
        {
            CurtailmentScheduleModel curtailmentScheduleModel = new CurtailmentScheduleModel();
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;

            string f = dueDate.ToShortDateString();
            DateTime dd = Convert.ToDateTime(f);


            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            List<CurtailmentShedule> curtailmentSchedule = curtailmentAccess.GetCurtailmentScheduleByDueDate(loanDetails.loanId, dd);

            curtailmentScheduleModel.CurtailmentScheduleInfoModel = new List<CurtailmentShedule>();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel.AddRange(curtailmentSchedule);
            curtailmentScheduleModel.DueDate = dueDate;
            return PartialView(curtailmentScheduleModel);
        }
        
        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// POST: Pay selected curtailment/s
        /// 
        /// </summary>
        /// <param name="selectedCurtailmentList"></param>
        /// <returns></returns>
        [HttpPost]
        public string PayCurtailments(SelectedCurtailmentList selectedCurtailmentList)
        {


            //dynamic stuff = JsonConvert.DeserializeObject(Request["SelectedCurtailmentScheduleInfoModel"]);

            //dynamic obj = serializer.Deserialize(, typeof(object));
            int userId = userData.UserId;
            string loanCode;
            
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return null;
            }


            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            string returnValue  = curtailmentAccess.updateCurtailmets(selectedCurtailmentList, loanDetails.loanId);

            if (returnValue!=null)
            {
                //insert to log
                string[] arrList = new string[selectedCurtailmentList.SelectedCurtailmentSchedules.Count];
                int i = 0;
                foreach (var x in selectedCurtailmentList.SelectedCurtailmentSchedules)
                {
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        arrList[i] = "Pay Curtailment(s) for unit(s): " + x.IDNumber +" ,Curtailment No: " +x.CurtNumber+" ,Curtailment Amount:"+x.CurtAmount+" ,Paid Date:"+x.PayDate;
                        i++;
                    }
                }

                //arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                ////user.UserRights = arrList.ToString();
                string units = string.Join(",", arrList);
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanDetails.loanId, "Pay Curtailments", units, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
            }
            return returnValue;
        }

        /// <summary>
        /// CreatedBy:Irfan
        /// CreatedDate:2016/3/18
        /// Search curtailments
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public ActionResult SearchCurtailment(string identificationNumber, string year, string make, string vehicleModel , CurtailmentScheduleModel CurtailmentList)
        {



            List<CurtailmentShedule> SearchList = new List<CurtailmentShedule>();
            
            
            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                //search through list elements
                Search sc = new Search();

                SearchList = sc.GetSearchCurtailmentList(CurtailmentList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower() );

                return PartialView(SearchList);
            }
            else
            {
                
                return PartialView(SearchList);
            }
        }

        
    }
}