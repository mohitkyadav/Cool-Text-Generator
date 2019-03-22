using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cool_Text_Generator
{
    class CoolNameAPI
    {
        Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
        const string BaseURI = "https://cool-name-api.glitch.me/";
        const string CoolServiceURI = "coolify?name=";
        const string UncoolServiceURI = "uncoolify?name=";

        public async void CoolifyAsync(string s, ObservableCollection<CoolName> coolNames)
        {
            Uri coolURI = new Uri(BaseURI + CoolServiceURI + s);
            //List<CoolName> coolNames = new List<CoolName>();
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(coolURI);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                JObject jsonObject = JObject.Parse(httpResponseBody);
                coolNames.Clear();
                foreach (var x in jsonObject)
                {
                    coolNames.Add(new CoolName(x.Key, x.Value.ToString()));
                }
            }
            catch (Exception ex)
            {
                coolNames.Clear();
                coolNames.Add(new CoolName("Error", "Maybe the remote server's down 😭"));
                coolNames.Add(new CoolName("Error", "Check if https://cool-name-api.glitch.me/ is working."));
                coolNames.Add(new CoolName("Error", "Check your internet connection"));
                coolNames.Add(new CoolName("Error", ex.Message.ToString()));
            }
        }

        public void Uncoolify(string s)
        {
            Uri uncoolURI = new Uri(BaseURI + UncoolServiceURI + s);
            System.Diagnostics.Debug.WriteLine(uncoolURI);
        }
    }
}
