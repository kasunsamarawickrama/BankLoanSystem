using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Controllers.ManageUser
{
    public class EditRightsController : Controller
    {
        // GET: EditRights
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/16
        /// Updated by   : kasun Samarawickrama
        /// Updated Date : 2016/01/18
        /// 
        /// Edit User Rights
        /// </summary>
        /// <param name="userId">login user </param>
        /// <param name="editorId">edit field user</param>
        /// <returns></returns>
        public ActionResult EditRights(string lbl1, string lbl2)

        {

            if (lbl1 != null) {
                ViewBag.SuccessMsg = lbl1;
            }
            else if (lbl2 != null)
            {
                ViewBag.ErrorMsg = lbl2;
            }
            else
            {
                ViewBag.SuccessMsg = "";
                ViewBag.ErrorMsg = "";
            }

            var userId= (int)Session["userId"];
            int ownerId = (int)Session["editUserId"];

            if (userId > 0)
            {
                var access = new UserRightsAccess();

                ///retrive all rights
                List<Right> rights = access.getRights();

                ///get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(ownerId);

                if (permissionString.Count == 1  ) {

                   
                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<string> intArray = new List<string>();

                        foreach (var charactor in charactors)
                        {
                            intArray.Add(charactor);
                        }

                        foreach (var chr in intArray)
                        {
                            foreach (var obj in rights)
                            {
                                if (string.Compare(obj.rightId, chr) == 0)
                                {
                                    obj.active = true;
                                }
                                obj.editorId = userId;
                                obj.userId = ownerId;

                            }
                        }
                    }
                    else {
                        foreach (var obj in rights)
                        {
                              
                            obj.editorId = userId;
                            obj.userId = ownerId;

                        }
                        
                    }
                    
                }
                else if (permissionString.Count != 1)
                {
                    return RedirectToAction("editUser", "ManageUsers");
                }
                ViewBag.userId = userId;
                ViewBag.ownerId = ownerId;

                return View(rights);
            }
            else
            {
                return RedirectToAction("editUser", "ManageUsers");
            }                
        }
 
        /// <summary>
        ///  CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/17
        /// 
        /// Updating the User Right String for a user Id
        /// </summary>
        /// <param name="rightList">List of Rights</param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult EditRights(IList<Right> rightList )
        {
            
            List<string> returnIntArray = new List<string>();

            for (int i=0; i< rightList.Count;)
            {
                if (rightList[i].active == true)
                {
                    returnIntArray.Add(rightList[i].rightId);
                }
                i++;
            }
            var resultRightIdString = string.Join(",", returnIntArray);

            var returnRight = new Right();

            returnRight.userId = rightList[0].userId;
            returnRight.editorId = rightList[0].editorId;
            returnRight.rightsPermissionString = resultRightIdString;

            var returnAccess = new UserRightsAccess();

            if (returnAccess.postNewRights(returnRight))
            {
                ViewBag.SuccessMsg = "Succesfully Updated";
                return RedirectToAction("EditRights", "EditRights", new { lbl1 = ViewBag.SuccessMsg });
                
            }
            else {
                ViewBag.ErrorMsg = "Sorry, rights can't update";
                return RedirectToAction("EditRights", "EditRights", new { lbl2 = ViewBag.ErrorMsg });
            }

            
        }

       
    }
}