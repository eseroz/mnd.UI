using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using DevExpress.Pdf;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using mnd.Logic;

namespace mnd.UI.GyModules.MesajModule
{
    public class MesajlasmaViewModel : MyMessangerBindableBase
    {
        private ObservableCollection<Mesaj> mesajlar;
        private Mesaj _aktifMesaj;
        private Mesaj _seciliMesaj;

        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));

        public ObservableCollection<Mesaj> Mesajlar { get => mesajlar; set => SetProperty(ref mesajlar, value); }
        public Mesaj SeciliMesaj { get => _seciliMesaj; set => SetProperty(ref _seciliMesaj, value); }

        public Guid AktifRowGuid { get; set; }

        public Mesaj AktifMesaj { get => _aktifMesaj; set => SetProperty(ref _aktifMesaj, value); }

        public DelegateCommand DosyaEkleCommand => new DelegateCommand(DosyaEkle);
        public DelegateCommand<Mesaj> DosyaAcCommand => new DelegateCommand<Mesaj>(DosyaAc);
        public DelegateCommand SendMessageCommand => new DelegateCommand(MesajEkle);

        public DelegateCommand WordExportCommand => new DelegateCommand(OnWordExport);

        public List<string> ImageExtensions => new List<string> { ".jpeg", ".jpg", ".png", ".bmp" };

        public List<string> WordExtensions => new List<string> { ".doc", ".docx", "rtf" };

        public bool MesajSec { get => _mesajSec; set => SetProperty(ref _mesajSec, value); }
        public bool MesajIsimBilgiSec { get => _mesajIsimBilgiSec; set => SetProperty(ref _mesajIsimBilgiSec, value); }

        private void OnWordExport()
        {
            RichEditDocumentServer server = new RichEditDocumentServer();
            DevExpress.XtraRichEdit.API.Native.Document doc = server.Document;

            foreach (var item in Mesajlar.Where(c => c.SeciliMi == true))
            {
                doc.AppendText(item.MesajIcerik);

                if (item.DokumanAdi != null)
                {
                    var para = doc.Paragraphs.Append();

                    var inx = item.DokumanIcerik.IndexOf(';');
                    var fileExtension = item.DokumanIcerik.Substring(0, inx);
                    var fileData = item.DokumanIcerik.Substring(inx + 1, item.DokumanIcerik.Length - inx - 1);

                    byte[] bytedizi = Convert.FromBase64String(fileData);

                    var stream = new MemoryStream(bytedizi);

                    if (ImageExtensions.Contains(fileExtension.ToLower()))
                    {
                        var img = doc.Images.Insert(para.Range.Start, DocumentImageSource.FromStream(stream));
                    }

                    if (WordExtensions.Contains(fileExtension.ToLower()))
                    {
                        var richDocServer = new RichEditDocumentServer();
                        richDocServer.LoadDocument(stream);

                        server.Document.AppendDocumentContent(richDocServer.Document.Range);
                    }

                    if (fileExtension.ToLower() == ".pdf")
                    {
                        int largestEdgeLength = 1000;

                        using (PdfDocumentProcessor processor = new PdfDocumentProcessor())
                        {
                            processor.LoadDocument(stream);

                            for (int i = 1; i <= processor.Document.Pages.Count; i++)
                            {
                                Bitmap image = processor.CreateBitmap(i, largestEdgeLength);

                                MemoryStream ms2 = new MemoryStream();

                                image.Save(ms2, ImageFormat.Bmp);

                                var paraResim = doc.Paragraphs.Append();
                                doc.Images.Insert(paraResim.Range.Start, DocumentImageSource.FromStream(ms2));
                            }
                        }
                    }
                }

                if (MesajIsimBilgiSec)
                {
                    var para5 = doc.Paragraphs.Append();
                    doc.AppendSingleLineText(item.Gonderen + "-" + item.MesajTarihi + Environment.NewLine);

                    doc.Paragraphs.Append();
                }
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Export_" + DateTime.Now.ToString("ddMMyyyy_HHmm");

            saveFileDialog.Filter = "word dosyası (*.docx)|*.docx|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = "docx";
            saveFileDialog.FilterIndex = 1;

            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (saveFileDialog.ShowDialog() == true)
            {
                server.SaveDocument(saveFileDialog.FileName, DocumentFormat.OpenXml);
                Process.Start(saveFileDialog.FileName);
            }
        }

        public string KullaniciAdSoyad { get; set; }
        public string AlanAdSoyad { get; set; }

        public MesajlasmaViewModel()
        {
            //parameterless gerekli olan yerler için
        }

        private Object Row;
        private bool _mesajSec;
        private bool _mesajIsimBilgiSec;

        public MesajlasmaViewModel(object row, string kullaniciAd)
        {
            Row = row;
            var rowGuid = (Guid)Row.GetType().GetProperty("RowGuid").GetValue(row);

            Row.GetType().GetProperty("OkunmamisMesajSayisi").SetValue(Row, 0);

            AktifRowGuid = rowGuid;
            AktifMesaj = new Mesaj();
            Mesajlar = new ObservableCollection<Mesaj>();

            KullaniciAdSoyad = kullaniciAd;

            MesajlariYukle();
        }

        private void MesajlariYukle()
        {
            GyMesajRepository mesajRepo = new GyMesajRepository();

            mesajlar = mesajRepo.MesajlariGetirFromEntityRef(AktifRowGuid);

            foreach (var mesaj in mesajlar)
            {
                mesaj.DokumanIcerik = mesajRepo.DokumanIcerikGetir(mesaj.Id);

                mesaj.GidenMi = mesaj.Gonderen == KullaniciAdSoyad ? true : false;

                if (mesaj.Okuyanlar == null) mesaj.Okuyanlar = KullaniciAdSoyad;

                if (!mesaj.Okuyanlar.Contains(KullaniciAdSoyad)) mesaj.Okuyanlar += ";" + KullaniciAdSoyad;
            }

            mesajRepo.Kaydet();

            if (mesajlar.Count > 0) SeciliMesaj = mesajlar.Last();
            OnPropertyChanged(nameof(Mesajlar));
        }

        public void DosyaEkle()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                var fileExtension = Path.GetExtension(filename);

                if (fileExtension == ".exe" || fileExtension == ".vbs")
                {
                    MessageBox.Show(filename + " dosyasını ekleyemezsiniz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                byte[] by = File.ReadAllBytes(filename);
                AktifMesaj.DokumanIcerik = fileExtension + ";" + Convert.ToBase64String(by);
                AktifMesaj.DokumanAdi = dlg.SafeFileName;

                MesajEkle();
            }
        }

        public void DosyaEkleFromPaste(string filename)
        {
            var fileExtension = Path.GetExtension(filename);

            if (fileExtension == ".exe" || fileExtension == ".vbs")
            {
                MessageBox.Show(filename + " dosyasını ekleyemezsiniz", "Pandap", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] by = File.ReadAllBytes(filename);

            AktifMesaj.DokumanIcerik = fileExtension + ";" + Convert.ToBase64String(by);
            AktifMesaj.DokumanAdi = Path.GetFileName(filename);

            MesajEkle();
        }

        public void ResimEkleFromPaste(byte[] by)
        {
            var fileExtension = ".bmp";

            AktifMesaj.DokumanIcerik = fileExtension + ";" + Convert.ToBase64String(by);
            AktifMesaj.DokumanAdi = Guid.NewGuid().ToString();

            MesajEkle();
        }

        private void DosyaAc(Mesaj mesaj)
        {
            GyMesajRepository mesajRepo = new GyMesajRepository();
            mesaj.DokumanIcerik = mesajRepo.DokumanIcerikGetir(mesaj.Id);

            if (mesaj.DokumanIcerik == null)
            {
                MessageBox.Show("Dosya içeriği eksik", "Mesaj", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var inx = mesaj.DokumanIcerik.IndexOf(';');
            var fileExtension = mesaj.DokumanIcerik.Substring(0, inx);
            var fileData = mesaj.DokumanIcerik.Substring(inx + 1, mesaj.DokumanIcerik.Length - inx - 1);

            byte[] bytedizi = Convert.FromBase64String(fileData);

            var b = new BlobInfo
            {
                Buffer = bytedizi,
                FileName = Guid.NewGuid().ToString() + fileExtension
            };

            Utils.BlobdanDosyaAç(b);
        }

        private void MesajEkle()
        {
            GyMesajRepository mesajRepo = new GyMesajRepository();
            var dokumentIcerik = AktifMesaj.DokumanIcerik;

            var yeniMesaj = new Mesaj
            {
                Gonderen = KullaniciAdSoyad,
                Okuyanlar = KullaniciAdSoyad,
                MesajIcerik = AktifMesaj.MesajIcerik,
                RefEntityGuid = AktifRowGuid,
                MesajTarihi = DateTime.Now,
                GidenMi = true,
                DokumanAdi = AktifMesaj.DokumanAdi,
                DokumanIcerik = AktifMesaj.DokumanIcerik,
            };

            Mesajlar.Add(yeniMesaj);

            mesajRepo.MesajEkle(yeniMesaj);

            var mesajSayisi = (int)Row.GetType().GetProperty("MesajSayisi").GetValue(Row);
            mesajSayisi = mesajSayisi + 1;

            Row.GetType().GetProperty("MesajSayisi").SetValue(Row, mesajSayisi);

            AktifMesaj.DokumanIcerik = null;
            AktifMesaj.MesajIcerik = string.Empty;

            AktifMesaj.DokumanAdi = null;

            Messenger.Default.Send(new KayitSatirMesajEvent(yeniMesaj.RefEntityGuid));
        }

        public void Paste()
        {
            if (Clipboard.ContainsImage())
            {
                BitmapSource bmpSrc = Clipboard.GetImage();

                var by = BitmapSourceToByte(bmpSrc);

                ResimEkleFromPaste(by);
            }

            if (Clipboard.ContainsFileDropList())
            {
                var files = Clipboard.GetFileDropList();

                foreach (var item in files)
                {
                    DosyaEkleFromPaste(item);
                }

                var mesaj = files.Count.ToString() + " adet dosya aktarıldı";
                BoundMessageQueue.Enqueue(mesaj, true);
            }
        }

        public void DropFiles(string[] files)
        {
            foreach (var item in files)
            {
                DosyaEkleFromPaste(item);
            }

            var mesaj = files.Length.ToString() + " adet dosya aktarıldı";
            BoundMessageQueue.Enqueue(mesaj, true);
        }

        public byte[] BitmapSourceToByte(BitmapSource source)
        {
            var encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
            var frame = System.Windows.Media.Imaging.BitmapFrame.Create(source);
            encoder.Frames.Add(frame);
            var stream = new MemoryStream();

            encoder.Save(stream);
            return stream.ToArray();
        }
    }
}