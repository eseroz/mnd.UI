using DevExpress.Mvvm;
using mnd.Common.Helpers;
using mnd.Logic.AppRepositories;
using mnd.Logic.BC_App.Domain;
using mnd.UI.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace mnd.UI.AppModules.NavMenuModule
{

    public class NavMenuViewModel : MyDxViewModelBase
    {
        private List<MenuItem> appMenu;
        private MenuItem selectedMenuItem;
     

        public DelegateCommand<MenuItem> MouseSecimYapildiCommand => new DelegateCommand<MenuItem>(OnMouseSecimYapildi, true);

        private void OnMouseSecimYapildi(MenuItem obj)
        {
            Messenger.Default.Send<MenuSelectedEvent>(new MenuSelectedEvent { SeciliMenu = obj });
        }



        public List<MenuItem> AppMenu { get => appMenu; set => SetProperty(ref appMenu, value); }

        public NavMenuViewModel()
        {
            MenuleriYukle();
        }

        private void MenuleriYukle()
        {
            NavMenuRepository1 repo = new NavMenuRepository1();

            var menuListe = repo.NavMenuItemGetir();


            AppMenu = menuListe
                .Where(c => c.ParentMenuId == null && YetkiliMi(c.YetkiliRoller))
                .Select(c => new MenuItem
                {
                    Caption = c.FormAd,
                    Icon = c.IconPath,
                    MenuId = c.MenuId,
                    IsBadge = c.IsBadge,

                    SubItems = menuListe.Where(m => (m.ParentMenuId == c.MenuId && m.ParentMenuId != m.MenuId) && YetkiliMi(m.YetkiliRoller))
                            .Select(p => new MenuItem
                            {
                                MenuId = p.MenuId,
                                Name = p.FormAd,
                                Caption = p.FormAd,
                                ViewModelName = p.VM_Name,
                                ViewName = p.ViewName,
                                ParameterObj = p.VM_ParamObj,
                                Icon = p.IconPath,
                                IsBadge = p.IsBadge,
                                SubItems = menuListe.Where(x => x.ParentMenuId == p.MenuId && x.ParentMenuId != x.MenuId && YetkiliMi(x.YetkiliRoller))
                                           .Select(s => new MenuItem
                                           {
                                               IsBadge = s.IsBadge,
                                               MenuId = s.MenuId,
                                               Name = s.FormAd,
                                               Caption = s.FormAd,
                                               ViewModelName = s.VM_Name,
                                               ViewName = s.ViewName,
                                               ParameterObj = s.VM_ParamObj,
                                               Icon = s.IconPath,
                                               SubItems = menuListe.Where(f => f.ParentMenuId == s.MenuId && f.ParentMenuId != f.MenuId && YetkiliMi(f.YetkiliRoller))
                                                           .Select(y => new MenuItem
                                                           {
                                                               IsBadge = y.IsBadge,
                                                               MenuId = y.MenuId,
                                                               Name = y.FormAd,
                                                               Caption = y.FormAd,
                                                               ViewModelName = y.VM_Name,
                                                               ViewName = y.ViewName,
                                                               ParameterObj = y.VM_ParamObj,
                                                               Icon = y.IconPath,
                                                               SubItems = null

                                                           })
                                                           .ToList()

                                           })
                                           .ToList()

                            })
                            .ToList()
                })
                .ToList();
        }

        public void SatinAlmaSurec_IstatistikleriniGuncelle(Dictionary<string, int> sonucListe, ObservableCollection<SurecTanim> satinAlmaSurecler)
        {
            var x = this.AppMenu.SelectMany(c => c.SubItems).SelectMany(p => p.SubItems);

            var menu1 = x.FirstOrDefault(c => c.MenuId == 81.2M);
            if (menu1 != null) menu1.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.TALEPKAYIT);
             

            var menu2 = x.FirstOrDefault(c => c.MenuId == 82M);
            if (menu2 != null) menu2.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.BIRIM_AMIR_ONAYINDA);

            var menu3 = x.FirstOrDefault(c => c.MenuId == 83M);
            if (menu3 != null) menu3.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.TALEP_ATAMA);

            var menu4 = x.FirstOrDefault(c => c.MenuId == 83.5M);
            if (menu4 != null) menu4.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.TEKLIF_ISTEME);

            var menu5 = x.FirstOrDefault(c => c.MenuId == 84.1M);
            if (menu5 != null) menu5.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.KARAR_FORMU_ONAY);

            var menu6 = x.FirstOrDefault(c => c.MenuId == 84.5M);
            if (menu6 != null) menu6.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.YONETICI_ONAYINDA);

            var menu7 = x.FirstOrDefault(c => c.MenuId == 84.7M);
            if (menu7 != null) menu7.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.ONAYLANAN_SIPARISLER);

            var menu8 = x.FirstOrDefault(c => c.MenuId == 85.0M);
            if (menu8 != null) menu8.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.VERILEN_SIPARISLER);

            var menu9 = x.FirstOrDefault(c => c.MenuId == 86.0M);
            if (menu9 != null) menu9.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.KALITE_ONAYI_BEKLEYENLER);

            var menu10 = x.FirstOrDefault(c => c.MenuId == 86.2M);
            if (menu10 != null) menu10.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.KALITE_ONAYLI_SIPARISLER);

            var menu11 = x.FirstOrDefault(c => c.MenuId == 86.3M);
            if (menu11 != null) menu11.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.KALITE_RED);

            var menu12 = x.FirstOrDefault(c => c.MenuId == 86.4M);
            if (menu12 != null) menu12.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.IRSALIYE_OLUSTURULANLAR);

            var menu13 = x.FirstOrDefault(c => c.MenuId == 86.5M);
            if (menu13 != null) menu13.Deger = sonucListe.GetValueOrDefault(SATINALMA_SURECDURUM.ARSIV);
        }

        internal void SatisSiparisSurec_IstatistikleriniGuncelle(Dictionary<string, int> sonucListe, ObservableCollection<SurecTanim> siparisSurecler)
        {
            var x = this.AppMenu.SelectMany(c => c.SubItems).SelectMany(p => p.SubItems);

            var a1 = x.FirstOrDefault(c => c.MenuId == 1.101M);
            if (a1 != null) a1.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.SATISTA);

            var a1_2 = x.FirstOrDefault(c => c.MenuId == 1.1015m);
            if (a1_2 != null) a1_2.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.SATIS_BOLGEYONETICIONAYINDA);

            var a2 = x.FirstOrDefault(c => c.MenuId == 1.1017M);
            if (a2 != null) a2.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.PLANLAMADA);

            var a3 = x.FirstOrDefault(c => c.MenuId == 1.1020M);
            if (a3 != null) a3.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.SATIS_YONETICIONAYINDA);

            var a4 = x.FirstOrDefault(c => c.MenuId == 1.1040M);
            if (a4 != null)
                a4.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.YONETICIONAYINDA);

            var a5 = x.FirstOrDefault(c => c.MenuId == 1.1050M);
            if (a5 != null)
                a5.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.MUSTERIONAYINDA);

            var a6 = x.FirstOrDefault(c => c.MenuId == 1.1060M);
            if (a6 != null)
                a6.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.MUSTERIONAYLI);


            var a7 = x.FirstOrDefault(c => c.MenuId == 1.1070M);
            if (a7 != null)
                a7.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.SIPARISKARANTINA);

            var a8 = x.FirstOrDefault(c => c.MenuId == 1.1080M);
            if (a8 != null)
                a8.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.KAPALISIPARIS);

            var a9 = x.FirstOrDefault(c => c.MenuId == 1.1090M);
            if (a9 != null)
                a9.Deger = sonucListe.GetValueOrDefault(SIPARISSURECDURUM.ARSIV);
        }

        public bool YetkiliMi(string yetkiler)
        {
            var kullaniciRol = AppPandap.AktifKullanici.KullaniciRol;

            if (kullaniciRol == KULLANICIROLLERI.ADMIN || kullaniciRol == KULLANICIROLLERI.YONETICI) return true;

            var yetkiListe = yetkiler.Split(';');

            if (yetkiler == "*") return true;

            if (yetkiler == "-") return false;

            if (yetkiListe.Contains("-" + kullaniciRol)) return false;

            if (yetkiListe.Contains("*")) return true;

            var yetkiVarMi = yetkiListe.Contains(kullaniciRol);

            return yetkiVarMi;
        }

    }




}