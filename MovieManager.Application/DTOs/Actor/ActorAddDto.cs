using Microsoft.AspNetCore.Http;
using MovieManager.Application.DTOs.MovieActor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorAddDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime BornDate { get; set; }
        public GenderDto Gender { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Country { get; set; }
        public int[] MovieIds { get; set; }

        //selectList z filmami do wyboru
        //selectList z krajami

    }
    public enum GenderDto
    {
        Male,Female
    }
}
