using CmVisualizer.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    [TypeConverter(typeof(ProjectMetricsConverter))]
    public class ProjectMetrics : MetricsData
    {
        private List<NamespaceMetrics> FunctionsMetrics = new List<NamespaceMetrics>();

        public string test;
    }
}
