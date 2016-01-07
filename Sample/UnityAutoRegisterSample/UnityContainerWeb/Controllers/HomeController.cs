using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityContainerWeb.Models;

namespace UnityContainerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService service;

        public HomeController(IUserService userService)
        {
            service = userService;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Result = service.Authorize("test", "test") ? "OK" : "FAILED";
            return View();
        }
    }
}