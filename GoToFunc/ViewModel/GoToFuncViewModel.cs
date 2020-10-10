using GoToFunc.DataAccess;
using GoToFunc.Model;
using GoToFunc.mvvm;
using Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.ViewModel
{
    public class GoToFuncViewModel : ViewModelBase, IGoToFuncViewModel
    {
        private GoToFuncDataService _ds;

        public bool IsEnabled { get { return _ds.CanFind(); } }

        private string _searchString;
        private const string SEARCH_STRING_PROPERTY_NAME = "SearchString";

        public string SearchString
        {
            get { return _searchString; }
            set { Set(SEARCH_STRING_PROPERTY_NAME, ref _searchString, value); }
        }

        private FuncItem _selectedItem;
        private const string SELECTED_ITEM_PROPERTY_NAME = "SelectedItem";

        public FuncItem SelectedItem
        {
            get { return _selectedItem; }
            set { Set(SELECTED_ITEM_PROPERTY_NAME, ref _selectedItem, value); }
        }

        public FuncItemObservableCollection FuncList { get; set; }

        private RelayCommand<object> _goToFuncCommand;

        public RelayCommand<object> GoToFuncCommand
        {
            get
            {
                return _goToFuncCommand ?? (_goToFuncCommand = new RelayCommand<object>((object p) =>
                {
                    if (_selectedItem != null)
                    {
                        _ds.GoToLine(_selectedItem.Location);
                        CloseCommand.Execute(p);
                    }
                }));
            }
        }


        private RelayCommand<object> _closeCommand;

        public RelayCommand<object> CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand<object>((object p) =>
                {
                    ((BaseDialogWindow)p).Close();
                }));
            }
        }


        private RelayCommand _doSearchCommand;

        public RelayCommand DoSearchCommand
        {
            get
            {
                return _doSearchCommand ?? (_doSearchCommand = new RelayCommand(
                    () =>
                    {
                        var fl = _ds.GetFunctions(SearchString);
                        FuncList.Clear();
                        foreach (var f in fl)
                        {
                            FuncList.Add(f);
                        }
                        if (FuncList.Count > 0)
                            SelectedItem = FuncList[0];
                    }, () =>
                    {
                        return _ds.CanFind();
                    }));
            }
        }

        public GoToFuncViewModel(GoToFuncDataService ds)
        {
            _ds = ds;
            FuncList = new FuncItemObservableCollection();
        }
    }
}
