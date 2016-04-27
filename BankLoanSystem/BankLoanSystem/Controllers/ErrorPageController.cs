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

        /// <summary>
        ///CreatedBy : Nadeeka
        /// CreatedDate: 04/27/2016
        /// 
        /// Create error log file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool CreateLogFile(string fileName)
        {
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

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 04/27/2016
        /// 
        /// Insert record to error log file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="page"></param>
        /// <param name="message"></param>
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
                if (CreateLogFile(System.Web.HttpContext.Current.Server.MapPath(mapPath)))
                {                                  
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