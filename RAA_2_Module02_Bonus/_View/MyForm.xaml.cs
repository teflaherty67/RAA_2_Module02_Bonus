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
        public _ViewModel.ViewModel viewModel { get; set; }

        public MyForm(UIApplication uiapp)
        {
            InitializeComponent();

            viewModel = new _ViewModel.ViewModel(uiapp);
            this.DataContext = viewModel;
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UpdateTypes();
        }

        private void lbxTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Element selectedType in lbxTypes.SelectedItems)
                viewModel.SelectedElemTypes.Add(selectedType);
        }

        private void cmbParameter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}
