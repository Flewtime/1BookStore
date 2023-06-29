using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class ShipmentFactory
    {

        public static Shipment createShipment(int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, string ShipmentTrackingID, int ShipmentTypeID)
        {
            Shipment shipment = new Shipment();
            shipment.ShipmentWeight = ShipmentWeight;
            shipment.ShipmentAddress = ShipmentAddress;
            shipment.ShipmentDateTime = ShipmentDateTime;
            shipment.ShipmentPrice = ShipmentPrice;
            shipment.ShipmentStatus = ShipmentStatus;
            shipment.ShipmentTrackingID = ShipmentTrackingID;
            shipment.ShipmentTypeID = ShipmentTypeID;
            return shipment;
        }
    }
}