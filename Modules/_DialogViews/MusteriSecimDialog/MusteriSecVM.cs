using DevExpress.Mvvm;
using mnd.Logic.BC_SatinAlmaYeni.Data;
using mnd.Logic.BC_SatinAlmaYeni.Domain;
using mnd.Logic.BC_Satis.Repositories;
using mnd.Logic.Helper;
using mnd.UI.Helper;
using mnd.UI.Modules.TeklifModule.MessangerEvents;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace mnd.UI.Modules._DialogViews.MusteriSecimDialog
{
    public class MusteriSecVM : MyDxViewModelBase
    {
        private string aramaMetin;

        public ObservableCollection<MusteriItemModel> Musteriler { get => musteriler; set => SetProperty(ref musteriler, value); }

        public DelegateCommand<MusteriItemModel> MusteriSecCommand => new DelegateCommand<MusteriItemModel>(OnMusteriSec, c => true);

        public DelegateCommand GeciciCariTanimlaCommand => new DelegateCommand(OnCariTanimla);

        public MusteriItemModel GeciciCariModel { get; set; }

        private void OnCariTanimla()
        {
            var yeniSira = 0;
            var yeniCariKod ="";

            if (Musteriler.Count == 0)
            {
                yeniSira = 1;
                yeniCariKod = "P" + yeniSira.ToString().PadLeft(4, '0');
            } else { 
                yeniSira = int.Parse(Musteriler.Last().CariKod.Replace("P", "")) + 1;
                yeniCariKod = "P" + yeniSira.ToString().PadLeft(4, '0');
            }


            GeciciCariModel = new MusteriItemModel();
            GeciciCariModel.UlkeKod = "TR";
            GeciciCariModel.CariKod = yeniCariKod;

            MessageResult result = DialogService.ShowDialog(
             dialogButtons: MessageButton.OKCancel,
             title: "Cari Ekle...",
             this
            );

            if (result == MessageResult.Cancel) return;

            Musteriler.Add(GeciciCariModel);

            AppRepositorySA appRape = new AppRepositorySA();

            var yeniCari = new CariGecici { CariKod = yeniCariKod, CariAd = GeciciCariModel.CariAd, UlkeKod = "TR" };


            appRape.GeciciCariEkle(yeniCari);
        }

        public DelegateCommand FormClosingCommand => new DelegateCommand(OnFormClosing);

        string cariKodBaslangicSayi = "120";

        public string GeciciCariAd { get => geciciCariAd; set => SetProperty(ref geciciCariAd, value); }

        private void OnFormClosing()
        {
            Messenger.Default.Send<MusteriSecildiEvent>(null);
        }

        public ICurrentWindowService CurrentWindow => ServiceContainer.GetService<ICurrentWindowService>(ServiceSearchMode.LocalOnly);
        private void OnMusteriSec(MusteriItemModel obj)
        {
            Messenger.Default.Send<MusteriSecildiEvent>(new MusteriSecildiEvent(obj));
            CurrentWindow.Close();
        }

        public string AramaMetin
        {
            get => aramaMetin;
            set
            {
                aramaMetin = value.ToUpper(CultureInfo.CreateSpecificCulture("Tr-tr"));
                var bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');

                if (aramaMetin.Length >= 2 || aramaMetin.Length == 0)
                {
                    if (cariKodBaslangicSayi == "320")
                    {
                        SaticilariYukle(aramaMetin);
                    }
                    else
                    {
                        MusterileriYukle(aramaMetin, bagliPlasiyerKodlari);

                    }

                }
            }
        }

        PandapMusteriRepository repo = new PandapMusteriRepository();
        private ObservableCollection<MusteriItemModel> musteriler;
        private string geciciCariAd;

        public MusteriSecVM(string param = "", ObservableCollection<MusteriItemModel> disardanListe = null)
        {
            if (disardanListe != null)
            {
                Musteriler = new ObservableCollection<MusteriItemModel>();

                foreach (var item in disardanListe)
                {
                    Musteriler.Add(item);

                }
                return;
            }

            if (param == "SatinAlma")
            {
                cariKodBaslangicSayi = "320";
                SaticilariYukle(String.Empty);
                return;
            }


            if (param.Length > 0)
            {
                cariKodBaslangicSayi = "320";
                SaticilariYukle(String.Empty);

            }
            else
            {
                var bagliPlasiyerKodlari = AppPandap.AktifKullanici.BagliNetsisPlasiyerKodlari.Split(';');
                MusterileriYukle(String.Empty, bagliPlasiyerKodlari);
            }

        }

        public void SaticilariYukle(string aramaMetin)
        {
            Musteriler = repo.PandapSaticilarQuery()
                .Select(c => new MusteriItemModel
                {
                    CariKod = c.CariKod,
                    CariAd = c.CariIsim,
                    UlkeKod = c.UlkeKod,
                    PlasiyerKod = c.PlasiyerKod,
                    DovizTipKod = c.DovizAd,
                    Sektor = c.Sektor,
                })
                .Where(c => c.CariKod.StartsWith(cariKodBaslangicSayi))
                .Where(c => c.CariKod.Contains(aramaMetin) || c.CariAd.Contains(aramaMetin))
                .ToObservableCollection();
        }

        public void MusterileriYukle(string aramaMetin, string[] bagliPlasiyerKodlari)
        {
            Musteriler = repo.PandapMusterileriQuery()
                .Select(c => new MusteriItemModel
                {
                    CariKod = c.CariKod,
                    CariAd = c.CariIsim,
                    UlkeKod = c.UlkeKod,
                    PlasiyerKod = c.PlasiyerKod,
                    DovizTipKod = c.DovizAd
                })
                .Where(c => bagliPlasiyerKodlari.Contains(c.PlasiyerKod))
                .Where(c => c.CariKod.Contains(aramaMetin) || c.CariAd.Contains(aramaMetin))
               .ToObservableCollection();
        }
    }
}
