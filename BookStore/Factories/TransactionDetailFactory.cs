using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class TransactionDetailFactory
    {

        public static TransactionDetail createTransactionDetail(int TransactionID, int BookID, int Qty)
        {
            TransactionDetail transactionDetail = new TransactionDetail();
            transactionDetail.TransactionID = TransactionID;
            transactionDetail.BookID = BookID;
            transactionDetail.Qty = Qty;
            return transactionDetail;
        }
    }
}