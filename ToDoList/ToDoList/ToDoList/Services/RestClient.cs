using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class RestClient
    {

        protected string BaseUrl { get; set; } = "http://192.168.10.230:62264/api/atividade/";

        public async Task<IEnumerable<Activity>> GetActivities() {
            return await GetAsJson();
        }

        protected async Task<IEnumerable<Activity>> GetAsJson()
        {
            var result = Enumerable.Empty<Activity>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );

                    var response = await httpClient.GetAsync(BaseUrl).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        if (!string.IsNullOrWhiteSpace(json))
                        {
                            result = await Task.Run(() =>
                            {
                                return JsonConvert.DeserializeObject<IEnumerable<Activity>>(json);
                            })
                            .ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception e)
            { }
            return result;
        }

        public async void SendActivity(List<Activity> activity)
        {
            using (var httpClient = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(activity);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                await httpClient.PostAsync(BaseUrl+"/sync", content);
            }
        }

        public async void DeleteActivity(Activity activity)
        {
            using (var httpClient = new HttpClient())
            {
                var data = JsonConvert.SerializeObject(activity);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                //var titulo = activity.Titulo.Replace(" ", "%20");
                await httpClient.DeleteAsync(BaseUrl +"/"+activity.IdFake);
            }
        }

        public async void UpdateEmail(Email email)
        {
            using (var httpClient = new HttpClient())
            {
                email.Id = 1;
                var data = JsonConvert.SerializeObject(email);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                await httpClient.PutAsync("http://192.168.10.230:62264/api/email/1", content);
            }
        }

        public async void UpdateActivity(Activity activity)
        {
            using (var httpClient = new HttpClient())
            {
                AuxClass aux = new AuxClass { Id = activity.IdFake, Titulo = activity.Titulo, Descricao = activity.Descricao, DataHora = activity.DataHora, Concluida = activity.Concluida, GUID = activity.GUID};
                var data = JsonConvert.SerializeObject(aux);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                await httpClient.PutAsync("http://192.168.10.230:62264/api/atividade/"+activity.IdFake, content);
            }
        }

    }
}
