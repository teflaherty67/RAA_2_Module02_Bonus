using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_2_Module02_Bonus._ViewModel
{
    public class ViewModel
    {
        public _Model.DocModel docModel { get; set; }
        public ObservableCollection<Category> CatList { get; set; }
        public ObservableCollection<Element> ElemTypeList { get; set; }
        public ObservableCollection<Parameter> ParamList { get; set; }
        public Category SelectedCategory { get; set; }
        public List<Element> SelectedElemTypes { get; set; }

        public ViewModel(UIApplication uiapp)
        {
            docModel = new _Model.DocModel(uiapp.ActiveUIDocument.Document);

            CatList = new ObservableCollection<Category>(docModel.GetAllCategories());
            ElemTypeList = new ObservableCollection<Element>();
        }

        public void UpdateTypes()
        {
            if (SelectedCategory != null)
            {
                ElemTypeList.Clear();

                foreach (Element curElem in docModel.GetAllElementTypesByCategory(SelectedCategory))
                {
                    ElemTypeList.Add(curElem);
                }
            }
        }
    }
}
