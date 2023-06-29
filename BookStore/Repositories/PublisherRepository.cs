using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class PublisherRepository : IDisposable
    {
        private Database1Entities1 db;
        private Model.Publisher publisher;

        public PublisherRepository()
        {
            db = Database.getDb();
            publisher = new Model.Publisher();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertPublisher(string PublisherCode, string PublisherName)
        {
            publisher = PublisherFactory.createPublisher(PublisherCode, PublisherName);
            db.Publishers.Add(publisher);
            db.SaveChanges();
        }

        public void deletePublisher(int PublisherID)
        {
            publisher = findPublisherByID(PublisherID);
            if (publisher != null)
            {
                db.Publishers.Remove(publisher);
                db.SaveChanges();
            }
        }

        public void updatePublisher(int PublisherID, string PublisherCode, string PublisherName)
        {
            publisher = findPublisherByID(PublisherID);
            if (publisher != null)
            {
                publisher.PublisherCode = PublisherCode;
                publisher.PublisherName = PublisherName;
                db.SaveChanges();
            }
        }

        public Publisher findPublisherByID(int PublisherID)
        {
            publisher = db.Publishers.Find(PublisherID);
            return publisher;
        }

        public Publisher findPublisherByCode(string PublisherCode)
        {
            publisher = (from p in db.Publishers where p.PublisherCode.Equals(PublisherCode) select p).FirstOrDefault();
            return publisher;
        }

        public List<Model.Publisher> getAllPublisher()
        {
            List<Model.Publisher> list = new List<Model.Publisher>();
            list = (from p in db.Publishers select p).ToList();
            return list;
        }
    }
}