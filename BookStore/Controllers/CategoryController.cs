using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class CategoryController
    {
        private CategoryHandler categoryHandler;

        public CategoryController()
        {
            categoryHandler = new CategoryHandler();
        }

        public string insertCategory(string CategoryName, Boolean checkCategoryName)
        {
            string validate = validateCategory(CategoryName, checkCategoryName);
            if (validate.Equals("Insert Success!"))
            {
                categoryHandler.insertCategory(CategoryName);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteCategory(int CategoryID, string Path)
        {
            categoryHandler.deleteCategory(CategoryID, Path);
        }

        public string updateCategory(int CategoryID, string CategoryName, Boolean checkCategoryName)
        {
            string validate = validateCategory(CategoryName, checkCategoryName);
            if(validate.Equals("Insert Success!"))
            {
                categoryHandler.updateCategory(CategoryID, CategoryName);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public Category findCategoryByID(int CategoryID)
        {
            return categoryHandler.findCategoryByID(CategoryID);
        }

        public List<Category> getAllCategory()
        {
            return categoryHandler.getAllCategory();
        }

        private string validateCategory(string CategoryName, Boolean checkCategoryName)
        {
            Regex validateCategoryName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");

            Boolean isCategoryNameExist = false;
            List<Category> categoryList = getAllCategory();
            foreach (var category in categoryList)
            {
                if (category.CategoryName.Equals(CategoryName))
                {
                    isCategoryNameExist = true;
                    break;
                }
            }

            if (CategoryName == "")
            {
                return "Please Fill All The Fields!";
            }
            else if (CategoryName.Length < 2)
            {
                return "Category Name Must be More Than 2 Characters!";
            }
            else if (!validateCategoryName.IsMatch(CategoryName))
            {
                return "Please Enter A Valid Category Name!";
            }
            else if (checkCategoryName && isCategoryNameExist)
            {
                return "Category Name Already Exist, Please Enter Another Category Name!";
            }

            return "Insert Success!";
        }
    }
}