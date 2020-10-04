using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Application.DTOs.Category;
using MovieManager.Application.Interfaces;

namespace MovieManager.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            return View(_categoryService.GetAllForIndex());
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View(_categoryService.GetMoviesByCategory(id));
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View(new CategoryAddDto());
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryAddDto newCategory)
        {
            await _categoryService.AddPost(newCategory);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _categoryService.EditGet(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryDto editedCategory)
        {
            await _categoryService.EditPost(editedCategory);
            return RedirectToAction(nameof(Index));
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _categoryService.GetById(id));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
