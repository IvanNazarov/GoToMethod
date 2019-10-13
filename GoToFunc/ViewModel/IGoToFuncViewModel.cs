using GoToFunc.Model;
using GoToFunc.mvvm;
using Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.ViewModel
{
    public interface IGoToFuncViewModel
    {
        string SearchString { get; set; }
        FuncItem SelectedItem { get; set; }
        bool IsEnabled { get; }
        FuncItemObservableCollection FuncList { get; set; }
        RelayCommand DoSearchCommand { get; }
    }
}
