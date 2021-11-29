using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Reflection;

namespace CheckPassway
{
    public class SinotechAPI : IExternalApplication
    {
        static string addinAssmeblyPath = Assembly.GetExecutingAssembly().Location;
        public Result OnStartup(UIControlledApplication a)
        {
            RibbonPanel ribbonPanel = null;
            try { a.CreateRibbonTab("捷運規範校核"); } catch { }
            try { ribbonPanel = a.CreateRibbonPanel("捷運規範校核", "空間需求校核"); }
            catch
            {
                List<RibbonPanel> panel_list = new List<RibbonPanel>();
                panel_list = a.GetRibbonPanels("捷運規範校核");
                foreach (RibbonPanel rp in panel_list)
                {
                    if (rp.Name == "空間需求校核")
                    {
                        ribbonPanel = rp;
                    }
                }
            }

            PushButton pushbutton1 = ribbonPanel.AddItem(
                new PushButtonData("Use Dimension", "使用通道尺寸標註",
                    addinAssmeblyPath, "CheckPassway.CreateDimensionType"))
                        as PushButton;
            pushbutton1.LargeImage = convertFromBitmap(Properties.Resources.CreateDimension);

            PushButton pushbutton2 = ribbonPanel.AddItem(
                new PushButtonData("Check Passway Width", "通道寬度檢核",
                    addinAssmeblyPath, "CheckPassway.CheckPassway"))
                        as PushButton;
            pushbutton2.LargeImage = convertFromBitmap(Properties.Resources.CheckPassway);
            //ribbonPanel.AddSeparator();

            return Result.Succeeded;
        }

        BitmapSource convertFromBitmap(System.Drawing.Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
