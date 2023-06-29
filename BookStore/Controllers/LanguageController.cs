using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class LanguageController
    {
        private LanguageHandler languageHandler;

        public LanguageController()
        {
            languageHandler = new LanguageHandler();
        }

        public string insertLanguage(string LanguageName, Boolean checkLanguageName)
        {
            string validate = validateLanguage(LanguageName, checkLanguageName);
            if(validate.Equals("Insert Success!"))
            {
                languageHandler.insertLanguage(LanguageName);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteLanguage(int LanguageID, string Path)
        {
            languageHandler.deleteLanguage(LanguageID, Path);
        }

        public string updateLanguage(int LanguageID, string LanguageName, Boolean checkLanguageName)
        {
            string validate = validateLanguage(LanguageName, checkLanguageName);
            if(validate.Equals("Insert Success!"))
            {
                languageHandler.updateLanguage(LanguageID, LanguageName);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public Language findLanguageByID(int LanguageID)
        {
            return languageHandler.findLanguageByID(LanguageID);
        }

        public List<Language> getAllLanguage()
        {
            return languageHandler.getAllLanguage();
        }

        private string validateLanguage(string LanguageName, Boolean checkLanguageName)
        {
            Regex validateLanguageName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");

            Boolean isLanguageNameExist = false;
            List<Language> languageList = getAllLanguage();
            foreach (var language in languageList)
            {
                if (language.LanguageName.Equals(LanguageName))
                {
                    isLanguageNameExist = true;
                    break;
                }
            }

            if (LanguageName == "")
            {
                return "Please Fill All The Fields!";
            }
            else if (LanguageName.Length < 2)
            {
                return "Language Name Must be More Than 2 Characters!";
            }
            else if(!validateLanguageName.IsMatch(LanguageName))
            {
                return "Please Enter A Valid Language Name!";
            }
            else if (checkLanguageName && isLanguageNameExist)
            {
                return "Language Name Already Exist, Please Enter Another Language Name!";
            }

            return "Insert Success!";
        }
    }
}