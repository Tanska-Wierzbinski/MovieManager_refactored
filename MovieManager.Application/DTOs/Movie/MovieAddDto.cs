using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Movie
{
    public class MovieAddDto
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile ImageFile { get; set; }
        public int[] CategoryIds { get; set; }
        public int[] ActorIds { get; set; }

        //selectList z kategoriami
        //selectList z aktorami
        //lista na nowych aktorów?
    }
}
