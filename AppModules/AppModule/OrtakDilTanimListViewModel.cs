using DevExpress.Mvvm;
using mnd.Logic.Model.App;
using mnd.Logic.Persistence;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules;
using System;
using System.Collections.ObjectModel;

namespace mnd.UI.AppModules.AppModule
{
    public class OrtakDilTanimListViewModel : MyDxViewModelBase, IForm
    {
        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);

        public DelegateCommand<OrtakDilTanim> KayitGuncellendiCommand => new DelegateCommand<OrtakDilTanim>(OnKayitGuncellendi, c => true);

        public DelegateCommand<OrtakDilTanim> YeniKayitOlusturuluyorCommand => new DelegateCommand<OrtakDilTanim>(OnYeniKayitOlusturuluyor, c => true);


        public DelegateCommand<OrtakDilTanim> OnaylaCommand => new DelegateCommand<OrtakDilTanim>(OnOnayla, c => true);

        public DelegateCommand<OrtakDilTanim> SilCommand => new DelegateCommand<OrtakDilTanim>(OnSil, c => true);
        private void OnOnayla(OrtakDilTanim obj)
        {
            obj.Onaylayan = AppPandap.AktifKullanici.AdSoyad;
            obj.OnaylanmaTarihi = DateTime.Now;
            uow.Commit();
        }

       

        private void OnSil(OrtakDilTanim obj)
        {
            OrtakDilListe.Remove(obj);
            uow.OrtakDilRepo.Sil(obj);
            uow.Commit();
        }

        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, c => true);

        private void OnExcelExport(object obj)
        {
            ExportService.ExportTo(ExportType.XLSX, "export.xls");
        }

        private void OnKaydet(object obj)
        {
            uow.Commit();
        }


        private void OnYeniKayitOlusturuluyor(OrtakDilTanim obj)
        {
            obj.Ekleyen = AppPandap.AktifKullanici.AdSoyad;
            obj.EklenmeTarihi = DateTime.Now;
            obj.RowGuid = Guid.NewGuid();
        }

        public void OnKayitGuncellendi(OrtakDilTanim item)
        {
            if(item.Id==0)
            {
                uow.OrtakDilRepo.EkleAsync(item);
            }
            else
            {
                item.Guncelleyen = AppPandap.AktifKullanici.AdSoyad;
                item.GuncellenmeTarihi = DateTime.Now;
            }

       
            uow.Commit();
        }

        public OrtakDilTanimListViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;
        }

        public void Load()
        {
            OrtakDilListe = uow.OrtakDilRepo.OrtakDilTanimListeGetir();

            OrtakDilListe.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);
        }

        private ObservableCollection<OrtakDilTanim> ortakDilListe;

        public ObservableCollection<OrtakDilTanim> OrtakDilListe
        {
            get => ortakDilListe;
            set => SetProperty(ref ortakDilListe, value);
        }

        public OrtakDilTanim SeciliOrtakDilTanim { get; set; }
    }
}