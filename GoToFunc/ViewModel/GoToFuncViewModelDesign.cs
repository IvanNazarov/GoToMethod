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
    public class GoToFuncViewModelDesign : ViewModelBase, IGoToFuncViewModel
    {
        private string _searchString = "dosearch";
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

        public bool IsEnabled { get { return true; } }

        public Action CloseAction { get; set; }

        public FuncItemObservableCollection FuncList { get; set; }

        private RelayCommand _doSearchCommand;

        public RelayCommand DoSearchCommand
        {
            get
            {
                return _doSearchCommand ?? (_doSearchCommand = new RelayCommand(
                    () =>
                    {
                        System.Windows.MessageBox.Show("doSearch");
                    }));
            }
        }

        private RelayCommand _exitCommand;

        public RelayCommand ExitCommand
        {
            get {
                return _exitCommand ?? (_exitCommand = new RelayCommand(
                  () =>
                  {
                      CloseAction?.Invoke();
                  }));
                }
        }

        public GoToFuncViewModelDesign()
        {
            FuncList = new FuncItemObservableCollection()
            {
                new Model.FuncItem
                {
                    Name = "DoSearch",
                    Class = "DoClass",
                    Namespace = "DoNamespace",
                    Parameters = new List<Model.ParamItem>
                    {
                        new Model.ParamItem
                        {
                            Name = "SomeParameter",
                            FullType = "System.String"
                        }
                    },
                    Location = 10
                },

                new Model.FuncItem
                {
                    Name = "Render",
                    Class = "DoClass",
                    Namespace = "DoNamespace",
                    Parameters = new List<Model.ParamItem>
                    {
                        new Model.ParamItem
                        {
                            Name = "phoneNumber",
                            FullType = "System.String"
                        }
                    },
                    Location = 1120
                }
            };
            _selectedItem = FuncList[0];
        }
    }
}
