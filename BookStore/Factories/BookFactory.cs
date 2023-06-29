using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class BookFactory
    {
        public static Book createBook(string BookISBN, string BookTitle, int BookPrice, int BookStock, int BookPage, int BookWeight, string BookDimension, string BookSynopsis, DateTime BookPublishedDate, string BookImage, int GenreID, int CategoryID, int AuthorID, int PublisherID, int LanguageID, int CoverTypeID)
        {
            Book book = new Book();
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
            return book;    
        }
    }
}