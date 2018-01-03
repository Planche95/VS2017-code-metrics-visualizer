using CmVisualizer.Models;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.Converters
{
    public class SolutionMetricsConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(IFormFile))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            SolutionMetrics solutionMetrics = new SolutionMetrics();

            if (value is IFormFile)
            {
                IFormFile file = (IFormFile)value;

                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;

                        XSSFWorkbook workBook = new XSSFWorkbook(stream);
                        ISheet sheet = workBook.GetSheetAt(0);

                        int i = (sheet.FirstRowNum + 1);
                        IRow currentRow = sheet.GetRow(i);

                        while (i <= sheet.LastRowNum)
                        {
                            while (currentRow != null && currentRow.GetCell((int)Column.Scope).ToString().Equals("Project"))
                            {
                                ProjectMetrics currentProjectMetrics = new ProjectMetrics()
                                {
                                    Maintability = (int)currentRow.GetCell((int)Column.MaintainabilityIndex).NumericCellValue,
                                    Cyclomanic = (int)currentRow.GetCell((int)Column.CyclomaticComplexity).NumericCellValue,
                                    DephOfInheritance = (int)currentRow.GetCell((int)Column.DepthOfInheritance).NumericCellValue,
                                    LinesOfCode = (int)currentRow.GetCell((int)Column.LinesOfCode).NumericCellValue,
                                    Name = currentRow.GetCell((int)Column.Project).ToString()
                                };
                                currentRow = sheet.GetRow(++i);

                                while (currentRow != null && currentRow.GetCell((int)Column.Scope).ToString().Equals("Namespace"))
                                {
                                    NamespaceMetrics currentNamespaceMetrics = new NamespaceMetrics()
                                    {
                                        Maintability = (int)currentRow.GetCell((int)Column.MaintainabilityIndex).NumericCellValue,
                                        Cyclomanic = (int)currentRow.GetCell((int)Column.CyclomaticComplexity).NumericCellValue,
                                        DephOfInheritance = (int)currentRow.GetCell((int)Column.DepthOfInheritance).NumericCellValue,
                                        LinesOfCode = (int)currentRow.GetCell((int)Column.LinesOfCode).NumericCellValue,
                                        Name = currentRow.GetCell((int)Column.NameSpace).ToString()
                                    };
                                    currentRow = sheet.GetRow(++i);

                                    while (currentRow != null && currentRow.GetCell((int)Column.Scope).ToString().Equals("Type"))
                                    {
                                        ClassMetrics currentClassMetrics = new ClassMetrics()
                                        {
                                            Maintability = (int)currentRow.GetCell((int)Column.MaintainabilityIndex).NumericCellValue,
                                            Cyclomanic = (int)currentRow.GetCell((int)Column.CyclomaticComplexity).NumericCellValue,
                                            DephOfInheritance = (int)currentRow.GetCell((int)Column.DepthOfInheritance).NumericCellValue,
                                            LinesOfCode = (int)currentRow.GetCell((int)Column.LinesOfCode).NumericCellValue,
                                            Name = currentRow.GetCell((int)Column.Type).ToString()
                                        };
                                        currentRow = sheet.GetRow(++i);

                                        while (currentRow != null && currentRow.GetCell((int)Column.Scope).ToString().Equals("Member"))
                                        {
                                            FunctionMetrics currentFunctionMetrics = new FunctionMetrics()
                                            {
                                                Maintability = (int)currentRow.GetCell((int)Column.MaintainabilityIndex).NumericCellValue,
                                                Cyclomanic = (int)currentRow.GetCell((int)Column.CyclomaticComplexity).NumericCellValue,
                                                LinesOfCode = (int)currentRow.GetCell((int)Column.LinesOfCode).NumericCellValue,
                                                Name = currentRow.GetCell((int)Column.Member).ToString()
                                            };
                                            currentRow = sheet.GetRow(++i);

                                            currentClassMetrics.FunctionsMetrics.Add(currentFunctionMetrics);
                                        }
                                        currentNamespaceMetrics.ClassesMetrics.Add(currentClassMetrics);
                                    }
                                    currentProjectMetrics.NamespacesMetrics.Add(currentNamespaceMetrics);
                                }
                                solutionMetrics.ProjectsMetrics.Add(currentProjectMetrics);
                            }
                        }
                    }
                }
            }

            return solutionMetrics;
        }
    }
}
