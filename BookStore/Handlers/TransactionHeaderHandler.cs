using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class TransactionHeaderHandler
    {
        private TransactionHeaderRepository transactionHeaderRepository;
        private TransactionDetailRepository transactionDetailRepository;

        public TransactionHeaderHandler()
        {
            transactionHeaderRepository = new TransactionHeaderRepository();
            transactionDetailRepository = new TransactionDetailRepository();
        }

        public void insertTransactionHeader(int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            transactionHeaderRepository.insertTransactionHeader(TransactionPrice, TransactionDateTime, UserID, PaymentID, ShipmentID);
        }

        public void deleteTransactionHeader(int TransactionID)
        {
            transactionDetailRepository.deleteAllTransactionDetailByTransaction(TransactionID);
            transactionHeaderRepository.deleteTransactionHeader(TransactionID);
        }

        public void updateTransactionHeader(int TransactionID, int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            transactionHeaderRepository.updateTransactionHeader(TransactionID, TransactionPrice, TransactionDateTime, UserID, PaymentID, ShipmentID);
        }

        public TransactionHeader findTransactionHeaderByID(int TransactionID)
        {
            return transactionHeaderRepository.findTransactionHeaderByID(TransactionID);
        }

        public TransactionHeader findLastTransactionHeader()
        {
            return transactionHeaderRepository.findLastTransactionHeader();
        }

        public List<TransactionHeader> getAllTransactionHeader()
        {
            return transactionHeaderRepository.getAllTransactionHeader();
        }

        public List<TransactionHeader> getAllTransactionHeaderByUser(int UserID)
        {
            return transactionHeaderRepository.getAllTransactionHeaderByUser(UserID);
        }

        public List<TransactionHeader> getAllTransactionHeaderByPayment(int PaymentID)
        {
            return transactionHeaderRepository.getAllTransactionHeaderByPayment(PaymentID);
        }

        public List<TransactionHeader> getAllTransactionHeaderByShipment(int ShipmentID)
        {
            return transactionHeaderRepository.getAllTransactionHeaderByShipment(ShipmentID);
        }

        public List<TransactionHeader> getAllTransactionHeaderByUserFinished(int UserID)
        {
            return transactionHeaderRepository.getAllTransactionHeaderByUserFinished(UserID);
        }
    }
}