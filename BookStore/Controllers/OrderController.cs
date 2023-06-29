using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;

namespace BookStore.Controllers
{
    public class OrderController
    {
        private OrderHandler orderHandler;

        public OrderController()
        {
            orderHandler = new OrderHandler();
        }

        public void insertOrder(int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            orderHandler.insertOrder(OrderPrice, OrderDateTime, UserID, ShipmentID, PaymentID);
        }

        public void deleteOrder(int OrderID)
        {
            orderHandler.deleteOrder(OrderID);
        }

        public void updateOrder(int OrderID, int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            orderHandler.updateOrder(OrderID, OrderPrice, OrderDateTime, UserID, ShipmentID, PaymentID);
        }

        public Order findOrderByID(int OrderID)
        {
            return orderHandler.findOrderByID(OrderID);
        }

        public Order findLastOrder()
        {
            return orderHandler.findLastOrder();
        }

        public List<Order> getAllOrder()
        {
            return orderHandler.getAllOrder();
        }

        public List<Order> getAllOrderByUser(int UserID)
        {
            return orderHandler.getAllOrderByUser(UserID);
        }
    }
}