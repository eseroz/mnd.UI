using DevExpress.DirectX.NativeInterop.Direct3D;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.POCO;
using Newtonsoft.Json;
using mnd.UI.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace mnd.UI.Modules.Dashboard.MakinaPLC
{
    public class MakinaPlcDataVM : MyDxViewModelBase
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private ObservableCollection<MakinaData> fH3 = new ObservableCollection<MakinaData>();
        private ObservableCollection<MakinaData> fH4 = new ObservableCollection<MakinaData>();

 
        public ObservableCollection<MakinaData> FH3 { get => fH3; set => SetProperty(ref fH3, value); }
        public ObservableCollection<MakinaData> FH4 { get => fH4; set => SetProperty(ref fH4 ,value); }
     

        public MakinaPlcDataVM(string formAdi)
        {
            FH3 = new ObservableCollection<MakinaData>();
            FH4 = new ObservableCollection<MakinaData>();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1d);
            timer.Tick += dispatcherTimer_Tick;
            timer.Start();


            dispatcherTimer_Tick(null, null);
        }

        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            HttpClient cli = new HttpClient();
            var yol = "http://192.168.1.198/PandapPlcApi/api/makina/FolyoHaddeData";


            var result = await cli.GetAsync(yol);

            var rawJson = await result.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<MakinaData>>(rawJson);


            if (data != null)
            {
                foreach (var item in data)
                {
                    item.Id = item.Id.Replace("Siemens.Fh3.Pandap.", "") + " ";
                    item.Id = item.Id.Replace("Siemens.Fh4.Pandap.", "") + " ";
                }
            }


            FH3 = data.Where(c => c.MakinaKod == "FH3").ToObservableCollection();
                FH3[3].V = FH3[3].V * 1000;
                FH3[4].V = FH3[4].V * 1000;
                var ruloAgirlik_FH3 = RuloAgirligiHesapla( ic_cap: 500, dis_cap: (int)FH3[1].V, en: FH3[2].V);
                FH3.Add(new MakinaData { V = ruloAgirlik_FH3 });  //5.index

            FH4 = data.Where(c => c.MakinaKod == "FH4")
                        .OrderBy(c=>int.Parse(c.Id.Substring(0,2).Replace("_", "")))
                       .ToObservableCollection();

            FH4[3].V = FH4[3].V * 1000;
            FH4[4].V = FH4[4].V * 1000;

            var ruloAgirlik_F4 = RuloAgirligiHesapla(ic_cap: 500, dis_cap: (int)FH4[1].V, en: FH4[2].V);
            FH4.Add(new MakinaData { V = ruloAgirlik_F4 }); 

            var gecenSureDk_F4 = DateTime.Now.AddTicks(-FH4[8].T).Minute;
            FH4.Add(new MakinaData { V = gecenSureDk_F4 }); 

            cli.Dispose();

        }


        public double RuloAgirligiHesapla(double ic_cap, double dis_cap, double en)
        {
            var sonuc = (((Math.Pow((dis_cap / 20d), 2)) * 3.14 * 2.71 * en / 10d) 
                        - (Math.Pow((ic_cap / 20d), 2)) * 3.14 * 2.73 * en / 10d) / 1000d;

            return sonuc;
        }

    }
}
