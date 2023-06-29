using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class PaymentFactory
    {

        public static Payment createPayment(int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            Payment payment = new Payment();
            payment.PaymentTotal = PaymentTotal;
            payment.PaymentDateTime = PaymentDateTime;
            payment.PaymentStatus = PaymentStatus;
            payment.PaymentMethodID = PaymentMethodID;
            return payment;
        }
    }
}