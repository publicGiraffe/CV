using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EducatedPerson
    {
        public PersonalInfo personalInfo { get; set; }

        [Required(ErrorMessage = "Tell us where you study(ied)")]
        public string InstituteName { get; set; }
        [Required(ErrorMessage = "Choose a degree")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Tell us what you study(ied)")]
        public string Speciality { get; set; }
        [Range(1900, 2050, ErrorMessage = "The year you will graduate(graduated)")]
        public int YearPassed { get; set; }

        public EducatedPerson(PersonalInfo personalInfo, string instituteName, string degree, string speciality, int yearPassed, List<Education> education)
        {
            this.personalInfo = personalInfo;
            InstituteName = instituteName;
            Degree = degree;
            Speciality = speciality;
            YearPassed = yearPassed;
            this.education = education;
        }
        public List<Education> education = new List<Education>();

        public EducatedPerson()
        {
        }
    }
}
