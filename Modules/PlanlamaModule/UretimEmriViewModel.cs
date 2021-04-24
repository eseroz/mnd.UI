using DevExpress.Mvvm;
using mnd.Logic.BC_Uretim;
using mnd.Logic.Model.Uretim;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace mnd.UI.Modules.PlanlamaModule
{
    public class UretimEmriViewModel : MyDxViewModelBase
    {
        public const string BURKAP = "BURKAP";

        public UnitOfWork uow { get; set; }
        private UretimEmriDTO uretimEmriDTO;

        public UretimEmriDTO UretimEmriDTO
        {
            get => uretimEmriDTO;
            set => SetProperty(ref uretimEmriDTO, value);
        }

        public bool YeniMi { get; set; }

        public DelegateCommand<object> UretimeGonderCommand => new DelegateCommand<object>(UretimeGonder, canUretimeGonder);
        public DelegateCommand<object> GuncelleCommand => new DelegateCommand<object>(Guncelle, CanGuncelle);

        private bool CanGuncelle(object arg)
        {
            return !YeniMi;
        }

        public DelegateCommand<object> YazdirCommand => new DelegateCommand<object>(Yazdir, true);

        private void Yazdir(object obj)
        {
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(9);

            var dsObject = UretimEmriDTO;

            var kombinCiktisiMetniOlsunMu = MessageBox.Show(AppPandap.AktifKullanici.AdSoyad + " Arka sayfada kombin var mı?", "Pandap", MessageBoxButton.OKCancel);

           if(MessageBoxResult.OK==kombinCiktisiMetniOlsunMu)
           {
                dsObject.DilmeSeperatorNot += Environment.NewLine +  "MİKTAR TAMAMLANINCA ARKA SAYFADAKİ KOMBİNE GEÇİNİZ";
           }
       

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);
        }

        private void Guncelle(object obj)
        {

            if (UretimEmriDTO.KombinMiktari_kg == 0 || UretimEmriDTO.MaxKombinEni.GetValueOrDefault() == 0 || UretimEmriDTO.KombinlerEnToplam.GetValueOrDefault() == 0)
            {
                MessageBox.Show("Kombin Miktarı / Maksimum Kombin Eni / Kombinler En toplam verilerinden en az biri eksik", "Pandap",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return;

            }

        

            uow.PlanlamaRepo.UretimEmriPlanlamaMiktarGuncelle(UretimEmriDTO.UretimEmriKod, UretimEmriDTO.PlanlananMiktar_kg);

            var uretimEmri = uow.PlanlamaRepo.UretimEmriGetirFromUretimKod(UretimEmriDTO.UretimEmriKod);


            uretimEmri.KombinMiktari_kg = UretimEmriDTO.KombinMiktari_kg;
            uretimEmri.MaxKombinEni = UretimEmriDTO.MaxKombinEni;
            uretimEmri.PlanlamaNot = UretimEmriDTO.PlanlamaNot;
            uretimEmri.OzelTalimat = UretimEmriDTO.OzelTalimat;
            uretimEmri.Kombinler = UretimEmriDTO.Kombinler;
            uretimEmri.KombinlerEnToplam = UretimEmriDTO.KombinlerEnToplam;

            UretimEmriDTO.PlanlamaRulolari?.ToList().ForEach(c => c.UretimEmriKod = UretimEmriDTO.UretimEmriKod);
            UretimEmriDTO.MakineAsamalari1?.ToList().ForEach(c => c.UretimEmriKod = UretimEmriDTO.UretimEmriKod);
            UretimEmriDTO.MakineAsamalari2?.ToList().ForEach(c => c.UretimEmriKod = UretimEmriDTO.UretimEmriKod);

            uow.PlanlamaRepo.PlanlamaRuloEkleVeyaGuncelle(UretimEmriDTO.PlanlamaRulolari);
            uow.PlanlamaRepo.MakinaAsama1_EkleVeyaGuncelle(UretimEmriDTO.MakineAsamalari1);
            uow.PlanlamaRepo.MakinaAsama2_EkleVeyaGuncelle(UretimEmriDTO.MakineAsamalari2);

            uow.Commit();

            MessageBox.Show("Üretim Emri Güncellendi");
        }


        public UretimEmriViewModel(string kalemKod, string uretimEmriKod, PlanlamaTakipDto planTakipDto)
        {
            uow = new UnitOfWork();

            YeniMi = String.IsNullOrEmpty(uretimEmriKod);

            if (YeniMi)
            {
                UretimEmriDTO = UretimEmriService.Create_UretimEmriDTO_FromKalem(kalemKod);
                UretimEmriDTO.UretimEmriKod = uow.PlanlamaRepo.YeniUretimEmriKodGetir_SiparisKalemden(kalemKod);
                UretimEmriDTO.EklenmeTarih = DateTime.Now;

                UretimEmriDTO.KartNo = planTakipDto.KartNo;
                UretimEmriDTO.PlanlamaBakiye = planTakipDto.Bakiye;

                if (!(UretimEmriDTO.KullanimAlani == "BURKAP" || UretimEmriDTO.KullanimAlani == "SMOOTHWALL"))
                {
                    UretimEmriDTO.KaydiriciOraniMinMaxStr = "";
                }

                if (UretimEmriDTO.AmbalajTipi == "KAFES") UretimEmriDTO.AmbalajTipi = planTakipDto.AmbalajKafesOlcu;

                RecetedenFormVerileriYukle();


            }
            else
            {
                UretimEmriDTO = UretimEmriService.Create_UretimEmriDTO_FromUretimEmriKod(uretimEmriKod);
                UretimEmriDTO.PlanlamaBakiye = planTakipDto.Bakiye;

                if (!(UretimEmriDTO.KullanimAlani == "BURKAP" || UretimEmriDTO.KullanimAlani == "SMOOTHWALL"))
                {
                    UretimEmriDTO.KaydiriciOraniMinMaxStr = "";
                }


                if (UretimEmriDTO.MakineAsamalari1.Count==0)
                {
                    RecetedenFormVerileriYukle();
                }
            }


     
        }

        public void RecetedenFormVerileriYukle()
        {
            var uygunReceteSatir = UygunReceteSatirGetir(UretimEmriDTO.UrunKod, UretimEmriDTO.KullanimAlani, UretimEmriDTO.HedefKalinlik);

            if (uygunReceteSatir == null)
            {
                MessageBox.Show(UretimEmriDTO.UrunKod + "  "
                    + "Kalınlık: " + UretimEmriDTO.HedefKalinlik.ToString() + " " +
                    UretimEmriDTO.KullanimAlani,
                    "Uygun Rota Kartı Bulunamadı", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var recete_makinaAsamalar= Makina1_AsamalariGetir(uygunReceteSatir);

            if(recete_makinaAsamalar!=null)

            foreach (var item in recete_makinaAsamalar)
            {
                item.UretimEmriKod = UretimEmriDTO.UretimEmriKod;
                UretimEmriDTO.MakineAsamalari1.Add(item);
            }

            // son kayıt çift haddleme proseduru
            if (UretimEmriDTO.UrunKod.Contains("P/M"))
            {
                var silinecekKayitIndex = recete_makinaAsamalar.Count - 1;
                UretimEmriDTO.MakineAsamalari1.RemoveAt(silinecekKayitIndex);

                var asama2 = new UretimEmriMakineAsama2
                {
                    Makine = recete_makinaAsamalar.Last().Makine,
                    ProsesMin = recete_makinaAsamalar.Last().ProsesMin,
                    ProsesMax = recete_makinaAsamalar.Last().ProsesMax,
                   
                };
                UretimEmriDTO.MakineAsamalari2.Add(asama2);
            };



            UretimEmriDTO.SonAsama1 = uygunReceteSatir.M_6;// DL;SP
            UretimEmriDTO.SonAsama2 = uygunReceteSatir.M_7 != null ? uygunReceteSatir.M_7 : string.Empty; // FTF    FTF varmı şeklinde olabilir kontrol et
            UretimEmriDTO.DilmeVeSeperatorAciklama = "MİKTAR TAMAMLANINCA ARKA SAYFADAKİ KOMBİNE GEÇİNİZ..";
            //UretimEmriDTO.Kombin = "Bu alan girilecek";
        }


        private ObservableCollection<UretimEmriMakineAsama1> Makina1_AsamalariGetir(Recete uygunReceteSatir)
        {
            var makinaAsamalar1 = new ObservableCollection<UretimEmriMakineAsama1>();

            if (uygunReceteSatir.M_1 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama1
                {
                    Makine = uygunReceteSatir.M_1,
                    ProsesMax = uygunReceteSatir.GK_1,
                    ProsesMin = uygunReceteSatir.CK_1
                });
            }

            if (uygunReceteSatir.M_2 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama1
                {
                    Makine = uygunReceteSatir.M_2,
                    ProsesMax = uygunReceteSatir.GK_2,
                    ProsesMin = uygunReceteSatir.CK_2
                });
            }

            if (uygunReceteSatir.M_3 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama1
                {
                    Makine = uygunReceteSatir.M_3,
                    ProsesMax = uygunReceteSatir.GK_3,
                    ProsesMin = uygunReceteSatir.CK_3
                });
            }

            if (uygunReceteSatir.M_4 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama1
                {
                    Makine = uygunReceteSatir.M_4,
                    ProsesMax = uygunReceteSatir.GK_4,
                    ProsesMin = uygunReceteSatir.CK_4
                });
            }

            if (uygunReceteSatir.M_5 != null)
            {
                makinaAsamalar1.Add(new UretimEmriMakineAsama1
                {
                    Makine = uygunReceteSatir.M_5,
                    ProsesMax = uygunReceteSatir.GK_5,
                    ProsesMin = uygunReceteSatir.CK_5
                });
            }

            return makinaAsamalar1;
        }

        public Recete UygunReceteSatirGetir(string urunKod, string kullanimAlani, decimal hedefKalinlik)
        {
            var receteler = uow.ReceteRepo.ReceteleriGetir();

            if (kullanimAlani == BURKAP)
            {
                return receteler.Where(c => c.RotaUrunKodlari.Contains(UretimEmriDTO.UrunKod)).
                        FirstOrDefault(c => c.HedefKalinlik == UretimEmriDTO.HedefKalinlik && c.KullanimAlani == BURKAP);
            }

            if (UretimEmriDTO.KullanimAlani != BURKAP)
            {
                var liste= receteler.Where(c => c.RotaUrunKodlari.Contains(UretimEmriDTO.UrunKod)).
                        FirstOrDefault(c => c.HedefKalinlik == UretimEmriDTO.HedefKalinlik && c.KullanimAlani == "Diger");

                return liste;
            }
            return null;
        }

        private bool canUretimeGonder(object arg)
        {
            return YeniMi;
        }

        private void UretimeGonder(object obj)
        {
            var uretimEmriDTO = UretimEmriDTO;

            if (uretimEmriDTO.PlanlamaRulolari.Count == 0 && uretimEmriDTO.KartNo.Contains("/01"))
            {
                MessageBox.Show("İlk kart olduğu için planlama ruloları girilmelidir.", "Üretime Gönder",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            if (uretimEmriDTO.KartNo.Contains("/01")==true && 
                (UretimEmriDTO.KombinMiktari_kg==0 || uretimEmriDTO.MaxKombinEni.GetValueOrDefault()==0 || UretimEmriDTO.KombinlerEnToplam.GetValueOrDefault()==0))
            {
               
                MessageBox.Show("Kombin Miktarı / Maksimum Kombin Eni / Kombinler En toplam verilerinden en az biri eksik", 
                    "Üretime Gönder", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return;

            }

            if (uretimEmriDTO.PlanlananMiktar_kg == 0)
            {
                MessageBox.Show("Planlanan Miktarı Giriniz", "Üretime Gönder");
                return;
            }

            if (String.IsNullOrEmpty(uretimEmriDTO.KartNo))
            {
                MessageBox.Show("Kart Numarasını Giriniz", "Üretime Gönder");
                return;
            }

            if (UretimEmriDTO.PlanlamaRulolari.Count == 0 && UretimEmriDTO.KartNo.Contains("/01"))
            {
                MessageBox.Show("İlk kart olduğu için planlama ruloları girilmelidir.", "Üretime Gönder",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;

            }

            UretimEmri uretimEmri = new UretimEmri();

            uretimEmri.KartNo = uretimEmriDTO.KartNo;
            uretimEmri.AnaKartNo = uretimEmriDTO.KartNo.Split('/')[0];
            uretimEmri.EklenmeTarih = DateTime.Now;

            uretimEmri.SiparisKalemKod = uretimEmriDTO.SiparisKalemKod;
            uretimEmri.UretimEmriKod = uretimEmriDTO.UretimEmriKod;

            uretimEmri.Uretim_PlanlananMiktar = uretimEmriDTO.PlanlananMiktar_kg;


            uretimEmriDTO.PlanlamaRulolari.ToList().Select(c => { uretimEmri.PlanlamaRulolari.Add(c);  return c;}).ToList();
            uretimEmriDTO.MakineAsamalari1.ToList().Select(c => { uretimEmri.MakineAsamalari1.Add(c); return c; }).ToList();
            uretimEmriDTO.MakineAsamalari2.ToList().Select(c => { uretimEmri.MakineAsamalari2.Add(c); return c; }).ToList();

         
            uretimEmri.OzelTalimat = uretimEmriDTO.OzelTalimat;
            uretimEmri.DilmeSeperatorNot = uretimEmriDTO.DilmeSeperatorNot;
            uretimEmri.PlanlamaNot = uretimEmriDTO.PlanlamaNot;

            uretimEmri.KombinlerEnToplam = uretimEmriDTO.KombinlerEnToplam.GetValueOrDefault();
            uretimEmri.Kombinler = uretimEmriDTO.Kombinler;


            uretimEmri.KombinMiktari_kg = UretimEmriDTO.KombinMiktari_kg;
            uretimEmri.MaxKombinEni = UretimEmriDTO.MaxKombinEni;
                  

            uretimEmri.EklenmeTarih = uretimEmriDTO.EklenmeTarih;
            
            
            uow.PlanlamaRepo.Add(uretimEmri);

            var kalem = uow.SiparisKalemRepo.SiparisKalemiGetir(uretimEmriDTO.SiparisKalemKod);

            //kalem.PLAN_PlanlanacakKalanMiktarToplam = kalem.PLAN_PlanlanacakKalanMiktarToplam ?? kalem.Miktar_kg.Value;
            //kalem.PLAN_PlanlananMiktarToplam += uretimEmriDTO.PlanlananMiktar_kg;

            //if (kalem.PLAN_UretimdekiMiktarToplam == 0) kalem.PLAN_UretimdekiMiktarToplam = uretimEmri.Uretim_PlanlananMiktar;

            //kalem.PLAN_PlanlanacakKalanMiktarToplam = kalem.Miktar_kg - kalem.PLAN_PlanlananMiktarToplam;

            uow.Commit();

            AnaKartRepository anakartRepo = new AnaKartRepository();

            var varMi = anakartRepo.AnakartVarMi(uretimEmri.AnaKartNo);

            if (varMi == false)
            {
                var anakart = new Anakart();
                anakart.AnakartNo = uretimEmri.AnaKartNo;
                anakartRepo.AnakartEkle(anakart);
            }


            MessageBox.Show("Planlama Gerçekleştirildi","Üretime Gönder");

        }
    }
}