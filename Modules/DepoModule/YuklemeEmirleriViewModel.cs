using DevExpress.Mvvm;
using mnd.Common;
using mnd.Common.Helpers;
using mnd.Logic.Model.Operasyon;
using mnd.Logic.Persistence;
using mnd.UI.Helper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace mnd.UI.Modules.DepoModule
{
    public class SqlDependencyIrsaliyePalet
    {
        public int PaletId { get; set; }
        public bool YuklendiMi { get; set; }
        public string IrsaliyeId { get; set; }

    }

    public class SqlDependencySevkiyatEmri
    {
        public int SevkiyatEmriId { get; set; }
        public DateTime YuklemeBaslamaTarih { get; set; }
        public string SevkSurecDurum { get; set; }
    }

    public class YuklemeEmirleriViewModel : MyDxViewModelBase, IForm, IDocumentContent
    {
        public ObservableCollection<SevkiyatEmri> SevkiyatEmirleri { get => _sevkiyatEmirleri; set => SetProperty(ref _sevkiyatEmirleri, value); }

        private SevkiyatEmri _seciliSevkiyatEmri;

        private string _aramaMetin;
        private int _maxPaletSayisi;
        private int _seciliSevkYukluPaletSayisi;
        private ObservableCollection<SevkiyatEmri> _sevkiyatEmirleri;
        private bool _paletBulunamadi;
        private MediaPlayer _player;

        public bool PaletBulundu { get => _paletBulundu; set => SetProperty(ref _paletBulundu, value); }


        public bool EkrandanYuklemeEmriDegistirebilirMi => AppPandap.AktifKullanici.KullaniciRol != KULLANICIROLLERI.MAMULDEPO;

        public SevkiyatEmri AktifEdilenYuklemeEmri { get; set; }


        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);

        public DelegateCommand TemizleCommand => new DelegateCommand(OnTemizle);

        public IrsaliyePaletDto SeciliKalanPalet { get => _seciliKalanPalet; set => SetProperty(ref _seciliKalanPalet, value); }

        private void OnTemizle()
        {
            tdpIrsaliyePalet.Stop();
        }

        private void OnFormLoaded()
        {
            if (SevkiyatEmirleri.Count > 0)
                SeciliSevkiyatEmri = SevkiyatEmirleri.OrderByDescending(c => c.YuklemeBaslamaTarih).First();
        }



        public MediaPlayer Player
        {
            get
            {
                if (_player == null)
                {
                    _player = new MediaPlayer();
                    return _player;
                }
                else
                    return _player;

            }

        }

        public Uri PaletBulunamadiSesUri => new Uri("Content/SmokeAlarm.mp3", UriKind.RelativeOrAbsolute);
        public Uri PaletAktarildiSesUri => new Uri("Content/Basarili.mp3", UriKind.RelativeOrAbsolute);
        public Uri PositiveSesUri => new Uri("Content/zapsplat_positive.mp3", UriKind.RelativeOrAbsolute);


        UnitOfWork uowSevkiyat = new UnitOfWork();




        public YuklemeEmirleriViewModel(string formAdi)
        {
            var dbPath = GlobalSettings.Default.SqlCnnString;

            SevkiyatEmirleri = uowSevkiyat.SevkiyatEmirRepo.YuklemedekiSevkiyatEmirleriGetir();

            foreach (var item in SevkiyatEmirleri)
            {
                item.PaletYuklemeVerileriniEkle();
                item.PaletNet_TKg_Yuklenen = item.YuklenenPaletler.Sum(c => c.PaletNet_Kg);
            }

            PaletBulunamadi = false;

            SeciliSevkiyatEmri = SevkiyatEmirleri.OrderByDescending(c => c.YuklemeBaslamaTarih).FirstOrDefault();
            AktifEdilenYuklemeEmri = SeciliSevkiyatEmri;

            tdpSevkiyatEmri = new SqlTableDependency<SqlDependencySevkiyatEmri>(dbPath, "SevkiyatEmri", "Operasyon");
            tdpIrsaliyePalet = new SqlTableDependency<SqlDependencyIrsaliyePalet>(dbPath, "IrsaliyePalet", "Operasyon");

            tdpSevkiyatEmri.OnChanged += TdpSevkiyatEmri_OnChanged;
            tdpIrsaliyePalet.OnChanged += TdpOperasyonPalet_OnChanged;

            tdpSevkiyatEmri.Start();
            tdpIrsaliyePalet.Start();


        }

        private void TdpSevkiyatEmri_OnChanged(object sender, RecordChangedEventArgs<SqlDependencySevkiyatEmri> e)
        {
            var tdb_sevkEmir = (e.Entity as SqlDependencySevkiyatEmri);

            uowSevkiyat = new UnitOfWork();
            SevkiyatEmirleri = uowSevkiyat.SevkiyatEmirRepo.YuklemedekiSevkiyatEmirleriGetir();

            foreach (var item in SevkiyatEmirleri)
            {
                item.PaletYuklemeVerileriniEkle();
                item.PaletNet_TKg_Yuklenen = item.YuklenenPaletler.Sum(c => c.PaletNet_Kg);
            }

            App.Current.Dispatcher.Invoke(() =>
            {
                SeciliSevkiyatEmri = SevkiyatEmirleri.FirstOrDefault(c => c.SevkiyatEmriId == tdb_sevkEmir.SevkiyatEmriId);
            });

        }

        private void TdpOperasyonPalet_OnChanged(object sender, RecordChangedEventArgs<SqlDependencyIrsaliyePalet> e)
        {
            if (SeciliSevkiyatEmri == null) return;

            var irsPaletDep = (e.Entity as SqlDependencyIrsaliyePalet);

            var irsPalet = SeciliSevkiyatEmri.TumPaletler.Where(c => c.PaletId == irsPaletDep.PaletId).FirstOrDefault();
            if (irsPalet == null) return;


            if (e.ChangeType == ChangeType.Update)
            {
                if (irsPaletDep.YuklendiMi == true)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        SeciliSevkiyatEmri.PaletYukle(irsPalet);
                    });
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        SeciliSevkiyatEmri.PaletCikar(irsPalet);
                    });

                }
            }
        }


        public bool PaletBulunamadi { get => _paletBulunamadi; set => SetProperty(ref _paletBulunamadi, value); }

        private SqlTableDependency<SqlDependencyIrsaliyePalet> tdpIrsaliyePalet;
        private SqlTableDependency<SqlDependencySevkiyatEmri> tdpSevkiyatEmri;

        private IrsaliyePaletDto _seciliKalanPalet;
        private bool _paletBulundu;

        public SevkiyatEmri SeciliSevkiyatEmri
        {
            get => _seciliSevkiyatEmri;
            set
            {
                SetProperty(ref _seciliSevkiyatEmri, value);
            }
        }

        public void Load()
        {

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

        public async void AraMetinFunction(string metin)
        {
            await BulEkleAsync(metin);
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

            PaletBulunamadi = false;
            Player.Stop();

            var palet = SeciliSevkiyatEmri.KalanPaletler.Where(c => c.PaletId == barkodNo).FirstOrDefault();



            if (palet != null)
            {
                UnitOfWork uowPalet = new UnitOfWork();
                SeciliKalanPalet = palet;

                var uretimPalet = uowPalet.PlanlamaRepo.PaletGetir(palet.PaletId);
                SeciliKalanPalet.MasuraSayisi = BobinSayiHelper.BobinAdetBul(uretimPalet.BobinlerBirlesik);


                PaletBulundu = true;
                Player.Open(PositiveSesUri);
                Player.Play();

                await Task.Delay(5000);
                Player.Stop();

                palet.YuklendiMi = true;


                var irs_palet = uowPalet.SevkiyatEmirRepo.IrsaliyePaletGetir(palet.PaletId);


                irs_palet.YuklendiMi = true;
                uowPalet.Commit();

                Player.Open(PaletAktarildiSesUri);
                Player.Play();

                AramaMetin = null;

                PaletBulundu = false;
            }
            else
            {
                PaletBulunamadi = true;
                Player.Open(PaletBulunamadiSesUri);
                Player.Play();
                await Task.Delay(7000);
                PaletBulunamadi = false;
            }

            AramaMetin = "";
        }

        public IDocumentOwner DocumentOwner { get; set; }
        public object Title => "hhh";



        public void OnClose(CancelEventArgs e)
        {
            tdpIrsaliyePalet.Stop();
            tdpSevkiyatEmri.Stop();
        }

        public void OnDestroy()
        {

        }
    }



}
