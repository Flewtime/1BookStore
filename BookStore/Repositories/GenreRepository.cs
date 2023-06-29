using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class GenreRepository : IDisposable
    {
        private Database1Entities1 db;
        private Genre genre;

        public GenreRepository()
        {
            db = Database.getDb();
            genre = new Genre();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertGenre(string GenreName)
        {
            genre = GenreFactory.createGenre(GenreName);
            db.Genres.Add(genre);
            db.SaveChanges();
        }

        public void deleteGenre(int GenreID)
        {
            genre = findGenreByID(GenreID);
            if (genre != null)
            {
                db.Genres.Remove(genre);
                db.SaveChanges();
            }
        }

        public void updateGenre(int GenreID, string GenreName)
        {
            genre = findGenreByID(GenreID);
            if (genre != null)
            {
                genre.GenreName = GenreName;
                db.SaveChanges();
            }
        }

        public Genre findGenreByID(int GenreID)
        {
            genre = db.Genres.Find(GenreID);
            return genre;
        }

        public List<Genre> getAllGenre()
        {
            List<Genre> list = new List<Genre>();
            list = (from g in db.Genres select g).ToList();
            return list;
        }
    }
}