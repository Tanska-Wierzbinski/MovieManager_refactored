using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieManager.Application.DTOs.Home;
using MovieManager.Application.Interfaces;
using MovieManager.Models;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            return View(_movieService.GetForHome());
        }

        public IActionResult SearchIndex(string searchString)
        {
            return View(_movieService.GetForSearch(searchString));
        }
    }
}
