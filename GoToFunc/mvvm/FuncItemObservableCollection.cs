using GoToFunc.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.mvvm
{
    public class FuncItemObservableCollection : ObservableCollection<FuncItem>
    {
        public FuncItemObservableCollection() : base() { }

        public FuncItemObservableCollection(IEnumerable<FuncItem> collection) : base(collection) { }

        public void RaiseOnCollectionChanged()
        {
            OnCollectionChanged(
                new System.Collections.Specialized.NotifyCollectionChangedEventArgs(
                    System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }
    }
}
