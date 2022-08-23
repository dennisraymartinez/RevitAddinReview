#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace RevitAddinReview
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSession01Challenge : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            int range = 100;
            XYZ insPoint = new XYZ(0, 0, 0);
            double offset = 0.041;
            double calcOffset = offset * doc.ActiveView.Scale;
            XYZ offsetPoint = new XYZ(0, calcOffset, 0);

            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(TextNoteType));

            using (Transaction t = new Transaction(doc))
            {
                t.Start("FizzBuzz");

                for (int i = 1; i <= range; i++)
                {
                    string result = "";

                    if (i % 3 == 0)
                    {
                        result = "Fizz";
                    }
                    if (i % 5 == 0)
                    {
                        result = "Buzz";
                    }
                    if (i % 3 == 0 && i % 5 == 0)
                    {
                        result = "FizzBuzz";
                    }
                    if (i % 3 != 0 && i % 5 != 00)
                    {
                        result = i.ToString();
                    }

                    Debug.Print(result);

                    CreateTextNote(doc, result, insPoint, collector.FirstElementId());
                    insPoint = insPoint.Subtract(offsetPoint);
                }

                t.Commit();

            }

            return Result.Succeeded;
        }
        
    }
}

