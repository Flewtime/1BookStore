using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class BookHandler
    {
        private BookRepository bookRepository;
        private ReviewRepository reviewRepository;
        private CartRepository cartRepository;
        private OrderDetailRepository orderDetailRepository;

        public BookHandler()
        {
            bookRepository = new BookRepository();
            reviewRepository = new ReviewRepository();
            cartRepository = new CartRepository();
            orderDetailRepository = new OrderDetailRepository();
        }

        public void insertBook(string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID)
        {
            bookRepository.insertBook(BookISBN, BookTitle, BookPrice, BookStock, BookPage, BookWeight, BookDimension, BookSynopsis, BookPublishedDate, BookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID);
        }

        public void deleteBook(int BookID, string Path)
        {
            reviewRepository.deleteAllReviewByBook(BookID);
            cartRepository.deleteAllCartByBook(BookID);
            orderDetailRepository.deleteAllOrderDetailByBook(BookID);
            bookRepository.deleteBook(BookID, Path);
        }

        public void updateBook(int BookID, string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID)
        {
            bookRepository.updateBook(BookID, BookISBN, BookTitle, BookPrice, BookStock, BookPage, BookWeight, BookDimension, BookSynopsis, BookPublishedDate, BookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID);
        }

        public Book findBookByID(int BookID)
        {
            return bookRepository.findBookByID(BookID);
        }

        public List<Book> getAllBook()
        {
            return bookRepository.getAllBook();
        }

        public List<Book> getAllBookByGenre(int GenreID)
        {
            return bookRepository.getAllBookByGenre(GenreID);
        }

        public List<Book> getAllBookByCategory(int CategoryID)
        {
            return bookRepository.getAllBookByCategory(CategoryID);
        }

        public List<Book> getAllBookByAuthor(int AuthorID)
        {
            return bookRepository.getAllBookByAuthor(AuthorID);
        }

        public List<Book> getAllBookByLanguage(int LanguageID)
        {
            return bookRepository.getAllBookByLanguage(LanguageID);
        }

        public List<Book> getAllBookByPublisher(int PublisherID)
        {
            return bookRepository.getAllBookByPublisher(PublisherID);
        }
    }
}