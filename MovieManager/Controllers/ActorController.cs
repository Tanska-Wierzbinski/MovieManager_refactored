using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Grade;
using MovieManager.Application.Interfaces;

namespace MovieManager.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;

        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        // GET: ActorController
        public ActionResult Index(GenderDto? gender, int yearMin, int yearMax, int gradeMin, int gradeMax, string[] countries, string sortOrder, int? pageNumber, int pageSize = 30)
        {

            return View(_actorService.GetAllForIndex(gender, yearMin, yearMax, gradeMin, gradeMax, countries, sortOrder, pageNumber, pageSize));
        }

        // GET: ActorController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await _actorService.GetDetails(id));
        }

        // GET: ActorController/Create
        public ActionResult Create()
        {
            return View(_actorService.AddGet());
        }

        // POST: ActorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ActorAddDto newActor)
        {
            await _actorService.AddPost(newActor);
            return RedirectToAction(nameof(Index));
        }

        // GET: ActorController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _actorService.EditGet(id));
        }

        // POST: ActorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ActorEditDto editedActor)
        {
            await _actorService.EditPost(editedActor);
            return RedirectToAction(nameof(Index));
        }

        // GET: ActorController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _actorService.GetById(id));
        }

        // POST: ActorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await _actorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> AddGrade(int actorId, int movieId, int grade, string author)
        {
            GradeAddDto newGrade = new GradeAddDto()
            {
                ActorId = actorId,
                MovieId = movieId,
                GradeValue = grade,
                Author = author
            };
            await _actorService.AddGrade(newGrade);
            return RedirectToAction(nameof(Details), new { id = actorId });
        }

        public async Task<ActionResult> DeleteImage(string imageName)
        {
            await _actorService.DeleteImage(imageName);
            return Ok();
        }
    }
}
