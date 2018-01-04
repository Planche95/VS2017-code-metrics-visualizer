using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmVisualizer.ViewModels
{
    public class ResultViewModel
    {
        public SingleResultViewModel solutionResult;/* = new SingleResultViewModel() { name = "Solution" };*/
        public SingleResultViewModel projectsResult; /*= new SingleResultViewModel() { name = "Projects" };*/
        public SingleResultViewModel namespacesResult; /*= new SingleResultViewModel() { name = "Namespaces" };*/
        public SingleResultViewModel classessResult; /*= new SingleResultViewModel() { name = "Classes" };*/
        public SingleResultViewModel functionsResult; /*= new SingleResultViewModel() { name = "Functions" };*/
    }
}
