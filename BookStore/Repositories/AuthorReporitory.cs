using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class AuthorRepository : IDisposable
    {
        private Database1Entities1 db;
        private Author author;

        public AuthorRepository()
        {
            db = Database.getDb();
            author = new Author ();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertAuthor(string AuthorName, DateTime AuthorDOB, string AuthorBiography, string AuthorImage)
        {
            author = AuthorFactory.createAuthor(AuthorName, AuthorDOB, AuthorBiography, AuthorImage);
            db.Authors.Add(author);
            db.SaveChanges();
        }

        public void deleteAuthor(int AuthorID, string Path)
        {
            author = findAuthorByID(AuthorID);
            if (author != null)
            {
                System.IO.File.Delete(Path + "/Assets/Authors/" + author.AuthorImage);
                db.Authors.Remove(author);
                db.SaveChanges();
            }
        }

        public void updateAuthor(int AuthorID, string AuthorName, DateTime AuthorDOB, string AuthorBiography, string AuthorImage)
        {
            author = findAuthorByID(AuthorID);
            if (author != null)
            {
                author.AuthorName = AuthorName;
                author.AuthorDOB = AuthorDOB;
                author.AuthorBiography = AuthorBiography;
                author.AuthorImage = AuthorImage;
                db.SaveChanges();
            }
        }

        public Author findAuthorByID(int AuthorID)
        {
            author = db.Authors.Find(AuthorID);
            return author;
        }

        public List<Author> getAllAuthor()
        {
            List<Author> list = new List<Author>();
            list = (from a in db.Authors select a).ToList();
            return list;
        }
    }
}