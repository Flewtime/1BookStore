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
    public partial class PaymentPage : System.Web.UI.Page
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
                PaymentController paymentController = new PaymentController();
                PaymentMethodController paymentMethodController = new PaymentMethodController();
                ShipmentController shipmentController = new ShipmentController();

                string shipmentTrackingID = Request["shipmentTrackingID"].ToString();
                var shipment = shipmentController.findShipmentByTrackingID(shipmentTrackingID);
                int shipmentPrice = shipment.ShipmentPrice;
                string rupiahShipmentPrice = shipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                txttotalprice.Text = rupiahShipmentPrice;

                List<PaymentMethod> paymentMethodList = paymentMethodController.getAllPaymentMethod();
                ddlpmethod.DataSource = paymentMethodList;
                ddlpmethod.DataTextField = "PaymentMethodName";
                ddlpmethod.DataValueField = "PaymentMethodID";
                ddlpmethod.DataBind();

                int paymentMethodID = int.Parse(ddlpmethod.SelectedValue);
                var paymentMethod = paymentMethodController.findPaymentMethodByID(paymentMethodID);
                int paymentMethodFee = paymentMethod.PaymentMethodFee;
                string rupiahPaymentMethodFee = paymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                txtpmethodfee.Text = rupiahPaymentMethodFee;

                int paymentTotal = shipmentPrice + paymentMethodFee;
                string rupiahPaymentTotal = paymentTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                txtptotal.Text = rupiahPaymentTotal;
            }   
        }

        protected void ddlpmethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaymentMethodController paymentMethodController = new PaymentMethodController();
            ShipmentController shipmentController = new ShipmentController();

            int paymentMethodID = int.Parse(ddlpmethod.SelectedValue);
            var paymentMethod = paymentMethodController.findPaymentMethodByID(paymentMethodID);
            int paymentMethodFee = paymentMethod.PaymentMethodFee;
            string rupiahPaymentMethodFee = paymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
            txtpmethodfee.Text = rupiahPaymentMethodFee;

            string shipmentTrackingID = Request["shipmentTrackingID"].ToString();
            var shipment = shipmentController.findShipmentByTrackingID(shipmentTrackingID);
            int shipmentPrice = shipment.ShipmentPrice;
            int paymentTotal = shipmentPrice + paymentMethodFee;
            string rupiahPaymentTotal = paymentTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
            txtptotal.Text = rupiahPaymentTotal;
        }

        protected void btnpfinish_Click(object sender, EventArgs e)
        {
            PaymentController paymentController = new PaymentController();
            OrderController orderController = new OrderController();
            OrderDetailController orderDetailController = new OrderDetailController();
            CartController cartController = new CartController();
            ShipmentController shipmentController = new ShipmentController();
            TransactionHeaderController transactionHeaderController = new TransactionHeaderController();
            TransactionDetailController transactionDetailController = new TransactionDetailController();

            int paymentTotal = int.Parse(txtptotal.Text.Replace("Rp", "").Replace(".", "").Replace(",00", ""));
            DateTime paymentDateTime = DateTime.Now;
            string paymentStatus = "Paid";
            int paymentMethodID = int.Parse(ddlpmethod.SelectedValue);

            paymentController.insertPayment(paymentTotal, paymentDateTime, paymentStatus, paymentMethodID);

            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;

            var payment = paymentController.findLastPayment();
            int PaymentID = payment.PaymentID;

            string shipmentTrackingID = Request["shipmentTrackingID"].ToString();
            var shipment = shipmentController.findShipmentByTrackingID(shipmentTrackingID);

            DateTime orderDateTime = DateTime.Now;
            int ShipmentID = shipment.ShipmentID;

            orderController.insertOrder(paymentTotal, orderDateTime, UserID, ShipmentID, PaymentID);
            
            transactionHeaderController.insertTransactionHeader(paymentTotal, paymentDateTime, UserID, PaymentID, ShipmentID);

            List<Cart> cartList = cartController.getAllCartByUser(UserID);
            var transactionHeader = transactionHeaderController.findLastTransactionHeader();
            int TransactionID = transactionHeader.TransactionID;
            var order = orderController.findLastOrder();
            int OrderID = order.OrderID;
            foreach(Cart c in cartList)
            {
                transactionDetailController.insertTransactionDetail(TransactionID, c.BookID, c.Qty);
                orderDetailController.insertOrderDetail(OrderID, c.BookID, c.Qty);
            }

            cartController.deleteAllCartByUser(UserID);

            Response.Redirect("~/Views/HomePage.aspx");
        }
    }
}