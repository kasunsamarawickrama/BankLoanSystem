using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult ErrorPage()
        {
            return View();
        }

        public bool CreateLogFile(string fileName)
        {
            //Check directory is already exists, if not create new
            //string mapPath = "~/LogFiles/"+fileName+".txt";
            // string mapPath = @"C:\Projects\BankLoanSystem\BankLoanSystem\BankLoanSystem\ErrorLog\"+ fileName+".txt"; 

            // Create the file.
            // string path = System.IO.Path.Combine(Server.MapPath("~/Content/Uploads/"), System.IO.Path.GetFileName(file.FileName));
            //if (System.IO.File.Exists(mapPath))
            //{
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("File created on - " + DateTime.Now.ToString());
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void InsertRecordToLogFile(string fileName, string page, string message)
        {
            try
            {
                string mapPath = @"~\ErrorLog\" + fileName+".txt";
                string folder = "~/ErrorLog/";
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(folder)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(folder));
                }
                //string mapPath = @"C:\Projects\BankLoanSystem\BankLoanSystem\BankLoanSystem\ErrorLog\" + fileName + ".txt";
                if (CreateLogFile(System.Web.HttpContext.Current.Server.MapPath(mapPath)))
                {
                    // Open the stream and read it back.                
                    using (TextWriter tw = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath(mapPath), true))
                    {
                        tw.WriteLine("\r\n" + DateTime.Now.ToString() + " - " + message  + " - " + page);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}