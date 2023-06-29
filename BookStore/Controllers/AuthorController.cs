using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class AuthorController
    {
        private AuthorHandler authorHandler;

        public AuthorController()
        {
            authorHandler = new AuthorHandler();
        }

        public string insertAuthor(string AuthorName, string AuthorDOB, string AuthorBiography, string AuthorImage, Boolean checkAuthorName)
        {
            string validate = validateAuthor(AuthorName, AuthorDOB.ToString(), AuthorBiography, AuthorImage, checkAuthorName);

            if(validate.Equals("Insert Success!"))
            {
                authorHandler.insertAuthor(AuthorName, DateTime.Parse(AuthorDOB), AuthorBiography, AuthorImage);
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteAuthor(int AuthorID, string Path)
        {
            authorHandler.deleteAuthor(AuthorID, Path);
        }

        public string updateAuthor(int AuthorID, string AuthorName, string AuthorDOB, string AuthorBiography, string AuthorImage, Boolean checkAuthorName)
        {
            string validate = validateAuthor(AuthorName, AuthorDOB, AuthorBiography, AuthorImage, checkAuthorName);
            if(validate.Equals("Insert Success!"))
            {
                authorHandler.updateAuthor(AuthorID, AuthorName, DateTime.Parse(AuthorDOB), AuthorBiography, AuthorImage);
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public Author findAuthorByID(int AuthorID)
        {
            return authorHandler.findAuthorByID(AuthorID);
        }

        public List<Author> getAllAuthor()
        {
            return authorHandler.getAllAuthor();
        }
        
        private string validateAuthor(string AuthorName, string AuthorDOB, string AuthorBiography, string AuthorImage , Boolean checkAuthorName)
        {
            Regex validateName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");
            Regex validateImage = new Regex("[^\\s]+(\\.(?i)(jpe?g|png|gif|bmp|jfif|))$");

            Boolean isAuthorNameExist = false;
            List<Author> authorList = getAllAuthor();
            foreach(Author author in authorList)
            {
                if (author.AuthorName.Equals(AuthorName))
                {
                    isAuthorNameExist = true;
                    break;
                }
            }

            if(AuthorName == "" || AuthorDOB == "" || AuthorBiography == "" || AuthorImage == "")
            {
                return "Please Fill or Choose All The Fields!";
            }
            else if(!validateName.IsMatch(AuthorName))
            {
                return "Please Enter A Valid Name!";
            }
            else if(checkAuthorName && isAuthorNameExist)
            {
                return "Author Name Already Exist, Please Enter Another Author Name!";
            }
            else if(!validateImage.IsMatch(AuthorImage))
            {
                return "Please Upload A Valid Image!";
            }

            return "Insert Success!";
        }
    }
}