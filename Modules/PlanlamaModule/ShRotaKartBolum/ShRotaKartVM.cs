using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.BC_Dokum.Data;
using mnd.Logic.BC_Dokum.Model;
using mnd.Logic.BC_Uretim;
using mnd.Logic.BC_Uretim.SH_RotaModel;
using mnd.Logic.Persistence;
using mnd.UI.AppModules.RaporDesignerModule;
using mnd.UI.Helper;
using mnd.UI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace mnd.UI.Modules.PlanlamaModule.ShRotaKartBolum
{
    public class ShRotaKartVM : MyDxViewModelBase
    {
        ShRecete_TrackingRepository repo_recete = new ShRecete_TrackingRepository();
        ShRotaKartTrackingRepository repo_shRotaKart = new ShRotaKartTrackingRepository();
        private ShRotaKart shRotaKartModel;

        public ShRotaKart ShRotaKartModel { get => shRotaKartModel; set => SetProperty(ref shRotaKartModel,value); }

        public ShRecete ShRecete { get; set; }

        public DelegateCommand<object> YazdirCommand => new DelegateCommand<object>(Yazdir, true);

        public DelegateCommand<object> GuncelleCommand => new DelegateCommand<object>(OnGuncelle);

        private void OnGuncelle(object obj)
        {
            try
            {
              


                repo_shRotaKart.RotaKartEkle(ShRotaKartModel);

                repo_shRotaKart.Kaydet();

                DokumRepository repo = new DokumRepository();
                var dokumBobinNumaralari = ShRotaKartModel.DokumBobinler.Select(c => c.BobinNo).ToList();
                repo.DokumKafileShKartNoGuncelle(dokumBobinNumaralari, ShRotaKartModel.KartNo);



                KayitModu = KayitModu.Default;

                MessageBox.Show("Rota kartı kaydedildi", "Rota Kartı", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata oluştu." + ex.Message, "Hata", MessageBoxButton.OK);
            }

         
        }

        public KayitModu KayitModu { get; set; }

        private void Yazdir(object obj)
        {
            if (KayitModu == KayitModu.Add)
            {
                MessageBox.Show("Önce kartı kaydediniz", "Pandap", MessageBoxButton.OK);
                return;
            }

            UnitOfWork uow = new UnitOfWork();
            var raporTanim = uow.RaporTanimRepo.RaporGetirFromId(62);

            var dsObject = ShRotaKartModel;

            PandapRaporHelper.ShowReport(raporTanim, dsObject, raporTanim.Width, raporTanim.Height, raporTanim.ZoomFaktor);


        }

        public ShRotaKartVM(string kartNo,KayitModu kayitModu, List<DokumBobin> seciliBobinListe)
        {
            KayitModu = kayitModu;

            if(kartNo.Length>0)
            {
                ShRotaKartModel = repo_shRotaKart.RotaKartiGetir(kartNo);
                return;
            }


            if (KayitModu==KayitModu.Add)
            {
                var shKartNo=repo_shRotaKart.YeniShNoGetir();

                ShRotaKartModel = new ShRotaKart();
                ShRotaKartModel.KartNo = shKartNo;
                ShRotaKartModel.AlasimKod = seciliBobinListe.First().AlasimTipKod;
                ShRotaKartModel.En = seciliBobinListe.First().ReelEn;
                ShRotaKartModel.DokumBobinAdedi = seciliBobinListe.Count;

                ShRotaKartModel.Kalinlik =null;
                ShRotaKartModel.Kondisyon = "";
                ShRotaKartModel.Olusturan = AppPandap.AktifKullanici.KullaniciId;

                ShRotaKartModel.Tarih = DateTime.Now;

                ShRotaKartModel.PropertyChanged += ShRotaKartModel_PropertyChanged;

                int i = 0;
                foreach (var bobin in seciliBobinListe)
                {
                    var dokumBobin = new ShRotaKartDokumBobin();
                    dokumBobin.ShRotaKartKartNo = ShRotaKartModel.KartNo;
                    dokumBobin.SiraNo = ++i;
                    dokumBobin.BobinNo = bobin.PlanBobinNo.GetValueOrDefault();
                    dokumBobin.DokumEni = bobin.ReelEn;
                    dokumBobin.DokumMiktari = bobin.ReelMiktar;
                    dokumBobin.Aciklama = bobin.Aciklama;

                    ShRotaKartModel.DokumBobinler.Add(dokumBobin);
                }

            }
        }

        private void ShRotaKartModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (KayitModu != KayitModu.Add) return;

            if (e.PropertyName=="Kondisyon" || e.PropertyName=="Kalinlik")
            {
                var model = ShRotaKartModel;

                var receteKod = $"{model.AlasimKod}-{model.Kondisyon}-{model.Kalinlik.GetValueOrDefault()}";

                ShRecete=repo_recete.UygunReceteGetir(receteKod);

                ShRotaKartModel.Fazlar.Clear();

                if (ShRecete == null) return;

                IList<PropertyInfo> props = new List<PropertyInfo>(ShRecete.GetType().GetProperties().Where(c => c.Name.Contains("Faz")));

                ShRotaKartModel.Fazlar.Clear();

                for (int i = 1; i <= 7; i++)
                {
                    var propListe = props.Where(c => c.Name.StartsWith("Faz" + i)).ToList();

                    var faz = new ShRotaKartFaz();
                    faz.SiraNo = i;
                    faz.MakinaIslem = propListe[0].GetValue(ShRecete, null).ToString();

                    var param1 = propListe[1].GetValue(ShRecete, null);
                    var param2 = propListe[2].GetValue(ShRecete, null);

                    faz.ProsesMetin = param1.ToString() + " - " + param2.ToString();

                    faz.ProsesMaxStr = param1.ToString();
                    faz.ProsesMinStr = param2.ToString();

                    decimal param1_decimal;
                    decimal param2_decimal;

                    var p1 = decimal.TryParse(param1.ToString(), out param1_decimal);
                    var p2 = decimal.TryParse(param2.ToString(), out param2_decimal);

                    if (p1 == true && p2 == true)
                    {
                        faz.EzmeYuzde = 1- Math.Round(param2_decimal / param1_decimal, 2);
                    }

                    if(faz.ProsesMetin.Trim()!="-") ShRotaKartModel.Fazlar.Add(faz);
                }

            }
        }
    }
}