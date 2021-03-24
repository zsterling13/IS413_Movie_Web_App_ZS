using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS413_Movie_Web_App_ZS.Models
{
    public class MovieDBContext : DbContext
    {
        //Constructor for the class
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        {

        }
        //DbSet or Table of Movie objects
        public DbSet<Add_Movie_Data> Movies { get; set; }
    }
}
