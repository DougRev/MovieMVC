﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models.Movie
{
    public class MovieEdit
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Genre { get; set; }
        public int ReleaseDate { get; set; }

    }
}
