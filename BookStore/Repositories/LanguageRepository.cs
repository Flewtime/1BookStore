using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;

namespace BookStore.Repositories
{
    public class LanguageRepository : IDisposable
    {
        private Database1Entities1 db;
        private Language language;

        public LanguageRepository()
        {
            db = Database.getDb();
            language = new Language();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertLanguage(string LanguageName)
        {
            language = LanguageFactory.createLanguage(LanguageName);
            db.Languages.Add(language);
            db.SaveChanges();
        }

        public void deleteLanguage(int LanguageID)
        {
            language = findLanguageByID(LanguageID);
            if (language != null)
            {
                db.Languages.Remove(language);
                db.SaveChanges();
            }
        }

        public void updateLanguage(int LanguageID, string LanguageName)
        {
            language = findLanguageByID(LanguageID);
            if (language != null)
            {
                language.LanguageName = LanguageName;
                db.SaveChanges();
            }
        }

        public Language findLanguageByID(int LanguageID)
        {
            language = db.Languages.Find(LanguageID);
            return language;
        }

        public List<Language> getAllLanguage()
        {
            List<Language> list = new List<Language>();
            list = (from l in db.Languages select l).ToList();
            return list;
        }
    }
}