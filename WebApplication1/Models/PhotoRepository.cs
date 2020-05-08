using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PhotoRepository
    {
        private static List<Photo> responsesPh = new List<Photo>();
        public static IEnumerable<Photo> Responses
        {
            get
            {
                return responsesPh;
            }
        }

        public static void AddResponse(Photo response)
        {
            responsesPh.Add(response);
        }
        public static void DeleteResponce(Photo response)
        {
            responsesPh.Remove(response);
        }
    }
}
