using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class TransactionDetailHandler
    {
        private TransactionDetailRepository transactionDetailRepository;

        public TransactionDetailHandler()
        {
            transactionDetailRepository = new TransactionDetailRepository();
        }

        public void insertTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            transactionDetailRepository.insertTransactionDetail(TransactionID, BookID, Qty);
        }

        public void deleteTransactionDetail(int TransactionID, int BookID)
        {
            transactionDetailRepository.deleteTransactionDetail(TransactionID, BookID);
        }

        public void updateTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            transactionDetailRepository.updateTransactionDetail(TransactionID, BookID, Qty);
        }

        public TransactionDetail findTransactionDetailByID(int TransactionID, int BookID)
        {
            return transactionDetailRepository.findTransactionDetailByID(TransactionID, BookID);
        }

        public List<TransactionDetail> getAllTransactionDetail()
        {
            return transactionDetailRepository.getAllTransactionDetail();
        }

        public List<TransactionDetail> getAllTransactionDetailByTransaction(int TransactionID)
        {
            return transactionDetailRepository.getAllTransactionDetailByTransaction(TransactionID);
        }
    }
}