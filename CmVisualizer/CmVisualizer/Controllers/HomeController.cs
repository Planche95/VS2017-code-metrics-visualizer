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

            ResultViewModel result = new ResultViewModel();
            float all, bad, mid, good;

            result.solutionResult = new SingleResultViewModel() { name = "Solution" };
            result.projectsResult = new SingleResultViewModel() { name = "Projects" };
            result.namespacesResult = new SingleResultViewModel() { name = "Namespaces" };
            result.classessResult = new SingleResultViewModel() { name = "Classes" };
            result.functionsResult = new SingleResultViewModel() { name = "Functions" };

            all = solutionMetrics.ProjectsMetrics.Count();
            bad = solutionMetrics.ProjectsMetrics
                .Where(pm => pm.Maintability < settingsProject[0])
                .Count();
            mid = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[0] <= pm.Maintability && settingsProject[1] > pm.Maintability)
                .Count();
            good = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[1] <= pm.Maintability)
                .Count();

            result.projectsResult.maintainability["bad"] = (bad / all) * 100;
            result.projectsResult.maintainability["mid"] = (mid / all) * 100;
            result.projectsResult.maintainability["good"] = (good / all) * 100;

            good = solutionMetrics.ProjectsMetrics
                .Where(pm => pm.Cyclomanic < settingsProject[2])
                .Count();
            mid = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[2] <= pm.Cyclomanic && settingsProject[3] > pm.Cyclomanic)
                .Count();
            bad = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[3] <= pm.Cyclomanic)
                .Count();

            result.projectsResult.cyclomaticComplexity["bad"] = (bad / all) * 100;
            result.projectsResult.cyclomaticComplexity["mid"] = (mid / all) * 100;
            result.projectsResult.cyclomaticComplexity["good"] = (good / all) * 100;

            good = solutionMetrics.ProjectsMetrics
                .Where(pm => pm.DephOfInheritance < settingsProject[4])
                .Count();
            mid = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[4] <= pm.DephOfInheritance && settingsProject[5] > pm.DephOfInheritance)
                .Count();
            bad = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[5] <= pm.DephOfInheritance)
                .Count();

            result.projectsResult.dephOfInheritance["bad"] = (bad / all) * 100;
            result.projectsResult.dephOfInheritance["mid"] = (mid / all) * 100;
            result.projectsResult.dephOfInheritance["good"] = (good / all) * 100;

            good = solutionMetrics.ProjectsMetrics
                .Where(pm => pm.LinesOfCode < settingsProject[6])
                .Count();
            mid = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[6] <= pm.LinesOfCode && settingsProject[7] > pm.LinesOfCode)
                .Count();
            bad = solutionMetrics.ProjectsMetrics
                .Where(pm => settingsProject[7] <= pm.LinesOfCode)
                .Count();

            result.projectsResult.linesOfCode["bad"] = (bad / all) * 100; ;
            result.projectsResult.linesOfCode["mid"] = (mid / all) * 100; ;
            result.projectsResult.linesOfCode["good"] = (good / all) * 100;

            return View(result);
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
