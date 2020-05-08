using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ExpRepository
    {
        private static List<Experience> responsesExp = new List<Experience>();
        public static IEnumerable<Experience> Responses
        {
            get
            {
                return responsesExp;
            }
        }

        public static void AddResponse(Experience response)
        {
            responsesExp.Add(response);
        }

        public static void DeleteResponce(Experience response)
        {
            responsesExp.Remove(response);
        }
    }
}
