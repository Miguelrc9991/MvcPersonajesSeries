using MvcPersonajesSeries.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcPersonajesSeries.Services
{
    public class ServiceApiPersonajesSeries
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;
        public ServiceApiPersonajesSeries(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/Series";
            List<Serie> series =
            await this.CallApiAsync<List<Serie>>(request);
            return series;
        }
        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            List<Personaje> personajes =
            await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int idpersonaje)
        {
            string request = "/api/Personajes/" + idpersonaje;
            Personaje personaje =
            await this.CallApiAsync<Personaje>(request);
            return personaje;
        }
        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/Series/" + idserie;
            Serie serie =
            await this.CallApiAsync<Serie>(request);
            return serie;
        }
        public async Task<List<Personaje>> FindPersonajesAsync(int idserie)
        {
            string request = "/api/Series/" + idserie;
            List<Personaje> personajes =
            await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }
        public async Task InsertPersonajeAsync
            (int idPersonaje, string nombrePersonaje, string imagen,int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje= idPersonaje;
                personaje.NombrePersonaje = nombrePersonaje;
                personaje.Imagen = imagen;
                personaje.IdSerie = idSerie;

                string json = JsonConvert.SerializeObject(personaje);
              
                StringContent content = new StringContent
                (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PostAsync(request, content);
            }
        }
        public async Task UpdateSerieAsync
            (int idSerie, string nombreSerie
            , string imagen,double puntuacion,int año)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Series";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie
                { IdSerie = idSerie, NombreSerie = nombreSerie, Imagen = imagen,Puntuacion=puntuacion,Año=año };
                string json = JsonConvert.SerializeObject(serie);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PutAsync(request, content);
            }
        }

        public async Task UpdateSeriePersonajeAsync
           (int idpersonaje,int idSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/"+idpersonaje+"/"+idSerie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = await this.FindPersonajeAsync(idpersonaje);
                personaje.IdSerie = idSerie;
                
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PutAsync(request, content);
            }
        }
        public async Task DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/" + id;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                await client.DeleteAsync(request);
            }
        }




    }
}
