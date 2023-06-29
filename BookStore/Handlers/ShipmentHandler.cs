using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class ShipmentHandler
    {
        private ShipmentRepository shipmentRepository;
        private OrderRepository orderRepository;
        private OrderHandler orderHandler;

        public ShipmentHandler()
        {
            shipmentRepository = new ShipmentRepository();
            orderRepository = new OrderRepository();
            orderHandler = new OrderHandler();
        }

        public void insertShipment(int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, int ShipmentTypeID)
        {
            shipmentRepository.insertShipment(ShipmentWeight, ShipmentAddress, ShipmentDateTime, ShipmentPrice, ShipmentStatus, ShipmentTypeID);
        }

        public void deleteShipment(int ShipmentID)
        {
            List<Order> orderList = orderRepository.getAllOrderByShipment(ShipmentID);
            if(orderList.Any())
            {
                foreach (Order o in orderList)
                {
                    orderHandler.deleteOrder(o.OrderID);
                }
            }

            shipmentRepository.deleteShipment(ShipmentID);
        }

        public void updateShipment(int ShipmentID, int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, string ShipmentTrackingID, int ShipmentTypeID)
        {
            shipmentRepository.updateShipment(ShipmentID, ShipmentWeight, ShipmentAddress, ShipmentDateTime, ShipmentPrice, ShipmentStatus, ShipmentTrackingID, ShipmentTypeID);
        }

        public Shipment findShipmentByID(int ShipmentID)
        {
            return shipmentRepository.findShipmentByID(ShipmentID);
        }

        public Shipment findLastShipment()
        {
            return shipmentRepository.findLastShipment();
        }

        public Shipment findShipmentByTrackingID(string ShipmentTrackingID)
        {
            return shipmentRepository.findShipmentByTrackingID(ShipmentTrackingID);
        }

        public List<Shipment> getAllShipment()
        {
            return shipmentRepository.getAllShipment();
        }

        public List<Shipment> getAllShipmentByTracking(string TrackingID)
        {
            return shipmentRepository.getAllShipmentByTracking(TrackingID);
        }
    }
}