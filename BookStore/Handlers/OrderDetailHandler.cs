using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class OrderDetailHandler
    {
        private OrderDetailRepository orderDetailRepository;

        public OrderDetailHandler()
        {
            orderDetailRepository = new OrderDetailRepository();
        }

        public void insertOrderDetail(int OrderID, int BookID, int Qty)
        {
            orderDetailRepository.insertOrderDetail(OrderID, BookID, Qty);
        }

        public void deleteOrderDetail(int OrderID, int BookID)
        {
            orderDetailRepository.deleteOrderDetail(OrderID, BookID);
        }

        public void updateOrderDetail(int OrderID, int BookID, int Qty)
        {
            orderDetailRepository.updateOrderDetail(OrderID, BookID, Qty);
        }

        public OrderDetail findOrderDetailByID(int OrderID, int BookID)
        {
            return orderDetailRepository.findOrderDetailByID(OrderID, BookID);
        }

        public List<OrderDetail> getAllOrderDetail()
        {
            return orderDetailRepository.getAllOrderDetail();
        }

        public List<OrderDetail> getAllOrderDetailByOrder(int OrderID)
        {
            return orderDetailRepository.getAllOrderDetailByOrder(OrderID);
        }

        public List<OrderDetail> getAllOrderDetailByBook(int BookID)
        {
            return orderDetailRepository.getAllOrderDetailByBook(BookID);
        }
    }
}