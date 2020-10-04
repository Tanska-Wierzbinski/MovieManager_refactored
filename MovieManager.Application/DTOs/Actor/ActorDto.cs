using MovieManager.Application.DTOs.Grade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return Name + " " + LastName; }
        }
        public DateTime? DeathDate { get; set; }
        public DateTime BornDate { get; set; }
        public int Age { get; set; }
        public double? AverageGrade { get; set; }
        public GenderDto Gender { get; set; }
        public string ImageName { get; set; }
        public string Country { get; set; }
        public IList<GradeDto> Grades { get; set; }

        public ActorDto()
        {
            Grades = new List<GradeDto>();
        }

    }
}
