using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PIRepository
    {
        private static List<PersonalInfo> responsesPI = new List<PersonalInfo>();
        public static IEnumerable<PersonalInfo> Responses
        {
            get
            {
                return responsesPI;
            }
        }

        public static void AddResponse(PersonalInfo response)
        {
            responsesPI.Add(response);
        }
    }
}
