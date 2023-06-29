using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class PublisherFactory
    {

        public static Model.Publisher createPublisher(string PublisherCode, string PublisherName)
        {
            Model.Publisher publisher = new Model.Publisher();
            publisher.PublisherCode = PublisherCode;
            publisher.PublisherName = PublisherName;
            return publisher;
        }
    }
}