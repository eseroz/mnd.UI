using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.PivotGrid;
using DevExpress.XtraPrinting;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace mnd.UI.Helper
{
    public class ExportService : DevExpress.Mvvm.UI.ServiceBase, IExportService
    {
        public TableView View
        {
            get { return (TableView)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        public static readonly DependencyProperty ViewProperty =
            DependencyProperty.Register("View", typeof(TableView), typeof(ExportService), new PropertyMetadata(null));

        public void ExportTo(ExportType fileType, string fileName)
        {
            if (View == null) return;
            switch (fileType)
            {
                case ExportType.XLSX:
                    ExcelExport(fileName);
                    break;

                case ExportType.WYSIWYG:
                    ExcelExport(fileName, true);
                    break;

                case ExportType.PDF:
                    View.ExportToPdf(fileName);
                    break;


            }
        }

        public void ExcelExport(string fileName, bool childEkliMi = false)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "excel dosyası (.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                if (childEkliMi)
                    View.ExportToXlsx(saveFileDialog.FileName, new XlsxExportOptionsEx { ExportType = DevExpress.Export.ExportType.WYSIWYG });
                else
                    View.ExportToXlsx(saveFileDialog.FileName);
            }
        }

        public void SaveLayout(string layoutFileName)
        {
            if (layoutFileName == null) return;

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + layoutFileName;
            View.Grid.SaveLayoutToXml(path);
        }

        public void RestoreLayout(string layoutFileName)
        {
            if (layoutFileName == null) return;

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + layoutFileName;
            if (File.Exists(path))
            {
                View.Grid.RestoreLayoutFromXml(path);
            }
        }

        public void ShowPreview()
        {
            if (View == null) return;

            var rootWindow = LayoutTreeHelper.GetVisualParents(AssociatedObject).FirstOrDefault(x => x is Window) as Window;
            if (rootWindow != null)
                View.ShowPrintPreviewDialog(rootWindow);
        }
    }


    public class ExportPivotService : DevExpress.Mvvm.UI.ServiceBase, IExportService
    {
        public PivotGridControl PivotNesne
        {
            get { return (PivotGridControl)GetValue(PivotNesneProperty); }
            set { SetValue(PivotNesneProperty, value); }
        }

        public static readonly DependencyProperty PivotNesneProperty =
            DependencyProperty.Register("PivotNesne", typeof(PivotGridControl), typeof(ExportService), new PropertyMetadata(null));


        public void ExportTo(ExportType fileType, string fileName)
        {
            if (PivotNesne == null) return;
            switch (fileType)
            {
                case ExportType.XLSX:
                    ExcelExport(fileName);
                    break;

                case ExportType.PDF:
                    PivotNesne.ExportToPdf(fileName);
                    break;
            }
        }

        public void ExcelExport(string fileName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = fileName;
            saveFileDialog.Filter = "excel dosyası (.xlsx)|*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                PivotNesne.ExportToXlsx(saveFileDialog.FileName, new XlsxExportOptionsEx
                {
                    ExportType = DevExpress.Export.ExportType.WYSIWYG,
                    ShowGridLines = true
                });
            }
        }

        public void SaveLayout(string layoutFileName)
        {
            if (layoutFileName == null) return;

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + layoutFileName;
            PivotNesne.SaveLayoutToXml(path);
        }

        public void RestoreLayout(string layoutFileName)
        {
            if (layoutFileName == null) return;

            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + layoutFileName;
            if (File.Exists(path))
            {
                PivotNesne.RestoreLayoutFromXml(path);
            }
        }

        public void ShowPreview()
        {
            if (PivotNesne == null) return;

            var rootWindow = LayoutTreeHelper.GetVisualParents(AssociatedObject).FirstOrDefault(x => x is Window) as Window;
            if (rootWindow != null)
                PivotNesne.ShowPrintPreviewDialog(rootWindow);
        }
    }


}