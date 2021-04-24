using DevExpress.Mvvm;
using Pandap.Logic.Model.App;
using Pandap.Logic.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandap.UI.AppModule.App
{
    public class OrtakDilTanimListViewModel
    {
        private UnitOfWork uow = new UnitOfWork();

        public DelegateCommand<object> KaydetCommand => new DelegateCommand<object>(OnKaydet, c => true);
        public virtual IMessageBoxService MessageBoxService { get { return null; } }

        public DelegateCommand<OrtakDilTanim> NewItemAddedCommand => new DelegateCommand<OrtakDilTanim>(NewItemRowUpdated, c => true);

        private  void OnKaydet(object obj)
        {
           uow.Commit();
        }


        public  void NewItemRowUpdated(OrtakDilTanim item)
        {
            uow.OrtakDilRepo.EkleAsync(item);
            uow.Commit();
        }

        public OrtakDilTanimListViewModel()
        {
            OrtakDilListe = uow.OrtakDilRepo.OrtakDilTanimListeGetir();
        }
        ObservableCollection<OrtakDilTanim> ortakDilListe;
        public ObservableCollection<OrtakDilTanim> OrtakDilListe
        {
            get => ortakDilListe;
            set => ortakDilListe = value;
        }

        public OrtakDilTanim SeciliOrtakDilTanim { get; set; }
    }
}
