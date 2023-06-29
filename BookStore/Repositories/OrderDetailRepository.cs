using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;
using System.Web.UI.WebControls.WebParts;

namespace BookStore.Repositories
{
    public class OrderDetailRepository : IDisposable
    {
        private Database1Entities1 db;
        private OrderDetail orderDetail;

        public OrderDetailRepository()
        {
            db = Database.getDb();
            orderDetail = new OrderDetail();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertOrderDetail(int OrderID, int BookID, int Qty)
        {
            orderDetail = OrderDetailFactory.createOrderDetail(OrderID, BookID, Qty);
            db.OrderDetails.Add(orderDetail);
            db.SaveChanges();
        }

        public void deleteOrderDetail(int OrderID, int BookID)
        {
            orderDetail = findOrderDetailByID(OrderID, BookID);
            if (orderDetail != null)
            {
                db.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
            }
        }

        public void deleteAllOrderDetailByOrder(int OrderID)
        {
            List<OrderDetail> list = getAllOrderDetailByOrder(OrderID);
            if (list.Any())
            {
                foreach (OrderDetail od in list)
                {
                    db.OrderDetails.Remove(od);
                    db.SaveChanges();
                }
            }
        }

        public void deleteAllOrderDetailByBook(int BookID)
        {
            List<OrderDetail> list = getAllOrderDetailByBook(BookID);
            if (list.Any())
            {
                foreach (OrderDetail od in list)
                {
                    db.OrderDetails.Remove(od);
                    db.SaveChanges();
                }
            }
        }

        public void updateOrderDetail(int OrderID, int BookID, int Qty)
        {
            orderDetail = findOrderDetailByID(OrderID, BookID);
            if (orderDetail != null)
            {
                orderDetail.OrderID = OrderID;
                orderDetail.BookID = BookID;
                orderDetail.Qty = Qty;
                db.SaveChanges();
            }
        }

        public OrderDetail findOrderDetailByID(int OrderID, int BookID)
        {
            orderDetail = (from od in db.OrderDetails where od.OrderID == OrderID && od.BookID == BookID select od).FirstOrDefault();
            return orderDetail;
        }

        public List<OrderDetail> getAllOrderDetail()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            list = (from od in db.OrderDetails select od).ToList();
            return list;
        }

        public List<OrderDetail> getAllOrderDetailByOrder(int OrderID)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            list = (from od in db.OrderDetails where od.OrderID == OrderID select od).ToList();
            return list;
        }

        public List<OrderDetail> getAllOrderDetailByBook(int BookID)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            list = (from od in db.OrderDetails where od.BookID == BookID select od).ToList();
            return list;
        }
    }
}