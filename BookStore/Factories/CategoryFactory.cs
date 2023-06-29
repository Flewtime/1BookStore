using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class CategoryFactory
    {
        public static Category createCategory(string CategoryName)
        {
            Category category = new Category();
            category.CategoryName = CategoryName;
            return category;
        }
    }
}