using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Fee
{
    /// <summary>
    /// CreatedBy:Nadeeka
    /// CreatedDate:2016/4/20
    /// 
    /// </summary>
    public class FeeController : Controller
    {
        User userData = new User();
        string lCode = string.Empty;

        // GET: Fee
        public ActionResult Index()
        {
            return View();
        }

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

                    if (HttpContext.Request.IsAjaxRequest())
                    {

                        
                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");
                    }
                    
                   
                }
            }
            catch
            {
                
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }

        /*

   Frontend page: Fee Page
   Title: Load the Fee Page
   Designed: Nadeeka
   User story:
   Developed: Nadeeka
   Date created: 4/20/2016

*/

        public ActionResult PayFees()
        {
            try
            {
                lCode = Session["loanCode"].ToString(); // take the loan code on session
            }

            // if there is no loan on Session -- redirect to login
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }

            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString()); // take the loan details of loan code

            // pass loan details to the view
            ViewBag.loanDetails = loanDetails;

            Loan loanSelected = new Loan();

            // take the loan which was selected from the dashboard and pass it to the view
            if (Session["loanDashboard"] != null)
            {
                loanSelected = (Loan)Session["loanDashboard"];
            }
            else if (Session["oneLoanDashboard"] != null)
            {
                loanSelected = (Loan)Session["oneLoanDashboard"];
            }
            else
            {
                // if there is no loan selected from dashboard -- wrong access
                return RedirectToAction("Login", "UserLogin");
            }
            ViewBag.loanSelected = loanSelected;


            // if user is a normal user, checking the user permission
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
                            if (x == "U07")
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

            // if user is a dealer user -- he is not allowed to fee page
            else if (userData.RoleId == 4)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }

            // take the due dates of advance fee, monthly loan fee and lot inspection fee
            string advDuedate;
            string monDueDate;
            string lotDuedate;
            bool returnValue = (new FeeAccess()).GetFeesDueDates(loanDetails.loanId, out advDuedate, out monDueDate, out lotDuedate);

            // pass due dates to view
            ViewBag.advDuedate = advDuedate;
            ViewBag.monDueDate = monDueDate;
            ViewBag.lotDuedate = lotDuedate;


            // return fee page
            return View();
        }


        /*

          Frontend page: Fee Page
          Title: get fees by selected date
          Designed: Nadeeka
          User story:
          Developed: Nadeeka
          Date created: 4/21/2016

       */
        
        public ActionResult PayFeesForSelectedDueDate(DateTime dueDate, string type)
        {
                LoanSetupStep1 loanDetails = new LoanSetupStep1();
                loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString()); // take the loan detail of selected loan
                // pass the loan details to the view
                ViewBag.loanDetails = loanDetails;

                FeeAccess feeAccess = new FeeAccess();
                List<Fees> lstFee = feeAccess.GetFeesByDueDate(loanDetails.loanId, dueDate, type); // get fees list by duedate and type of fee
                FeesModel feeModel = new FeesModel();
                feeModel.FeeModelList = new List<Fees>();
                feeModel.Type = type;

                // if list exists, add to model and session for searching
                if (lstFee != null && lstFee.Count > 0)
                {
                    feeModel.FeeModelList.AddRange(lstFee);
                    Session["feeList"] = feeModel.FeeModelList;
                   
                }

               // return partial view of selected fee page
                return PartialView(feeModel);
                

                        
        }


        /*

         Frontend page: Fee Page
         Title: pay fees for selected items
         Designed: Nadeeka
         User story:
         Developed: Nadeeka
         Date created: 4/22/2016

      */
       
        [HttpPost]
        public int PayFees(List<Fees> lstFee, string type)
        {
            int userId = userData.UserId;  // set the user id
            FeeAccess feeAccess = new FeeAccess();
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString()); // take the loan details by loan code
            int returnValue = feeAccess.updateFees(lstFee, lstFee[0].PaidDate, loanDetails.loanId, userId); // update fee details

            //if successfully updated, insert to log 
            if (returnValue == 1)
            {
                Log log;

                // if fee type is advance -- save details to log as advance fee
                if (type == "advanceFee")
                {
                    List<string> IDNumbers = new List<string>();
                    foreach (var fee in lstFee)
                    {
                        IDNumbers.Add(fee.IdentificationNumber);
                    }
                    log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanDetails.loanId, "Pay Fees", "Advance Fee Paid for the unit(s) : " + string.Join(",", IDNumbers) + " , Pay Date : " + lstFee[0].PaidDate.ToString("dd/MM/yyyy"), DateTime.Now);
                    (new LogAccess()).InsertLog(log);

                }

                // if fee type is monthly loan -- save details to log as monthly loan fee
                else if (type == "monthlyLoanFee")
                {
                    List<string> DueDates = new List<string>();
                    foreach (var fee in lstFee)
                    {
                        DueDates.Add(fee.DueDate);
                    }

                    log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanDetails.loanId, "Pay Fees", " Monthly Loan Fee Paid for the due date(s) : { " + string.Join(",", DueDates) + "}" + ", Pay Date : " + lstFee[0].PaidDate.ToString("dd/MM/yyyy"), DateTime.Now);
                    (new LogAccess()).InsertLog(log);
                }

                // if fee type is lot inspection fee -- save details to log as lot inspection fee
                else if (type == "lotInspectionFee")
                {
                    List<string> DueDates = new List<string>();

                    foreach (var fee in lstFee)
                    {
                        DueDates.Add(fee.DueDate);
                    }

                    log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanDetails.loanId, "Pay Fees", "Lot Inspection Fee Paid for the due date(s) : { " + string.Join(",", DueDates) + " } " + ", Pay Date : " + lstFee[0].PaidDate.ToString("dd/MM/yyyy"), DateTime.Now);
                    (new LogAccess()).InsertLog(log);

                }
            }

            // return the value to view
            return returnValue;
        }


        /*

         Frontend page: Fee Page
         Title: get fees by selected date
         Designed: Nadeeka
         User story:
         Developed: Nadeeka
         Date created: 4/25/2016

      */
        
        public ActionResult SearchFee(string identificationNumber, string year, string make, string vehicleModel, FeesModel CurtailmentList)
        {
            List<Fees> SearchList = new List<Fees>();

            // if fee list exists on session
            if (Session["feeList"] != null)
            {
                List<Fees> feeModel = (List<Fees>)Session["feeList"];

                // if atleast one searching criteria matches -- do search
                if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
                {
                    //search through list of elements
                    Search sc = new Search();

                    SearchList = sc.GetSearchFeeList(feeModel, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower()); // take filtered elements
                     // pass the list to view
                    return PartialView(SearchList);
                }

                // if no searching criteria matched -- retunt empty list
                else
                {

                    return PartialView(SearchList);
                }
            }

            // if fee list not exist on session -- return empty list
            else
            {

                return PartialView(SearchList);
            }
        }

    }
}