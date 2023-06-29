using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class GenreHandler
    {
        private GenreRepository genreRepository;
        private BookRepository bookRepository;
        private BookHandler bookHandler;

        public GenreHandler()
        {
            genreRepository = new GenreRepository();
            bookRepository = new BookRepository();
            bookHandler = new BookHandler();
        }

        public void insertGenre(string GenreName)
        {
            genreRepository.insertGenre(GenreName);
        }

        public void deleteGenre(int GenreID, string Path)
        {
            List<Book> bookList = bookRepository.getAllBookByGenre(GenreID);
            if(bookList.Any())
            {
                foreach (Book b in bookList)
                {
                    bookHandler.deleteBook(b.BookID, Path);
                }
            }

            genreRepository.deleteGenre(GenreID);
        }

        public void updateGenre(int GenreID, string GenreName)
        {
            genreRepository.updateGenre(GenreID, GenreName);
        }

        public Genre findGenreByID(int GenreID)
        {
            return genreRepository.findGenreByID(GenreID);
        }

        public List<Genre> getAllGenre()
        {
            return genreRepository.getAllGenre();
        }
    }
}