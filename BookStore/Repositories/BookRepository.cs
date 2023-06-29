using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class BookRepository : IDisposable
    {
        private Database1Entities1 db;
        private Book book;

        public BookRepository()
        {
            db = Database.getDb();
            book = new Book();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertBook(string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID)
        {
            book = BookFactory.createBook(BookISBN, BookTitle, BookPrice, BookStock, BookPage, BookWeight, BookDimension, BookSynopsis, BookPublishedDate, BookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID);
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void deleteBook(int BookID, string Path)
        {
            book = findBookByID(BookID);
            if (book != null)
            {
                System.IO.File.Delete(Path + "/Assets/Books/" + book.BookImage);
                db.Books.Remove(book);
                db.SaveChanges();
            }
        }

        public void updateBook(int BookID, string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID)
        {
            book = findBookByID(BookID);
            if (book != null)
            {
                book.BookISBN = BookISBN;
                book.BookTitle = BookTitle;
                book.BookPrice = BookPrice;
                book.BookStock = BookStock;
                book.BookPage = BookPage;
                book.BookWeight = BookWeight;
                book.BookDimension = BookDimension;
                book.BookSynopsis = BookSynopsis;
                book.BookPublishedDate = BookPublishedDate;
                book.BookImage = BookImage;
                book.GenreID = GenreID;
                book.CategoryID = CategoryID;
                book.AuthorID = AuthorID;
                book.PublisherID = PublisherID;
                book.LanguageID = LanguageID;
                book.CoverTypeID = CoverTypeID;
                db.SaveChanges();
            }
        }

        public Book findBookByID(int BookID)
        {
            book = db.Books.Find(BookID);
            return book;
        }

        public List<Book> getAllBook()
        {
           List<Book> list = new List<Book>();
           list = (from b in db.Books select b).ToList();
           return list;
        }

        public List<Book> getAllBookByGenre(int GenreID)
        {
            List<Book> list = new List<Book>();
            list = (from b in db.Books where b.GenreID == GenreID select b).ToList();
            return list;
        }

        public List<Book> getAllBookByCategory(int CategoryID)
        {
            List<Book> list = new List<Book>();
            list = (from b in db.Books where b.CategoryID == CategoryID select b).ToList();
            return list;
        }

        public List<Book> getAllBookByAuthor(int AuthorID)
        {
            List<Book> list = new List<Book>();
            list = (from b in db.Books where b.AuthorID == AuthorID select b).ToList();
            return list;
        }

        public List<Book> getAllBookByPublisher(int PublisherID)
        {
            List<Book> list = new List<Book>();
            list = (from b in db.Books where b.PublisherID == PublisherID select b).ToList();
            return list;
        }

        public List<Book> getAllBookByLanguage(int LanguageID)
        {
            List<Book> list = new List<Book>();
            list = (from b in db.Books where b.LanguageID == LanguageID select b).ToList();
            return list;
        }

        public List<Book> getAllBookByCoverType(int CoverTypeID)
        {
            List<Book> list = new List<Book>();
            list = (from b in db.Books where b.CoverTypeID == CoverTypeID select b).ToList();
            return list;
        }

        public void addStock(int BookID, int BookStock)
        {
            book = findBookByID(BookID);
            if (book != null)
            {
                book.BookStock += BookStock;
                db.SaveChanges();
            }
        }

        public void reduceStock(int BookID, int BookStock)
        {
            book = findBookByID(BookID);
            if (book != null)
            {
                book.BookStock -= BookStock;
                db.SaveChanges();
            }
        }
    }
}