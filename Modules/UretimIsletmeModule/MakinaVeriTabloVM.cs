using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using DevExpress.Xpf.Grid;
using MaterialDesignThemes.Wpf;
using mnd.Common.Helpers;
using mnd.Logic.BC_App;
using mnd.Logic.BC_Satis.Data_LookUp.Model;
using mnd.Logic.BC_Satis.Repositories;
using mnd.Logic.BC_Uretim;
using mnd.Logic.Helper;
using mnd.UI.Helper;

namespace mnd.UI.Modules.UretimIsletmeModule
{
    public class MakinaVeriTabloVM : MyDxViewModelBase
    {
        private UserControl dataGridContent;

        private LookUpDataService lookUp = new LookUpDataService();

        private string makinaGrupKod;

        private int makinaKod;

        private UretimTabloRepository repo = new UretimTabloRepository();

        private Uretim_UretimTablo seciliSatir;

        private DateTime seciliTarih;

        private string seciliYerlesim;

        private ObservableCollection<Uretim_UretimTablo> uretimTabloSatirlar;

        private string vardiya;

        IsMerkeziRepository repoIsMerkezi = new IsMerkeziRepository();

        public MakinaVeriTabloVM(string formAdi)
        {
            SeciliTarih = DateTime.Now.AddDays(-1).Date;

            Vardiyalar = new ObservableCollection<string>();

            Vardiyalar.Add("08:00 - 20:00");
            Vardiyalar.Add("20:00 - 08:00");

            OperatorListe = KullaniciDataServices
                                .KullaniciGetirFromRol(KULLANICIROLLERI.URETIM_OPERATOR)
                                .Select(c => new OperatorBilgi
                                { KullaniciId = c.KullaniciId, AdSoyad = c.AdSoyad, YetkiliMakinalar = c.YetkiliMakinalar?.Split(';').ToList() })
                                .ToList();

            MakinaListe = repoIsMerkezi.UretimMakinalariGetir()
                            .Select(c => new Makina { MakinaKod = c.Kod, MakinaAd = c.Tanim })
                            .ToList();

            MakinaDuruslari = UretimDataService.MakinaDuruslariGetir();

            AlasimListe = lookUp.AlasimTipleriGetir();

            MakinaKod = MakinaListe[0].MakinaKod;

            Vardiya = "08:00 - 20:00";

            Yerlesimler = new List<string>();

            Yerlesimler.Add("1");
            Yerlesimler.Add("2");

            SeciliYerlesim = "1";
        }

