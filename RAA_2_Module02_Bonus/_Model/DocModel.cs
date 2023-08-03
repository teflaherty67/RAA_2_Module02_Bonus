using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAA_2_Module02_Bonus._Model
{
    public class DocModel
    {
        public Document Doc { get; set; }
        public DocModel(Document curDoc)
        {
            Doc = curDoc;
        }

        internal List<Category> GetAllCategories()
        {
            // create a list to hold the categories
            List<Category> m_Categories = new List<Category>();

            // loop through the categories
            foreach (Category curCat in Doc.Settings.Categories)
            {
                // add the current category to the list
                m_Categories.Add(curCat);
            }

            // sort the list alphabetically
            List<Category> m_SortedList = m_Categories.OrderBy(x => x.Name).ToList();

            // return the sorted list
            return m_SortedList;
        }

        internal Category GetCategoryByName(Document curDoc, string catName)
        {
            // get all the categories
            List<Category> m_categories = GetAllCategories();

            // loop through the list to find a match
            foreach (Category curCat in m_categories)
            {
                if (curCat.Name == catName)
                    return curCat;
            }

            return null;
        }

        internal static List<ElementType> GetElementTypesByName(Document doc, string catName, List<string> typeNames)
        {
            List<ElementType> m_types = new List<ElementType>();

            foreach (string type in typeNames)
            {
                ElementType curType = GetElementTypeByName(doc, catName, type);

                if (curType != null)
                    m_types.Add(curType);
            }

            return m_types;
        }

        internal List<Element> GetAllElementTypesByCategory(Category category)
        {
            FilteredElementCollector collector = new FilteredElementCollector(Doc);
            collector.OfCategoryId(category.Id);
            collector.WhereElementIsElementType();

            List<Element> groupedList = collector.GroupBy(x => x.Name).Select(x => x.First()).ToList();
            List<Element> sortedList = groupedList.OrderBy(x => x.Name).ToList();

            return sortedList;
        }

        private static ElementType GetElementTypeByName(Document doc, string catName, string name)
        {
            FilteredElementCollector m_col = new FilteredElementCollector(doc)
                .OfClass(typeof(ElementType));

            foreach (ElementType curType in m_col)
            {
                if (curType.Name == name && curType.Category.Name == catName)
                    return curType;
            }

            return null;
        }

        internal List<Parameter> GetAllParametersFromElement(ElementType curType)
        {
            List<Parameter> m_returnList = new List<Parameter>();

            // loop through the parameters of curType
            foreach (Parameter curParam in curType.Parameters)
            {
                m_returnList.Add(curParam);
            }

            return m_returnList;
        }

        internal static Parameter GetParameterByName(Document doc, string catName, string typeName, string paramName)
        {
            ElementType curType = GetElementTypeByName(doc, catName, typeName);

            Parameter curParam = curType.GetParameters(paramName).FirstOrDefault();

            if (curParam != null)
                return curParam;

            return null;
        }

        internal static List<Parameter> GetParametersByName(Document curDoc, string catName, List<string> typeNames, string paramName)
        {
            List<Parameter> m_returnList = new List<Parameter>();

            foreach (string typeName in typeNames)
            {
                Parameter curParam = GetParameterByName(curDoc, catName, typeName, paramName);

                if (curParam != null)
                    m_returnList.Add(curParam);
            }

            return m_returnList;
        }

        internal List<Parameter> GetAllParametersFromElementTypes(List<Element> selectedElemTypes)
        {
            List<Parameter> paramList = new List<Parameter>();

            foreach (Element curType in selectedElemTypes)
            {
                paramList.AddRange(GetAllParametersFromElement(curType));
            }

            List<Parameter> groupedList = paramList.GroupBy(x => x.Definition.Name).Select(x => x.First()).ToList();
            List<Parameter> sortedList = groupedList.OrderBy(x => x.Definition.Name).ToList();

            return sortedList;
        }
    }
}
