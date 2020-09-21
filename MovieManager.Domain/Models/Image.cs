using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Domain.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Name { get; set; }

       // [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
