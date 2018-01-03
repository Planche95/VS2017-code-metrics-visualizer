using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    public class SolutionMetricsBuilder
    {
        private object _source;

        public SolutionMetricsBuilder From(object source)
        {
            _source = source;
            return this;
        }

        public SolutionMetrics Build()
        {
            return (SolutionMetrics)TypeDescriptor.GetConverter(typeof(SolutionMetrics)).ConvertFrom(_source);
        }
    }
}
