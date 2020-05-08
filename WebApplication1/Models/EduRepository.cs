using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EduRepository
    {
        private static List<Education> responsesEdu = new List<Education>();
        public static IEnumerable<Education> Responses
        {
            get
            {
                return responsesEdu;
            }
        }

        public static void AddResponse(Education response)
        {
            responsesEdu.Add(response);
        }

        public static void DeleteResponce(Education response)
        {
            responsesEdu.Remove(response);
        }

    }
}
