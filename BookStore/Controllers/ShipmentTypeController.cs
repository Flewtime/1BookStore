using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class ShipmentTypeController
    {
        private ShipmentTypeHandler shipmentTypeHandler;

        public ShipmentTypeController()
        {
            shipmentTypeHandler = new ShipmentTypeHandler();
        }

        public string insertShipmentType(string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice, Boolean checkShipmentTypeName)
        {
            string validate = validateShipmentType(ShipmentTypeName, ShipmentTypeDeliveryTime.ToString(), ShipmentTypePrice.ToString(), checkShipmentTypeName);
            if(validate.Equals("Insert Success!"))
            {
                shipmentTypeHandler.insertShipmentType(ShipmentTypeName, ShipmentTypeDeliveryTime, ShipmentTypePrice);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteShipmentType(int ShipmentTypeID)
        {
            shipmentTypeHandler.deleteShipmentType(ShipmentTypeID);
        }

        public string updateShipmentType(int ShipmentTypeID, string ShipmentTypeName, int ShipmentTypeDeliveryTime, int ShipmentTypePrice, Boolean checkShipmentTypeName)
        {
            string validate = validateShipmentType(ShipmentTypeName, ShipmentTypeDeliveryTime.ToString(), ShipmentTypePrice.ToString(), checkShipmentTypeName);
            if (validate.Equals("Insert Success!"))
            {
                shipmentTypeHandler.updateShipmentType(ShipmentTypeID, ShipmentTypeName, ShipmentTypeDeliveryTime, ShipmentTypePrice);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public ShipmentType findShipmentTypeByID(int ShipmentTypeID)
        {
            return shipmentTypeHandler.findShipmentTypeByID(ShipmentTypeID);
        }

        public List<ShipmentType> getAllShipmentType()
        {
            return shipmentTypeHandler.getAllShipmentType();
        }

        private string validateShipmentType(string ShipmentTypeName, string ShipmentTypeDeliveryTime, string ShipmentTypePrice, Boolean checkShipmentTypeName)
        {
            Regex validateShipmentTypeName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");
            Regex validateShipmentTypeDeliveryTime = new Regex("^\\d+$");
            Regex validateShipmentTypePrice = new Regex("^\\d+$");

            Boolean isShipmentTypeNameExist = false;
            List<ShipmentType> shipmentTypeList = getAllShipmentType();
            foreach(var shipmentType in shipmentTypeList)
            {
                if (shipmentType.ShipmentTypeName.Equals(ShipmentTypeName))
                {
                    isShipmentTypeNameExist = true;
                    break;
                }
            }

            if(ShipmentTypeName == "" || ShipmentTypeDeliveryTime == "" || ShipmentTypePrice == "")
            {
                return "Please Fill All The Fields!";
            }
            else if(ShipmentTypeName.Length < 2)
            {
                return "Shipment Type Name Must be More Than 2 Characters!";
            }
            else if (!validateShipmentTypeName.IsMatch(ShipmentTypeName))
            {
                return "Please Enter A Valid Shipment Type Name!";
            }
            else if (isShipmentTypeNameExist && checkShipmentTypeName)
            {
                return "Shipment Type Name Already Exist, Please Enter Another Shipment Type Name!";
            }
            else if (!validateShipmentTypeDeliveryTime.IsMatch(ShipmentTypeDeliveryTime))
            {
                return "Please Enter A Valid Shipment Type Delivery Time That Only Contains Numbers!";
            }
            else if (!validateShipmentTypePrice.IsMatch(ShipmentTypePrice))
            {
                return "Please Enter A Valid Shipment Type Price That Only Contains Numbers!";
            }

            return "Insert Success!";
        }
    }
}