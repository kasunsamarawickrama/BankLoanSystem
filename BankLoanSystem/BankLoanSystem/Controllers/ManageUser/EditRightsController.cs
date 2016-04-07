using System.Collections.Generic;
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
            ViewBag.login = false;
            if (lbl1 != null)
            {
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
            if (Session["userId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            if (Session["editUserIds"] == null)
            {
                return RedirectToAction("editUser", "UserManagement");
            }
            int userId = (int)Session["userId"];
            int ownerId = (int)Session["editUserIds"];

            if (userId > 0)
            {
                var access = new UserRightsAccess();

                ///retrive all rights
                List<Right> rights = access.getRights();

                ///get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(ownerId,0);

                if (permissionString.Count == 1)
                {


                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<string> intArray = new List<string>();

                        foreach (var charactor in charactors)
                        {
                            intArray.Add(charactor);
                        }
                        foreach (var obj in rights)
                        {
                            obj.active = true;
                        }
                        foreach (var chr in intArray)
                        {
                            foreach (var obj in rights)
                            {
                                if (string.Compare(obj.rightId, chr) == 0)
                                {
                                    obj.active = false;
                                }
                                obj.editorId = userId;
                                obj.userId = ownerId;

                            }
                        }
                    }
                    else
                    {
                        foreach (var obj in rights)
                        {

                            obj.editorId = userId;
                            obj.userId = ownerId;

                        }

                    }

                }

                else if (permissionString.Count == 0)
                {
                    foreach (var obj in rights)
                    {

                        obj.editorId = userId;
                        obj.userId = ownerId;

                    }

                }
                else
                {
                    return RedirectToAction("editUser", "UserManagement");
                }
                ViewBag.userId = userId;
                ViewBag.ownerId = ownerId;

                return PartialView(rights);
            }
            else
            {
                return RedirectToAction("editUser", "UserManagement");
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
        public ActionResult EditRights(IList<Right> rightList)
        {
            int userId = (int)Session["userId"];
            int ownerId = (int)Session["editUserIds"];
            UserManageAccess uma = new UserManageAccess();
            // not allow to edit rights for admin and superadmin... not allow user to use this page
            if (uma.getUserRole(ownerId) < 3 || uma.getUserRole(userId) == 3)
            {
                return new HttpStatusCodeResult(404);
            }
            List<string> returnIntArray = new List<string>();

            for (int i = 0; i < rightList.Count;)
            {
                if (rightList[i].active == false)
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
            else
            {
                ViewBag.ErrorMsg = "Sorry, rights can't update";
                return RedirectToAction("EditRights", "EditRights", new { lbl2 = ViewBag.ErrorMsg });
            }
        }


        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/16
        /// Updated by   : kasun Samarawickrama
        /// Updated Date : 2016/01/18
        /// 
        /// Edit User Rights
        /// </summary>
        /// <returns></returns>
        public ActionResult SetRights(string lbl1, string lbl2)
        {
            ViewBag.login = false;
            if (lbl1 != null)
            {
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
            if (Session["userId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            if (Session["editUserIds"] == null)
            {
                return RedirectToAction("Create", "CreateUser");
            }
            int userId = (int)Session["userId"];
            int ownerId = (int)Session["editUserIds"];

            UserManageAccess uma = new UserManageAccess();
            // not allow to edit rights for admin and superadmin... not allow user to use this page
            if (uma.getUserRole(ownerId) < 3 || uma.getUserRole(userId) == 3)
            {
                return new HttpStatusCodeResult(404);
            }

            if (userId > 0)
            {
                var access = new UserRightsAccess();

                ///retrive all rights
                List<Right> rights = access.getRights();

                ///get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(ownerId,0);

                if (permissionString.Count == 1)
                {


                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<string> intArray = new List<string>();

                        foreach (var charactor in charactors)
                        {
                            intArray.Add(charactor);
                        }
                        foreach (var obj in rights)
                        {
                            obj.active = true;
                        }
                        foreach (var chr in intArray)
                        {
                            foreach (var obj in rights)
                            {
                                if (string.Compare(obj.rightId, chr) == 0)
                                {
                                    obj.active = false;
                                }
                                obj.editorId = userId;
                                obj.userId = ownerId;

                            }
                        }
                    }
                    else
                    {
                        foreach (var obj in rights)
                        {

                            obj.editorId = userId;
                            obj.userId = ownerId;

                        }

                    }

                }

                else if (permissionString.Count == 0)
                {
                    foreach (var obj in rights)
                    {

                        obj.editorId = userId;
                        obj.userId = ownerId;

                    }

                }
                else
                {
                    return RedirectToAction("Create", "CreateUser");
                }
                ViewBag.userId = userId;
                ViewBag.ownerId = ownerId;

                return PartialView(rights);
            }
            else
            {
                return RedirectToAction("Create", "CreateUser");
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
        public ActionResult SetRights(IList<Right> rightList)
        {

            List<string> returnIntArray = new List<string>();

            for (int i = 0; i < rightList.Count;)
            {
                if (rightList[i].active == false)
                {
                    returnIntArray.Add(rightList[i].rightId);
                }
                i++;
            }
            var resultRightIdString = string.Join(",", returnIntArray);

            var returnRight = new Right();

            returnRight.userId = rightList[0].userId;
            returnRight.editorId = rightList[0].editorId;



            UserManageAccess uma = new UserManageAccess();
            // not allow to edit rights for admin and superadmin... not allow user to use this page
            if (uma.getUserRole(returnRight.userId) < 3 || uma.getUserRole(returnRight.editorId) == 3)
            {
                return new HttpStatusCodeResult(404);
            }
            returnRight.rightsPermissionString = resultRightIdString;

            var returnAccess = new UserRightsAccess();

            if (returnAccess.postNewRights(returnRight))
            {
                ViewBag.SuccessMsg = "Succesfully Updated";
                return RedirectToAction("SetRights", "EditRights", new { lbl1 = ViewBag.SuccessMsg });
            }
            else
            {
                ViewBag.ErrorMsg = "Sorry, rights can't update";
                return RedirectToAction("SetRights", "EditRights", new { lbl2 = ViewBag.ErrorMsg });
            }
        }
    }
}