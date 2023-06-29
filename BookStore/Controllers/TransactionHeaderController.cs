using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;

namespace BookStore.Controllers
{
    public class TransactionHeaderController
    {
        private TransactionHeaderHandler transactionHeaderHandler;

        public TransactionHeaderController()
        {
            transactionHeaderHandler = new TransactionHeaderHandler();
        }

        public void insertTransactionHeader(int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            transactionHeaderHandler.insertTransactionHeader(TransactionPrice, TransactionDateTime, UserID, PaymentID, ShipmentID);
        }

        public void deleteTransactionHeader(int TransactionID)
        {
            transactionHeaderHandler.deleteTransactionHeader(TransactionID);
        }

        public void updateTransactionHeader(int TransactionID, int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            transactionHeaderHandler.updateTransactionHeader(TransactionID, TransactionPrice, TransactionDateTime, UserID, PaymentID, ShipmentID);
        }

        public TransactionHeader findTransactionHeaderByID(int TransactionID)
        {
            return transactionHeaderHandler.findTransactionHeaderByID(TransactionID);
        }

        public TransactionHeader findLastTransactionHeader()
        {
            return transactionHeaderHandler.findLastTransactionHeader();
        }

        public List<TransactionHeader> getAllTransactionHeader()
        {
            return transactionHeaderHandler.getAllTransactionHeader();
        }

        public List<TransactionHeader> getAllTransactionHeaderByUser(int UserID)
        {
            return transactionHeaderHandler.getAllTransactionHeaderByUser(UserID);
        }

        public List<TransactionHeader> getAllTransactionHeaderByPayment(int PaymentID)
        {
            return transactionHeaderHandler.getAllTransactionHeaderByPayment(PaymentID);
        }

        public List<TransactionHeader> getAllTransactionHeaderByShipment(int ShipmentID)
        {
            return transactionHeaderHandler.getAllTransactionHeaderByShipment(ShipmentID);
        }

        public List<TransactionHeader> getAllTransactionHeaderByUserFinished(int UserID)
        {
            return transactionHeaderHandler.getAllTransactionHeaderByUserFinished(UserID);
        }
    }
}