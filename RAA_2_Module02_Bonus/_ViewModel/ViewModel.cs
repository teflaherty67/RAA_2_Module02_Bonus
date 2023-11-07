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
        public Parameter SelectedParam { get; set; }
        public string LabelContent { get; set; }
        public string ParamDataType { get; set; }
        public string NewValue { get; set; }

        public ViewModel(UIApplication uiapp)
        {
            docModel = new _Model.DocModel(uiapp.ActiveUIDocument.Document);

            CatList = new ObservableCollection<Category>(docModel.GetAllCategories());
            ElemTypeList = new ObservableCollection<Element>();
            ParamList = new ObservableCollection<Parameter>();
            SelectedElemTypes = new List<Element>();
        }

        public void UpdateTypes()
        {
            // check if the user selected a category
            if (SelectedCategory != null)
            {
                // clear out the element type list
                ElemTypeList.Clear();

                // loop through the elements in the selected category
                foreach (Element curElem in docModel.GetAllElementTypesByCategory(SelectedCategory))
                {
                    // add the current elemement to the type list
                    ElemTypeList.Add(curElem);
                }
            }
        }

        public void UpdateParameters()
        {
            if (SelectedElemTypes != null)
            {
                ParamList.Clear();
                foreach (Parameter curParam in docModel.GetAllParmatersFromElementTypes(SelectedElemTypes))
                {
                    ParamList.Add(curParam);
                }
            }
        }

        internal void UpdateParamValueString()
        {
            if (SelectedParam.StorageType == StorageType.String)
            {
                LabelContent = "Set Parameter Value (as string):";
                ParamDataType = "string";
            }
            else if (SelectedParam.StorageType == StorageType.Integer)
            {
                LabelContent = "Set Parameter Value (as integer):";
                ParamDataType = "integer";
            }
            else if (SelectedParam.StorageType == StorageType.Double)
            {
                LabelContent = "Set Parameter Value (as double):";
                ParamDataType = "double";
            }
            else
            {
                ParamDataType = "none";
                LabelContent = "Set Parameter Value:";
            }
        }
    }
}
