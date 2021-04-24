using Newtonsoft.Json;
using mnd.Common.NetsisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using mnd.UI.AppModules.SplashScreenModule;

namespace mnd.UI.Modules.OperasyonModule
{
    public class NetsisHelper
    {
        public static async Task NetsiseIrsaliyeGonder(NetsisIrsaliye netsisIrsaliye)
        {
            SplashScreenHelper.Instance.ShowLoadingScreen();


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(AppPandap.WebApiNetsisPath);

            var response = await client.PostAsync("/api/netsis",
                        new StringContent(JsonConvert.SerializeObject(netsisIrsaliye).ToString(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                SplashScreenHelper.Instance.HideSplashScreen();
                var sonuc = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result);

                MessageBox.Show(sonuc, "Netsis Kayıt", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Hata Oluştu", response.StatusCode.ToString(), MessageBoxButton.OK, MessageBoxImage.Information);
            }

            SplashScreenHelper.Instance.HideSplashScreen();
        }
    }
}
