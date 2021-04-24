using DevExpress.Mvvm;
using mnd.Logic.BC_PandapForms;
using mnd.Logic.BC_PandapForms.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mnd.UI.Modules.UretimIsletmeModule.KestiriciBakim
{
    public class GunlukFormListVM
    {
        PandapFormRepository repo = new PandapFormRepository();

        public DelegateCommand<FormGunluk> DuzenleCommand => new DelegateCommand<FormGunluk>(onDuzenle, true);

        private void onDuzenle(FormGunluk formGunluk)
        {
            FormYanitlarListVM vm = new FormYanitlarListVM(formGunluk);


            var doc = AppPandap.pDocumentManagerService.CreateDocument("FormYanitlarListView", vm);
            doc.Title = "Yanıtlar";
            doc.Show();

          
        }

        public List<FormGunluk> GunlukFormlar { get; set; }
        public GunlukFormListVM(string formAdi)
        {
            GunlukFormlar = repo.GunlukFormlariGetir();
        }
      
    }
}
