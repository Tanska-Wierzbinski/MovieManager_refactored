using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Application.DTOs.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieAddDto
    {
        public string Name { get; set; }
        public string Director { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public IFormFile ImageFile { get; set; }
        public int[] CategoryIds { get; set; }
        public int[] ActorIds { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Actors { get; set; }
        public List<ActorAddDto> NewActors { get; set; }

        public MovieAddDto()
        {
            NewActors = new List<ActorAddDto>();
        }
        //selectList z kategoriami
        //selectList z aktorami
        //lista na nowych aktorów?
    }
}
