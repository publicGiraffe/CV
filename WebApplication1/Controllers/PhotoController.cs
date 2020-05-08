using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PhotoController : Controller
    {

        public ActionResult Photo()
        {
            Photo imageModel = new Photo(null,null);
            return View(imageModel);
        }
        public ActionResult PhotoLoad(Photo imageModel)
        {
            return View(imageModel);
        }
 
        [HttpPost]
        public ActionResult Add(Photo imageModel)
        {
            if(ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                Path.GetFileName(imageModel.ImageFile.FileName);
                string path = "C:\\Users\\Administrator\\source\\repos\\CV\\WebApplication1\\pic\\";

                imageModel.ImagePath = Path.Combine(path, imageModel.ImageFile.FileName);
                using (var stream = new FileStream(imageModel.ImagePath, FileMode.Create))
                {
                    imageModel.ImageFile.CopyTo(stream);
                }
                PhotoRepository.AddResponse(imageModel);
                return View("PhotoLoad", imageModel);
            }
            else
            {
                return View("PhotoLoad", imageModel);

            }

        }
        public ActionResult DeleteFromList(Photo pic)
        {
            PhotoRepository.DeleteResponce(pic);
            return View("Photo");
        }
    }
}