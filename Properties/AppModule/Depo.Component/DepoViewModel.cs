using System;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using Pandap.Logic.Model;
using Pandap.Logic.Model.Uretim;
using Pandap.Logic.Model._DTOs;
using Pandap.Logic.Persistence;

namespace Pandap.UI.Depolar
{
    public class DepoViewModel : MyBindableBase
    {
        public DepoViewModel()
        {
            SeciliTabIndex = 0;

            DepodakiPaletler = uow.UretimEmriRepo.DepoOnayiBekleyenPaletleriGetir();
        }

        public DelegateCommand<object> DepoyaKabulCommand => new DelegateCommand<object>(OnDepoyaKabul, CanDepoyaKabul);

        private bool CanDepoyaKabul(object arg)
        {
            return SeciliTabIndex == 0;
        }

        public DelegateCommand<object> DepoRedCommand => new DelegateCommand<object>(OnDepoRed, CanRed);

        private bool CanRed(object arg)
        {
            return SeciliTabIndex == 1;
        }

        public DelegateCommand<object> DepoKarantinayaGonderCommand => new DelegateCommand<object>(OnDepoKarantinayaGonder, CanKarantinayaGonder);

        private bool CanKarantinayaGonder(object arg)
        {
            return SeciliTabIndex == 2;
        }

        public DelegateCommand<object> SevkiyatIslemCommand => new DelegateCommand<object>(OnSevkiyatIslem, CanSevkiyatIslem);

        private bool CanSevkiyatIslem(object arg)
        {
            return true;
        }

        private void OnSevkiyatIslem(object obj)
        {
            if (SeciliMamulDepoStok == null) return;

            var _palet = uow.UretimEmriRepo.PaletGetir(SeciliMamulDepoStok.PaletNo.Value);

            _palet.SevkiyatTarihi = DateTime.Now.Date;
            _palet.SevkiyatAracPlaka = "06TEST233";
            _palet.SevkiyatAracPlaka = "Ahmet Aydın (test)";

            uow.Commit();

            DepodakiPaletler = uow.UretimEmriRepo.Depo_OnayliPaletleriGetir();
            MamulDepoStoklar = uow.UretimEmriRepo.MamulDepoStoklariGetir();
            SevkEdilenlerDto = uow.UretimEmriRepo.SevkEdilenPaletler();
        }

        private void OnDepoKarantinayaGonder(object obj)
        {
        }

        private void OnDepoRed(object obj)
        {
        }

        private Palet depo_seciliPalet;

        public Palet DepoSeciliPalet
        {
            get { return depo_seciliPalet; }
            set
            {
                SetProperty(ref depo_seciliPalet, value);
            }
        }

        private void OnDepoyaKabul(object obj)
        {
            DepoSeciliPalet.DepoyaAktarilmaOnayTarihi = DateTime.Now.Date;
            uow.Commit();

            DepodakiPaletler = uow.UretimEmriRepo.DepoOnayiBekleyenPaletleriGetir();
        }

        private UnitOfWork uow = new UnitOfWork();

        private MamulDepoStokDto seciliMamulDepoStok;

        public MamulDepoStokDto SeciliMamulDepoStok
        {
            get => seciliMamulDepoStok;
            set => SetProperty(ref seciliMamulDepoStok, value);
        }

        public ObservableCollection<MamulDepoStokDto> sevkEdilenlerDto;

        public ObservableCollection<MamulDepoStokDto> SevkEdilenlerDto
        {
            get => sevkEdilenlerDto; set => SetProperty(ref sevkEdilenlerDto, value);
        }

        public ObservableCollection<MamulDepoStokDto> mamulDepoStokDto;

        public ObservableCollection<MamulDepoStokDto> MamulDepoStoklar
        {
            get => mamulDepoStokDto; set => SetProperty(ref mamulDepoStokDto, value);
        }

        public ObservableCollection<Palet> depodakiPaletler;

        public ObservableCollection<Palet> DepodakiPaletler
        {
            get => depodakiPaletler; set => SetProperty(ref depodakiPaletler, value);
        }
        

        int seciliTabIndex;
        public int SeciliTabIndex
        {
            get
            {
                return seciliTabIndex;
            }
            set
            {
               if( SetProperty(ref seciliTabIndex, value))
                {
                    if (value == 0) DepodakiPaletler = uow.UretimEmriRepo.DepoOnayiBekleyenPaletleriGetir();

                    if (value==1) MamulDepoStoklar = uow.UretimEmriRepo.MamulDepoStoklariGetir();

                    if (value==2) SevkEdilenlerDto = uow.UretimEmriRepo.SevkEdilenPaletler();
                }

            }
        }


    }
}