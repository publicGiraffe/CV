using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LaPerson
    {
        public PersonalInfo personalInfo { get; set; }
        public List<Language> languages = new List<Language>();

        public LaPerson(PersonalInfo personalInfo, List<Language> languages, string languageTitle, string level)
        {
            this.personalInfo = personalInfo;
            this.languages = languages;
            LanguageTitle = languageTitle;
            Level = level;
        }

        public LaPerson()
        {
        }

        [Required(ErrorMessage = "Tell us which language you study(ied)")]
        public string LanguageTitle { get; set; }
        [Required(ErrorMessage = "***")]
        public string Level { get; set; }

    }
}
