using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieManager.Application.DTOs.Home;
using MovieManager.Application.Interfaces;
using MovieManager.Domain.Interfaces;
using MovieManager.Models;

namespace MovieManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieRepository _movieRepository;
        public HomeController(IMovieService movieService, IMovieRepository movieRepository)
        {
            _movieService = movieService;
            _movieRepository = movieRepository;
        }

        public ActionResult Index()
        {
            return View(_movieService.GetForHome());
        }

        public ActionResult SearchIndex(string searchString)
        {
            return View(_movieService.GetForSearch(searchString));
        }
    }
}
