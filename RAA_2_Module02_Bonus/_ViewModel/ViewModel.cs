using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_2_Module02_Bonus._ViewModel
{
    public class ViewModel
    {
        public _Model.DocModel docModel { get; set; }

        public ViewModel(UIApplication uiapp)
        {
            docModel = new _Model.DocModel(uiapp.ActiveUIDocument.Document);
        }
    }
}
