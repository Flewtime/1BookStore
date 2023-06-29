using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class PaymentHandler
    {
        private PaymentRepository paymentRepository;
        private OrderRepository orderRepository;
        private OrderHandler orderHandler;

        public PaymentHandler()
        {
            paymentRepository = new PaymentRepository();
            orderRepository = new OrderRepository();
            orderHandler = new OrderHandler();
        }

        public void insertPayment(int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            paymentRepository.insertPayment(PaymentTotal, PaymentDateTime, PaymentStatus, PaymentMethodID);
        }

        public void deletePayment(int PaymentID)
        {
            List<Order> orderList = orderRepository.getAllOrderByPayment(PaymentID);
            if(orderList.Any())
            {
                foreach (Order o in orderList)
                {
                    orderHandler.deleteOrder(o.OrderID);
                }
            }

            paymentRepository.deletePayment(PaymentID);
        }

        public void updatePayment(int PaymentID, int PaymentTotal, DateTime PaymentDateTime, string PaymentStatus, int PaymentMethodID)
        {
            paymentRepository.updatePayment(PaymentID, PaymentTotal, PaymentDateTime, PaymentStatus, PaymentMethodID);
        }

        public Payment findPaymentByID(int PaymentID)
        {
            return paymentRepository.findPaymentByID(PaymentID);
        }

        public Payment findLastPayment()
        {
            return paymentRepository.findLastPayment();
        }

        public List<Payment> getAllPayment()
        {
            return paymentRepository.getAllPayment();
        }

        public List<Payment> getAllPaymentByStatus(string PaymentStatus)
        {
            return paymentRepository.getAllPaymentByStatus(PaymentStatus);
        }
    }
}