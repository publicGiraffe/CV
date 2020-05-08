using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EducationController : Controller
    {

        public ActionResult Education()
        {
            PersonalInfo person = PIRepository.Responses.Last();
            return View(person);
        }

        public ActionResult EducationAdd()
        {
            EducatedPerson person = new EducatedPerson(PIRepository.Responses.Last(), null, null, null, 2020, new List<Education>());
            return View("EducationLoad", person);
        } 

       [HttpPost]

        public ActionResult EducationLoad(EducatedPerson personDTO)
        {
            if(ModelState.IsValid)
            {
                EduRepository.AddResponse(new Education(personDTO.InstituteName, personDTO.Degree, personDTO.Speciality, personDTO.YearPassed));
                EducatedPerson person = new EducatedPerson();
                person.personalInfo = PIRepository.Responses.Last();
                person.education = EduRepository.Responses.ToList();
                return View("EducationLoad", person);
            }
            else
            {
                EducatedPerson person = new EducatedPerson(PIRepository.Responses.Last(), null, null, null, 2020, new List<Education>());
                return View("EducationLoad", person);
            }
            
        }

        public ActionResult DeleteFromList(string speciality)
        {
            EducatedPerson person = new EducatedPerson();
            person.personalInfo = PIRepository.Responses.Last();
            person.education = EduRepository.Responses.ToList();
            EduRepository.DeleteResponce(person.education.FirstOrDefault(t => t.Speciality == speciality));
            person.education = EduRepository.Responses.ToList();
            return View("EducationLoad", person);
        }
    }
}