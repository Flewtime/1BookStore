using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class LanguageHandler
    {
        private LanguageRepository languageRepository;
        private BookRepository bookRepository;
        private BookHandler bookHandler;

        public LanguageHandler()
        {
            languageRepository = new LanguageRepository();
            bookRepository = new BookRepository();
            bookHandler = new BookHandler();
        }

        public void insertLanguage(string LanguageName)
        {
            languageRepository.insertLanguage(LanguageName);
        }

        public void deleteLanguage(int LanguageID, string Path)
        {
            List<Book> bookList = bookRepository.getAllBookByLanguage(LanguageID);
            if (bookList.Any())
            {
                foreach(Book b in bookList)
                {
                    bookHandler.deleteBook(b.BookID, Path);
                }
            }

            languageRepository.deleteLanguage(LanguageID);
        }

        public void updateLanguage(int LanguageID, string LanguageName)
        {
            languageRepository.updateLanguage(LanguageID, LanguageName);
        }

        public Language findLanguageByID(int LanguageID)
        {
            return languageRepository.findLanguageByID(LanguageID);
        }

        public List<Language> getAllLanguage()
        {
            return languageRepository.getAllLanguage();
        }
    }
}