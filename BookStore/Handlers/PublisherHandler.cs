using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class PublisherHandler
    {
        private PublisherRepository publisherRepository;
        private BookRepository bookRepository;
        private BookHandler bookHandler;

        public PublisherHandler()
        {
            publisherRepository = new PublisherRepository();
            bookRepository = new BookRepository();
            bookHandler = new BookHandler();
        }

        public void insertPublisher(string PublisherCode, string PublisherName)
        {
            publisherRepository.insertPublisher(PublisherCode, PublisherName);
        }

        public void deletePublisher(int PublisherID, string Path)
        {
            List<Book> bookList = bookRepository.getAllBookByPublisher(PublisherID);
            if(bookList.Any())
            {
                foreach (Book b in bookList)
                {
                    bookHandler.deleteBook(b.BookID, Path);
                }
            }

            publisherRepository.deletePublisher(PublisherID);
        }

        public void updatePublisher(int PublisherID, string PublisherCode, string PublisherName)
        {
            publisherRepository.updatePublisher(PublisherID, PublisherCode, PublisherName);
        }

        public Model.Publisher findPublisherByID(int PublisherID)
        {
            return publisherRepository.findPublisherByID(PublisherID);
        }

        public Model.Publisher findPublisherByCode(string PublisherCode)
        {
            return publisherRepository.findPublisherByCode(PublisherCode);
        }

        public List<Model.Publisher> getAllPublisher()
        {
            return publisherRepository.getAllPublisher();
        }
    }
}