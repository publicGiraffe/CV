using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Experience
    {
        public Experience(string company, string job, int durationNumber, string durationType)
        {
            Company = company;
            Job = job;
            DurationNumber = durationNumber;
            DurationType = durationType;
        }

        [Required(ErrorMessage = "Enter where have you previously studyied")]
        public string Company { get; set; }

        [Required(ErrorMessage = "***")]
        public string Job { get; set; }

        [Required(ErrorMessage = "Input the number correctly")]
        public int DurationNumber { get; set; }

        [Required(ErrorMessage = "***")]
        public string DurationType { get; set; }
    }
}
