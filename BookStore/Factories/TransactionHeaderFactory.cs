using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class TransactionHeaderFactory
    {

        public static TransactionHeader createTransactionHeader(int TransactionPrice, DateTime TransactionDateTime, int UserID, int PaymentID, int ShipmentID)
        {
            TransactionHeader transactionHeader = new TransactionHeader();
            transactionHeader.TransactionPrice = TransactionPrice;
            transactionHeader.TransactionDateTime = TransactionDateTime;
            transactionHeader.UserID = UserID;
            transactionHeader.PaymentID = PaymentID;
            transactionHeader.ShipmentID = ShipmentID;
            return transactionHeader;
        }
    }
}