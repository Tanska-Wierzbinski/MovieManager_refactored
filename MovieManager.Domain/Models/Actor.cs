using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public Gender Gender { get; set; }
        public string Country { get; set; }
        public string ImageName { get; set; }
        public IList<MovieActor> MovieActors { get; set; }
        public IList<Grade> Grades { get; set; }

        public int GetCurrentAge()
        {
            int Age;
            if (DeathDate == null)
            {
                DateTime now = DateTime.Now;

                Age = now.Year - BornDate.Year; if (BornDate.Date > now.AddYears(-Age)) { Age--; }
            }
            else
            {
                Age = DeathDate.Value.Year - BornDate.Year; if (BornDate.Date > DeathDate.Value.AddYears(-Age)) { Age--; }
            }
            return Age;
        }
        public double? GetAverageGrade()
        {
            if (Grades.Any())
            {
                return Math.Round(Grades.Average(g => g.GradeValue), 1);
            }
            else return null;

        }
    }

    public enum Gender
    {
        Male=2,
        Female=0
    }
}
