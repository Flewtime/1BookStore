using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class ShipmentTypeRepository : IDisposable
    {
        private Database1Entities1 db;
        private ShipmentType shipmentType;

        public ShipmentTypeRepository()
        {
            db = Database.getDb();
            shipmentType = new ShipmentType();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertShipmentType(string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice)
        {
            shipmentType = ShipmentTypeFactory.createShipmentType(ShipmentTypeName, ShipmentTypeDeliveryTime, ShipmentTypePrice);
            db.ShipmentTypes.Add(shipmentType);
            db.SaveChanges();
        }

        public void deleteShipmentType(int ShipmentTypeID)
        {
            shipmentType = findShipmentTypeByID(ShipmentTypeID);
            if (shipmentType != null)
            {
                db.ShipmentTypes.Remove(shipmentType);
                db.SaveChanges();
            }
        }

        public void updateShipmentType(int ShipmentTypeID, string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice)
        {
            shipmentType = findShipmentTypeByID(ShipmentTypeID);
            if (shipmentType != null)
            {
                shipmentType.ShipmentTypeName = ShipmentTypeName;
                shipmentType.ShipmentTypeDeliveryTime = ShipmentTypeDeliveryTime;
                shipmentType.ShipmentTypePrice = ShipmentTypePrice;
                db.SaveChanges();
            }
        }

        public ShipmentType findShipmentTypeByID(int ShipmentTypeID)
        {
            shipmentType = db.ShipmentTypes.Find(ShipmentTypeID);
            return shipmentType;
        }

        public List<ShipmentType> getAllShipmentType()
        {
            List<ShipmentType> list = new List<ShipmentType>();
            list = (from st in db.ShipmentTypes select st).ToList();
            return list;
        }
    }
}