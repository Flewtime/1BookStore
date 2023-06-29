using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class AuthorHandler
    {
        private AuthorRepository authorRepository;
        private BookRepository bookRepository;
        private BookHandler bookHandler;

        public AuthorHandler()
        {
            authorRepository = new AuthorRepository();
            bookRepository = new BookRepository();
            bookHandler = new BookHandler();
        }

        public void insertAuthor(string AuthorName, DateTime AuthorDOB, string AuthorBiography, string AuthorImage)
        {
            authorRepository.insertAuthor(AuthorName, AuthorDOB, AuthorBiography, AuthorImage);
        }

        public void deleteAuthor(int AuthorID, string Path)
        {
            List<Book> bookList = bookRepository.getAllBookByAuthor(AuthorID);
            if(bookList.Any())
            {
                foreach (Book b in bookList)
                {
                    bookHandler.deleteBook(b.BookID, Path);
                }
            }

            authorRepository.deleteAuthor(AuthorID, Path);
        }

        public void updateAuthor(int AuthorID, string AuthorName, DateTime AuthorDOB, string AuthorBiography, string AuthorImage)
        {
            authorRepository.updateAuthor(AuthorID, AuthorName, AuthorDOB, AuthorBiography, AuthorImage);
        }

        public Author findAuthorByID(int AuthorID)
        {
            return authorRepository.findAuthorByID(AuthorID);
        }

        public List<Author> getAllAuthor()
        {
            return authorRepository.getAllAuthor();
        }
    }
}