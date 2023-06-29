using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;

namespace BookStore.Controllers
{
    public class OrderDetailController
    {
        private OrderDetailHandler orderDetailHandler;

        public OrderDetailController()
        {
            orderDetailHandler = new OrderDetailHandler();
        }

        public void insertOrderDetail(int OrderID, int BookID, int Qty)
        {
            orderDetailHandler.insertOrderDetail(OrderID, BookID, Qty);
        }

        public void deleteOrderDetail(int OrderID, int BookID)
        {
            orderDetailHandler.deleteOrderDetail(OrderID, BookID);
        }

        public void updateOrderDetail(int OrderID, int BookID, int Qty)
        {
            orderDetailHandler.updateOrderDetail(OrderID, BookID, Qty);
        }

        public OrderDetail findOrderDetailByID(int OrderID, int BookID)
        {
            return orderDetailHandler.findOrderDetailByID(OrderID, BookID);
        }

        public List<OrderDetail> getAllOrderDetail()
        {
            return orderDetailHandler.getAllOrderDetail();
        }

        public List<OrderDetail> getAllOrderDetailByOrder(int OrderID)
        {
            return orderDetailHandler.getAllOrderDetailByOrder(OrderID);
        }

        public List<OrderDetail> getAllOrderDetailByBook(int BookID)
        {
            return orderDetailHandler.getAllOrderDetailByBook(BookID);
        }
    }
}