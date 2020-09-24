using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.DTOs.Review;
using MovieManager.Application.Interfaces;

namespace MovieManager.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: ReviewController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: ReviewController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: ReviewController/Create
        public ActionResult Create()
        {
            return View(new ReviewAddDto());
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReviewAddDto newReview)
        {
            await _reviewService.AddPost(newReview);
            return RedirectToAction(nameof(Index));
        }

        // GET: ReviewController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _reviewService.EditGet(id));
        }

        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ReviewDto editedReview)
        {
            await _reviewService.EditPost(editedReview);
            return RedirectToAction(nameof(Index));
        }

        // GET: ReviewController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _reviewService.GetById(id));
        }

        // POST: ReviewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await _reviewService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
