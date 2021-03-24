using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IS413_Movie_Web_App_ZS.Models
{
    public class Add_Movie_Data
    {
        [Key, Required]
        public int MovieID { get; set; }
        [Required]
        public String Movie_Title { get; set; }
        [Required]
        public String Category { get; set; }
        [Required]
        public String Director { get; set; }
        [Required]
        public String Rating { get; set; }
        /* 1888 was chosen as the starting year since research showed that the first piece of film that could be viewed was done in 1888*/
        [Required, Range(1888,2021)]
        public String Year { get; set; }

        //This property does not represent whether the record has been edited, but rather if the movie is an edited version of another movie
        public bool Edited { get; set; } = false;
        public String Lent_To { get; set; }
        [MaxLength(25)]
        public String Notes { get; set; }
    }
}
