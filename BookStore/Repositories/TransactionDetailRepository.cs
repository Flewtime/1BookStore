using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class TransactionDetailRepository : IDisposable
    {
        private Database1Entities1 db;
        private TransactionDetail transactionDetail;

        public TransactionDetailRepository()
        {
            db = Database.getDb();
            transactionDetail = new TransactionDetail();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            transactionDetail = TransactionDetailFactory.createTransactionDetail(TransactionID, BookID, Qty);
            db.TransactionDetails.Add(transactionDetail);
            db.SaveChanges();
        }

        public void deleteTransactionDetail(int TransactionID, int BookID)
        {
            transactionDetail = findTransactionDetailByID(TransactionID, BookID);
            if (transactionDetail != null)
            {
                db.TransactionDetails.Remove(transactionDetail);
                db.SaveChanges();
            }
        }

        public void deleteAllTransactionDetailByTransaction(int TransactionID)
        {
            List<TransactionDetail> list = getAllTransactionDetailByTransaction(TransactionID);
            if (list.Any())
            {
                foreach (TransactionDetail td in list)
                {
                    db.TransactionDetails.Remove(td);
                    db.SaveChanges();
                }
            }
        }

        public void updateTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            transactionDetail = findTransactionDetailByID(TransactionID, BookID);
            if (transactionDetail != null)
            {
                transactionDetail.TransactionID = TransactionID;
                transactionDetail.BookID = BookID;
                transactionDetail.Qty = Qty;
                db.SaveChanges();
            }
        }

        public TransactionDetail findTransactionDetailByID(int TransactionID, int BookID)
        {
            transactionDetail = (from td in db.TransactionDetails where td.TransactionID == TransactionID && td.BookID == BookID select td).FirstOrDefault();
            return transactionDetail;
        }

        public List<TransactionDetail> getAllTransactionDetail()
        {
            List<TransactionDetail> list = new List<TransactionDetail>();
            list = (from td in db.TransactionDetails select td).ToList();
            return list;
        }

        public List<TransactionDetail> getAllTransactionDetailByTransaction(int TransactionID)
        {
            List<TransactionDetail> list = new List<TransactionDetail>();
            list = (from td in db.TransactionDetails where td.TransactionID == TransactionID select td).ToList();
            return list;
        }
    }
}