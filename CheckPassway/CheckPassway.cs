using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;

namespace CheckPassway
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class CheckPassway : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(Autodesk.Revit.UI.ExternalCommandData commandData,
                      ref string message,
                      ElementSet elements)
        {
            IExternalEventHandler handler_check_dimension_event = new CheckDimensionExternalEvent();
            ExternalEvent check_dimension_exEvent = ExternalEvent.Create(handler_check_dimension_event);
            //commandData.Application.Idling += Application_Idling;
            RevitDocument m_connect = new RevitDocument(commandData.Application);
            PasswayWidth passway = new PasswayWidth(m_connect, check_dimension_exEvent);
            passway.Show();

            return Autodesk.Revit.UI.Result.Succeeded;
        }
        private void Application_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
