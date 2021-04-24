using DevExpress.Mvvm;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using mnd.Common.Helpers;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.Helper;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules.UretimOperatorEkranlariModule.DurusEkleme;
using mnd.UI.Modules.UretimOperatorEkranlariModule.MakinaDurusRapor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace mnd.UI.Modules.UretimOperatorEkranlariModule
{
    public class DhOperatorVM : MyDxViewModelBase
    {
        private string _aramaMetin;

        public ObservableCollection<BobinItemVM> BobinItemVmListe { get => bobinItemVmListe; set => SetProperty(ref bobinItemVmListe, value); }


        DokumTrackingRepository repo_bobin = new DokumTrackingRepository();


        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));

        private string makinaKod;
        private ObservableCollection<BobinItemVM> bobinItemVmListe;

        public DhOperatorVM(string formAd)
        {
          

        }

        private void OnBobinIptal(BobinIslemIptalEvent bobinIptalEvent)
        {
            var bobin = bobinIptalEvent.Bobin;

            var vm = BobinItemVmListe.Where(c => c.Bobin.Id == bobin.Id).First();

            BobinItemVmListe.Remove(vm);

            IlkBobiniAktifEt();
        }

   

        public void IlkBobiniAktifEt()
        {
            BobinItemVmListe.Select(c => { c.IslemGorenBobinMi = false; return c; });

            var b = BobinItemVmListe.FirstOrDefault();
            if(b!=null) b.IslemGorenBobinMi = true;

        }

        public string AramaMetin
        {
            get => _aramaMetin;
            set
            {
                if (SetProperty(ref _aramaMetin, value))
                {
                    AraMetinFunction(_aramaMetin);
                }

            }
        }



        public DelegateCommand FormUnloadedCommand => new DelegateCommand(UnLoadForm);

        private void UnLoadForm()
        {
            Messenger.Default.Unregister<BobinCikartildiEvent>(this, OnBobinCikartildi);
            Messenger.Default.Unregister<BobinIslemIptalEvent>(this, OnBobinIptal);
         
        }

        private void OnBobinCikartildi(BobinCikartildiEvent bobinCikisEvent)
        {
            var bobin = bobinCikisEvent.Bobin;

            var vm = BobinItemVmListe.Where(c => c.Bobin.Id == bobin.Id).First();

            BobinItemVmListe.Remove(vm);

            BoundMessageQueue.Enqueue("Bobini Döküm Bitiş Depoya Gönderiniz...");

            IlkBobiniAktifEt();
        }

        public string MakinaKod { get => makinaKod; set => SetProperty(ref makinaKod, value); }
      
        public async void AraMetinFunction(string metin)
        {
            await BulEkleAsync(metin);
        }

        public void Load(object parametreler)
        {
            MakinaKod = parametreler.ToString();

            BobinItemVmListe = repo_bobin.DokumBobinListeGetirFromKonum(MakinaKod)
                .Select(c => new BobinItemVM(c))
                .ToObservableCollection();

            IlkBobiniAktifEt();

            Messenger.Default.Register<BobinCikartildiEvent>(this, OnBobinCikartildi);

            Messenger.Default.Register<BobinIslemIptalEvent>(this, OnBobinIptal);

        }

      

        private async Task BulEkleAsync(string aramaMetin)
        {
            if (aramaMetin == null) return;
            if (aramaMetin.Length == 0) return;

            int barkodNo;

            var uygun = int.TryParse(aramaMetin, out barkodNo);

            if (uygun == false)
            {
                AramaMetin = "";
                return;
            }

            var bobin = repo_bobin.DokumBobinGetir(aramaMetin);

            if (bobin != null)
            {

                var varMi = BobinItemVmListe.FirstOrDefault(u => u.Bobin.PlanBobinNo == bobin.PlanBobinNo);
                if (varMi != null)
                {
                    BoundMessageQueue.Enqueue("Bu bobin daha önceden eklenmiş");
                    AramaMetin = "";
                    return;
                }

                bobin.BobinKonum = MakinaKod;
                bobin.BobinIslemDurum = BOBIN_ISLEMADIM_DURUM.HAZIR;
                bobin.BobinIslemBaslamaTarihi = null;
                bobin.BobinIslemBitisTarihi =null;


                BobinItemVM bobinItemVM = new BobinItemVM(bobin);

                BobinItemVmListe.Add(bobinItemVM);

                BobinItemVmListe.Select(c => { c.IslemGorenBobinMi = false;return c; });
                BobinItemVmListe.First().IslemGorenBobinMi = true;

                IlkBobiniAktifEt();
                repo_bobin.Kaydet();
                

            }

            AramaMetin = "";
        }


    }
}


