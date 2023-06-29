using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;
using BookStore.Controllers;

namespace BookStore.Repositories
{
    public class ShipmentRepository : IDisposable
    {
        private Database1Entities1 db;
        private Shipment shipment;

        public ShipmentRepository()
        {
            db = Database.getDb();
            shipment = new Shipment();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertShipment(int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, int ShipmentTypeID)
        {
            string ShipmentTrackingID = makeShipmentTrackingID();
            shipment = ShipmentFactory.createShipment(ShipmentWeight, ShipmentAddress, ShipmentDateTime, ShipmentPrice, ShipmentStatus, ShipmentTrackingID, ShipmentTypeID);
            db.Shipments.Add(shipment);
            db.SaveChanges();
        }

        private string makeShipmentTrackingID()
        {
            shipment = findLastShipment();
            string newShipmentTrackingID = "";
            string oldShipmentTrackingID = "";
            if (shipment == null)
            {
                newShipmentTrackingID = "SHP0000001";
            }
            else
            {
                oldShipmentTrackingID = shipment.ShipmentTrackingID;
                int oldShipmentTrackingIDNumber = int.Parse(oldShipmentTrackingID.Replace("SHP", ""));
                int newShipmentTrackingIDNumber = oldShipmentTrackingIDNumber + 1;
                newShipmentTrackingID = "SHP" + newShipmentTrackingIDNumber.ToString("D7");
            }

            return newShipmentTrackingID;
        }

        public void deleteShipment(int ShipmentID)
        {
            shipment = findShipmentByID(ShipmentID);
            if (shipment != null)
            {
                db.Shipments.Remove(shipment);
                db.SaveChanges();
            }
        }

        public void updateShipment(int ShipmentID, int ShipmentWeight, string ShipmentAddress, DateTime ShipmentDateTime, int ShipmentPrice, string ShipmentStatus, string ShipmentTrackingID, int ShipmentTypeID)
        {
            shipment = findShipmentByID(ShipmentID);
            if (shipment != null)
            {
                shipment.ShipmentWeight = ShipmentWeight;
                shipment.ShipmentAddress = ShipmentAddress;
                shipment.ShipmentDateTime = ShipmentDateTime;
                shipment.ShipmentPrice = ShipmentPrice;
                shipment.ShipmentStatus = ShipmentStatus;
                shipment.ShipmentTrackingID = ShipmentTrackingID;
                shipment.ShipmentTypeID = ShipmentTypeID;
                db.SaveChanges();
            }
        }

        public Shipment findShipmentByID(int ShipmentID)
        {
            shipment = db.Shipments.Find(ShipmentID);
            return shipment;
        }

        public Shipment findLastShipment()
        {
            shipment = (from s in db.Shipments select s).ToList().LastOrDefault();
            if(shipment != null)
            {
                return shipment;
            }
            else
            {
                return null;
            }
        }

        public Shipment findShipmentByTrackingID(string TrackingID)
        {
            shipment = (from s in db.Shipments where s.ShipmentTrackingID.Equals(TrackingID) select s).FirstOrDefault();
            if (shipment != null)
            {
                return shipment;
            }
            else
            {
                return null;
            }
        }

        public List<Shipment> getAllShipment()
        {
            List<Shipment> list = new List<Shipment>();
            list = (from s in db.Shipments select s).ToList();
            return list;
        }

        public List<Shipment> getAllShipmentByTracking(string TrackingID)
        {
            List<Shipment> list = new List<Shipment>();
            list = (from s in db.Shipments where s.ShipmentTrackingID.Equals(TrackingID) select s).ToList();
            return list;
        }

        public List<Shipment> getAllShipmentByStatus(string Status)
        {
            List<Shipment> list = new List<Shipment>();
            list = (from s in db.Shipments where s.ShipmentStatus.Equals(Status) select s).ToList();
            return list;
        }

        public List<Shipment> getAllShipmentByType(int ShipmentTypeID)
        {
            List<Shipment> list = new List<Shipment>();
            list = (from s in db.Shipments where s.ShipmentTypeID == ShipmentTypeID  select s).ToList();
            return list;
        }
    }
}