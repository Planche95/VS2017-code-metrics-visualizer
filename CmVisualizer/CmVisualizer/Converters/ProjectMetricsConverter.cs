using CmVisualizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Converters
{
    public class ProjectMetricsConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                return new ProjectMetrics();
            }

            if (value is string)
            {
                return new ProjectMetrics() { test = "test" };
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
