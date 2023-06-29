using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class PaymentMethodRepository : IDisposable
    {
        private Database1Entities1 db;
        private PaymentMethod paymentMethod;

        public PaymentMethodRepository()
        {
            db = new Database1Entities1();
            paymentMethod = new PaymentMethod();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertPaymentMethod(string PaymentMethodName, int PaymentMethodFee)
        {
            paymentMethod = PaymentMethodFactory.createPaymentMethod(PaymentMethodName, PaymentMethodFee);
            db.PaymentMethods.Add(paymentMethod);
            db.SaveChanges();
        }

        public void deletePaymentMethod(int PaymentMethodID)
        {
            paymentMethod = findPaymentMethodByID(PaymentMethodID);
            if (paymentMethod != null)
            {
                db.PaymentMethods.Remove(paymentMethod);
                db.SaveChanges();
            }
        }

        public void updatePaymentMethod(int PaymentMethodID, string PaymentMethodName, int PaymentMethodFee)
        {
            paymentMethod = findPaymentMethodByID(PaymentMethodID);
            if (paymentMethod != null)
            {
                paymentMethod.PaymentMethodName = PaymentMethodName;
                paymentMethod.PaymentMethodFee = PaymentMethodFee;
                db.SaveChanges();
            }
        }

        public PaymentMethod findPaymentMethodByID(int PaymentMethodID)
        {
            paymentMethod = db.PaymentMethods.Find(PaymentMethodID);
            return paymentMethod;
        }

        public List<PaymentMethod> getAllPaymentMethod()
        {
            List<PaymentMethod> list = new List<PaymentMethod>();
            list = (from pm in db.PaymentMethods select pm).ToList();
            return list;
        }
    }
}