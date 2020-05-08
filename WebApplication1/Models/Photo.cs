using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace WebApplication1.Models
{
    public class Photo
    {
        public Photo()
        {
        }

        public Photo(string imagePath, IFormFile imageFile)
        {
            ImagePath = imagePath;
            ImageFile = imageFile;
        }

        public string ImagePath { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
