using CmVisualizer.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    public class ProjectMetrics : MetricsData
    {
        private List<NamespaceMetrics> namespacesMetrics = new List<NamespaceMetrics>();

        public List<NamespaceMetrics> NamespacesMetrics
        {
            get { return namespacesMetrics; }
            set { namespacesMetrics = value; }
        }
    }
}
