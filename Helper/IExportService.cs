namespace mnd.UI.Helper
{
    public interface IExportService
    {
        void ExportTo(ExportType fileType, string fileName);

        void ShowPreview();

        void SaveLayout(string layoutFileName);

        void RestoreLayout(string layoutFileName);
    }


}