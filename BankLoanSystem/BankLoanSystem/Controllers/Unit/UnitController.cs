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
                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("~/Login/UserLogin");
                    }
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }

        public ActionResult setLoanCode(string loancode) {
            Session["loanCode"]= loancode;

            return RedirectToAction("AddUnit");
        }

        public ActionResult AddUnit()
        {
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

            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });

            if (userData.RoleId == 3)
            {
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    var checkAdvance = false;

                    string rgts = "";
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList =null;
                    if (rgts != "") {
                        rgtList = rgts.Split(',');
                    }
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            if (x == "U004")
                            {
                                checkPermission = true;
                            }
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

            LoanSetupStep1 loan = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            int loanId = loan.loanId;
            Session["addUnitloan"] = loan;


            ViewBag.loanDetails = loan;
            if (loan.selectedUnitTypes.Count == 1) {
                ViewBag.UnitTypeId = loan.selectedUnitTypes[0].unitTypeName;
            }

            Models.Unit unit = new Models.Unit();

            //int userRole = (new UserManageAccess()).getUserRole(userId);
            //if (userRole == 3)
            //{
            //    var access = new UserRightsAccess();
            //    List<Right> permission = access.getRightsString(userId);
            //    if (permission.Count >= 1)
            //    {
            //        string permissionString = permission[0].rightsPermissionString;
            //    }
            //}
            
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

            if (title != null)
            {
                bool isTitleTrack = title.IsTitleTrack;
                if (isTitleTrack)
                    ViewBag.IsTitleTrack = "Yes";

                string upload = title.TitleAcceptMethod;
                if (!string.IsNullOrEmpty(upload) && upload == "Scanned Title Adequate")
                    ViewBag.Upload = "Yes";

            }

            UnitAccess ua = new UnitAccess();
            LoanPaymentDetails loanPaymentDetails = ua.GetLoanPaymentDetailsByLoanId(loanId);

            unit.Balance = loanPaymentDetails.BalanceAmount;

            ViewBag.Editable = loan.isEditAllowable ? "Yes" : "No";

            //set user role to restrict add & advance unit if this user is dealer user(role id = 4)
            ViewBag.RoleId = userData.RoleId; 

            return PartialView(unit);
        }


        [HttpPost]
        [ActionName("AddUnit")]
        public ActionResult AddUnitPost(Models.Unit unit, List<HttpPostedFileBase> fileUpload)
        {
            int flag = 0;
           

           int userId = userData.UserId;

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

            GeneratesCode gc = new GeneratesCode();

            if (Session["loanCode"] == null) {
                return RedirectToAction("UserLogin", "Login", null);
            }
            string loanCode = Session["loanCode"].ToString();
            //check vin unique

            int num = 0;
            if (unit.UnitTypeId == 1)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.vehicle.IdentificationNumber, unit.LoanId);
            }
            else if (unit.UnitTypeId == 2)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.rv.IdentificationNumber , unit.LoanId);

            }
            else if (unit.UnitTypeId == 3)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.camper.IdentificationNumber, unit.LoanId);
            }
            else if (unit.UnitTypeId == 4)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.atv.IdentificationNumber, unit.LoanId);
            }
            else if (unit.UnitTypeId == 5)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.boat.IdentificationNumber, unit.LoanId);
            }
            else if (unit.UnitTypeId == 6)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.motorcycle.IdentificationNumber, unit.LoanId);
            }
            else if (unit.UnitTypeId == 7)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.snowmobile.IdentificationNumber, unit.LoanId);
            }
            else if (unit.UnitTypeId == 8)
            {
                num = (new UnitAccess()).IsUniqueVinForaLoan(unit.heavyequipment.SerialNumber, unit.LoanId);
            }
            if (num != 0 && num != 1)
            {
                unit.UnitId = gc.GenerateUnitId(loanCode, unit.LoanId);

                //unit.UnitId = "LEN11_01-56454-78-676-000003";
                //if()
                string IDNumber;
                UnitAccess ua = new UnitAccess();
                var res = ua.InsertUnit(unit, userId, out IDNumber);

                //if mention advance fee, then insert in to fee table
                if (res == true && unit.AddAndAdvance)
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



                flag = 1;
                if (res)
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

                            res = ua.InsertTitleDocumentUploadInfo(xmlDoc, unit.UnitId);

                        }
                        catch (Exception ex)
                        {
                            flag = 0;
                            throw ex;
                        }
                    }
                    TempData["Msg"] = 1;

                    return RedirectToAction("AddUnit");
                }
            }
            TempData["Msg"] = 2;
            return RedirectToAction("AddUnit", unit);

            //return Json(new { flag = 0 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Auther: kasun
        /// get the models accrding to the vehicle model year and make
        /// </summary>
        /// <param name="make"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetModels(string make, int year,int unitId)
        {
            List <UnitYearMakeModel> modelList = (new UnitAccess()).GetVehicleModelsByMakeYear(make, year, unitId);

            SelectList modelSelectList = new SelectList(modelList, "VehicleModel", "VehicleModel");
            //var obj = new
            //{
            //    modelSelectList = modelSelectList
            //};
            return Json(modelSelectList);
        }

        
        //[HttpPost]
        //public void StoreMakeModels(IList<VehicleUnit> List)
        //{
        //    (new UnitAccess()).StoreMakeModels(List);
        //}

        /// <summary>
        /// Auther: kasun
        /// get the mskes accrding to the vehicle model year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMakes(int year,int unitId)
        {
            List<UnitYearMakeModel> makeList = (new UnitAccess()).GetVehicleMakesByYear(year,unitId);

            SelectList makeSelectList = new SelectList(makeList, "VehicleMake", "VehicleMake");
            //var obj = new
            //{
            //    modelSelectList = modelSelectList
            //};
            return Json(makeSelectList);
        }

        /// <summary>
        /// Auther: kasun
        /// get year accrding to the vehicle vin number
        /// </summary>
        /// <param name="vin"></param>
        /// <returns>year</returns>
        [HttpPost]
        public ActionResult GetYearByVin(string vin)
        {
            int year = (new UnitAccess()).DecodeVINYear(vin);

            return Json(year);
        }

        /// <summary>
        /// Auther: kasun
        /// get make accrding to the vehicle vin number
        /// </summary>
        /// <param name="vin"></param>
        /// <returns>year</returns>
        [HttpPost]
        public ActionResult GetMakeByVin(string vin)
        {
            string make = (new UnitAccess()).DecodeVINMake(vin);

            return Json(make);
        }


        [HttpPost]
        public ActionResult GetModelByVin(string makex, int yearx)
        {
            string str = "";
            List<UnitYearMakeModel> modelList = (new UnitAccess()).GetVehicleModelsByMakeYear(makex, yearx, 1);
            if (modelList != null && modelList.Count > 0) {
                if (modelList.Count == 1)
                {
                    str = modelList[0].VehicleModel;
                }
                else {
                    str = "";
                }
            }           
            return Json(str);
        }

        public ActionResult LoanInfo(string title, string msg)
        {
            ViewBag.Msg = msg;
            int userId;
            string loanCode;
            try {
                userId = userData.UserId;

                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }

            ViewBag.Title = title;
            
            ViewBag.Username = userData.UserName;
            BranchAccess ba = new BranchAccess();
          
        
          
                ViewBag.roleId = userData.RoleId;
            // get the Company type for front end view
            int comType = ba.getCompanyTypeByUserId(userId);
            ViewBag.loanCompanyType = (comType == 1) ? "Dealer" : "Lender";

            LoanSetupStep1 loan = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            Session["addUnitloan"] = loan;
            NonRegBranch nonRegBranch = ba.getNonRegBranchByNonRegBranchId(loan.nonRegisteredBranchId);

            ViewBag.NonRegBranchName = nonRegBranch.BranchName;

            //dealer email address, this will be used on curtailment page
            Session["DealerEmail"] = nonRegBranch.BranchEmail;

            ViewBag.loanBranchAddress =  (nonRegBranch.BranchAddress1 != "" ? nonRegBranch.BranchAddress1 : "") + (nonRegBranch.BranchAddress2 != "" ? ", " + nonRegBranch.BranchAddress2 : "") + (nonRegBranch.BranchCity != "" ? ", " + nonRegBranch.BranchCity : "");

            ViewBag.CurtailmentDueDate = loan.CurtailmentDueDate;

            ViewBag.LoanNumber = loan.loanNumber;

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

            return PartialView((new UnitAccess()).GetLoanPaymentDetailsByLoanId(loanSetupStep1.loanId));

        }


        public ActionResult GetLinkBar()
        {

            int userId = userData.UserId;
            string loanCode = "";

            UserRightsAccess access = new UserRightsAccess();

            /////retrive all rights
            List<Right> rights = new List<Right>();

            int userRole = userData.RoleId;
            if ((Session["loanCode"] != null) && (!string.IsNullOrEmpty(Session["loanCode"].ToString())))
            {
                loanCode = Session["loanCode"].ToString();
            }

            if (userRole == 3)
            {
                rights = access.GetUserRightsByLoanCode(loanCode, userId);
            }
            ViewBag.Role = userRole;
            //if (userRole == 3)
            //{
            //    ///get permission string for the relevent user
            //    List<Right> permissionString = access.getRightsString(userId,0);
            //    if (permissionString.Count == 1)
            //    {


            //        string permission = permissionString[0].rightsPermissionString;
            //        if (permission != "")
            //        {
            //            string[] charactors = permission.Split(',');

            //            List<Right> temprights = new List<Right>();

            //            foreach (var charactor in charactors)
            //            {
            //                foreach (var obj in rights)
            //                {
            //                    if (string.Compare(obj.rightId, charactor) == 0)
            //                    {
            //                        temprights.Add(obj);
            //                        break;

            //                    }

            //                }
            //            }

            //            rights = temprights;


            //        }
            //        else
            //        {
            //            rights = new List<Right>();
            //        }


            //    }

            //    else if (permissionString.Count == 0)
            //    {

            //        rights = new List<Right>();
            //    }



            //}

            //string loanCode = Session["loanCode"].ToString();

            if (Session["addUnitloan"] == null)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });
            }

            LoanSetupStep1 loan = (LoanSetupStep1)Session["addUnitloan"];

            //Title ttl = (new TitleAccess()).getTitleDetails(loan.loanId);
            if (Session["IsTitleTrack"] != null)
            {
                if (int.Parse(Session["IsTitleTrack"].ToString())==1)
                    {
                        ViewBag.ttlAccess = 1;
                    }
                    else
                    {
                        ViewBag.ttlAccess = 0;

                    }
                
                
            }
            else
            {
                ViewBag.ttlAccess = 0;

            }

            if ((Session["oneLoanDashboard"] != null) && (!string.IsNullOrEmpty(Session["oneLoanDashboard"].ToString())))
            {
                Loan loanObj = new Loan();
                    loanObj = (Loan)Session["oneLoanDashboard"];
                if ((loanObj.LotInspectionFee == 1) || (loanObj.MonthlyLoanFee == 1) || (loanObj.AdvanceFee == 1))
                    {
                        ViewBag.FeeLB = 1;
                    }
                    else
                    {
                        ViewBag.FeeLB = 0;
                    }
            }
           else if ((Session["loanDashboard"] != null) && (!string.IsNullOrEmpty(Session["loanDashboard"].ToString())))
            {
                Loan loanObj = new Loan();
                loanObj = (Loan)Session["loanDashboard"];
                if ((loanObj.LotInspectionFee == 1) || (loanObj.MonthlyLoanFee == 1) || (loanObj.AdvanceFee == 1))
                {
                    ViewBag.FeeLB = 1;
                }
                else
                {
                    ViewBag.FeeLB = 0;
                }
            }
            else if ((Session["oneLoanDashboard"] == null) && (Session["loanDashboard"] == null))
            {
                return RedirectToAction("UserLogin", "Login");
            }
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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
        /// <summary>
        /// check Vin already wxist on loan 
        /// </summary>
        /// <param name="vin"></param>
        /// <returns></returns>
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

                    string alertmsg = " Dealer User " + user.FirstName + " " + user.LastName + " requested to advance " + user.NoOfUnitsAdded + " new unit(s) for loan number " + user.LoanNumber + " on " + user.AddedDate + ". Please login to the system and go to advance page to advance the items. ";
                    int rep = (new UserAccess()).InsertDearlerUserRequest(0,0,user.UserIdForSendReq, Code,alertmsg);

                    string body = "Hi , <br /><br /> Dealer User " + user.FirstName + " " + user.LastName + " requested to advance " + user.NoOfUnitsAdded + " new unit(s) for loan number " + user.LoanNumber +" on "+user.AddedDate+

                                  ". Please login to the system and go to advance page to advance item(s). <br /><br/> Thanks <br />.";

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

    }
}