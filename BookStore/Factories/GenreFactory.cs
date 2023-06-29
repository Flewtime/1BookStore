using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class GenreFactory
    {
        
        public static Genre createGenre(string GenreName)
        {
            Genre genre = new Genre();
            genre.GenreName = GenreName;
            return genre;
        }
    }
}