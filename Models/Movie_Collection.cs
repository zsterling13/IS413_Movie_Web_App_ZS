using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS413_Movie_Web_App_ZS.Models
{
    public class Movie_Collection
    {
        private static List<Add_Movie_Data> applications = new List<Add_Movie_Data>();

        public static IEnumerable<Add_Movie_Data> Applications => applications;

        public static void AddApplication(Add_Movie_Data application)
        {
            applications.Add(application);
        }
    }
}
