using CmVisualizer.Converters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    [TypeConverter(typeof(SolutionMetricsConverter))]
    public class SolutionMetrics
    {
        private List<ProjectMetrics> projectsMetrics = new List<ProjectMetrics>();

        public List<ProjectMetrics> ProjectsMetrics
        {
            get { return projectsMetrics; }
            set { projectsMetrics = value; }
        }
    }
}
