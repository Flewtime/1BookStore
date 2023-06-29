using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class OrderFactory
    {

        public static Order createOrder(int OrderPrice, DateTime OrderDateTime, int UserID, int ShipmentID, int PaymentID)
        {
            Order order = new Order();
            order.OrderPrice = OrderPrice;
            order.OrderDateTime = OrderDateTime;
            order.UserID = UserID;
            order.ShipmentID = ShipmentID;
            order.PaymentID = PaymentID;
            return order;
        }
    }
}