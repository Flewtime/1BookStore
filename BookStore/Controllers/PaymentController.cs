using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;

namespace BookStore.Controllers
{
    public class PaymentController
    {
        private PaymentHandler paymentHandler;

        public PaymentController()
        {
            paymentHandler = new PaymentHandler();
        }

        public void insertPayment(int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            paymentHandler.insertPayment(PaymentTotal, PaymentDateTime, PaymentStatus, PaymentMethodID);
        }

        public void deletePayment(int PaymentID)
        {
            paymentHandler.deletePayment(PaymentID);
        }

        public void updatePayment(int PaymentID, int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            paymentHandler.updatePayment(PaymentID, PaymentTotal, PaymentDateTime, PaymentStatus, PaymentMethodID);
        }

        public Payment findPaymentByID(int PaymentID)
        {
            return paymentHandler.findPaymentByID(PaymentID);
        }

        public Payment findLastPayment()
        {
            return paymentHandler.findLastPayment();
        }

        public List<Payment> getAllPayment()
        {
            return paymentHandler.getAllPayment();
        }

        public List<Payment> getAllPaymentByStatus(string PaymentStatus)
        {
            return paymentHandler.getAllPaymentByStatus(PaymentStatus);
        }
    }
}