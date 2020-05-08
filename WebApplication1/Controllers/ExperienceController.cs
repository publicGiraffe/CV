using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ExperienceController : Controller
    {
        public ActionResult Experience()
        {
            PersonalInfo person = PIRepository.Responses.Last();
            return View(person);
        }

        public ActionResult ExperienceAdd()
        {
            ExpPerson person = new ExpPerson(PIRepository.Responses.Last(), new List<Experience>(), null, null, 0, null);
            return View("ExperienceLoad", person);
        }

        [HttpPost]

        public ActionResult ExperienceLoad(ExpPerson personDTO)
        {
            if(ModelState.IsValid)
            {
                ExpRepository.AddResponse(new Experience(personDTO.Company, personDTO.Job, personDTO.DurationNumber, personDTO.DurationType));
                ExpPerson person = new ExpPerson();
                person.personalInfo = PIRepository.Responses.Last();
                person.experiences = ExpRepository.Responses.ToList();
                return View("ExperienceLoad", person);
            }
            else
            {
                ExpPerson person = new ExpPerson(PIRepository.Responses.Last(), new List<Experience>(), null, null, 0, null);
                return View("ExperienceLoad", person);

            }
        }

        public ActionResult DeleteFromList(string company)
        {
            ExpPerson person = new ExpPerson();
            person.personalInfo = PIRepository.Responses.Last();
            person.experiences = ExpRepository.Responses.ToList();
            ExpRepository.DeleteResponce(person.experiences.FirstOrDefault(t => t.Company == company));
            person.experiences = ExpRepository.Responses.ToList();
            return View("ExperienceLoad", person);
        }
    }
}