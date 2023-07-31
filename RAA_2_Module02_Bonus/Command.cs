#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace RAA_2_Module02_Bonus
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document curDoc = uidoc.Document;

            // put any code needed for the form here
            List<Category> categories = Utils.GetAllCategories(curDoc);

            // open form
            MyForm curForm = new MyForm(curDoc, categories)
            {
                Width = 450,
                Height = 550,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // get form data and do something

            if (curForm.DialogResult == false)
                return Result.Cancelled;

            List<Parameter> paramList = Utils.GetParametersByName(curDoc, curForm.catName, 
                curForm.TypeNames, curForm.ParamName);

            if (paramList.Count > 0)
            {
                using (Transaction t = new Transaction(curDoc))
                {
                    t.Start("Set type parameters");

                    foreach (Parameter param in paramList)
                    {
                        string newValue = curForm.GetNewValue();

                        if (curForm.ParamDataType == "double")
                        {
                            double paramDouble = Convert.ToDouble(newValue);
                            param.Set(paramDouble);
                        }
                        else if (curForm.ParamDataType == "integer")
                        {
                            int paramInt = Convert.ToInt32(newValue);
                            param.Set(paramInt);
                        }
                        else if (curForm.ParamDataType == "string")
                        {
                            param.Set(newValue);
                        }
                    }

                    t.Commit();
                }
            }

            TaskDialog.Show("Complete", "Updated " + paramList.Count.ToString() + " types.");

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
