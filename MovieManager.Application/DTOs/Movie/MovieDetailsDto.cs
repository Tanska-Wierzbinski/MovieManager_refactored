using Microsoft.AspNetCore.Mvc.Rendering;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieDetailsDto : MovieDto
    {
        public IList<ActorDto> Actors { get; set; }
        public ReviewDto Review { get; set; }
        public IList<SelectListItem> SelectListGrade { get; set; }
        public MovieDetailsDto()
        {
            Review = new ReviewDto();
            SelectListGrade = new List<SelectListItem>();
            for(int i=1;i<=10;i++)
            {
                SelectListGrade.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            };
        }
    }
}
