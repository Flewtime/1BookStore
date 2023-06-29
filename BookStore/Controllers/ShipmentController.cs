using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;

namespace BookStore.Controllers
{
    public class ShipmentController
    {
        private ShipmentHandler shipmentHandler;

        public ShipmentController()
        {
            shipmentHandler = new ShipmentHandler();
        }

        public void insertShipment(int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, int ShipmentTypeID)
        {
            shipmentHandler.insertShipment(ShipmentWeight, ShipmentAddress, ShipmentDateTime, ShipmentPrice, ShipmentStatus, ShipmentTypeID);
        }

        public void deleteShipment(int ShipmentID)
        {
            shipmentHandler.deleteShipment(ShipmentID);
        }

        public void updateShipment(int ShipmentID, int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, string ShipmentTrackingID, int ShipmentTypeID)
        {
            shipmentHandler.updateShipment(ShipmentID, ShipmentWeight, ShipmentAddress, ShipmentDateTime, ShipmentPrice, ShipmentStatus, ShipmentTrackingID, ShipmentTypeID);
        }

        public Shipment findShipmentByID(int ShipmentID)
        {
            return shipmentHandler.findShipmentByID(ShipmentID);
        }

        public Shipment findLastShipment()
        {
            return shipmentHandler.findLastShipment();
        }

        public Shipment findShipmentByTrackingID(string ShipmentTrackingID)
        {
            return shipmentHandler.findShipmentByTrackingID(ShipmentTrackingID);
        }

        public List<Shipment> getAllShipment()
        {
            return shipmentHandler.getAllShipment();
        }

        public List<Shipment> getAllShipmentByTracking(string TrackingID)
        {
            return shipmentHandler.getAllShipmentByTracking(TrackingID);
        }
    }
}