using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    public abstract class MetricsData
    {
        protected int Maintability;
        protected int Cyclomanic;
        protected int DephOfInheritance;
        protected int LinesOfCode;
    }
}
