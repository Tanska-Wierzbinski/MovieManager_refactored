using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Grade
{
    public class GradeAddDto
    {
        public int GradeValue { get; set; }
        public string Author { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }
    }
}
