﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace mnd.UI.Helper
{
    public static class PrintHelper
    {
        public static FixedDocument GetFixedDocument(FrameworkElement toPrint, PrintDialog printDialog)
        {
            var capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            var pageSize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);
            var visibleSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            var fixedDoc = new FixedDocument();

            toPrint.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            toPrint.Arrange(new Rect(new Point(0, 0), toPrint.DesiredSize));
            //
            var size = toPrint.DesiredSize;

            double yOffset = 0;
            while (yOffset < size.Height)
            {
                var vb = new VisualBrush(toPrint)
                {
                    Stretch = Stretch.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Top,
                    ViewboxUnits = BrushMappingMode.Absolute,
                    TileMode = TileMode.None,
                    Viewbox = new Rect(0, yOffset, visibleSize.Width, visibleSize.Height)
                };
                var pageContent = new PageContent();
                var page = new FixedPage();
                ((IAddChild)pageContent).AddChild(page);
                fixedDoc.Pages.Add(pageContent);
                page.Width = pageSize.Width;
                page.Height = pageSize.Height;
                var canvas = new Canvas();
                FixedPage.SetLeft(canvas, capabilities.PageImageableArea.OriginWidth);
                FixedPage.SetTop(canvas, capabilities.PageImageableArea.OriginHeight);
                canvas.Width = visibleSize.Width;
                canvas.Height = visibleSize.Height;
                canvas.Background = vb;
                page.Children.Add(canvas);
                yOffset += visibleSize.Height;
            }
            return fixedDoc;
        }

        public static void ShowPrintPreview(FixedDocument fixedDoc)
        {
            var wnd = new Window();
            var viewer = new DocumentViewer();
            viewer.Document = fixedDoc;
            wnd.Content = viewer;
            wnd.ShowDialog();
        }

        public static void PrintNoPreview(PrintDialog printDialog, FixedDocument fixedDoc)
        {
            printDialog.PrintDocument(fixedDoc.DocumentPaginator, "Test Print No Preview");
        }
    }
}