using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;

namespace BookStore.Repositories
{
    public class CategoryRepository : IDisposable
    {
        private Database1Entities1 db;
        private Category category;

        public CategoryRepository()
        {
            db = Database.getDb();
            category = new Category();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertCategory(string CategoryName)
        {
            category = CategoryFactory.createCategory(CategoryName);
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public void deleteCategory(int CategoryID)
        {
            category = findCategoryByID(CategoryID);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public void updateCategory(int CategoryID, string CategoryName)
        {
            category = findCategoryByID(CategoryID);
            if (category != null)
            {
                category.CategoryName = CategoryName;
                db.SaveChanges();
            }
        }

        public Category findCategoryByID(int CategoryID)
        {
            category = db.Categories.Find(CategoryID);
            return category;
        }

        public List<Category> getAllCategory()
        {
            List<Category> list = new List<Category>();
            list = (from c in db.Categories select c).ToList();
            return list;
        }
    }
}