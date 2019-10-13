using EnvDTE;
using EnvDTE80;
using GoToFunc.Extensions;
using GoToFunc.Model;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToFunc.DataAccess
{
    public class GoToFuncDataService
    {
        private DTE2 _dte;

        public GoToFuncDataService(DTE2 dte)
        {
            _dte = dte;
        }

        public bool CanFind()
        {
            return _dte.ActiveDocument.Language == "CSharp";
        }

        public void GoToLine(int line)
        {
            if (line <= 0)
                return;
            var selection = _dte.ActiveDocument.Selection as TextSelection;
            selection.GotoLine(line);
        }

        public IEnumerable<FuncItem> GetFunctions(string searchString)
        {
            var items = new List<FuncItem>();
            var fcm = _dte.ActiveDocument.ProjectItem.FileCodeModel as FileCodeModel;
            #region CodeElements
            foreach (var ns in fcm.CodeElements)
            {
                if (ns is CodeNamespace)
                {
                    var @namespace = ns as CodeNamespace;
                    foreach (var cl in @namespace.Children)
                    {
                        if (cl is CodeClass)
                        {
                            var @class = cl as CodeClass;
                            foreach (var f in @class.Children)
                            {
                                if (f is CodeFunction)
                                {
                                    var @function = f as CodeFunction;
                                    if (!string.IsNullOrEmpty(searchString) && !@function.Name.ToLower().Contains(searchString.ToLower()))
                                        continue;
                                    var fi = new FuncItem
                                    {
                                        Name = @function.Name,
                                        Namespace = @namespace.Name,
                                        Class = @class.Name,
                                        Location = @function.StartPoint.Line
                                    };
                                    var parms = new List<ParamItem>();
                                    foreach (CodeParameter param in @function.Parameters)
                                    {
                                        parms.Add(new ParamItem
                                        {
                                            Name = param.Name,
                                            FullType = param.Type.AsFullName
                                        });
                                    }
                                    fi.Parameters = parms;
                                    items.Add(fi);
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            return items;
        }
    }
}
