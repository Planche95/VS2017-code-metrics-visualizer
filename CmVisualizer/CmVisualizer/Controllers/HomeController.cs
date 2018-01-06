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
        public IActionResult Result(SourceInfoViewModel model)
        {

            SolutionMetrics solutionMetrics = new SolutionMetricsBuilder()
                                                    .From(model.ExcelMetrics)
                                                    .Build();

            int[] settingsProject = (int[])TempData.Peek("Project");
            int[] settingsNamespace = (int[])TempData.Peek("Namespace");
            int[] settingsClass = (int[])TempData.Peek("Class");
            int[] settingsFunction = (int[])TempData.Peek("Function");

            ResultViewModel result = new ResultViewModel
            {
                projectsResult = CalculateMetrics("Projects", settingsProject,
                solutionMetrics.ProjectsMetrics
                                .Cast<MetricsData>()
                                .ToList()),

                namespacesResult = CalculateMetrics("Namespaces", settingsNamespace,
                solutionMetrics.ProjectsMetrics
                                .SelectMany(pm => pm.NamespacesMetrics)
                                .Cast<MetricsData>()
                                .ToList()),

                classessResult = CalculateMetrics("Classes", settingsClass,
                solutionMetrics.ProjectsMetrics
                                .SelectMany(pm => pm.NamespacesMetrics)
                                .SelectMany(nm => nm.ClassesMetrics)
                                .Cast<MetricsData>()
                                .ToList()),

                functionsResult = CalculateMetrics("Functions", settingsFunction,
                solutionMetrics.ProjectsMetrics
                                .SelectMany(pm => pm.NamespacesMetrics)
                                .SelectMany(nm => nm.ClassesMetrics)
                                .SelectMany(cm => cm.FunctionsMetrics)
                                .Cast<MetricsData>()
                                .ToList())
            };

            return View(result);
        }

        private SingleResultViewModel CalculateMetrics(string name, int[] settings, List<MetricsData> metrics)
        {
            SingleResultViewModel result = new SingleResultViewModel() { name = name };

            float all, bad, mid, good;

            all = metrics.Count();
            bad = metrics
                .Where(pm => pm.Maintability < settings[0])
                .Count();
            mid = metrics
                .Where(pm => settings[0] <= pm.Maintability && settings[1] > pm.Maintability)
                .Count();
            good = metrics
                .Where(pm => settings[1] <= pm.Maintability)
                .Count();

            result.maintainability["bad"] = Math.Round((bad / all) * 100, 2);
            result.maintainability["mid"] = Math.Round((mid / all) * 100, 2);
            result.maintainability["good"] = Math.Round((good / all) * 100, 2);

            good = metrics
                .Where(pm => pm.Cyclomanic < settings[2])
                .Count();
            mid = metrics
                .Where(pm => settings[2] <= pm.Cyclomanic && settings[3] > pm.Cyclomanic)
                .Count();
            bad = metrics
                .Where(pm => settings[3] <= pm.Cyclomanic)
                .Count();

            result.cyclomaticComplexity["bad"] = Math.Round((bad / all) * 100, 2);
            result.cyclomaticComplexity["mid"] = Math.Round((mid / all) * 100, 2);
            result.cyclomaticComplexity["good"] = Math.Round((good / all) * 100, 2);

            good = metrics
                .Where(pm => pm.DephOfInheritance < settings[4])
                .Count();
            mid = metrics
                .Where(pm => settings[4] <= pm.DephOfInheritance && settings[5] > pm.DephOfInheritance)
                .Count();
            bad = metrics
                .Where(pm => settings[5] <= pm.DephOfInheritance)
                .Count();

            result.dephOfInheritance["bad"] = Math.Round((bad / all) * 100, 2);
            result.dephOfInheritance["mid"] = Math.Round((mid / all) * 100, 2);
            result.dephOfInheritance["good"] = Math.Round((good / all) * 100, 2);

            good = metrics
                .Where(pm => pm.LinesOfCode < settings[6])
                .Count();
            mid = metrics
                .Where(pm => settings[6] <= pm.LinesOfCode && settings[7] > pm.LinesOfCode)
                .Count();
            bad = metrics
                .Where(pm => settings[7] <= pm.LinesOfCode)
                .Count();

            result.linesOfCode["bad"] = Math.Round((bad / all) * 100, 2);
            result.linesOfCode["mid"] = Math.Round((mid / all) * 100, 2);
            result.linesOfCode["good"] = Math.Round((good / all) * 100, 2);

            result.overall["bad"] = Math.Round((result.linesOfCode["bad"] + result.cyclomaticComplexity["bad"] +
                                        result.maintainability["bad"] + result.dephOfInheritance["bad"])/4, 2);
            result.overall["mid"] = Math.Round((result.linesOfCode["mid"] + result.cyclomaticComplexity["mid"] +
                                        result.maintainability["mid"] + result.dephOfInheritance["mid"]) / 4, 2);
            result.overall["good"] = Math.Round((result.linesOfCode["good"] + result.cyclomaticComplexity["good"] +
                                        result.maintainability["good"] + result.dephOfInheritance["good"]) / 4, 2);


            return result;
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

        //[HttpPost]
        //public IActionResult Result(ResultViewModel result)
        //{
        //    return View(result);
        //}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
