using DevExpress.DataAccess.ObjectBinding;
using DevExpress.Mvvm;
using DevExpress.XtraReports.UI;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Persistence;
using Pandap.UI.AppModule._RaporDesigner;
using Pandap.UI.Helper;
using Pandap.UI.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Pandap.UI.AppModule._Planlama
{
    public class UretimEmriViewModel : MyDxViewModelBase
    {
        public const string BURKAP = "BURKAP";


        public UnitOfWork uow { get; set; }
        UretimEmriDTO uretimEmriDTO;
        public UretimEmriDTO UretimEmriDTO
        {
            get => uretimEmriDTO;
            set => SetProperty(ref uretimEmriDTO, value);
        }

        public DelegateCommand<object> UretimeGonderCommand => new DelegateCommand<object>(UretimeGonder, canUretimeGonder);
        public DelegateCommand<object> GuncelleCommand => new DelegateCommand<object>(Guncelle, true);
        public DelegateCommand<object> YazdirCommand => new DelegateCommand<object>(Yazdir, true);

        public DelegateCommand<object> RotaKartiDesignCommand => new DelegateCommand<object>(RotaKartiDesign, true);

        private void RotaKartiDesign(object obj)
        {
            PandapRaporDesigner rpRotaKarti = new PandapRaporDesigner();
            rpRotaKarti.designer.DocumentOpened += (sender, e) =>
            {
                var v = e.Document;

                var ds = v.ReportModel.DataSource as ObjectDataSource;

                if (ds != null)
                    ds.DataSource = UretimEmriDTO;
                else
                    MessageBox.Show("Data Source Tanımı yapınız...");
            };


            rpRotaKarti.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            rpRotaKarti.ShowDialog();


        }

        private void Yazdir(object obj)
        {

            var reportPath = @"Content\RotaKarti.repx";

            var fs = new FileStream(reportPath, FileMode.Open);
            MemoryStream ms = new MemoryStream();
            fs.CopyTo(ms);

            XtraReport xr = XtraReport.FromStream(ms, true);

            var ds = xr.DataSource as ObjectDataSource;

            if (ds != null) {
                ds.DataSource = UretimEmriDTO;
            }
            else
                MessageBox.Show("Data Source Tanımlı değil");

            PandapRaporSimpleViever view = new PandapRaporSimpleViever();
            view.report_view_control.DocumentSource = xr;
            view.report_view_control.ZoomFactor = 1;
            view.Width = 990; view.Height = 780;

            view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            xr.CreateDocument();

            view.ShowDialog();
            fs.Close();
        }

      


        private  void Guncelle(object obj)
        {

            var uretimEmri=uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(UretimEmriDTO.UretimEmriKod);

            UretimEmriDTO.PlanlamaRulolari?.ToList().ForEach(c => c.UretimEmriKod = UretimEmriDTO.UretimEmriKod);

            uow.PlanlamaRepo.PlanlamaRuloEkleVeyaGuncelle(UretimEmriDTO.PlanlamaRulolari);
            uow.Commit();

            MessageBox.Show("Planlama Ruloları Güncellendi");
        }

        public UretimEmriViewModel(string kalemKod, string uretimEmriKod)
        {
            uow = new UnitOfWork();

            if(uretimEmriKod!=null)
            {
                UretimEmriDTO = UretimEmriService.Create_UretimEmriDTO_FromUretimEmriKod(uretimEmriKod);
            }
            else
            {
                UretimEmriDTO = UretimEmriService.Create_UretimEmriDTO_FromKalem(kalemKod);
                UretimEmriDTO.UretimEmriKod = uow.PlanlamaRepo.YeniUretimEmriKodGetir_SiparisKalemden(kalemKod);

            }

            var uygunReceteSatir = UygunReceteSatirGetir(UretimEmriDTO.UrunKod, UretimEmriDTO.KullanimAlani, UretimEmriDTO.HedefKalinlik);

            if (uygunReceteSatir == null)
            {
                MessageBox.Show(UretimEmriDTO.UrunKod + "  "
                    + "Kalınlık: " + UretimEmriDTO.HedefKalinlik.ToString() + " " +
                    UretimEmriDTO.KullanimAlani,
                    "Uygun Rota Kartı Bulunamadı", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            if (uretimEmriKod == null)
                UretimEmriDTO.PlanlamaRulolari = new ObservableCollection<UretimEmriRulo>();
           

            UretimEmriDTO.MakineAsamalari1 = Makina1_AsamalariGetir(uygunReceteSatir);
          

            UretimEmriDTO.MakineAsamalari2 = new ObservableCollection<UretimEmriMakineAsama>();

            UretimEmriDTO.SonAsama1= uygunReceteSatir.M_6;// DL;SP
            UretimEmriDTO.SonAsama2 = uygunReceteSatir.M_7 != null ? uygunReceteSatir.M_7 : string.Empty; // FTF    FTF varmı şeklinde olabilir kontrol et

            var ciftHaddeleme = UretimEmriDTO.UrunKod.Contains("P/M") ? UretimEmriDTO.MakineAsamalari1.Last() : null;

            if (ciftHaddeleme != null)
            {
                UretimEmriDTO.MakineAsamalari1.Remove(ciftHaddeleme);
                UretimEmriDTO.MakineAsamalari2.Add(ciftHaddeleme);
            };

            UretimEmriDTO.DilmeVeSeperatorAciklama = "MİKTAR TAMAMLANINCA ARKA SAYFADAKİ KOMBİNE GEÇİNİZ..";
            UretimEmriDTO.Kombin = "Bu alan girilecek";
        }

        private ObservableCollection<UretimEmriMakineAsama> Makina1_AsamalariGetir(Recete uygunReceteSatir)
        {
            var makinaAsamalar1 = new ObservableCollection<UretimEmriMakineAsama>();

            if (uygunReceteSatir.M_1 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama
                {
                    Makine = uygunReceteSatir.M_1,
                    ProsesMax = uygunReceteSatir.GK_1,
                    ProsesMin = uygunReceteSatir.CK_1
                });
            }

            if (uygunReceteSatir.M_2 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama
                {
                    Makine = uygunReceteSatir.M_2,
                    ProsesMax = uygunReceteSatir.GK_2,
                    ProsesMin = uygunReceteSatir.CK_2
                });
            }

            if (uygunReceteSatir.M_3 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama
                {
                    Makine = uygunReceteSatir.M_3,
                    ProsesMax = uygunReceteSatir.GK_3,
                    ProsesMin = uygunReceteSatir.CK_3
                });
            }

            if (uygunReceteSatir.M_4 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama
                {
                    Makine = uygunReceteSatir.M_4,
                    ProsesMax = uygunReceteSatir.GK_4,
                    ProsesMin = uygunReceteSatir.CK_4
                });
            }

            if (uygunReceteSatir.M_5 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama
                {
                    Makine = uygunReceteSatir.M_5,
                    ProsesMax = uygunReceteSatir.GK_5,
                    ProsesMin = uygunReceteSatir.CK_5
                });
            }

            return makinaAsamalar1;
        }


        public Recete UygunReceteSatirGetir(string urunKod,string kullanimAlani,decimal hedefKalinlik)
        {
            var receteler = uow.ReceteRepo.ReceteleriGetir();

            if (kullanimAlani == BURKAP)
            {
                return receteler.Where(c => c.RotaUrunKodlari.Contains(UretimEmriDTO.UrunKod)).
                        FirstOrDefault(c => c.HedefKalinlik == UretimEmriDTO.HedefKalinlik && c.KullanimAlani == BURKAP);
            }

            if (UretimEmriDTO.KullanimAlani != BURKAP)
            {
                return receteler.Where(c => c.RotaUrunKodlari.Contains(UretimEmriDTO.UrunKod)).
                        FirstOrDefault(c => c.HedefKalinlik == UretimEmriDTO.HedefKalinlik && c.KullanimAlani == "Diger");
            }
            return null;

        }


        private bool canUretimeGonder(object arg)
        {
            return true;
        }

        private  void UretimeGonder(object obj)
        {
            var o = UretimEmriDTO;

            if(o.PlanlananMiktar_kg==0)
            {
                MessageBox.Show("Planlanan Miktarı Giriniz");
                return;
            }

            if (String.IsNullOrEmpty(o.KartNo))
            {
                MessageBox.Show("Kart Numarasını Giriniz");
                return;
            }



            UretimEmri uretimEmri = new UretimEmri();

           

            uretimEmri.KartNo = o.KartNo;
            uretimEmri.EklenmeTarih = DateTime.Now;

            uretimEmri.SiparisKalemKod = o.SiparisKalemKod;
            uretimEmri.UretimEmriKod = o.UretimEmriKod;

            uretimEmri.Uretim_PlanlananMiktar = o.PlanlananMiktar_kg;
            uretimEmri.PlanlamaRulolari = o.PlanlamaRulolari;


            uretimEmri.DilmeSeperatorNot = o.DilmeSeperatorNot;
            uretimEmri.PlanlamaNot = o.PlanlamaNot;
            uretimEmri.KombinlerEnToplam = o.KombinlerEnToplam;
            uretimEmri.Kombinler = o.Kombinler;

            uretimEmri.EklenmeTarih = DateTime.Now;


            uow.PlanlamaRepo.Add(uretimEmri);

            var kalem = uow.SiparisKalemRepo.SiparisKalemiGetir(o.SiparisKalemKod);

            kalem.PLAN_PlanlanacakKalanMiktarToplam = kalem.PLAN_PlanlanacakKalanMiktarToplam ?? kalem.Miktar_kg.Value;
            kalem.PLAN_PlanlananMiktarToplam += o.PlanlananMiktar_kg;

            if (kalem.PLAN_UretimdekiMiktarToplam == 0) kalem.PLAN_UretimdekiMiktarToplam = uretimEmri.Uretim_PlanlananMiktar;

            kalem.PLAN_PlanlanacakKalanMiktarToplam = kalem.Miktar_kg - kalem.PLAN_PlanlananMiktarToplam;

            uow.Commit();

            MessageBox.Show("Planlama Gerçekleştirildi");
        }
    }
}