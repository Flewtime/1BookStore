using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using System.Web.UI.HtmlControls;

namespace BookStore.Views
{
    public partial class OrderPage : System.Web.UI.Page
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
                ordertitle.Visible = true;

                OrderController orderController = new OrderController();

                var user = (User)Session["sessionLogin"];
                int UserID = user.UserID;
                List<Order> orderList = orderController.getAllOrderByUser(UserID);

                ordertitle.InnerText = "All Orders (" + orderList.Count + ")";

                bindGrid(UserID);

                if (!orderList.Any())
                {
                    lblonone.Visible = true;
                }
                else
                {
                    lblonone.Visible = false;
                }

                foreach(Order o in orderList)
                {
                    if (o.Payment.PaymentStatus.Equals("Paid") && o.Shipment.ShipmentStatus.Equals("Delivered"))
                    {
                        orderController.deleteOrder(o.OrderID);
                    }
                }
            }
        }

        private void bindGrid(int UserID)
        {
            OrderController orderController = new OrderController();

            DataTable dt = new DataTable();
            dt.Columns.Add("OrderID");
            dt.Columns.Add("UserName");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("OrderTime");
            dt.Columns.Add("TotalBookPrice");
            dt.Columns.Add("ShipmentPrice");
            dt.Columns.Add("PaymentFee");
            dt.Columns.Add("OrderGrandTotal");
            dt.Columns.Add("ShipmentTrackingID");
            dt.Columns.Add("PaymentStatus");
            dt.Columns.Add("ShipmentStatus");

            List<Model.Order> orderList = orderController.getAllOrderByUser(UserID);
            foreach (Model.Order order in orderList)
            {
                int totalWeight = 0;
                int totalBookPrice = 0;
                foreach (OrderDetail od in order.OrderDetails)
                {
                    totalWeight += od.Book.BookWeight * od.Qty;
                    totalBookPrice += od.Book.BookPrice * od.Qty;
                }

                int shipmentPrice = order.Shipment.ShipmentType.ShipmentTypePrice * totalWeight;

                DataRow dr = dt.NewRow();
                dr["OrderID"] = order.OrderID;
                dr["UserName"] = order.User.UserName;
                dr["OrderDate"] = order.OrderDateTime.ToString("dd/MM/yyyy");
                dr["OrderTime"] = order.OrderDateTime.ToString("HH:mm");
                dr["TotalBookPrice"] = totalBookPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["ShipmentPrice"] = shipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID")); 
                dr["PaymentFee"] = order.Payment.PaymentMethod.PaymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["OrderGrandTotal"] = order.OrderPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["ShipmentTrackingID"] = order.Shipment.ShipmentTrackingID;
                dr["PaymentStatus"] = order.Payment.PaymentStatus;
                dr["ShipmentStatus"] = order.Shipment.ShipmentStatus;
                dt.Rows.Add(dr);
            }

            rorder.DataSource = dt;
            rorder.DataBind();
        }

        protected void rorder_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                OrderController orderController = new OrderController();
                OrderDetailController orderDetailController = new OrderDetailController();

                var row = e.Item.FindControl("orderrow") as HtmlTableRow;
                var drv = (DataRowView)e.Item.DataItem;
                int OrderID = Convert.ToInt32(drv["OrderID"]);

                Repeater innerRepeater = e.Item.FindControl("rorderdetail") as Repeater;
                innerRepeater.DataSource = orderDetailController.getAllOrderDetailByOrder(OrderID);
                innerRepeater.DataBind();
            }
        }
    }
}