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
    public partial class TransactionHistoryPage : System.Web.UI.Page
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
                transactionhistorytitle.Visible = true;

                TransactionHeaderController transactionHeaderController = new TransactionHeaderController();

                var user = (User)Session["sessionLogin"];
                int UserID = user.UserID;
                List<TransactionHeader> transactionHeaderList = transactionHeaderController.getAllTransactionHeaderByUserFinished(UserID);

                transactionhistorytitle.InnerText = "All Transaction Histories (" + transactionHeaderList.Count + ")";

                bindGrid(UserID);

                if (!transactionHeaderList.Any())
                {
                    lblthnone.Visible = true;
                }
                else
                {
                    lblthnone.Visible = false;
                }
            }
        }

        private void bindGrid(int UserID)
        {
            TransactionHeaderController transactionHeaderController = new TransactionHeaderController();

            DataTable dt = new DataTable();
            dt.Columns.Add("TransactionID");
            dt.Columns.Add("UserName");
            dt.Columns.Add("TransactionDate");
            dt.Columns.Add("TransactionTime");
            dt.Columns.Add("TotalBookPrice");
            dt.Columns.Add("ShipmentPrice");
            dt.Columns.Add("PaymentFee");
            dt.Columns.Add("TransactionGrandTotal");
            dt.Columns.Add("PaymentStatus");
            dt.Columns.Add("ShipmentStatus");

            List<Model.TransactionHeader> transactionHeaderList = transactionHeaderController.getAllTransactionHeaderByUserFinished(UserID);
            foreach (Model.TransactionHeader th in transactionHeaderList)
            {
                int totalWeight = 0;
                int totalBookPrice = 0;
                foreach (TransactionDetail td in th.TransactionDetails)
                {
                    totalWeight += td.Book.BookWeight * td.Qty;
                    totalBookPrice += td.Book.BookPrice * td.Qty;
                }

                int shipmentPrice = th.Shipment.ShipmentType.ShipmentTypePrice * totalWeight;

                DataRow dr = dt.NewRow();
                dr["TransactionID"] = th.TransactionID;
                dr["UserName"] = th.User.UserName;
                dr["TransactionDate"] = th.TransactionDateTime.ToString("dd/MM/yyyy");
                dr["TransactionTime"] = th.TransactionDateTime.ToString("HH:mm");
                dr["TotalBookPrice"] = totalBookPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["ShipmentPrice"] = shipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["PaymentFee"] = th.Payment.PaymentMethod.PaymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["TransactionGrandTotal"] = th.TransactionPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["PaymentStatus"] = th.Payment.PaymentStatus;
                dr["ShipmentStatus"] = th.Shipment.ShipmentStatus;
                dt.Rows.Add(dr);
            }

            rtransactionheader.DataSource = dt;
            rtransactionheader.DataBind();
        }

        protected void rtransactionheader_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TransactionDetailController transactionDetailController = new TransactionDetailController();

                var row = e.Item.FindControl("transactionhistoryrow") as HtmlTableRow;
                var drv = (DataRowView)e.Item.DataItem;
                int TransactionID = Convert.ToInt32(drv["TransactionID"]);

                Repeater innerRepeater = e.Item.FindControl("rtransactiondetail") as Repeater;
                innerRepeater.DataSource = transactionDetailController.getAllTransactionDetailByTransaction(TransactionID);
                innerRepeater.DataBind();
            }
        }
    }
}