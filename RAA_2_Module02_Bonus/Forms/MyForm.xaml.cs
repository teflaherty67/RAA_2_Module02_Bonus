using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace RAA_2_Module02_Bonus
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MyForm : Window
    {
        public string catName;
        public Document Doc;
        public List<string> TypeNames;
        public string ParamName;
        public string ParamDataType;

        public MyForm(Document curDoc, List<Category> catList)
        {
            InitializeComponent();

            Doc = curDoc;

            foreach (Category curCat in catList)
            { 
                cmbCategory.Items.Add(curCat.Name);
            }
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // clear the listbox
            lbxTypes.Items.Clear();

            catName = cmbCategory.SelectedItem.ToString();

            Category selectedCat = Utils.GetCategoryByName(Doc, catName);

            if (selectedCat != null)
            {
                FilteredElementCollector m_col = new FilteredElementCollector(Doc)
                    .OfCategoryId(selectedCat.Id)
                    .WhereElementIsElementType();

                List<Element> m_groupedList = m_col.GroupBy(x => x.Name).Select(x => x.First()).ToList();
                List<Element> m_sortedList = m_groupedList.OrderBy(x => x.Name).ToList();

                foreach(Element curElem in m_sortedList)
                {
                    lbxTypes.Items.Add(curElem.Name);
                }
            }
        }

        private void lbxTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeNames = new List<string>();

            cmbParameter.Items.Clear();

            foreach (string type in lbxTypes.SelectedItems)
            {
                TypeNames.Add(type);               
            }

            List<ElementType> selectedElemTypes = Utils.GetElementTypesByName(Doc,catName, TypeNames);

            if (selectedElemTypes.Count > 0)
            {
                // create a list to hold the parameters from the types
                List<Parameter> paramList = new List<Parameter>();

                // llop through the tpyes and get all parameters
                foreach (ElementType curType in selectedElemTypes)
                {
                    paramList.AddRange(Utils.GetAllParametersFromElement(curType));
                }

                List<Parameter> groupedList = paramList.GroupBy(x => x.Definition.Name).Select(x => x.First()).ToList();
                List<Parameter> sortedList = groupedList.OrderBy(x => x.Definition.Name).ToList();

                foreach (Parameter curParam in sortedList)
                {
                    cmbParameter.Items.Add(curParam.Definition.Name);
                }
            }
        }

        private void cmbParameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbxValue.Text = "";
            tbxValue.IsEnabled = true;

            ParamName = cmbParameter.SelectedItem.ToString();

            Parameter curParam = Utils.GetParameterByName(Doc, catName, TypeNames.FirstOrDefault(), ParamName);

            if (curParam.StorageType == StorageType.String)
            {
                lblValue.Content = "Set parameter value (as string):";
                ParamDataType = "string";
            }
            else if (curParam.StorageType == StorageType.Integer)
            {
                lblValue.Content = "Set parameter value (as integer):";
                ParamDataType = "integer";
            }
            else if (curParam.StorageType == StorageType.Double)
            {
                lblValue.Content = "Set parameter value (as double):";
                ParamDataType = "double";
            }
            else
            {
                ParamDataType = "none";
                lblValue.Content = "Set parameter value:";
                tbxValue.Text = "Cannot set this parameter.";
                tbxValue.IsEnabled = false;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        public string GetNewValue()
        {
            return tbxValue.Text;
        }
    }
}
