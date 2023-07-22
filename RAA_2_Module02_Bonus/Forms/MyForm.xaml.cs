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

            foreach (string type in lbxTypes.SelectedItems)
            {
                TypeNames.Add(type);               
            }

            List<ElementType> selectedElemTypes = Utils.GetElementTypesByName(Doc,catName, TypeNames);
        }
    }
}
