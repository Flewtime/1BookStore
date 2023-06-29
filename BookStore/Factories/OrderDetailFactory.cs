using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class OrderDetailFactory
    {

        public static OrderDetail createOrderDetail(int OrderID, int BookID, int Qty)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.OrderID = OrderID;
            orderDetail.BookID = BookID;
            orderDetail.Qty = Qty;
            return orderDetail;
        }
    }
}