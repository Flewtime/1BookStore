using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class AuthorFactory
    {
        public static Author createAuthor(string AuthorName, DateTime AuthorDOB, string AuthorBiography, string AuthorImage)
        {
            Author author = new Author();
            author.AuthorName = AuthorName;
            author.AuthorDOB = AuthorDOB;
            author.AuthorBiography = AuthorBiography;
            author.AuthorImage = AuthorImage;
            return author;
        }
    }
}