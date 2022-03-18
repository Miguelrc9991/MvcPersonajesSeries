using Microsoft.AspNetCore.Mvc;
using MvcPersonajesSeries.Models;
using MvcPersonajesSeries.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPersonajesSeries.Controllers
{
    public class ServidorController : Controller
    {
        private ServiceApiPersonajesSeries service;

        public ServidorController(ServiceApiPersonajesSeries service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series =
                await this.service.GetSeriesAsync();
            return View(series);
        }
        public async Task<IActionResult> Details(int idserie)
        {
            Serie serie = await this.service.FindSerieAsync(idserie);
            return View(serie);
        }
        public async Task<IActionResult> GetListPersonajes()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesAsync();
            return View(personajes);
        }
        public async Task<IActionResult> GetPersonajes(int idserie)
        {
            List<Personaje> personajes =
                await this.service.FindPersonajesAsync(idserie);
            return View(personajes);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync
            (personaje.IdPersonaje, personaje.NombrePersonaje,personaje.Imagen,personaje.IdSerie);
            return RedirectToAction("GetListPersonajes");

        }
        public async Task<IActionResult> EditSerie(int idserie)
        {
            Serie serie =
            await this.service.FindSerieAsync(idserie);
            return View(serie);
        }

        [HttpPost]
        public async Task<IActionResult> EditSerie(Serie serie)
        {
            await this.service.UpdateSerieAsync(serie.IdSerie
            , serie.NombreSerie, serie.Imagen,serie.Puntuacion,serie.Año);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int idpersonaje)
        {
            await this.service.DeletePersonajeAsync(idpersonaje);
            return RedirectToAction("GetListPersonajes");
        }

        public async Task<IActionResult> EditSeriePersonajes()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            ViewData["PERSONAJES"] = personajes;
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> EditSeriePersonajes(int idpersonaje,int idserie)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(idpersonaje);
            personaje.IdSerie = idpersonaje;


            await this.service.UpdateSeriePersonajeAsync(personaje.IdPersonaje,personaje.IdSerie);
            return RedirectToAction("GetListPersonajes");
        }

    }
}
