using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.BC_Operasyon;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.AppModule;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.Modules.OperasyonModule
{
    public class HaftalikYuklemePlanViewModel : MyDxViewModelBase
    {
        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));



        [YetkiKontrol]
        public DelegateCommand KaydetCommand { get; set; }
        public DelegateCommand FormUnLoadedCommand => new DelegateCommand(OnFormUnLoaded);
        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);

        public bool FormLoaded { get; set; }

        public HaftalikYuklemePlanViewModel()
        {

        }

        public HaftalikYuklemePlanViewModel(string formMenuAd)
        {
            FormMenuAd = formMenuAd;

            Haftalar = new ObservableCollection<int>();
            Yillar = new ObservableCollection<int>();

            _seciliHafta = CalenderUtil.GetWeekNumberFromDate(DateTime.Now);
            _seciliYil = DateTime.Now.Year;

            for (int i = 1; i <= 52; i++)
            {
                Haftalar.Add(i);
            }

            for (int i = 2019; i <= SeciliYil + 1; i++)
            {
                Yillar.Add(i);
            }


            var uow = new UnitOfWork();
            FormPermissions = uow.AppRepo.FormPermissions(AppPandap.AktifKullanici.KullaniciRol, FormMenuAd);


            KaydetCommand = new DelegateCommand(OnKaydet, () => YetkiliMi(nameof(KaydetCommand)));

        }


        public bool YetkiliMi(string komut)
        {
            var yetki = FormPermissions.Any(c => c.Komut == komut);
            return yetki;
        }
        private void OnFormLoaded()
        {

            if (FormLoaded == true) return;

            SeciliHafta = _seciliHafta;
            SeciliYil = _seciliYil;

            if (AppPandap.UygulamaAyarlar.ServiceBrokerAktifMi)
                SqlTable_Dependency<HaftalikPlanDTO>.Default.Basla("HaftalikYuklemePlan", "Operasyon", AppPandap.AktifKullanici.KullaniciId);

            Messenger.Default.Register<HaftalikPlanDTO>(this, HaftalikPlanDegisti);

            FormLoaded = true;
        }

        private void OnFormUnLoaded()
        {
            SqlTable_Dependency<HaftalikPlanDTO>.Default.Durdur();
            Messenger.Default.Unregister<HaftalikPlanDTO>(this, HaftalikPlanDegisti);
        }

        private void HaftalikPlanDegisti(HaftalikPlanDTO c)
        {
            var mesaj = "Haftalık Plan Değişti " + c.UpdateUserId ?? "";
            BoundMessageQueue.Enqueue(mesaj, true);

            repo = new HaftalikYuklemeRepository();
            HaftalikYuklemePlan = repo.HaftalikYuklemePlaniGetir(SeciliYil, SeciliHafta);

        }


        private bool CanKaydet()
        {
            return AppPandap.AktifKullanici.KullaniciRol == KULLANICIROLLERI.PLANLAMA_YONETICI;
        }

        private void OnKaydet()
        {
            this.HaftalikYuklemePlan.ToList().ForEach(c => c.UpdateUserId = AppPandap.AktifKullanici.KullaniciId);

            repo.Kaydet();

            var mesaj = "Kayıt İşlemi Başarıyla Tamamlandı";
            BoundMessageQueue.Enqueue(mesaj, true);
        }

        public ObservableCollection<HaftalikYuklemePlan> HaftalikYuklemePlan
        { get => _haftalikYuklemePlan; set => SetProperty(ref _haftalikYuklemePlan, value); }

        HaftalikYuklemeRepository repo = new HaftalikYuklemeRepository();
        private ObservableCollection<HaftalikYuklemePlan> _haftalikYuklemePlan;

        private int _seciliHafta;
        private int _seciliYil;

        public int SeciliYil { get => _seciliYil; set => SetProperty(ref _seciliYil, value); }

        public ObservableCollection<int> Haftalar { get; set; }

        public ObservableCollection<int> Yillar { get; set; }


        public int SeciliHaftaId { get; set; }
        public string FormMenuAd { get; }

        public int SeciliHafta
        {
            get => _seciliHafta;
            set
            {
                SetProperty(ref _seciliHafta, value);

                HaftalikYuklemePlan = repo.HaftalikYuklemePlaniGetir(_seciliYil, _seciliHafta);

            }
        }






    }
}
