using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Recodme.RD.FullStoQReborn.BusinessLayer.Commercial;
using WebAPI.Models;
using WebAPI.Models.CommercialViewModel;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly RegionBusinessObject _rbo = new RegionBusinessObject();
        private readonly CompanyBusinessObject _cbo = new CompanyBusinessObject();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var getRboOperation = _rbo.ListNotDeleted();
            var getCboOperation = _cbo.ListNotDeleted();


            var rList = new List<SelectListItem>();
            foreach (var item in getRboOperation.Result)
            {
                rList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            var cList = new List<SelectListItem>();
            foreach (var item in getCboOperation.Result)
            {
                cList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }

            ViewBag.Regions = rList;
            ViewBag.Companies = cList;
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Administration()
        {
            return View();
        }

        
        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Manager()
        {
            return View();
        }

        public IActionResult Security()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
