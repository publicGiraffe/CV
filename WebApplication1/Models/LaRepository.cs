using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class LaRepository
    {
        private static List<Language> responsesLa = new List<Language>();
        public static IEnumerable<Language> Responses
        {
            get
            {
                return responsesLa;
            }
        }

        public static void AddResponse(Language response)
        {
            responsesLa.Add(response);
        }

        public static void DeleteResponce(Language response)
        {
            responsesLa.Remove(response);
        }
    }
}
