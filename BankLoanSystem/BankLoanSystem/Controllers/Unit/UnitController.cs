using System;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using BankLoanLocal.Models;
using BankLoanSystem.Code;

namespace BankLoanSystem.Controllers.Unit
{
    public class UnitController : Controller
    {
        private static LoanSetupStep1 _loan;
        User userData = new Models.User();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["AuthenticatedUser"] != null)
            {
                try
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                }
                catch
                {
                    filterContext.Controller.TempData.Add("UserLogin", "Login");
                }
            }
            else
            {
                filterContext.Controller.TempData.Add("UserLogin", "Login");
                //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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
                else if (Flag == 0)
                {
                    ViewBag.Msg = "Error";
                }
            }
           

            int userId = userData.UserId;

            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return new HttpStatusCodeResult(404, "Failed find loan.");

            string loanCode = Session["loanCode"].ToString();

            _loan = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            int loanId = _loan.loanId;



            ViewBag.loanDetails = _loan;
            if (_loan.selectedUnitTypes.Count == 1) {
                ViewBag.UnitTypeId = _loan.selectedUnitTypes[0].unitTypeName;
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

            _loan.loanId = loanId;
            unit.AdvancePt = _loan.advancePercentage;
            unit.LoanId = loanId;
            unit.LoanAmount = _loan.loanAmount;
            unit.AdvanceDate = DateTime.Now;
            unit.StartDate = _loan.startDate;
            unit.EndDate = _loan.maturityDate;

            //get company type
            //1 - Lender
            //2 - Dealer
            BranchAccess ba = new BranchAccess();
            int companyType = ba.getCompanyTypeByUserId(userId);

            ViewBag.CompabyType = companyType;

            //Check title 
            TitleAccess ta = new TitleAccess();
            Title title = ta.getTitleDetails(_loan.loanId);

            if (title != null)
            {
                bool isTitleTrack = title.IsTitleTrack;
                if (isTitleTrack)
                    ViewBag.IsTitleTrack = "Yes";

                string upload = title.TitleAcceptMethod;
                if (!string.IsNullOrEmpty(upload) && upload == "scanned title adequate")
                    ViewBag.Upload = "Yes";

            }

            UnitAccess ua = new UnitAccess();
            LoanPaymentDetails loanPaymentDetails = ua.GetLoanPaymentDetailsByLoanId(loanId);

            unit.Balance = loanPaymentDetails.BalanceAmount;

            ViewBag.Editable = _loan.isEditAllowable ? "Yes" : "No";

            return PartialView(unit);
        }


        [HttpPost]
        [ActionName("AddUnit")]
        public ActionResult AddUnitPost(Models.Unit unit, string btnAdd, List<HttpPostedFileBase> fileUpload)
        {
            int flag = 0;
           

           int userId = userData.UserId;

            switch (btnAdd)
            {
                case "Add":
                    unit.IsAdvanced = false;
                    unit.AddAndAdvance = false;
                    break;
                case "Add and Advance":
                    unit.IsAdvanced = true;
                    unit.AddAndAdvance = true;
                    break;
            }

            GeneratesCode gc = new GeneratesCode();
            unit.UnitId = gc.GenerateUnitId(_loan.loanNumber, unit.LoanId);

            //if()

            UnitAccess ua = new UnitAccess();
            var res = ua.InsertUnit(unit, userId);
            flag = 1;
            if (res)
            {
                //Handling file attachments
                UserManageAccess uma = new UserManageAccess();
                GetDetails user = uma.getUserById(userId);

                //Check directory is already exists, if not create new
                string mapPath = "~/Images/UnitImages/" + user.CompanyCode + "/" + _loan.RegisteredBranchCode + "/";
                if (!Directory.Exists(Server.MapPath(mapPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(mapPath));
                }

                List<TitleUpload> titleList = new List<TitleUpload>();

                int imageNo = 1;
                if (fileUpload != null)
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
            TempData["Msg"] = 1;
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
        public ActionResult GetModels(string make, int year)
        {
            List <UnitYearMakeModel> modelList = (new UnitAccess()).GetVehicleModelsByMakeYear(make, year);

            SelectList modelSelectList = new SelectList(modelList, "VehicleModel", "VehicleModel");
            //var obj = new
            //{
            //    modelSelectList = modelSelectList
            //};
            return Json(modelSelectList);
        }

        /// <summary>
        /// Auther: kasun
        /// get the mskes accrding to the vehicle model year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMakes(string make, int year)
        {
            List<UnitYearMakeModel> makeList = (new UnitAccess()).GetVehicleMakesByYear(year);

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
                return new HttpStatusCodeResult(404,"Session Expired");
            }

            ViewBag.Title = title;
            User user = (new UserAccess()).retreiveUserByUserId(userId);
            ViewBag.Username = user.UserName;
            BranchAccess ba = new BranchAccess();
            ViewBag.Branch = (ba.getBranchByBranchId(user.BranchId)).BranchName;
            ViewBag.roleId = user.RoleId;
            // get the Company type for front end view
            int comType = ba.getCompanyTypeByUserId(userId);
            ViewBag.loanCompanyType = (comType == 1) ? "Dealer" : "Lender";

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            NonRegBranch nonRegBranch = ba.getNonRegBranchByNonRegBranchId(loanSetupStep1.nonRegisteredBranchId);
            ViewBag.loanBranchAddress = nonRegBranch.BranchName + " - " + (nonRegBranch.BranchAddress1 != "" ? nonRegBranch.BranchAddress1 : "") + (nonRegBranch.BranchAddress2 != "" ? "," + nonRegBranch.BranchAddress2 : "") + (nonRegBranch.BranchCity != "" ? "," + nonRegBranch.BranchCity : "");

            ViewBag.LoanNumber = loanSetupStep1.loanNumber;
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
                return new HttpStatusCodeResult(404, "Session Expired");
            }
            //int userId = 57;

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            return PartialView((new UnitAccess()).GetLoanPaymentDetailsByLoanId(loanSetupStep1.loanId));

        }


        public ActionResult GetLinkBar()
        {

            int userId = userData.UserId;


            var access = new UserRightsAccess();

            ///retrive all rights
            List<Right> rights = access.getRights();

            int userRole = (new UserManageAccess()).getUserRole(userId);

            if (userRole == 3)
            {
                ///get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(userId);
                if (permissionString.Count == 1)
                {


                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<Right> temprights = new List<Right>();

                        foreach (var charactor in charactors)
                        {
                            foreach (var obj in rights)
                            {
                                if (string.Compare(obj.rightId, charactor) == 0)
                                {
                                    temprights.Add(obj);
                                    break;

                                }

                            }
                        }

                        rights = temprights;


                    }
                    else
                    {
                        rights = new List<Right>();
                    }


                }

                else if (permissionString.Count == 0)
                {

                    rights = new List<Right>();
                }



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
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404, "Session Expired");
            }
            //int userId = 57;

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            return PartialView((new UnitAccess().GetJustAddedUnitDetails(userId, loanSetupStep1.loanId)));
        }
    }
}