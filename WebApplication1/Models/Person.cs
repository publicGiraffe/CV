using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Person
    {
        public Person(PersonalInfo personalInfo, Photo photo, List<Education> gainedEducation, List<Experience> gainedExperience, List<Language> languages)
        {
            this.personalInfo = personalInfo;
            this.photo = photo;
            this.gainedEducation = gainedEducation;
            this.gainedExperience = gainedExperience;
            Languages = languages;
        }

        public PersonalInfo personalInfo { get; set; }
        public Photo photo { get; set; }
        
        public List<Education> gainedEducation { get; set; }
      
        public List<Experience> gainedExperience { get; set; }

        public List<Language> Languages { get; set; }

        public override string ToString()
        {
            return base.ToString();

        }
    }
}
