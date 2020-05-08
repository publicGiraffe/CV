using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Language
    {
        public Language(string languageTitle, string level)
        {
            LanguageTitle = languageTitle;
            Level = level;
        }

        [Required(ErrorMessage = "Tell us which language you study(ied)")]
        public string LanguageTitle { get; set; }
        [Required(ErrorMessage = "***")]
        public string Level { get; set; }


    }
}
