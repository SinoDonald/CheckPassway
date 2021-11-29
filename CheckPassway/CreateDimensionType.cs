using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Linq;

namespace CheckPassway
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    [Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)]
    public class CreateDimensionType : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(Autodesk.Revit.UI.ExternalCommandData commandData,
                      ref string message,
                      ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            FilteredElementCollector dimensiontypes = new FilteredElementCollector(doc).OfClass(typeof(DimensionType));
            DimensionType dim_type = null;
            foreach (DimensionType dimensiontype in dimensiontypes)
            {
                if (dimensiontype.Name == "Sino_dimension")
                {
                    dim_type = dimensiontype;
                }
            }
            if (dim_type == null)
            {
                DimensionType standard_dimension_type = null;
                DimensionType dimensiontype = null;
                standard_dimension_type = new FilteredElementCollector(doc).OfClass(typeof(DimensionType)).FirstOrDefault<Element>(e => e.Name.Equals("AEDIM")) as DimensionType;
                if (standard_dimension_type != null)
                {
                    dimensiontype = standard_dimension_type;
                }
                else
                {
                    FilteredElementCollector dimensiontype_collector = new FilteredElementCollector(doc).OfClass(typeof(DimensionType));
                    foreach (DimensionType di_type in dimensiontype_collector)
                    {
                        if (di_type.StyleType == DimensionStyleType.Linear && di_type.GetSimilarTypes().Count > 0)
                        {
                            dimensiontype = di_type;
                            break;
                        }
                    }
                }
                using (Transaction tran = new Transaction(doc, "Create DimensionType"))
                {
                    tran.Start();
                    dim_type = dimensiontype.Duplicate("Sino_dimension") as DimensionType;
                    tran.Commit();
                }
            }
            //ElementType elem_type = doc.GetElement(dim_type.Id) as ElementType;
            //ElementType test_type = new FilteredElementCollector(doc).OfClass(typeof(TextNoteType)).FirstElement() as ElementType;
            //ElementType elem_type = dim_type;
            //uidoc.PostRequestForElementTypePlacement(elem_type);
            //uidoc.PostRequestForElementTypePlacement(dim_type);
            using (Transaction tran = new Transaction(doc, "Set Dimension Default Type"))
            {
                tran.Start();
                doc.SetDefaultElementTypeId(ElementTypeGroup.LinearDimensionType, dim_type.Id);
                tran.Commit();
            }
            RevitCommandId revitCommandId = RevitCommandId.LookupPostableCommandId(PostableCommand.AlignedDimension);
            uidoc.Application.PostCommand(revitCommandId);
            //MessageBox.Show("Sino_dimesion建立完成");
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
