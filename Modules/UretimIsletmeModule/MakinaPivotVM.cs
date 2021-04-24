using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DevExpress.Mvvm;
using DevExpress.Xpf.PivotGrid;
using mnd.Logic.BC_App;
using mnd.Logic.BC_App.Domain;
using mnd.Logic.BC_Uretim;
using mnd.UI.Helper;

namespace mnd.UI.Modules.UretimIsletmeModule
{
    public class MakinaPivotVM : MyDxViewModelBase
    {
        private UretimTabloRepository repo = new UretimTabloRepository();
        IsMerkeziRepository repoIsMerkezi = new IsMerkeziRepository();

        private int dxLayoutId;
        private List<DxGridLayout> dxGridLayouts;
        private string yeniRaporAd;
        private List<Uretim_UretimTablo> uretimTablo;

        public List<Uretim_UretimTablo> UretimTablo { get => uretimTablo; set => SetProperty(ref uretimTablo, value); }

        public List<MakinaDurusTanim> MakinaDuruslari { get; set; }

        public List<DxGridLayout> DxGridLayouts { get => dxGridLayouts; set => SetProperty(ref dxGridLayouts, value); }

        public DelegateCommand<PivotGridControl> FormLoadedCommand => new DelegateCommand<PivotGridControl>(OnFormLoaded);

        public DelegateCommand<PivotGridControl> LoadXmlDataCommand => new DelegateCommand<PivotGridControl>(OnRestoreLayout);

        public DelegateCommand<PivotGridControl> KaydetXmlDataCommand => new DelegateCommand<PivotGridControl>(OnSaveLayout);

        public DelegateCommand<PivotGridControl> LayoutChangedCommand => new DelegateCommand<PivotGridControl>(OnLayoutChanged);

        public DelegateCommand EkranYenileCommand => new DelegateCommand(OnEkranYenile);

        private void OnEkranYenile()
        {


            var makinaListe = repoIsMerkezi.UretimMakinalariGetir()
                         .Select(c => new Makina { MakinaKod = c.Kod, MakinaAd = c.Tanim })
                         .ToList();

            DxGridLayouts = DxLayoutService.LayoutListeGetir("Makina", null);

            UretimTablo = repo.UretimTabloGetir()
                .ToList();
            MakinaDuruslari = UretimDataService.MakinaDuruslariGetir();

            foreach (var item in UretimTablo)
            {
                item.MakinaDuruşAd = MakinaDuruslari.Where(c => c.DurusKod == item.DuruşKodu).FirstOrDefault()?.DurusAd;
                item.MakinaDurusGrupAd = MakinaDuruslari.Where(c => c.DurusKod == item.DuruşKodu).FirstOrDefault()?.DurusGrup;
                item.MakinaAd = makinaListe.Find(c => c.MakinaKod == item.MakinaKod).MakinaAd;

            }

        }

        public DelegateCommand YenidenAdlandirCommand => new DelegateCommand(OnYenidenAdlandir);

        public string YeniRaporAd { get => yeniRaporAd; set => SetProperty(ref yeniRaporAd, value); }

        private void OnYenidenAdlandir()
        {
            var layout = DxGridLayouts.First(c => c.Id == DxLayoutId);
            var raporAdTumu = layout.Baslik;

            var orjLeft = raporAdTumu.Substring(0, 8);

            var orjRight = raporAdTumu.Substring(8);

            YeniRaporAd = orjRight.Replace(": ", "");

            MessageResult result = DialogService.ShowDialog(
                dialogButtons: MessageButton.OKCancel,
                title: "Yeniden Adlandır...",
                this
            );

            if (result == MessageResult.Cancel) return;

            var text = orjLeft + ": " + YeniRaporAd;

            layout.Baslik = text;

            DxLayoutService.YenidenAdlandir(DxLayoutId, text);
        }

        private void OnLayoutChanged(PivotGridControl pvt)
        {
            OnRestoreLayout(pvt);
        }

        private void OnSaveLayout(PivotGridControl pvt)
        {
            MemoryStream layoutStream = new MemoryStream();
            pvt.SaveLayoutToStream(layoutStream);

            layoutStream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(layoutStream, System.Text.Encoding.UTF8);
            string xml_text = reader.ReadToEnd();

            DxLayoutService.KaydetLayout(DxLayoutId, xml_text);
        }

        private void OnRestoreLayout(PivotGridControl pvt)
        {
            if (DxLayoutId == 0) return;

            var layout = DxLayoutService.XmlDataGetirFromId(DxLayoutId);

            if (layout.XmlData == null) return;

            var stream = GenerateStreamFromString(layout.XmlData);

            pvt.RestoreLayoutFromStream(stream);
        }

        public int DxLayoutId
        {
            get => dxLayoutId;
            set
            {
                SetProperty(ref dxLayoutId, value);
            }
        }

        public Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public MakinaPivotVM(string formAd)
        {
            OnEkranYenile();

          
        }

        private void OnFormLoaded(PivotGridControl pvt)
        {
            if (DxGridLayouts.Count == 0)
            {
                MemoryStream layoutStream = new MemoryStream();
                pvt.SaveLayoutToStream(layoutStream);

                layoutStream.Seek(0, SeekOrigin.Begin);
                StreamReader reader = new StreamReader(layoutStream, System.Text.Encoding.UTF8);
                string xml_text = reader.ReadToEnd();

                for (int i = 1; i <= 10; i++)
                {
                    var layoutData = new DxGridLayout { Baslik = "Rapor-" + i.ToString().PadLeft(2, '0'), GrupAd = "makina", KullaniciId = "", XmlData = xml_text };

                    DxLayoutService.EkleLayout(layoutData);
                }

                DxGridLayouts = DxLayoutService.LayoutListeGetir("Makina", null);
            }

            DxLayoutId = DxGridLayouts.First().Id;
        }
    }
}