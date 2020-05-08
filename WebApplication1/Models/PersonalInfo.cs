using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PersonalInfo
    {
        [Required(ErrorMessage = "Please enter your surname.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Check your birthday.")]

        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Please input your phone number.")]

        public string Phonenumber { get; set; }

        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Choose where you live")]

        public string City { get; set; }

        [Required(ErrorMessage = "Tell us a little about your skills.")]
        public string Skills { get; set; }

    }
}
