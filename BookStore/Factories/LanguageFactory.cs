using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class LanguageFactory
    {
        public static Language createLanguage(string LanguageName)
        {
            Language language = new Language();
            language.LanguageName = LanguageName;
            return language;
        }
    }
}