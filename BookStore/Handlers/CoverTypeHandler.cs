using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class CoverTypeHandler
    {
        private CoverTypeRepository coverTypeRepository;
        private BookRepository bookRepository;
        private BookHandler bookHandler;

        public CoverTypeHandler()
        {
            coverTypeRepository = new CoverTypeRepository();
            bookRepository = new BookRepository();
            bookHandler = new BookHandler();
        }

        public void insertCoverType(string CoverTypeName, string CoverTypeMaterial)
        {
            coverTypeRepository.insertCoverType(CoverTypeName, CoverTypeMaterial);
        }

        public void deleteCoverType(int CoverTypeID, string Path)
        {
            List<Book> bookList = bookRepository.getAllBookByCoverType(CoverTypeID);
            if (bookList.Any())
            {
                foreach (Book b in bookList)
                {
                    bookHandler.deleteBook(b.BookID, Path);
                }
            }

            coverTypeRepository.deleteCoverType(CoverTypeID);
        }

        public void updateCoverType(int CoverTypeID, string CoverTypeName, string CoverTypeMaterial)
        {
            coverTypeRepository.updateCoverType(CoverTypeID, CoverTypeName, CoverTypeMaterial);
        }

        public CoverType findCoverTypeByID(int CoverTypeID)
        {
            return coverTypeRepository.findCoverTypeByID(CoverTypeID);
        }

        public List<CoverType> getAllCoverType()
        {
            return coverTypeRepository.getAllCoverType();
        }
    }
}