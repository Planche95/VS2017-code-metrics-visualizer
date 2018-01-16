using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Models
{
    public abstract class MetricsData
    {
        protected int maintability;
        protected int cyclomanic;
        protected int dephOfInheritance;
        protected int classCoupling;
        protected int linesOfCode;
        protected string name;

        public int Maintability
        {
            get { return maintability; }
            set { maintability = value; }
        }

        public int Cyclomanic
        {
            get { return cyclomanic; }
            set { cyclomanic = value; }
        }

        public int DephOfInheritance
        {
            get { return dephOfInheritance; }
            set { dephOfInheritance = value; }
        }

        public int ClassCoupling
        {
            get { return classCoupling; }
            set { classCoupling = value; }
        }

        public int LinesOfCode
        {
            get { return linesOfCode; }
            set { linesOfCode = value; }
        }

        public string Name
        {
            get{ return name; }
            set{ name = value; }
        }
    }
}
