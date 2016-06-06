using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.DeleteUnit
{
    public class DeleteUnitController : Controller
    {
        User _userData = new User();


        // GET: DeleteUnit
        public ActionResult Index()
        {
            return View();
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["AuthenticatedUser"] != null)
                {
                    _userData = ((User)Session["AuthenticatedUser"]);
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
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }


        public ActionResult Delete()
        {
            string loanCode;

            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
            if (_userData.RoleId == 3)
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
                            if (x == "U008")
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
            else if (_userData.RoleId == 4)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }
            LoanSetupStep1 loan = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            UnitDeleteViewModel unitDeleteViewModel = new UnitDeleteViewModel();

            //get all units
            UnitAccess ua = new UnitAccess();
            unitDeleteViewModel.DeleteUnits = ua.GetAllUnitsByLoanId(loan.loanId);
            Session["UnitDeleteList"] = unitDeleteViewModel.DeleteUnits;
            Session["deleteUnitloanId"] = loan.loanId;
            ViewBag.DeleteList = unitDeleteViewModel.DeleteUnits;

            return View(unitDeleteViewModel);
        }
        
        public UnitDeleteViewModel GetGridViewDetails(int? viewType, string id, string year, string make, string model)
        {
            UnitDeleteViewModel unitModel = new UnitDeleteViewModel();

            if (Session["UnitDeleteList"] != null)
            {
                unitModel.DeleteUnits = (List<UnitDeleteModel>)Session["UnitDeleteList"];

                if (viewType == 1)
                    unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.UnitStaus == 1).ToList();
                else if (viewType == 2)
                    unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.UnitStaus == 0).ToList();
                else if (viewType == 5)
                {
                    //unitModel.DeleteUnits = unitModel.DeleteUnits.Where( x => ( year != "" && x.Year == year) && (make != "" && x.Make == make)).ToList();
                    if (id != "" && year != "" && make != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Year == year && x.Make == make && x.Model == model).ToList();
                    }
                    else if (id != "" && year != "" && make != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Year == year && x.Make == make).ToList();
                    }
                    else if (id != "" && year != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Year == year && x.Model == model).ToList();
                    }
                    else if (id != "" && make != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Make == make && x.Model == model).ToList();
                    }
                    else if (id != "" && year != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Year == year).ToList();
                    }
                    else if (id != "" && make != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Make == make).ToList();
                    }
                    else if (id != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id && x.Model == model).ToList();
                    }
                    else if (year != "" && make != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Year == year && x.Make == make && x.Model == model).ToList();
                    }
                    else if (year != "" && make != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Year == year && x.Make == make).ToList();
                    }
                    else if (year != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Year == year && x.Model == model).ToList();
                    }
                    else if (make != "" && model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Make == make && x.Model == model).ToList();
                    }
                    else if (id != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.IdentificationNumber.Substring(x.IdentificationNumber.Length - 6) == id).ToList();
                    }
                    else if (year != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Year == year).ToList();
                    }
                    else if (make != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Make == make).ToList();
                    }
                    else if (model != "")
                    {
                        unitModel.DeleteUnits = unitModel.DeleteUnits.Where(x => x.Model == model).ToList();
                    }
                    else
                    {
                        unitModel.DeleteUnits = new List<UnitDeleteModel>();
                    }
                }
            }
            return unitModel;
        }

        public ActionResult GridView(int? viewType, string id, string year, string make, string model)
        {
            id = id.ToUpper();
            DeleteSearchUnit seachUnit = new DeleteSearchUnit();

            seachUnit.ViewType = viewType;
            seachUnit.Id = id;
            seachUnit.Year = year;
            seachUnit.Make = make;
            seachUnit.Model = model;

            Session["DeleteSearchUnit"] = seachUnit;

            UnitDeleteViewModel unitModel = GetGridViewDetails(viewType, id, year, make, model);

            return PartialView(unitModel);
        }

        public ActionResult FeeDetails(string unitId)
        {
            decimal paidCurtAmount = 0.00M;

            UnitFeeTypeViewModel feeDetailsModel = new UnitFeeTypeViewModel();

            UnitAccess ua = new UnitAccess();
            feeDetailsModel.UnitFeeTypes = ua.GetUnitFeeType(unitId);

            UnitFeeTypeViewModel feeDetailsModelNew = new UnitFeeTypeViewModel();

            var lists = feeDetailsModel.UnitFeeTypes.GroupBy(x=>x.CurtNumber).ToList();
            feeDetailsModelNew.UnitFeeTypes = new List<UnitFeeType>();

            foreach (var grp in lists)
            {
                int i = 0;
                var count = grp.Select(p => new { p.LoanId, p.UnitId, p.CurtNumber, p.TblName, p.FeeType, p.PaidAmount, p.PaidDate }).Distinct().Count();

                foreach (var d in grp)
                {
                    if (count == 3)
                    {
                        if (d.TblName == "CS")
                        {
                            i++;
                            continue;
                        }
                        paidCurtAmount += d.PaidAmount;
                        feeDetailsModelNew.UnitFeeTypes.Add(new UnitFeeType { LoanId = d.LoanId, UnitId = d.UnitId, CurtNumber = d.CurtNumber, TblName = d.TblName, FeeType = d.FeeType, PaidAmount = d.PaidAmount, PaidDate = d.PaidDate });
                        i++;
                    }
                    else
                    {
                        if(d.TblName != "F")
                            paidCurtAmount += d.PaidAmount;
                        feeDetailsModelNew.UnitFeeTypes.Add(new UnitFeeType { LoanId = d.LoanId, UnitId = d.UnitId, CurtNumber = d.CurtNumber, TblName = d.TblName, FeeType = d.FeeType, PaidAmount = d.PaidAmount, PaidDate = d.PaidDate });
                    }
                }
            }
            Session["PaidCurtAmount"] = paidCurtAmount;
            return PartialView(feeDetailsModelNew);
        }

        public ActionResult DeleteUnitPost(string unitId,string identificationNo)
        {
            decimal paidCurtAmount = 0.00M;
            if (Session["PaidCurtAmount"] != null)
            {
                paidCurtAmount = (decimal) Session["PaidCurtAmount"];
            }


            int loanId = (int) Session["deleteUnitloanId"];
            UnitAccess ua = new UnitAccess();
            UnitDeleteViewModel unitModel = new UnitDeleteViewModel();
            int res = ua.DeleteUnit(loanId, unitId, paidCurtAmount);
            
            if (res == 1)
            {
                if (Session["UnitDeleteList"] != null)
                {
                    unitModel.DeleteUnits = (List<UnitDeleteModel>) Session["UnitDeleteList"];
                    var itemToRemove = unitModel.DeleteUnits.Single(r => r.UnitId == unitId);
                    unitModel.DeleteUnits.Remove(itemToRemove);
                    Session["UnitDeleteList"] = unitModel.DeleteUnits;
                }

                Log log = new Log(_userData.UserId, _userData.Company_Id, _userData.BranchId, loanId, "Delete Unit", "Delete Unit:" + identificationNo, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
            }
            else
            {
                return new HttpStatusCodeResult(400, "Failed to delete unit, try again!");
            }

            if (Session["DeleteSearchUnit"] != null)
            {
                DeleteSearchUnit seachUnit = (DeleteSearchUnit) Session["DeleteSearchUnit"];
                unitModel = GetGridViewDetails(seachUnit.ViewType, seachUnit.Id, seachUnit.Year, seachUnit.Make,
                    seachUnit.Model);
            }
            ViewBag.DeleteList = unitModel.DeleteUnits;
            return PartialView("GridView", unitModel);
        }
    }
}