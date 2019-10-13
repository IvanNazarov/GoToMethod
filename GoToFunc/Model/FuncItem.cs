using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.Model
{
    public class ParamItem
    {
        public string Name { get; set; }
        public string Type
        {
            get
            {
                var type = FullType.Split('.');
                if(type.Count() > 0)
                    return type[type.Count() - 1];
                return string.Empty;
            }
        }
        public string FullType { get; set; }
    }

    public class FuncItem
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Class { get; set; }
        public List<ParamItem> Parameters { get; set; }
        public int Location { get; set; }

        public string View
        {
            get
            {
                var result = new StringBuilder(Name);
                result.Append("(");
                var count = Parameters.Count();
                for (int i = 0; i < count; i++)
                {
                    var param = string.Format("{0} {1}{2}", Parameters[i].Type, Parameters[i].Name, i < count - 1 ? ", " : "");
                    result.Append(param);
                }
                result.Append(")");
                return result.ToString();
            }
        }
    }
}
