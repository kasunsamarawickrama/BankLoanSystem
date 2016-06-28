using System;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using BankLoanSystem.Code;
using BankLoanSystem.Reports;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Controllers.Unit
{
    public class UnitController : Controller
    {
        //private static LoanSetupStep1 _loan;
        User userData = new Models.User();

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

                        //new HttpStatusCodeResult(404, "Failed to Setup company.");
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

        public ActionResult setLoanCode(string loancode) {
            Session["loanCode"]= loancode;

            return RedirectToAction("AddUnit");
        }

        /*

        Frontend page   : Add Unit 
        Title           : Add or Advance Units
        Designed        : Kasun Samarawickrama
        User story      : 
        Developed       : Kasun Samarawickrama
        Date created    : 02/24/2016

        */
        public ActionResult AddUnit()
        {
            // Handle Record successfully update or Error message

            int Flag = 0;
            if (TempData["Msg"] != null)
            {
                Flag = int.Parse(TempData["Msg"].ToString());
                if (Flag == 1)
                {
                    ViewBag.Msg = "Success";
                }
                else if (Flag == 3)
                {
                    ViewBag.Msg = "Requested";
                }
                else if (Flag == 2)
                {
                    ViewBag.Msg = "Error";
                }
            }
           

            int userId = userData.UserId;
            ViewBag.Role = userData.RoleId; ;

            //Check loan is null or not
            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });

            // for role id3 - user section
            if (userData.RoleId == 3)
            {
                // check user has rights to access this loan - if not redirect to dashboard
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    var checkAdvance = false;

                    // check user permission to the site
                    string rgts = "";
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList =null;
                    //spit the permission string
                    if (rgts != "") {
                        rgtList = rgts.Split(',');
                    }
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            //check user have rights to add unit page
                            if (x == "U004")
                            {
                                checkPermission = true;
                            }
                            // check user have right to advance units in this page
                            if (x == "U001")
                            {
                                checkAdvance = true;
                            }
                        }
                        if (checkAdvance == true)
                        {
                            ViewBag.advanceAllow = true;
                        }
                        else {
                            ViewBag.advanceAllow = false;
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
            string loanCode = Session["loanCode"].ToString();
            // retrive loan details
            LoanSetupStep1 loan = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            int loanId = loan.loanId;
            Session["addUnitloan"] = loan;


            ViewBag.loanDetails = loan;
            //set default unit type for add unit page
            if (loan.selectedUnitTypes.Count == 1) {
                ViewBag.UnitTypeId = loan.selectedUnitTypes[0].unitTypeName;
            }

            Models.Unit unit = new Models.Unit();

            unit.AdvancePt = loan.advancePercentage;
            unit.LoanId = loanId;
            unit.LoanAmount = loan.loanAmount;
            unit.AdvanceDate = DateTime.Now;
            unit.StartDate = loan.startDate;
            unit.EndDate = loan.maturityDate;

            //get company type
            //1 - Lender
            //2 - Dealer
            BranchAccess ba = new BranchAccess();
            int companyType = ba.getCompanyTypeByUserId(userId);

            ViewBag.CompabyType = companyType;
            ViewBag.RoleId = userData.RoleId;

            //Check title 
            TitleAccess ta = new TitleAccess();
            Title title = ta.getTitleDetails(loan.loanId);
            // check title track allow or not
            if (title != null)
            {
                bool isTitleTrack = title.IsTitleTrack;
                if (isTitleTrack)
                    ViewBag.IsTitleTrack = "Yes";

                string upload = title.TitleAcceptMethod;
                if (!string.IsNullOrEmpty(upload) && upload == "Scanned Title Adequate")
                    ViewBag.Upload = "Yes";

            }
            // loan Details for (loan Detail box) in the page 
            UnitAccess ua = new UnitAccess();
            LoanPaymentDetails loanPaymentDetails = ua.GetLoanPaymentDetailsByLoanId(loanId);

            unit.Balance = loanPaymentDetails.BalanceAmount;
            // check balane field is editable or not for this loan
            ViewBag.Editable = loan.isEditAllowable ? "Yes" : "No";

            
            //set user role to restrict add & advance unit if this user is dealer user(role id = 4)
            ViewBag.RoleId = userData.RoleId; 

            return PartialView(unit);
        }

        /*

        Frontend page   : Add Unit
        Title           : Add or Advance Units Post method
        Designed        : Kasun Samarawickrama
        User story      : 
        Developed       : Kasun Samarawickrama
        Date created    : 02/24/2016

        */
        [HttpPost]
        [ActionName("AddUnit")]
        public ActionResult AddUnitPost(Models.Unit unit, List<HttpPostedFileBase> fileUpload)
        {

           int userId = userData.UserId;
            // check this is an advance or add unit
            switch (unit.AdvanceNow)
            {
                case "No":
                    unit.IsAdvanced = false;
                    unit.AddAndAdvance = false;
                    break;
                case "Yes":
                    unit.IsAdvanced = true;
                    unit.AddAndAdvance = true;
                    break;
            }

            if (Session["loanCode"] == null) {
                return RedirectToAction("UserLogin", "Login", null);
            }
            string loanCode = Session["loanCode"].ToString();
            
            //check this posted vin unique in database

            int num = 0;
            // vehile ID number
            if (unit.UnitTypeId == 1)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.vehicle.IdentificationNumber, unit.LoanId);
            }
            // rv ID number
            else if (unit.UnitTypeId == 2)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.rv.IdentificationNumber , unit.LoanId);

            }
            // camper ID number
            else if (unit.UnitTypeId == 3)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.camper.IdentificationNumber, unit.LoanId);
            }
            // atv ID number
            else if (unit.UnitTypeId == 4)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.atv.IdentificationNumber, unit.LoanId);
            }
            // boat ID number
            else if (unit.UnitTypeId == 5)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.boat.IdentificationNumber, unit.LoanId);
            }
            // motorcycle ID number 
            else if (unit.UnitTypeId == 6)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.motorcycle.IdentificationNumber, unit.LoanId);
            }
            // snowmobile ID number 
            else if (unit.UnitTypeId == 7)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.snowmobile.IdentificationNumber, unit.LoanId);
            }
            // heavyequipment ID number 
            else if (unit.UnitTypeId == 8)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.heavyequipment.SerialNumber, unit.LoanId);
            }

            //only allow to add if and only if vin already not existing in this loan
            if (num != 0 && num != 1)
            {

                string IDNumber;
                UnitAccess ua = new UnitAccess();
                //inserting the unit to the database
                string res = ua.InsertUnit(unit, userId, loanCode, out IDNumber);

                //if mention advance fee, then insert in to fee table
                if (!string.IsNullOrEmpty(res) && unit.AddAndAdvance)
                {


                    if ((Session["loanDashboard"] != null) || (Session["oneLoanDashboard"] != null))
                    {
                        Loan loanObj = new Loan();
                        if (Session["loanDashboard"] != null)
                        {
                            loanObj = (Loan)Session["loanDashboard"];
                        }
                        else
                        {
                            loanObj = (Loan)Session["oneLoanDashboard"];
                        }
                        if (loanObj.AdvanceFee == 1)
                        {
                            //check advance amount and other details

                            ua.insertFreeDetails(unit);
                        }
                    }
                }
                
                if (!string.IsNullOrEmpty(res))
                {
                    if (Session["addUnitloan"] == null)
                    {
                        return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });
                    }
                    LoanSetupStep1 loan = (LoanSetupStep1)Session["addUnitloan"];
                    //insert to log 
                    Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, unit.LoanId, "Add Unit", (unit.AddAndAdvance ? "Added and advanced" : "Added") + " unit : " + IDNumber + ", Cost Amount : " + unit.Cost + (unit.Cost * loan.advancePercentage / 100 != unit.AdvanceAmount ? ", Edited Advance amount " + unit.AdvanceAmount : ", Advance amount : " + unit.AdvanceAmount), DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                    //Handling file attachments

                    //Check directory is already exists, if not create new
                    string mapPath = "~/Uploads/" + loan.RegisteredCompanyCode + "/" + loan.RegisteredBranchCode + "/";
                    if (!Directory.Exists(Server.MapPath(mapPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(mapPath));
                    }

                    List<TitleUpload> titleList = new List<TitleUpload>();

                    int imageNo = 1;
                    // if unit successfully updated then upload files
                    if (unit.FileName != null && fileUpload != null)
                    {
                        foreach (var file in fileUpload)
                        {
                            if (file != null && Array.Exists(unit.FileName.Split(','), s => s.Equals(file.FileName)))
                            {
                                if (file.ContentLength > 1 * 1024 * 1024)
                                {
                                    break;
                                }
                                unit.UnitId = res;
                                string extension = Path.GetExtension(file.FileName);

                                string filename = unit.UnitId + "_" + imageNo.ToString("00") + extension;

                                file.SaveAs(Server.MapPath(mapPath + filename));
                                string filepathtosave = mapPath + filename;

                                //add file information to list
                                TitleUpload title = new TitleUpload();
                                title.UploadId = imageNo;
                                title.FilePath = filepathtosave;
                                title.UnitId = unit.UnitId;
                                title.OriginalFileName = file.FileName;

                                titleList.Add(title);

                                imageNo++;
                            }
                        }

                        try
                        {
                            XElement xEle = new XElement("Titles",
                                from title in titleList
                                select new XElement("Title",
                                    new XElement("FilePath", title.FilePath),
                                    new XElement("UnitId", title.UnitId),
                                    new XElement("OriginalFileName", title.OriginalFileName)
                                    ));
                            string xmlDoc = xEle.ToString();

                            bool res1 = ua.InsertTitleDocumentUploadInfo(xmlDoc, unit.UnitId);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    TempData["Msg"] = 1;

                    return RedirectToAction("AddUnit");
                }
            }
            TempData["Msg"] = 2;
            return RedirectToAction("AddUnit", unit);
        }

        /*

        Frontend page   : Add Unit
        Title           : Get the models according to the vehicle model year and make
        Designed        : Kasun Samarawickrama
        User story      : 
        Developed       : Kasun Samarawickrama
        Date created    : 2016/2/25

        */
        [HttpPost]
        public ActionResult GetModels(string make, int year,int unitId)
        {
            List <UnitYearMakeModel> modelList = (new UnitAccess()).GetVehicleModelsByMakeYear(make, year, unitId);

            SelectList modelSelectList = new SelectList(modelList, "VehicleModel", "VehicleModel");

            return Json(modelSelectList);
        }


        /*

        Frontend page   : Add Unit
        Title           : Get the makes according to the vehicle model year
        Designed        : Kasun Samarawickrama
        User story      : 
        Developed       : Kasun Samarawickrama
        Date created    : 2016/2/25

        */
        [HttpPost]
        public ActionResult GetMakes(int year,int unitId)
        {
            List<UnitYearMakeModel> makeList = (new UnitAccess()).GetVehicleMakesByYear(year,unitId);

            SelectList makeSelectList = new SelectList(makeList, "VehicleMake", "VehicleMake");

            return Json(makeSelectList);
        }

        /*

        Frontend page   : Add Unit
        Title           : Get year accrding to the vehicle vin number
        Designed        : Kasun Samarawickrama
        User story      : 
        Developed       : Kasun Samarawickrama
        Date created    : 2016/2/25

        */
        [HttpPost]
        public ActionResult GetYearByVin(string vin)
        {
            int year = (new UnitAccess()).DecodeVINYear(vin);

            return Json(year);
        }


        public ActionResult LoanInfo(string title, string msg)
        {
            ViewBag.Msg = msg;
            int userId;
            string loanCode = null;
            try {
                userId = userData.UserId;

                
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }

            if(Session["loanCode"] != null) { 
            loanCode = Session["loanCode"].ToString();
            }

            ViewBag.Title = title;
            
            ViewBag.Username = userData.UserName;
            BranchAccess ba = new BranchAccess();
          
        
          
                ViewBag.roleId = userData.RoleId;
            // get the Company type for front end view
            int comType = ba.getCompanyTypeByUserId(userId);
            ViewBag.loanCompanyType = (comType == 1) ? "Dealer" : "Lender";


            if(loanCode != null) {
            LoanSetupStep1 loan = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            Session["addUnitloan"] = loan;
            NonRegBranch nonRegBranch = ba.getNonRegBranchByNonRegBranchId(loan.nonRegisteredBranchId);

            ViewBag.NonRegBranchName = nonRegBranch.BranchName;

            //dealer email address, this will be used on curtailment page
            Session["DealerEmail"] = nonRegBranch.BranchEmail;

            ViewBag.loanBranchAddress =  (nonRegBranch.BranchAddress1 != "" ? nonRegBranch.BranchAddress1 : "") + (nonRegBranch.BranchAddress2 != "" ? ", " + nonRegBranch.BranchAddress2 : "") + (nonRegBranch.BranchCity != "" ? ", " + nonRegBranch.BranchCity : "");

            ViewBag.CurtailmentDueDate = loan.CurtailmentDueDate;

            ViewBag.LoanNumber = loan.loanNumber;

            }

            return View();
        }

        public ActionResult LoanPaymentDetails()
        {
            int userId;
            string loanCode;
            try
            {
                userId = userData.UserId;

                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            //int userId = 57;

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            LoanPaymentDetails advance = (new UnitAccess()).GetLoanPaymentDetailsByLoanId(loanSetupStep1.loanId);
            ViewBag.AvailableBalance = advance.Amount - advance.UsedAmount;

            return PartialView(advance);

        }

        /// <summary>
        /// Frontend Page:Bottom Link Bar of each page in floor plan management section
        /// Title: return view according to user rights and loan setup details
        /// Designed: Irfan MAM
        /// User Story:
        /// Developed: Piyumi P
        /// Date created:
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLinkBar()
        {
        //assign logged user id to int variable
            int userId = userData.UserId;
            string loanCode = "";

            UserRightsAccess access = new UserRightsAccess();

            //retrive all rights
            List<Right> rights = new List<Right>();
            //assign logged user role to int variable
            int userRole = userData.RoleId;
            //check Session["loanCode"] is not null and not empty
            if ((Session["loanCode"] != null) && (!string.IsNullOrEmpty(Session["loanCode"].ToString())))
            {
            //convert session to string variable
                loanCode = Session["loanCode"].ToString();
            }
            //check user role is user
            if (userRole == 3)
            {
            //retrieve rigts given for the loan 
                rights = access.GetUserRightsByLoanCode(loanCode, userId);
            }
            //assign user role to viewbag variable
            ViewBag.Role = userRole;

            //check Session["addUnitloan"] is null
            if (Session["addUnitloan"] == null)
            {
            //return to login page
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });
            }
            //convert session to loan object
            LoanSetupStep1 loan = (LoanSetupStep1)Session["addUnitloan"];
            //check  Session["IsTitleTrack"] is not null
            if (Session["IsTitleTrack"] != null)
            {
            //check session value
                if (int.Parse(Session["IsTitleTrack"].ToString())==1)
                    {
                    //if 1 - title need to be tracked
                        ViewBag.ttlAccess = 1;
                    }
                    else
                    {
                    //else title no need to be tracked
                        ViewBag.ttlAccess = 0;

                    }
                
                
            }
            else
            {
                //else title no need to be tracked
                ViewBag.ttlAccess = 0;

            }
            //check Session["oneLoanDashboard"] which contains loan data if logged user has one loan is not null and not empty
            if ((Session["oneLoanDashboard"] != null) && (!string.IsNullOrEmpty(Session["oneLoanDashboard"].ToString())))
            {
                Loan loanObj = new Loan();
                //convert session to loan object
                    loanObj = (Loan)Session["oneLoanDashboard"];
                    //check if loan has at least one fee
                if ((loanObj.LotInspectionFee == 1) || (loanObj.MonthlyLoanFee == 1) || (loanObj.AdvanceFee == 1))
                    {
                    //assign value 1 for ViewBag.FeeLB
                    ViewBag.FeeLB = 1;
                    }
                    else
                    {
                    //assign value 0 for ViewBag.FeeLB
                    ViewBag.FeeLB = 0;
                    }
            }
            //check Session["loanDashboard"] which contains loan data if logged user select in popup is not null and not empty
            else if ((Session["loanDashboard"] != null) && (!string.IsNullOrEmpty(Session["loanDashboard"].ToString())))
            {
                Loan loanObj = new Loan();
                //convert session to loan object
                loanObj = (Loan)Session["loanDashboard"];
                //check if loan has at least one fee
                if ((loanObj.LotInspectionFee == 1) || (loanObj.MonthlyLoanFee == 1) || (loanObj.AdvanceFee == 1))
                {
                    //assign value 1 for ViewBag.FeeLB
                    ViewBag.FeeLB = 1;
                }
                else
                {
                    //assign value 0 for ViewBag.FeeLB
                    ViewBag.FeeLB = 0;
                }
            }
            //check Session["loanDashboard"] and check Session["oneLoanDashboard"] is null
            else if ((Session["oneLoanDashboard"] == null) && (Session["loanDashboard"] == null))
            {
            //return to login page
                return RedirectToAction("UserLogin", "Login");
            }
            //return right list to partial view
            return PartialView(rights);

        }



        public ActionResult GetJustAddedUnits()
        {

            int userId;
            string loanCode;
            try
            {
                userId = userData.UserId;

                loanCode = Session["loanCode"].ToString();
                ViewBag.UserRole = userData.RoleId;
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }
            //int userId = 57;

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);



            return PartialView((new UnitAccess().GetJustAddedUnitDetails(userId, loanSetupStep1.loanId)));
        }

        public ActionResult AddUnitReport()
        {
            if (Session["addUnitloan"] == null)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });
            }

            LoanSetupStep1 loan = (LoanSetupStep1)Session["addUnitloan"];

            ViewBag.LoanId = loan.loanId;
            ViewBag.UserId = userData.UserId;
            return View();
        }

        /*

        Frontend page   : Add Unit
        Title           : Check Vin already wxist on loan 
        Designed        : Kasun Samarawickrama
        User story      : 
        Developed       : Kasun Samarawickrama
        Date created    : 

        */
        [HttpPost]
        public JsonResult IsVinExists(string identificationNumber)
        {
            LoanSetupStep1 loan = (LoanSetupStep1)Session["addUnitloan"];

            //check user name is already exist.  
            int loanId = loan.loanId;
            int num = (new UnitAccess()).IsUniqueVinForaLoan(identificationNumber, loanId);

            return Json(num, JsonRequestBehavior.AllowGet);
        }

       
      
        public ActionResult AddUnitRequestAdvance()
        {

            Models.User user = new Models.User();
            if (Session["loanCode"] == null)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }
            else {
                string Code = Session["loanCode"].ToString();
                user = (new UserAccess()).GetDealerUserDetails(userData.UserId, Code);

                if (user != null)
                {

                    string alertmsg = " Dealer User " + user.FirstName + " " + user.LastName + " requested to advance " + user.NoOfUnitsAdded + " new unit(s) for loan number " + user.LoanNumber + " on " + user.AddedDate + ". Please go to advance page to advance the items. ";
                    int rep = (new UserAccess()).InsertDearlerUserRequest(0,0,user.UserIdForSendReq, Code,alertmsg);

                    string body = "Hi , <br /><br /> Dealer User " + user.FirstName + " " + user.LastName + " requested to advance " + user.NoOfUnitsAdded + " new unit(s) for loan number " + user.LoanNumber +" on "+user.AddedDate+". Please login to the system and go to advance page to advance item(s). <br /><br/> Thanks. <br />";

                    Email email = new Email(user.UserEmailForSendReq);


                    email.SendMail(body, "Request Advance");

                    //Log log = new Log(userData.UserId, userData.Company_Id, user.BranchId, user.LoanId, "Create Dealer Account", "Inserted Dealer : " + user.UserName, DateTime.Now);

                    //int islog = (new LogAccess()).InsertLog(log);

                   TempData["msg"] = 3;
                    return RedirectToAction("AddUnit", "Unit");

                }
                else
                {
                    TempData["Msg"] = 0;
                    return RedirectToAction("AddUnit", "Unit");
                }
                
            }


            //return View();
        }

        public FileResult PrintPage()
        {
            if (Session["addUnitloan"] == null) return null;
            LoanSetupStep1 loan = (LoanSetupStep1)Session["addUnitloan"];

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            RptDivAddUnit justAddedUnitReport = new RptDivAddUnit();
            var rptViewerPrint = justAddedUnitReport.PrintPage(loan.loanId, userData.UserId);

            var bytes = rptViewerPrint.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            var fsResult = File(bytes, "application/pdf");
            return fsResult;
        }

    }
}