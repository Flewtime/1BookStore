using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class CategoryHandler
    {
        CategoryRepository categoryRepository;
        BookRepository bookRepository;
        BookHandler bookHandler;

        public CategoryHandler()
        {
            categoryRepository = new CategoryRepository();
            bookRepository = new BookRepository();
            bookHandler = new BookHandler();
        }

        public void insertCategory(string CategoryName)
        {
            categoryRepository.insertCategory(CategoryName);
        }

        public void deleteCategory(int CategoryID, string Path)
        {
            List<Book> bookList = bookRepository.getAllBookByCategory(CategoryID);
            if (bookList.Any())
            {
                foreach(Book b in bookList)
                {
                    bookHandler.deleteBook(b.BookID, Path);
                }
            }

            categoryRepository.deleteCategory(CategoryID);
        }

        public void updateCategory(int CategoryID, string CategoryName)
        {
            categoryRepository.updateCategory(CategoryID, CategoryName);
        }

        public Category findCategoryByID(int CategoryID)
        {
            return categoryRepository.findCategoryByID(CategoryID);
        }

        public List<Category> getAllCategory()
        {
            return categoryRepository.getAllCategory();
        }
    }
}