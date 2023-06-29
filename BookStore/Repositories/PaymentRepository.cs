using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class PaymentRepository : IDisposable
    {
        private Database1Entities1 db;
        private Model.Payment payment;

        public PaymentRepository()
        {
            db = Database.getDb();
            payment = new Model.Payment();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertPayment(int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            payment = PaymentFactory.createPayment(PaymentTotal, PaymentDateTime, PaymentStatus, PaymentMethodID);
            db.Payments.Add(payment);
            db.SaveChanges();
        }

        public void deletePayment(int PaymentID)
        {
            payment = findPaymentByID(PaymentID);
            if (payment != null)
            {
                db.Payments.Remove(payment);
                db.SaveChanges();
            }
        }

        public void updatePayment(int PaymentID, int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            payment = findPaymentByID(PaymentID);
            if (payment != null)
            {
                payment.PaymentTotal = PaymentTotal;
                payment.PaymentDateTime = PaymentDateTime;
                payment.PaymentStatus = PaymentStatus;
                payment.PaymentMethodID = PaymentMethodID;
                db.SaveChanges();
            }
        }

        public Payment findPaymentByID(int PaymentID)
        {
            payment = db.Payments.Find(PaymentID);
            return payment;
        }

        public Payment findLastPayment()
        {
            payment = (from payment in db.Payments select payment).ToList().LastOrDefault();
            if(payment != null)
            {
                return payment;
            }
            else
            {
                return null;
            }
        }

        public List<Payment> getAllPayment()
        {
            List<Payment> list = new List<Payment>();
            list = (from p in db.Payments select p).ToList();
            return list;
        }

        public List<Payment> getAllPaymentByStatus(string PaymentStatus)
        {
            List<Payment> list = new List<Payment>();
            list = (from p in db.Payments where p.PaymentStatus.Equals(PaymentStatus) select p).ToList();
            return list;
        }

        public List<Payment> getAllPaymentByMethod(int PaymentMethodID)
        {
            List<Payment> list = new List<Payment>();
            list = (from p in db.Payments where p.PaymentMethodID == PaymentMethodID select p).ToList();
            return list;
        }
    }
}