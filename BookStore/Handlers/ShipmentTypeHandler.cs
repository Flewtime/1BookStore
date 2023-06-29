using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class ShipmentTypeHandler
    {
        private ShipmentTypeRepository shipmentTypeRepository;
        private ShipmentRepository shipmentRepository;
        private ShipmentHandler shipmentHandler;

        public ShipmentTypeHandler()
        {
            shipmentTypeRepository = new ShipmentTypeRepository();
            shipmentRepository = new ShipmentRepository();
            shipmentHandler = new ShipmentHandler();
        }

        public void insertShipmentType(string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice)
        {
            shipmentTypeRepository.insertShipmentType(ShipmentTypeName, ShipmentTypeDeliveryTime, ShipmentTypePrice);
        }

        public void deleteShipmentType(int ShipmentTypeID)
        {
            List<Shipment> shipmentList = shipmentRepository.getAllShipmentByType(ShipmentTypeID);
            if(shipmentList.Any())
            {
                foreach (Shipment s in shipmentList)
                {
                    shipmentHandler.deleteShipment(s.ShipmentID);
                }
            }

            shipmentTypeRepository.deleteShipmentType(ShipmentTypeID);
        }

        public void updateShipmentType(int ShipmentTypeID, string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice)
        {
            shipmentTypeRepository.updateShipmentType(ShipmentTypeID, ShipmentTypeName, ShipmentTypeDeliveryTime, ShipmentTypePrice);
        }

        public ShipmentType findShipmentTypeByID(int ShipmentTypeID)
        {
            return shipmentTypeRepository.findShipmentTypeByID(ShipmentTypeID);
        }

        public List<ShipmentType> getAllShipmentType()
        {
            return shipmentTypeRepository.getAllShipmentType();
        }
    }
}