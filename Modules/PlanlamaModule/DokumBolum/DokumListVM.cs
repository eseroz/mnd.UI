using DevExpress.Mvvm;
using Newtonsoft.Json;
using mnd.Common.Helpers;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.Logic.BC_Uretim;
using mnd.Logic.BC_Uretim.SH_RotaModel;
using mnd.UI.GyModules.MesajModule;
using mnd.UI.Helper;
using mnd.UI.Modules.PlanlamaModule.RotaKartBolum;
using mnd.UI.Modules.PlanlamaModule.ShRotaKartBolum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mnd.UI.Modules.PlanlamaModule.DokumBolum
{
    public class DokumListVM : MyDxViewModelBase
    {
        private string seciliDokumHatti;

        public DelegateCommand BobinEkleCommand => new DelegateCommand(OnBobinEkle, true);

        public DelegateCommand ShRotaKartiOlusturCommand => new DelegateCommand(OnSH_RotaKartiOlustur, true);

        public bool SecimiSerbestBirak
        {
            get => secimiSerbestBirak; 
            set
            {
                SetProperty(ref secimiSerbestBirak, value);

                foreach (var item in DokumKafileListe)
                {
                    item.SecimiSerbestBirak = value;
                }
            }
        }

        private void OnSH_RotaKartiOlustur()
        {

            ShRotaKartView _view = new ShRotaKartView();


            var seciliBobinListe = DokumKafileListe.Where(c => c.Sec == true).ToList();

            var vm = new ShRotaKartVM(string.Empty, KayitModu.Add, seciliBobinListe);

            _view.DataContext = vm;

            _view.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _view.ShowDialog();

            SecimiSerbestBirak = false;
            OnEkraniYenile(null);
        }

        public List<string> DokumHatlari => new List<string> { "DH1", "DH2", "DH3" };
        public ObservableCollection<DokumBobin> DokumKafileListe { get => dokumKafileListe; set => SetProperty(ref dokumKafileListe, value); }

        DokumRepository repo = new DokumRepository();
        private ObservableCollection<DokumBobin> dokumKafileListe;
        private DokumBobin seciliKayit;
        private int _seciliYil;
        private bool secimiSerbestBirak;

        public IExportService ExportService1 => ServiceContainer.GetService<IExportService>("servis1");
        public DelegateCommand<object> ExcelExportCommand => new DelegateCommand<object>(OnExcelExport, true);


        private void OnExcelExport(object obj)
        {
            var tarih = DateTime.Now.ToString("ddMMyyyy_HHmm");

            ExportService.ExportTo(ExportType.XLSX, $"{SeciliDokumHatti}_{tarih}.xlsx");
        }

        public DokumBobin SeciliKayit
        {
            get => seciliKayit;
            set
            {
                if (seciliKayit != null) seciliKayit.PropertyChanged -= SeciliKayit_PropertyChanged;

                SetProperty(ref seciliKayit, value);

                if (seciliKayit != null) seciliKayit.PropertyChanged += SeciliKayit_PropertyChanged;

            }
        }

        private void SeciliKayit_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsChanged" && e.PropertyName != "RotaKartinaSecilebilirMi")
            {
                var dokum = sender as DokumBobin;
                repo.DokumGuncelle(dokum);
            }

        }

        public string SeciliDokumHatti
        {
            get => seciliDokumHatti; set
            {
                SetProperty(ref seciliDokumHatti, value);
                DokumKafileListe = repo.DokumKafileListeGetir(value, _seciliYil);

                SeciliKayit = DokumKafileListe.Count > 0 ? DokumKafileListe.Last() : null;
            }
        }

        public ObservableCollection<int> Yillar { get; set; }
        public int SeciliYil
        {
            get => _seciliYil;
            set
            {
                SetProperty(ref _seciliYil, value);

                DokumKafileListe = repo.DokumKafileListeGetir(seciliDokumHatti, value);

                SeciliKayit = DokumKafileListe.Count > 0 ? DokumKafileListe.Last() : null;
            }
        }

        public DokumListVM(string formAdi)
        {
            SeciliDokumHatti = "DH1";
            _seciliYil = DateTime.Now.Year;

            Yillar = new ObservableCollection<int>();

            for (int i = 2019; i <= SeciliYil + 1; i++)
            {
                Yillar.Add(i);
            }

            DokumKafileListe = repo.DokumKafileListeGetir(seciliDokumHatti, _seciliYil);
            SeciliKayit = DokumKafileListe.Count > 0 ? DokumKafileListe.Last() : null;

            DokumKafileListe.MesajSayilariniGuncelle(AppPandap.AktifKullanici.KullaniciId);

         


        }

        public int SonKafileGrupNoGetir()
        {
            var sonKafile = DokumKafileListe.Where(c => c.DokumHattiKod == SeciliDokumHatti).LastOrDefault();


            if (sonKafile != null) return sonKafile.PlanBobinNo.GetValueOrDefault();

            if (SeciliDokumHatti == "DH1") return int.Parse("1" + (SeciliYil - 2000) + "0000");
            if (SeciliDokumHatti == "DH2") return int.Parse("2" + (SeciliYil - 2000) + "0000");
            if (SeciliDokumHatti == "DH3") return int.Parse("3" + (SeciliYil - 2000) + "0000");

            return 0;


        }


        public DelegateCommand<object> EkranYenileCommand => new DelegateCommand<object>(OnEkraniYenile, c => true);

        private void OnEkraniYenile(object obj)
        {
            VerileriYukle();
        }

        private void VerileriYukle()
        {
            DokumKafileListe = repo.DokumKafileListeGetir(SeciliDokumHatti, _seciliYil);
        }

        private void OnBobinEkle()
        {
            BobinEkleFormVM vm = new BobinEkleFormVM();
            vm.DokumKafileEdit = new DokumBobin();
            vm.DokumKafileEdit.PlanTarihi = DateTime.Now.Date;

            vm.DokumKafileEdit.DokumHattiKod = SeciliDokumHatti;



            BobinEkleForm form = new BobinEkleForm();


            form.DataContext = vm;

            var cev = form.ShowDialog();

            if (cev == true)
            {
                ShRecete_TrackingRepository repo_recete = new ShRecete_TrackingRepository();

                var dokumBobin = vm.DokumKafileEdit;

                var dokumHattiSonKayit = DokumKafileListe.LastOrDefault();
                var dokumHattiSonSayi = dokumHattiSonKayit == null ? 0 : dokumHattiSonKayit.DH_SatirId;


                var bobinSiraNoSonSayi = dokumHattiSonKayit?.AlasimTipKod == dokumBobin.AlasimTipKod
                    ? dokumHattiSonKayit.BobinSiraNo
                    : 0;

                dokumBobin.PlanBobinNo_Grup = SonKafileGrupNoGetir();

               

                var receteKod = $"{dokumBobin.AlasimTipKod}-{dokumBobin.Kondisyon}-{dokumBobin.ShReceteNihaiKalinlik}";

                var uygun_recete = repo_recete.UygunReceteGetir(receteKod);

                if(uygun_recete==null)
                {
                    MessageBox.Show($"{receteKod} için uygun reçete bulunamadı Kalite birimine tanımlatınız.", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var receteFazlar = ReceteFazlariGetir(uygun_recete);

                for (int i = 1; i <= dokumBobin.BobinAdet; i++)
                {
                    var yeniDokum = JsonConvert.DeserializeObject<DokumBobin>(JsonConvert.SerializeObject(dokumBobin));

                    yeniDokum.RowGuid = Guid.NewGuid();
                    yeniDokum.PlanBobinNo = yeniDokum.PlanBobinNo_Grup + i;
                    yeniDokum.BobinSiraNo = bobinSiraNoSonSayi + i;
                    yeniDokum.DH_SatirId = dokumHattiSonSayi + i;
                    yeniDokum.BobinKonum = BOBIN_KONUM.DH;
                    yeniDokum.DokumHattiKod = SeciliDokumHatti;

                    yeniDokum.DokumBobinIslemAdimlari = new ObservableCollection<DokumBobinIslemAdim>();

                    int sn = 0;
                    foreach (var faz in receteFazlar)
                    {
                        sn++;
                        var islemAdim = new DokumBobinIslemAdim();
                        islemAdim.SiraNo = sn;
                        islemAdim.BobinNo = yeniDokum.PlanBobinNo.GetValueOrDefault();
                        islemAdim.EzmeYuzde = faz.EzmeYuzde;
                        islemAdim.ProsesMax = faz.ProsesMax;
                        islemAdim.ProsesMin = faz.ProsesMin;
                        islemAdim.ProsesMinStr = faz.ProsesMinStr;
                        islemAdim.ProsesMaxStr = faz.ProsesMaxStr;
                        islemAdim.ProsesMetin = faz.ProsesMetin;
                        islemAdim.MakinaIslem = faz.MakinaIslem;
                        islemAdim.AlasimTipKod = yeniDokum.AlasimTipKod;

                        yeniDokum.DokumBobinIslemAdimlari.Add(islemAdim);

                    }

                    var ilkIslemAdim = yeniDokum.DokumBobinIslemAdimlari.First();
                    ilkIslemAdim.AktifMi = true;

                    yeniDokum.Nereye = yeniDokum.GidecegiYerGetir();

                    DokumKafileListe.Add(yeniDokum);

                    repo.DokumEkle(yeniDokum);
                }
            }

            SeciliKayit = DokumKafileListe.Count > 0 ? DokumKafileListe.Last() : null;

        }

        private List<ShRotaKartFaz> ReceteFazlariGetir(ShRecete uygun_recete)
        {
            IList<PropertyInfo> props = new List<PropertyInfo>(uygun_recete.GetType().GetProperties().Where(c => c.Name.Contains("Faz")));
            var fazlar = new List<ShRotaKartFaz>();

            for (int i = 1; i <= 7; i++)
            {
                var propListe = props.Where(c => c.Name.StartsWith("Faz" + i)).ToList();

                var faz = new ShRotaKartFaz();
                faz.SiraNo = i;
                faz.MakinaIslem = propListe[0].GetValue(uygun_recete, null).ToString();

                var param1 = propListe[1].GetValue(uygun_recete, null);
                var param2 = propListe[2].GetValue(uygun_recete, null);

                faz.ProsesMetin = param1.ToString() + " - " + param2.ToString();

                faz.ProsesMaxStr = param1.ToString();
                faz.ProsesMinStr = param2.ToString();

                decimal param1_decimal;
                decimal param2_decimal;

                var p1 = decimal.TryParse(param1.ToString(), out param1_decimal);
                var p2 = decimal.TryParse(param2.ToString(), out param2_decimal);

                if (p1 == true && p2 == true)
                {
                    faz.EzmeYuzde = 1 - Math.Round(param2_decimal / param1_decimal, 2);
                }

                if (faz.ProsesMetin.Trim() != "-") fazlar.Add(faz);
            }

            return fazlar;
        }
    }
}
