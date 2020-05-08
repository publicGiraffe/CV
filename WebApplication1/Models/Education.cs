using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Education
    {
        public Education(string instituteName, string degree, string speciality, int yearPassed)
        {
            InstituteName = instituteName;
            Degree = degree;
            Speciality = speciality;
            YearPassed = yearPassed;
        }


        [Required(ErrorMessage = "Tell us where you study(ied)")]
        public string InstituteName { get; set; }
        [Required(ErrorMessage = "Choose a degree")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "Tell us what you study(ied)")]
        public string Speciality { get; set; }
        [Range(1900, 2050, ErrorMessage = "The year you (will) have graduated")]
        public int YearPassed { get; set; }

    }
}
