
using DevExpress.Mvvm;
using mnd.Logic.BC_PandapForms;
using mnd.Logic.BC_PandapForms.DataModels;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Windows;

namespace mnd.UI.Modules.UretimIsletmeModule.KestiriciBakim
{
    public class FormYanitlarListVM : MyDxViewModelBase
    {
        public List<FormYatayData> FormYanitlari { get => formYanitlari; set =>SetProperty(ref formYanitlari, value); }
        public List<FormSoru> FormSorular { get;  set; }
        public FormGunluk FormGunluk { get; }

        PandapFormRepository repo = new PandapFormRepository();
        private List<FormYatayData> formYanitlari;

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");

        
        public DelegateCommand<object> ExcelExportCommand =>
      new DelegateCommand<object>(o => ExportService1.ExportTo(ExportType.XLSX, "DokumPlan.xlsx"), true);

        public DelegateCommand EkranYenileCommand => new DelegateCommand(onEkranYenile, true);

        public DelegateCommand ProblemliAcCommand => new DelegateCommand(onProblemliAc, true);

        private void onProblemliAc()
        {
            MessageBox.Show("Problem hakkında detay");
        }

        private void onEkranYenile()
        {
  
            FormYanitlari = repo.FormYanitlariGetir(FormGunluk.Id);

            FormSorular = repo.FormSorulariGetir(FormGunluk.FormAd);
        }

        public FormYanitlarListVM(FormGunluk formGunluk)
        {

            FormYanitlari = repo.FormYanitlariGetir(formGunluk.Id);
            FormSorular = repo.FormSorulariGetir(formGunluk.FormAd);

            this.FormGunluk = formGunluk;
        }
    }
}
