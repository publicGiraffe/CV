using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LanguageController : Controller
    {
        public ActionResult Language()
        {
            PersonalInfo person = PIRepository.Responses.Last();
            return View(person);
        }
        public ActionResult LanguageAdd()
        {
            LaPerson person = new LaPerson(PIRepository.Responses.Last(), new List<Language>(), null, null);
            return View("LanguageLoad", person);
        }
        [HttpPost]

        public ActionResult LanguageLoad(LaPerson personDTO)
        {
            if (ModelState.IsValid)
            {
                LaRepository.AddResponse(new Language(personDTO.LanguageTitle, personDTO.Level));
                LaPerson person = new LaPerson();
                person.personalInfo = PIRepository.Responses.Last();
                person.languages = LaRepository.Responses.ToList();
                return View("LanguageLoad", person);
            }
            else
            {
                LaPerson person = new LaPerson(PIRepository.Responses.Last(), new List<Language>(), null, null);
                return View("LanguageLoad", person);
            }
        }

        public ActionResult DeleteLanguageFromList(string lt)
        {
            LaPerson person = new LaPerson();
            person.personalInfo = PIRepository.Responses.Last();
            person.languages = LaRepository.Responses.ToList();
            LaRepository.DeleteResponce(person.languages.FirstOrDefault(t => t.LanguageTitle == lt));
            person.languages = LaRepository.Responses.ToList();
            return View("LanguageLoad", person);
        }
    }
}