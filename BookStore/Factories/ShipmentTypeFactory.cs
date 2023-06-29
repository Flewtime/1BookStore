using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class ShipmentTypeFactory
    {

        public static ShipmentType createShipmentType(string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice)
        {
            ShipmentType shipmentType = new ShipmentType();
            shipmentType.ShipmentTypeName = ShipmentTypeName;
            shipmentType.ShipmentTypeDeliveryTime = ShipmentTypeDeliveryTime;
            shipmentType.ShipmentTypePrice = ShipmentTypePrice;
            return shipmentType;
        }
    }
}