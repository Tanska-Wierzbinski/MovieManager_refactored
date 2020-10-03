using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Movie;
using MovieManager.Application.Interfaces;

namespace MovieManager.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: MovieController
        public ActionResult Index(int yearMin, int yearMax, int gradeMin, int gradeMax, int[] categories, string sortOrder, int? pageNumber, int pageSize = 5)
        {
            return View(_movieService.GetAllForIndex(yearMin, yearMax, gradeMin, gradeMax, categories, sortOrder, pageNumber, pageSize));
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _movieService.GetDetails(id));
        }

        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View(_movieService.AddGet());
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieAddDto newMovie)
        {
            await _movieService.AddPost(newMovie);
            return RedirectToAction(nameof(Index));
        }

        // GET: MovieController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _movieService.EditGet(id));
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MovieEditDto editedMovie)
        {
            await _movieService.EditPost(editedMovie);
            return RedirectToAction(nameof(Index));
        }

        // GET: MovieController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _movieService.GetById(id));
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await _movieService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult AddNewActor()
        {
            //var tempActor = new TempActor();
            //return PartialView("~/Views/Shared/EditorTemplates/TempActor.cshtml", tempActor);
            return PartialView("~/Views/Shared/EditorTemplates/ActorAddDto.cshtml", new ActorAddDto());
        }

        public async Task<ActionResult> DeleteImage(string imageName)
        {
            await _movieService.DeleteImage(imageName);
            return Ok();
        }
    }
}
