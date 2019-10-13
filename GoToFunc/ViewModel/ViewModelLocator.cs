using EnvDTE;
using EnvDTE80;
using GoToFunc.DataAccess;
using Microsoft.VisualStudio.Shell;
using Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.ViewModel
{
    public class ViewModelLocator
    {
        private static DTE2 _dte;

        static ViewModelLocator()
        {
            _dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
        }

        public IGoToFuncViewModel SearchModel
        {
            get
            {
                if (ViewModelBase.IsInDesignModeStatic) {
                    return new GoToFuncViewModelDesign();
                }
                return new GoToFuncViewModel(new GoToFuncDataService(_dte));
            }
        }
    }
}
