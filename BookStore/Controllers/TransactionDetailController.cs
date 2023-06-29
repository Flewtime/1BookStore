using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;

namespace BookStore.Controllers
{
    public class TransactionDetailController
    {
        private TransactionDetailHandler transactionDetailHandler;

        public TransactionDetailController()
        {
            transactionDetailHandler = new TransactionDetailHandler();
        }

        public void insertTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            transactionDetailHandler.insertTransactionDetail(TransactionID, BookID, Qty);
        }

        public void deleteTransactionDetail(int TransactionID, int BookID)
        {
            transactionDetailHandler.deleteTransactionDetail(TransactionID, BookID);
        }

        public void updateTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            transactionDetailHandler.updateTransactionDetail(TransactionID, BookID, Qty);
        }

        public TransactionDetail findTransactionDetailByID(int TransactionID, int BookID)
        {
            return transactionDetailHandler.findTransactionDetailByID(TransactionID, BookID);
        }

        public List<TransactionDetail> getAllTransactionDetail()
        {
            return transactionDetailHandler.getAllTransactionDetail();
        }

        public List<TransactionDetail> getAllTransactionDetailByTransaction(int TransactionID)
        {
            return transactionDetailHandler.getAllTransactionDetailByTransaction(TransactionID);
        }
    }
}