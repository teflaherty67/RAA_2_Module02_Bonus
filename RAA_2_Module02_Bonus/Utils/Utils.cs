using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace RAA_2_Module02_Bonus
{
    internal static class Utils
    {
        internal static RibbonPanel CreateRibbonPanel(UIControlledApplication app, string tabName, string panelName)
        {
            RibbonPanel currentPanel = GetRibbonPanelByName(app, tabName, panelName);

            if (currentPanel == null)
                currentPanel = app.CreateRibbonPanel(tabName, panelName);

            return currentPanel;
        }       

        internal static RibbonPanel GetRibbonPanelByName(UIControlledApplication app, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in app.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }

            return null;
        }

        internal static List<Category> GetAllCategories(Document curDoc)
        {
            // create a list to hold the categories
            List<Category> m_Categories = new List<Category>();

            // loop through the categories
            foreach (Category curCat in curDoc.Settings.Categories)
            {
                // add the current category to the list
                m_Categories.Add(curCat);
            }

            // sort the list alphabetically
            List<Category> m_SortedList = m_Categories.OrderBy(x => x.Name).ToList();

            // return the sorted list
            return m_SortedList;
        }

        internal static Category GetCategoryByName(Document curDoc, string catName)
        {
            // get all the categories
            List<Category> m_categories = GetAllCategories(curDoc);

            // loop through the list to find a match
            foreach (Category curCat in m_categories)
            {
                if (curCat.Name == catName)
                    return curCat;
            }

            return null;
        }
    }
}
