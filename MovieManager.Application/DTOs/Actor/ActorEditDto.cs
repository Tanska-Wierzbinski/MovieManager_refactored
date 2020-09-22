using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.DTOs.Actor
{
    public class ActorEditDto : ActorAddDto
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
