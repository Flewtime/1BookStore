using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class PublisherController
    {
        private PublisherHandler publisherHandler;

        public PublisherController()
        {
            publisherHandler = new PublisherHandler();
        }

        public void insertPublisher(string PublisherCode, string PublisherName)
        {
            publisherHandler.insertPublisher(PublisherCode, PublisherName);
        }

        public void deletePublisher(int PublisherID, string Path)
        {
            publisherHandler.deletePublisher(PublisherID, Path);
        }

        public void updatePublisher(int PublisherID, string PublisherCode, string PublisherName)
        {
            publisherHandler.updatePublisher(PublisherID, PublisherCode, PublisherName);
        }

        public Model.Publisher findPublisherByID(int PublisherID)
        {
            return publisherHandler.findPublisherByID(PublisherID);
        }

        public Model.Publisher findPublisherByCode(string PublisherCode)
        {
            return publisherHandler.findPublisherByCode(PublisherCode);
        }

        public List<Model.Publisher> getAllPublisher()
        {
            return publisherHandler.getAllPublisher();
        }

        private string validatePublisher(string PublisherCode, string PublisherName, Boolean checkPublisherCode, Boolean checkPublisherName)
        {
            Regex validatePublisherCode = new Regex("^\\d+$");
            Regex validatePublisherName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");

            Boolean isPublisherCodeExist = false;
            Boolean isPublisherNameExist = false;
            List<Model.Publisher> publisherList = getAllPublisher();
            foreach (var publisher in publisherList)
            {
                if (publisher.PublisherCode.Equals(PublisherCode))
                {
                    isPublisherCodeExist = true;
                }

                if (publisher.PublisherName.Equals(PublisherName))
                {
                    isPublisherNameExist = true;
                }
            }

            if (PublisherCode == "" || PublisherName == "")
            {
                return "Please Fill All The Fields!";
            }
            else if(!validatePublisherCode.IsMatch(PublisherCode))
            {
                return "Please Enter A Valid Publisher Code!";
            }
            else if (checkPublisherCode && isPublisherCodeExist)
            {
                return "Publisher Code Already Exist, Please Enter Another Publisher Code That Only Contains Numbers!";
            }
            else if(PublisherName.Length < 2)
            {
                return "Publisher Name Must be More Than 2 Characters!";
            }
            else if (!validatePublisherName.IsMatch(PublisherName))
            {
                return "Please Enter A Valid Publisher Name!";
            }
            else if(checkPublisherName && isPublisherNameExist)
            {
                return "Publisher Name Already Exist, Please Enter Another Publisher Name!";
            }

            return "Insert Success!";
        }
    }
}