using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class PaymentMethodHandler
    {
        private PaymentMethodRepository paymentMethodRepository;
        private PaymentRepository paymentRepository;
        private PaymentHandler paymentHandler;

        public PaymentMethodHandler()
        {
            paymentMethodRepository = new PaymentMethodRepository();
            paymentRepository = new PaymentRepository();
            paymentHandler = new PaymentHandler();
        }

        public void insertPaymentMethod(string PaymentMethodName, int PaymentMethodFee)
        {
            paymentMethodRepository.insertPaymentMethod(PaymentMethodName, PaymentMethodFee);
        }

        public void deletePaymentMethod(int PaymentMethodID)
        {
            List<Payment> paymentList = paymentRepository.getAllPaymentByMethod(PaymentMethodID);
            if(paymentList.Any())
            {
                foreach(Payment p in paymentList)
                {
                    paymentHandler.deletePayment(p.PaymentID);
                }
            }

            paymentMethodRepository.deletePaymentMethod(PaymentMethodID);
        }

        public void updatePaymentMethod(int PaymentMethodID, string PaymentMethodName, int PaymentMethodFee)
        {
            paymentMethodRepository.updatePaymentMethod(PaymentMethodID, PaymentMethodName, PaymentMethodFee);
        }

        public PaymentMethod findPaymentMethodByID(int PaymentMethodID)
        {
            return paymentMethodRepository.findPaymentMethodByID(PaymentMethodID);
        }

        public List<PaymentMethod> getAllPaymentMethod()
        {
            return paymentMethodRepository.getAllPaymentMethod();
        }
    }
}