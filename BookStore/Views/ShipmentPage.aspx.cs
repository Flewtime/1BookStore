using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;

namespace BookStore.Views
{
    public partial class ShipmentPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            if (Session["sessionLogin"] == null && Request.Cookies["cookieID"] == null)
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }
            else
            {
                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }

                var u = (User)Session["sessionLogin"];
                if (u.UserRole.Equals("Customer"))
                {
                }
                else
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                }
            }


            if (!IsPostBack)
            {
                CartController cartController = new CartController();
                BookController bookController = new BookController();
                ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

                var user = (User)Session["sessionLogin"];
                int UserID = user.UserID;

                List<Cart> cartList = cartController.getAllCartByUser(UserID);
                int shipmentWeight = 0;
                int totalPriceBefore = 0;
                foreach(var c in cartList)
                {
                    int BookID = c.BookID;
                    var book = bookController.findBookByID(BookID);
                    shipmentWeight = shipmentWeight + (book.BookWeight * c.Qty);
                    totalPriceBefore = totalPriceBefore + (book.BookPrice * c.Qty);
                }
                txtsweight.Text = shipmentWeight.ToString() + " gram";

                string userAddress = user.UserAddress;
                txtsaddress.Text = userAddress;

                List<Model.ShipmentType> shipmentTypeList = shipmentTypeController.getAllShipmentType();
                ddlstype.DataSource = shipmentTypeList;
                ddlstype.DataTextField = "ShipmentTypeName";
                ddlstype.DataValueField = "ShipmentTypeID";
                ddlstype.DataBind();

                int ShipmentTypeID = int.Parse(ddlstype.SelectedValue);
                var shipment = shipmentTypeController.findShipmentTypeByID(ShipmentTypeID);
                int shipmentTypePrice = shipment.ShipmentTypePrice;
                string shipmentTypePriceRupiah = shipmentTypePrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                txtstypeprice.Text = shipmentTypePriceRupiah;

                txtpricebefore.Text = totalPriceBefore.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));

                int totalShipmentPrice = totalPriceBefore + (shipmentWeight * shipmentTypePrice);
                string rupiahTotalPrice = totalShipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                txtsprice.Text = rupiahTotalPrice;
            }
        }

        protected void btnsnext_Click(object sender, EventArgs e)
        {
            ShipmentController shipmentController = new ShipmentController();

            int shipmentWeight = int.Parse(txtsweight.Text.Replace(" gram", ""));
            String shipmentAddress = txtsaddress.Text;
            DateTime shipmentDateTime = DateTime.Now;
            int shipmentPrice = int.Parse(txtsprice.Text.Replace("Rp", "").Replace(".", "").Replace(",00", ""));
            string shipmentStatus = "Pending";
            int shipmentTypeID = int.Parse(ddlstype.SelectedValue);

            shipmentController.insertShipment(shipmentWeight, shipmentAddress, shipmentDateTime, shipmentPrice, shipmentStatus, shipmentTypeID);

            var shipment = shipmentController.findLastShipment();
            string shipmentTrackingID = shipment.ShipmentTrackingID;

            Response.Redirect("~/Views/PaymentPage.aspx?shipmentTrackingID=" + shipmentTrackingID);
        }

        protected void ddlstype_SelectedIndexChanged(object sender, EventArgs e)
        {
            CartController cartController = new CartController();
            BookController bookController = new BookController();
            ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;

            List<Cart> cartList = cartController.getAllCartByUser(UserID);
            int shipmentWeight = 0;
            int totalPriceBefore = 0;
            foreach (var c in cartList)
            {
                int BookID = c.BookID;
                var book = bookController.findBookByID(BookID);
                shipmentWeight = shipmentWeight + (book.BookWeight * c.Qty);
                totalPriceBefore = totalPriceBefore + (book.BookPrice * c.Qty);
            }

            int ShipmentTypeID = int.Parse(ddlstype.SelectedValue);
            var shipmentType = shipmentTypeController.findShipmentTypeByID(ShipmentTypeID);
            int shipmentTypePrice = shipmentType.ShipmentTypePrice;
            string rupiahShipmentTypePrice = shipmentTypePrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
            txtstypeprice.Text = rupiahShipmentTypePrice;

            int totalShipmentPrice = totalPriceBefore + (shipmentWeight * shipmentTypePrice);
            string rupiahTotalPrice = totalShipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
            txtsprice.Text = rupiahTotalPrice;
        }
    }
}