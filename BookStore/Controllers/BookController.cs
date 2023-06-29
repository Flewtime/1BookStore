using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using System.Text.RegularExpressions;
using System.Diagnostics.Eventing.Reader;

namespace BookStore.Controllers
{
    public class BookController
    {
        private BookHandler bookHandler;

        public BookController()
        {
            bookHandler = new BookHandler();
        }

        public string insertBook(string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID, Boolean checkBookISBN)
        {
            string validate = validateBook(BookISBN, BookTitle, BookPrice.ToString(), BookStock.ToString(), BookPage.ToString(), BookWeight.ToString(), BookDimension, BookSynopsis, BookPublishedDate.ToString(), BookImage, GenreID.ToString(), CategoryID.ToString(), AuthorID.ToString(), PublisherID.ToString(), LanguageID.ToString(), CoverTypeID.ToString(), checkBookISBN);
            if(validate.Equals("Insert Success!"))
            {
                bookHandler.insertBook(BookISBN, BookTitle, BookPrice, BookStock, BookPage, BookWeight, BookDimension, BookSynopsis, BookPublishedDate, BookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteBook(int BookID, string Path)
        {
            bookHandler.deleteBook(BookID, Path);
        }

        public string updateBook(int BookID, string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID, Boolean checkBookISBN)
        {
            string validate = validateBook(BookISBN, BookTitle, BookPrice.ToString(), BookStock.ToString(), BookPage.ToString(), BookWeight.ToString(), BookDimension, BookSynopsis, BookPublishedDate.ToString(), BookImage, GenreID.ToString(), CategoryID.ToString(), AuthorID.ToString(), PublisherID.ToString(), LanguageID.ToString(), CoverTypeID.ToString(), checkBookISBN);
            if(validate.Equals("Insert Success!"))
            {
                bookHandler.updateBook(BookID, BookISBN, BookTitle, BookPrice, BookStock, BookPage, BookWeight, BookDimension, BookSynopsis, BookPublishedDate, BookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public Book findBookByID(int BookID)
        {
            return bookHandler.findBookByID(BookID);
        }

        public List<Book> getAllBook()
        {
            return bookHandler.getAllBook();
        }

        public List<Book> getAllBookByGenre(int GenreID)
        {
            return bookHandler.getAllBookByGenre(GenreID);
        }

        public List<Book> getAllBookByCategory(int CategoryID)
        {
            return bookHandler.getAllBookByCategory(CategoryID);
        }

        public List<Book> getAllBookByAuthor(int AuthorID)
        {
            return bookHandler.getAllBookByAuthor(AuthorID);
        }

        public List<Book> getAllBookByLanguage(int LanguageID)
        {
            return bookHandler.getAllBookByLanguage(LanguageID);
        }

        public List<Book> getAllBookByPublisher(int PublisherID)
        {
            return bookHandler.getAllBookByPublisher(PublisherID);
        }

        private string validateBook(string BookISBN, string BookTitle, string BookPrice, string BookStock, string BookPage, string BookWeight, string BookDimension, string BookSynopsis, string BookPublishedDate, string BookImage, string GenreID, string CategoryID, string AuthorID, string PublisherID, string LanguageID, string CoverTypeID, Boolean checkBookISBN)
        {
            Regex validateBookISBN = new Regex("^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\\d-]+$");
            Regex validateBookPrice = new Regex("^\\d+$");
            Regex validateBookStock = new Regex("^\\d+$");
            Regex validateBookPage = new Regex("^\\d+$");
            Regex validateBookWeight = new Regex("^\\d+$");
            Regex validateBookDimension = new Regex("(\\d+(?:\\.\\d+)?) cm X (\\d+(?:\\.\\d+)?) cm X (\\d+(?:\\.\\d+)?) cm");

            Boolean isBookISBNExist = false;
            List<Book> bookList = getAllBook();
            foreach (Book book in bookList)
            {
                if (book.BookISBN.Equals(BookISBN))
                {
                    isBookISBNExist = true;
                }
            }

            if(BookISBN == "" || BookTitle == "" || BookPrice == "" || BookStock == "" || BookPage == "" || BookWeight == "" || BookDimension == "" || BookSynopsis == "" || BookPublishedDate == "" || BookImage == "" || GenreID == "" || CategoryID == "" || AuthorID == "" || PublisherID == "" || LanguageID == "" || CoverTypeID == "")
            {
                return "Please Fill or Choose or Upload All The Fields!";
            }
            else if (!validateBookISBN.IsMatch(BookISBN))
            {
                return "Please Enter A Valid Book ISBN!";
            }
            else if (checkBookISBN && isBookISBNExist)
            {
                return "Book ISBN Already Exist, Please Enter Another Book ISBN!";
            }
            else if(BookTitle.Length < 2)
            {
                return "Book Title Must be More Than 2 Characters!";
            }
            else if (!validateBookPrice.IsMatch(BookPrice))
            {
                return "Please Enter A Valid Book Price That Only Contains Numbers!";
            }
            else if (!validateBookStock.IsMatch(BookStock))
            {
                return "Please Enter A Valid Book Stock That Only Contains Numbers!";
            }
            else if(int.Parse(BookStock) <= 0)
            {
                return "Book Stock Must Be More Than 0!";
            }
            else if (!validateBookPage.IsMatch(BookPage))
            {
                return "Please Enter A Valid Book Page That Only Contains Numbers!";
            }
            else if(int.Parse(BookPage) <= 0)
            {
                return "Book Page Must Be More Than 0!";
            }
            else if (!validateBookWeight.IsMatch(BookWeight))
            {
                return "Please Enter A Valid Book Weight That Only Contains Numbers!";
            }
            else if(int.Parse(BookWeight) <= 0)
            {
                return "Book Weight Must Be More Than 0!";
            }
            else if (!validateBookDimension.IsMatch(BookDimension))
            {
                return "Please Enter A Valid Book Dimension (Format: xx cm X xx cm X xx cm)!";
            }
            else if(BookSynopsis.Length <= 100)
            {
                return "Book Synopsis Must be More Than 100 Characters!";
            }

            return "Insert Success!";
        }
    }
}