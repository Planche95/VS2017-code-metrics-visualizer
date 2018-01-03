using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    public class NamespaceMetrics : MetricsData
    {
        private List<ClassMetrics> classesMetrics = new List<ClassMetrics>();

        public List<ClassMetrics> ClassesMetrics
        {
            get { return classesMetrics; }
            set { classesMetrics = value; }
        }
    }
}
