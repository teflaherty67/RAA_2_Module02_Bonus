﻿using System;
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
            catName = cmbCategory.SelectedItem.ToString();

            Category selectedCat = Utils.GetCategoryByName(Doc, catName);


        }
    }
}
