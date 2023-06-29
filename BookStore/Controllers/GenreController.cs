using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class GenreController
    {
        private GenreHandler genreHandler;

        public GenreController()
        {
            genreHandler = new GenreHandler();
        }

        public string insertGenre(string GenreName, Boolean checkGenreName)
        {
            string validate = validateGenre(GenreName, checkGenreName);
            if(validate.Equals("Insert Success!"))
            {
                genreHandler.insertGenre(GenreName);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteGenre(int GenreID, string Path)
        {
            genreHandler.deleteGenre(GenreID, Path);
        }

        public string updateGenre(int GenreID, string GenreName, Boolean checkGenreName)
        {
            string validate = validateGenre(GenreName, checkGenreName);
            if(validate.Equals("Insert Success!"))
            {
                genreHandler.updateGenre(GenreID, GenreName);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public Genre findGenreByID(int GenreID)
        {
            return genreHandler.findGenreByID(GenreID);
        }

        public List<Genre> getAllGenre()
        {
            return genreHandler.getAllGenre();
        }

        private string validateGenre(string GenreName, Boolean checkGenreName)
        {
            Regex validateGenreName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");

            Boolean isGenreNameExist = false;
            List<Genre> genreList = getAllGenre();
            foreach (var genre in genreList)
            {
                if (genre.GenreName.Equals(GenreName))
                {
                    isGenreNameExist = true;
                    break;
                }
            }

            if (GenreName == "")
            {
                return "Please Fill All The Fields!";
            }
            else if (GenreName.Length < 2)
            {
                return "Genre Name Must be More Than 2 Characters!";
            }
            else if (!validateGenreName.IsMatch(GenreName))
            {
                return "Please Enter A Valid Genre Name!";
            }
            else if (checkGenreName && isGenreNameExist)
            {
                return "Genre Name Already Exist, Please Enter Another Genre Name!";
            }

            return "Insert Success!";
        }
    }
}