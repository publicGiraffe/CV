using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ExpPerson
    {
        public PersonalInfo personalInfo { get; set; }
        public List<Experience> experiences = new List<Experience>();

        public ExpPerson(PersonalInfo personalInfo, List<Experience> experiences, string company, string job, int durationNumber, string durationType)
        {
            this.personalInfo = personalInfo;
            this.experiences = experiences;
            Company = company;
            Job = job;
            DurationNumber = durationNumber;
            DurationType = durationType;
        }

        public ExpPerson()
        {
        }

        [Required(ErrorMessage = "Enter where have you previously worked")]
        public string Company { get; set; }

        [Required(ErrorMessage = "***")]
        public string Job { get; set; }

        [Required(ErrorMessage = "Input the number correctly")]
        public int DurationNumber { get; set; }

        [Required(ErrorMessage = "***")]
        public string DurationType { get; set; }

    }
}
