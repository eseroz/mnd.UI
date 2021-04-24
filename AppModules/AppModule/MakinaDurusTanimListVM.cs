using DevExpress.Mvvm;
using mnd.Logic.BC_Uretim;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules;
using System;
using System.Collections.ObjectModel;

namespace mnd.UI.AppModules.AppModule
{
    public class MakinaDurusListVM : MyDxViewModelBase, IForm
    {

        MakinaDurusRepository repo = new MakinaDurusRepository();

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<MakinaDurusTanim> KayitGuncellendiCommand => new DelegateCommand<MakinaDurusTanim>(OnKayitGuncellendi, c => true);

        public DelegateCommand<MakinaDurusTanim> YeniKayitOlusturuluyorCommand => new DelegateCommand<MakinaDurusTanim>(OnYeniKayitOlusturuluyor, c => true);


        public DelegateCommand<MakinaDurusTanim> OnaylaCommand => new DelegateCommand<MakinaDurusTanim>(OnOnayla, c => true);

        public DelegateCommand<MakinaDurusTanim> SilCommand => new DelegateCommand<MakinaDurusTanim>(OnSil, c => true);
        private void OnOnayla(MakinaDurusTanim obj)
        {

            repo.Kaydet();
        }

       

        private void OnSil(MakinaDurusTanim obj)
        {
        
        }

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnKaydet(object obj)
        {
            repo.Kaydet();
        }


        private void OnYeniKayitOlusturuluyor(MakinaDurusTanim obj)
        {
          
        }

        public void OnKayitGuncellendi(MakinaDurusTanim item)
        {
            if(item.Id==0)
            {
                repo.MakinaDurusTanimEkle(item);
            }
            else
            {
                item.Guncelleyen = AppPandap.AktifKullanici.AdSoyad;
                item.GuncellenmeTarihi = DateTime.Now;
            }


            repo.Kaydet();
        }

        public MakinaDurusListVM(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            MakinaDurusListe = repo.MakinaDurusTanimlariGetir();

        }

        private ObservableCollection<MakinaDurusTanim> ortakDilListe;

        public ObservableCollection<MakinaDurusTanim> MakinaDurusListe
        {
            get => ortakDilListe;
            set => SetProperty(ref ortakDilListe, value);
        }

        public MakinaDurusTanim SeciliOrtakDilTanim { get; set; }
    }
}