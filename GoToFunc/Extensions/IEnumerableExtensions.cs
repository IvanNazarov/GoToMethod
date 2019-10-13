using GoToFunc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool Contains(this IEnumerable<FuncItem> functions, string searchString)
        {
            searchString.ToLower();
            return functions.Any(f => f.Name.ToLower().Contains(searchString));
        }
    }
}
