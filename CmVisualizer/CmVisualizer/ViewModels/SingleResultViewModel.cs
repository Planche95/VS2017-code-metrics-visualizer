using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.ViewModels
{
    public class SingleResultViewModel
    {
        public string name;

        public Dictionary<string, double> overall = new Dictionary<string, double>()
        {
            { "bad", 0.0 },
            { "mid", 0.0 },
            { "good", 0.0 }
        };

        public Dictionary<string, double> maintainability = new Dictionary<string, double>()
        {
            { "bad", 5.0 },
            { "mid", 5.0 },
            { "good", 5.0 }
        };

        public Dictionary<string, double> cyclomaticComplexity = new Dictionary<string, double>()
        {
            { "bad", 10.0 },
            { "mid", 10.0 },
            { "good", 10.0 }
        };
        public Dictionary<string, double> dephOfInheritance = new Dictionary<string, double>()
        {
            { "bad", 15.0 },
            { "mid", 15.0 },
            { "good", 15.0 }
        };
        public Dictionary<string, double> linesOfCode = new Dictionary<string, double>()
        {
            { "bad", 20.0 },
            { "mid", 20.0 },
            { "good", 20.0 }
        };
    }
}
