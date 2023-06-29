using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class TransactionHeaderRepository : IDisposable
    {
        private Database1Entities1 db;
        private TransactionHeader transactionHeader;

        public TransactionHeaderRepository()
        {
            db = Database.getDb();
            transactionHeader = new TransactionHeader();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertTransactionHeader(int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            transactionHeader = TransactionHeaderFactory.createTransactionHeader(TransactionPrice, TransactionDateTime, UserID, PaymentID, ShipmentID);
            db.TransactionHeaders.Add(transactionHeader);
            db.SaveChanges();
        }

        public void deleteTransactionHeader(int TransactionID)
        {
            transactionHeader = findTransactionHeaderByID(TransactionID);
            if (transactionHeader != null)
            {
                db.TransactionHeaders.Remove(transactionHeader);
                db.SaveChanges();
            }
        }

        public void updateTransactionHeader(int TransactionID, int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            transactionHeader = findTransactionHeaderByID(TransactionID);
            if (transactionHeader != null)
            {
                transactionHeader.TransactionPrice = TransactionPrice;
                transactionHeader.TransactionDateTime = TransactionDateTime;
                transactionHeader.UserID = UserID;
                transactionHeader.PaymentID = PaymentID;
                transactionHeader.ShipmentID = ShipmentID;
                db.SaveChanges();
            }
        }

        public TransactionHeader findTransactionHeaderByID(int TransactionID)
        {
            transactionHeader = db.TransactionHeaders.Find(TransactionID);
            return transactionHeader;
        }

        public TransactionHeader findLastTransactionHeader()
        {
            transactionHeader = (from th in db.TransactionHeaders select th).ToList().LastOrDefault();
            if(transactionHeader != null)
            {
                return transactionHeader;
            }
            else
            {
                return null;
            }
        }

        public List<TransactionHeader> getAllTransactionHeader()
        {
            List<TransactionHeader> list = new List<TransactionHeader>();
            list = (from th in db.TransactionHeaders select th).ToList();
            return list;
        }

        public List<TransactionHeader> getAllTransactionHeaderByUser(int UserID)
        {
            List<TransactionHeader> list = new List<TransactionHeader>();
            list = (from th in db.TransactionHeaders where th.UserID == UserID select th).ToList();
            return list;
        }

        public List<TransactionHeader> getAllTransactionHeaderByPayment(int PaymentID)
        {
            List<TransactionHeader> list = new List<TransactionHeader>();
            list = (from th in db.TransactionHeaders where th.PaymentID == PaymentID select th).ToList();
            return list;
        }

        public List<TransactionHeader> getAllTransactionHeaderByShipment(int ShipmentID)
        {
            List<TransactionHeader> list = new List<TransactionHeader>();
            list = (from th in db.TransactionHeaders where th.ShipmentID == ShipmentID select th).ToList();
            return list;
        }

        public List<TransactionHeader> getAllTransactionHeaderByUserFinished(int UserID)
        {
            List<TransactionHeader> list = new List<TransactionHeader>();
            list = (from th in db.TransactionHeaders where th.UserID == UserID && th.Shipment.ShipmentStatus.Equals("Delivered") && th.Payment.PaymentStatus.Equals("Paid") select th).ToList();
            return list;
        }
    }
}