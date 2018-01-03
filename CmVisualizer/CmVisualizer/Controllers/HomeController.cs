using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmVisualizer.Models;
using System.ComponentModel;
using CmVisualizer.ViewModels;

namespace CmVisualizer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Daj to do konstruktora
            ProjectMetrics pm = (ProjectMetrics)TypeDescriptor.GetConverter(typeof(ProjectMetrics)).ConvertFrom("30 solid");

            return View();
        }

        [HttpPost]
        public IActionResult CalculateResult(SourceInfoViewModel model)
        {
            return RedirectToAction("About");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
