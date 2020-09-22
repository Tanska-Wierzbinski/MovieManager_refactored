using MovieManager.Application.DTOs.Grade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorIndexDto
    {
        public GenderDto? gender { get; set; }
        public int yearMin { get; set; }
        public int yearMax { get; set; }
        public int gradeMin { get; set; }
        public int gradeMax { get; set; }
        public string[] countries { get; set; }
        public string sortOrder { get; set; }
        public int? pageNumber { get; set; }
        public int pageSize { get; set; }
        public List<ActorDto> Actors { get; set; }
        public IList<GradeDto> Grades { get; set; }
    }
}
