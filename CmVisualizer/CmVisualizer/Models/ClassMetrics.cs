using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    public class ClassMetrics : MetricsData
    {
        private List<FunctionMetrics> functionsMetrics = new List<FunctionMetrics>();

        public List<FunctionMetrics> FunctionsMetrics
        {
            get { return functionsMetrics; }
            set { functionsMetrics = value; }
        }
    }
}
