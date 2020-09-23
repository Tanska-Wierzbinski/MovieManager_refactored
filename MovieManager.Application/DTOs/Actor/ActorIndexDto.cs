using MovieManager.Application.DTOs.Grade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorIndexDto
    {
        public GenderDto? Gender { get; set; }
        public int YearMin { get; set; }
        public int YearMax { get; set; }
        public int GradeMin { get; set; }
        public int GradeMax { get; set; }
        public string[] Countries { get; set; }
        public string SortOrder { get; set; }
        public int? PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<ActorDto> Actors { get; set; }
        //public IList<GradeDto> Grades { get; set; }
    }
}
