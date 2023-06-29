using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class OrderHandler
    {
        private OrderRepository orderRepository;
        private OrderDetailRepository orderDetailRepository;

        public OrderHandler()
        {
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
        }

        public void insertOrder(int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            orderRepository.insertOrder(OrderPrice, OrderDateTime, UserID, ShipmentID, PaymentID);
        }

        public void deleteOrder(int OrderID)
        {
            orderDetailRepository.deleteAllOrderDetailByOrder(OrderID);
            orderRepository.deleteOrder(OrderID);
        }

        public void updateOrder(int OrderID, int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            orderRepository.updateOrder(OrderID, OrderPrice, OrderDateTime, UserID, ShipmentID, PaymentID);
        }

        public Order findOrderByID(int OrderID)
        {
            return orderRepository.findOrderByID(OrderID);
        }

        public Order findLastOrder()
        {
            return orderRepository.findLastOrder();
        }

        public List<Order> getAllOrder()
        {
            return orderRepository.getAllOrder();
        }

        public List<Order> getAllOrderByUser(int UserID)
        {
            return orderRepository.getAllOrderByUser(UserID);
        }
    }
}