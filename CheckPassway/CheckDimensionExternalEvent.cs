using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;

namespace CheckPassway
{
    public class CheckDimensionExternalEvent : IExternalEventHandler
    {
        public void Execute(UIApplication uiapp)
        {
            UIDocument uidoc = uiapp.ActiveUIDocument;
            if (null == uidoc)
            {
                return; // no document, nothing to do
            }
            Document doc = uidoc.Document;
            Autodesk.Revit.DB.View view = doc.ActiveView;

            using (Transaction tran = new Transaction(doc, "modify the element"))
            {
                tran.Start();
                //doc.Delete(PlatformWidth.select_elementIDList);
                tran.Commit();
            }
        }
        public string GetName()
        {
            return "check event";
        }
    }
}
