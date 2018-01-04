using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmVisualizer.Models;
using System.ComponentModel;
using CmVisualizer.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using Microsoft.AspNetCore.Hosting;

namespace CmVisualizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        //public Dictionary<string, int[]> data = new Dictionary<string, int[]>()
        //{
        //    { "Project", new int[]{ 1, 2, 3, 4, 5, 6, 7, 8 } },
        //    { "Namespace", new int[]{ 9, 10, 11, 12, 13, 14, 15, 16 } },
        //    { "Class", new int[]{ 17, 18, 19, 20, 21, 22, 23, 24 } },
        //    { "Function", new int[]{ 25, 26, 27, 28, 29, 30, 31, 32 } }
        //};

        //private string currentTab = "Project";

        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            TempData["currentTab"] = "Project";

            TempData["Project"] =  new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            TempData["Namespace"] = new int[] { 9, 10, 11, 12, 13, 14, 15, 16 };
            TempData["Class"] = new int[] { 17, 18, 19, 20, 21, 22, 23, 24 };
            TempData["Function"] = new int[] { 25, 26, 27, 28, 29, 30, 31, 32 };

            return View();
        }

        [HttpPost]
        public IActionResult CalculateResult(SourceInfoViewModel model)
        {

            SolutionMetrics solutionMetrics = new SolutionMetricsBuilder()
                                                    .From(model.ExcelMetrics)
                                                    .Build();

            return RedirectToAction("About");
        }

        public class Ugly
        {
            public string Name { get; set; }
            public int[] Values { get; set; }
        }

        [HttpPost]
        public IActionResult ChangeSetting(Ugly ugly)
        {
            TempData["currentTab"] = ugly.Name;

            return Json(new { success = TempData.Peek(ugly.Name) });
        }

        [HttpPost]
        public IActionResult SaveSetting(Ugly ugly)
        {
            TempData[(string)TempData.Peek("currentTab")] = ugly.Values;

            return Json(new { });
        }

        public IActionResult Result()
        {
            return View(new ResultViewModel());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