        public List<AlasimTip> AlasimListe { get; }

        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));

        public DelegateCommand FormLoadedCommand => new DelegateCommand(OnFormLoaded);

        public bool IsFormLoaded { get; set; }

        public DelegateCommand KaydetCommand => new DelegateCommand(OnKaydet);

        public List<MakinaDurusTanim> MakinaDuruslari { get; }

     
        public int MakinaKod
        {
            get => makinaKod; set
            {
                OnKaydet();

                SetProperty(ref makinaKod, value);

                if (IsFormLoaded) ExportService.RestoreLayout(makinaKod + seciliYerlesim + "_v1.xml");

                if (IsFormLoaded) VerileriYukle();

            }
        }

        public List<Makina> MakinaListe { get; set; }

        public List<OperatorBilgi> OperatorListe { get; }

        public DateTime SeciliTarih
        {
            get => seciliTarih; set
            {
                seciliTarih = value;

                if (IsFormLoaded) VerileriYukle();
            }
        }

        public string SeciliYerlesim
        {
            get => seciliYerlesim; set
            {
                SetProperty(ref seciliYerlesim, value);

                if (IsFormLoaded)
                {
                    var yol = makinaKod + seciliYerlesim + "_v1.xml";
                    ExportService.RestoreLayout(yol);
                }
            }
        }

        public ObservableCollection<Uretim_UretimTablo> UretimTabloSatirlar
        { get => uretimTabloSatirlar; set => SetProperty(ref uretimTabloSatirlar, value); }

        public string Vardiya
        {
            get => vardiya; set
            {
                OnKaydet();
                vardiya = value;

           

                if (IsFormLoaded) VerileriYukle();
            }
        }

        public ObservableCollection<string> Vardiyalar { get; }
        public DelegateCommand VeriKontrolEtCommand => new DelegateCommand(OnVeriKontrolEt);

        public DelegateCommand<string> YerlesimKaydetCommand => new DelegateCommand<string>(OnYerlesimKaydet, true);

        private void OnYerlesimKaydet(string obj)
        {
            var yol = makinaKod + seciliYerlesim + "_v1.xml";

            ExportService.SaveLayout(yol);
        }

        public List<string> Yerlesimler { get; set; }

        public Uretim_UretimTablo SatirGetirFromKafileNo(string kafileNo)
        {
            var kafileSatir = UretimTabloSatirlar
                .OrderByDescending(c => c.Id)
                .Where(c => c.GirişKafileNo?.ToUpper() == kafileNo)
                .FirstOrDefault();

            return kafileSatir;
        }

        public string ValidateAll()
        {
            var mesaj = "";
            int i = 0;
            foreach (var item in UretimTabloSatirlar)
            {
                if (item.Id == 0) break;
                i++;
                item.SiraNo = i;
                var uyarı = item.VeriTutarliMi();
                mesaj += uyarı;
            }

            return mesaj;
        }

        public void VerileriYukle()
        {
            if (!IsFormLoaded) return;
            repo = new UretimTabloRepository();

            UretimTabloSatirlar = repo.UretimTabloGetir(seciliTarih, Vardiya, makinaKod).ToObservableCollection();

            foreach (var item in UretimTabloSatirlar)
            {
                item.MakinaDuruşAd = MakinaDuruslari.Where(c => c.DurusKod == item.DuruşKodu).FirstOrDefault()?.DurusAd;
            }

            for (int i = 0; i < 1000 - UretimTabloSatirlar.Count; i++)
            {
                Thread.Sleep(1);

                var yeniSayi = DateTime.Now.Ticks + 10000;
                UretimTabloSatirlar.Add(new Uretim_UretimTablo { Id = yeniSayi});
            }
        }

        private void OnFormLoaded()
        {
            IsFormLoaded = true;

            ExportService.RestoreLayout(makinaKod + seciliYerlesim + "_v1.xml");

            VerileriYukle();
        }

        private void OnKaydet()
        {
            if (UretimTabloSatirlar == null) return;
            try
            {
                foreach (var item in UretimTabloSatirlar)
                {
                    if (item.Operatör!=null && item.KayitEklenmeTarihi == null)
                    {
                         repo.Ekle(item);
                    }
                }

                repo.Kaydet();

                var mesaj = "Başarıyla kayıt yapıldı";

                BoundMessageQueue.Enqueue(mesaj);

             }
            catch (Exception ex)
            {
                var mesaj = "Hata Oluştu:" + ex.Message + Environment.NewLine + ex.InnerException?.Message;
                MessageBox.Show(mesaj);
            }
        }

        private void OnVeriKontrolEt()
        {
            var sonuc = ValidateAll();

            if (sonuc.Length > 0)
            {
                MessageBox.Show(sonuc, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                MessageBox.Show("Verilerde sorun bulunamadı", "Pandap", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //////////////
        ///

        public DelegateCommand<CellValueChangedEventArgs> CellValueChangedCommand =>
                                   new DelegateCommand<CellValueChangedEventArgs>(OnKeyCellValueChanged);

        public DelegateCommand<TableView> IcerigiTemizleCommand => new DelegateCommand<TableView>(OnIcerikTemizle);

        public DelegateCommand<KeyEventArgs> KeyDownCommand => new DelegateCommand<KeyEventArgs>(OnKeyDown);

        public DelegateCommand<PastingFromClipboardEventArgs> PastingFromClipboardCommand =>
                        new DelegateCommand<PastingFromClipboardEventArgs>(OnPastingFromClipboard);

        public DelegateCommand<TableView> SilCommand => new DelegateCommand<TableView>(OnSil);

        public DelegateCommand<TableView> UsteSatirEkleCommand => new DelegateCommand<TableView>(OnUsteSatirEkle);

        public List<string[]> GetClipboardData()
        {
            var rawDataStr = Clipboard.GetText();

            List<string[]> clipboardData = new List<string[]>();
            string[] rows = rawDataStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (var item in rows)
            {
                clipboardData.Add(item.Split('\t'));
            }

            return clipboardData;
        }

        private void OnCellValueChanged(string fieldName, object row, object value)
        {
            var seciliSatir = (Uretim_UretimTablo)row;
            var satir_index = uretimTabloSatirlar.IndexOf(seciliSatir);

            if (seciliSatir.KayitEklenmeTarihi ==null && value != null) OnYeniKayit(seciliSatir);

            if (fieldName == "GirişKafileNo" && seciliSatir.MakinaKod == 351)
            {
                seciliSatir.ÇıkışKafileNo = seciliSatir.GirişKafileNo;
            }

            if (fieldName == "GirişKafileNo" && value != null)
            {
                var kafileNo = value.ToString().Length == 0 ? "" : value?.ToString();

                KafileNodanDegerleriAta(kafileNo, seciliSatir, ref value);
            }

            if (fieldName == "DuruşKodu")
            {
                if (seciliSatir.DuruşKodu != null)
                    seciliSatir.MakinaDuruşAd = MakinaDuruslari.FirstOrDefault(c => c.DurusKod == seciliSatir.DuruşKodu)?.DurusAd;
            }

            if (fieldName == "GirişBobinNo" && seciliSatir.MakinaKod == 351)
            {
                seciliSatir.ÇıkışBobinNo = seciliSatir.GirişBobinNo;
            }

            if (fieldName == "BaşlangıçSaati" && satir_index == 0)
            {
                if (satir_index == 0 && seciliSatir.BaşlangıçSaati != null)
                {
                    seciliSatir.BaşlangıçSaati = new DateTime(
                    SeciliTarih.Year, seciliTarih.Month, seciliTarih.Day,
                    seciliSatir.BaşlangıçSaati.Value.Hour, seciliSatir.BaşlangıçSaati.Value.Minute, 0);


                    seciliSatir.BitişSaati = seciliSatir.BaşlangıçSaati;
                }
              
            }

            if (fieldName == "BitişSaati" && seciliSatir.BitişSaati != null)
            {
                seciliSatir.BitişSaati = new DateTime(SeciliTarih.Year, seciliTarih.Month, seciliTarih.Day,
                                                 seciliSatir.BitişSaati.Value.Hour, seciliSatir.BitişSaati.Value.Minute, 0);

                if(satir_index==0)
                {
                    seciliSatir.BitişSaati = DateTime.Parse(value.ToString()) ;
                }

                if (satir_index > 0)
                {
                    var oncekiSatir = UretimTabloSatirlar[satir_index - 1];

                    seciliSatir.BaşlangıçSaati = oncekiSatir.BitişSaati;

                    if (seciliSatir.BitişSaati < oncekiSatir.BitişSaati)
                    {
                        var yeniTarih = seciliTarih.AddDays(1);
                        seciliSatir.BitişSaati = new DateTime(yeniTarih.Year, yeniTarih.Month, yeniTarih.Day,
                                              seciliSatir.BitişSaati.Value.Hour, seciliSatir.BitişSaati.Value.Minute, 0);
                    }

                    if (satir_index < uretimTabloSatirlar.Count)
                    {
                        var sonrakiSatir = UretimTabloSatirlar[satir_index + 1];

                        if (sonrakiSatir.BitişSaati != null) sonrakiSatir.BaşlangıçSaati = seciliSatir.BitişSaati;
                    }
                }
            }
        }

        private void OnIcerikTemizle(TableView w1)
        {
            var cells = w1.GetSelectedCells();

            foreach (var cell in cells)
            {
                var r = w1.Grid.GetRow(cell.RowHandle) as Uretim_UretimTablo;

                w1.Grid.SetCellValue(cell.RowHandle, cell.Column, null);

                r.OnPropertyChanged(nameof(r.BitişSaati));
                r.OnPropertyChanged(nameof(r.DuruşKodu));
            }
        }

        private void OnKeyCellValueChanged(CellValueChangedEventArgs e)
        {
            var row = e.Cell.Row;
            var value = e.Cell.Value;
            var fieldName = e.Cell.Property;

                OnCellValueChanged(fieldName, row, value);
        }

        private void OnKeyDown(KeyEventArgs e)
        {
            var w1 = (e.Source as TableView);

            if (w1 == null) return; // sağ menu açıkken key basılması halinde boş geliyor

            var g1 = w1.Grid;

            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = true;
                OnKaydet();
            }

            if (e.Key == Key.Left && g1.CurrentColumn.VisibleIndex == 1) e.Handled = true;
            if (e.Key == Key.Right && (g1.CurrentColumn.VisibleIndex == w1.VisibleColumns.Count - 1)) e.Handled = true;

            if (e.Key == Key.Enter)
            {
                w1.MoveNextRow();
                g1.UnselectAll();
                w1.SelectCell(w1.FocusedRowHandle, (GridColumn)g1.CurrentColumn);
            }

            if (e.Key == Key.Delete)
            {
                OnIcerikTemizle(w1);
            }
        }

        private void OnPastingFromClipboard(PastingFromClipboardEventArgs e)
        {
            e.Handled = true;

            var w1 = (e.Source as GridControl).View as TableView;
            var g1 = w1.Grid;

            var clipboardData = GetClipboardData();

            var selectedRows = w1.GetSelectedRows();
            var selectedCells = w1.GetSelectedCells();

            var first_row_handle = selectedRows.First().RowHandle;
            var first_col_index = selectedCells.First().Column.ActualVisibleIndex;

            var cColLength = clipboardData.First().Length;

            w1.FocusedRowHandle = first_row_handle;

            try
            {
                VeriYapistir(w1, g1, clipboardData, selectedRows, first_col_index, cColLength);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            w1.FocusedRowHandle--;
        }

        private void VeriYapistir(TableView w1, GridControl g1, List<string[]> clipboardData, IList<GridRowInfo> selectedRows, int first_col_index, int cColLength)
        {
            foreach (var row in selectedRows)
            {
                if (row.RowHandle >= w1.FocusedRowHandle)
                {
                    w1.FocusedRowHandle = row.RowHandle;

                    for (int i = 0; i < clipboardData.Count; i++)
                    {
                        for (int j = 0; j < cColLength; j++)
                        {
                            var column = w1.VisibleColumns[first_col_index + j];

                            if (column.FieldName != "Id" && column.FieldName != "KayitDurumu")
                            {
                                g1.SetCellValue(w1.FocusedRowHandle, column, clipboardData[i][j]);
                                OnCellValueChanged(column.FieldName, g1.GetRow(w1.FocusedRowHandle), clipboardData[i][j]);
                            }
                        }

                        w1.FocusedRowHandle++;
                    }
                }
            }
        }

        private void OnSil(TableView obj)
        {
            var x = obj.GetSelectedRows().Select(c => c.Row).ToList();
            x.ForEach(item =>
            {
                var ut = item as Uretim_UretimTablo;
                repo.Sil(ut);
                UretimTabloSatirlar.Remove(ut);

            });
        }

        private void OnYeniKayit(Uretim_UretimTablo satir)
        {
            satir.Tarih = SeciliTarih;
            satir.MakinaKod = MakinaKod;
            satir.Vardiya = Vardiya;

            var operatorIsimSatir = UretimTabloSatirlar.Where(c => String.IsNullOrEmpty(c.Operatör) == false).LastOrDefault();

            if (operatorIsimSatir != null) satir.Operatör = operatorIsimSatir.Operatör;
        }

        private void OnUsteSatirEkle(TableView w1)
        {
            var obj = w1.Grid.SelectedItem as Uretim_UretimTablo;

            var yeni = new Uretim_UretimTablo();
            yeni.Operatör = obj.Operatör;
            yeni.Vardiya = obj.Vardiya;

            yeni.MakinaKod = obj.MakinaKod;

            var aktifIndex = UretimTabloSatirlar.IndexOf(obj);

            var ustIndex = aktifIndex - 1;

            yeni.Tarih = obj.Tarih;

            if (ustIndex >= 0)
            {
                var ust = UretimTabloSatirlar[ustIndex];
                yeni.başlangıçSaati = ust.BitişSaati;
                yeni.Id = (obj.Id + ust.Id) / 2;
            }
            else
            {
                yeni.Id = obj.Id - 500;
            }

            UretimTabloSatirlar.Insert(aktifIndex, yeni);

            w1.FocusedRowHandle--;
            w1.Grid.UnselectAll();
            w1.SelectCell(w1.FocusedRowHandle, (GridColumn)w1.Grid.CurrentColumn);
        }

        public void KafileNodanDegerleriAta(string kafileNo, Uretim_UretimTablo seciliSatir, ref object value)
        {
            var bulunan = UretimTabloSatirlar
              .OrderByDescending(c => c.Id)
              .Where(c => c.Id != seciliSatir.Id)
              .Where(c => c.GirişKafileNo?.Length > 0)
              .Where(c => c.GirişKafileNo == kafileNo || kafileNo.ToUpper() == "X")
              .FirstOrDefault();

            if (bulunan == null) return;

            if (value.ToString().ToUpper() == "X") value = bulunan.GirişKafileNo;

            seciliSatir.GirişKalınlık = bulunan.GirişKalınlık;
            seciliSatir.ÇıkışKalınlık = bulunan.ÇıkışKalınlık;

            seciliSatir.GirişEni = bulunan.GirişEni;
            seciliSatir.ÇıkışEni = bulunan.ÇıkışEni;

            seciliSatir.PasNo = bulunan.PasNo;
            seciliSatir.NihaiSonPas = bulunan.NihaiSonPas;

            seciliSatir.Alaşım = bulunan.Alaşım;
            seciliSatir.Sonrakiİşlem = bulunan.Sonrakiİşlem;

        }
    }
}