﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.UnitTitle
{
    public class UnitTitleController : Controller
    {
        // GET: Title
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TitleStatusUpdate()
        {
            return View();
        }
    }
}