using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class OrderRepository : IDisposable
    {
        private Database1Entities1 db;
        private Order order;

        public OrderRepository()
        {
            db = Database.getDb();
            order = new Order();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertOrder(int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            order = OrderFactory.createOrder(OrderPrice, OrderDateTime, UserID, ShipmentID, PaymentID);
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void deleteOrder(int OrderID)
        {
            order = findOrderByID(OrderID);
            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }

        public void updateOrder(int OrderID, int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            order = findOrderByID(OrderID);
            if (order != null)
            {
                order.OrderPrice = OrderPrice;
                order.OrderDateTime = OrderDateTime;
                order.UserID = UserID;
                order.ShipmentID = ShipmentID;
                order.PaymentID = PaymentID;
                db.SaveChanges();
            }
        }

        public Order findOrderByID(int OrderID)
        {
            order = db.Orders.Find(OrderID);
            return order;
        }

        public Order findLastOrder()
        {
            order = (from order in db.Orders select order).ToList().LastOrDefault();
            return order;
        }

        public List<Order> getAllOrder()
        {
            List<Order> list = new List<Order>();
            list = (from o in db.Orders select o).ToList();
            return list;
        }

        public List<Order> getAllOrderByUser(int UserID)
        {
            List<Order> list = new List<Order>();
            list = (from o in db.Orders where o.UserID == UserID select o).ToList();
            return list;
        }

        public List<Order> getAllOrderByPayment(int PaymentID)
        {
            List<Order> list = new List<Order>();
            list = (from o in db.Orders where o.PaymentID == PaymentID select o).ToList();
            return list;
        }

        public List<Order> getAllOrderByShipment(int ShipmentID)
        {
            List<Order> list = new List<Order>();
            list = (from o in db.Orders where o.ShipmentID == ShipmentID select o).ToList();
            return list;
        }
    }
}