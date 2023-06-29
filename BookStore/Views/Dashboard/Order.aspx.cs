using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using System.Xml.Linq;

namespace BookStore.Views.Dashboard
{
    public partial class Order : System.Web.UI.Page
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

                var u = (Model.User)Session["sessionLogin"];
                if (u.UserRole.Equals("Admin"))
                {
                    if (!IsPostBack)
                    {
                        orderview.Visible = true;
                        lblosearch.Visible = false;

                        bindGrid();
                    }
                }
                else
                {
                    Response.Redirect("~/Views/LoginPage.aspx");
                }
            }
        }

        private void bindGrid()
        {
            OrderController orderController = new OrderController();

            DataTable dt = new DataTable();
            dt.Columns.Add("OrderID");
            dt.Columns.Add("OrderPrice");
            dt.Columns.Add("PaymentMethodName");
            dt.Columns.Add("PaymentMethodFee");
            dt.Columns.Add("PaymentTotal");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("OrderTime");
            dt.Columns.Add("UserName");
            dt.Columns.Add("ShipmentTrackingID");
            dt.Columns.Add("PaymentID");

            List<Model.Order> orderList = orderController.getAllOrder();
            foreach (Model.Order order in orderList)
            {
                DataRow dr = dt.NewRow();
                dr["OrderID"] = order.OrderID;
                dr["OrderPrice"] = order.OrderPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["PaymentMethodName"] = order.Payment.PaymentMethod.PaymentMethodName;
                dr["PaymentMethodFee"] = order.Payment.PaymentMethod.PaymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["PaymentTotal"] = order.Payment.PaymentTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["OrderDate"] = order.OrderDateTime.ToString("dd/MM/yyyy");
                dr["OrderTime"] = order.OrderDateTime.ToString("HH:mm:ss");
                dr["UserName"] = order.User.UserName;
                dr["ShipmentTrackingID"] = order.Shipment.ShipmentTrackingID;
                dr["PaymentID"] = order.PaymentID;
                dt.Rows.Add(dr);
            }

            gvorder.DataSource = dt;
            gvorder.DataBind();
        }

        protected void btnoSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtoSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvorder.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("OrderID like '%" + searchTerm + "%' or OrderPrice like '%" + searchTerm + "%' or PaymentMethodName like '%" + searchTerm + "%' or PaymentMethodFee like '%" + searchTerm + "%' or PaymentTotal like '%" + searchTerm + "%' or OrderDate like '%" + searchTerm + "%' or OrderTime like '%" + searchTerm + "%' or UserName like '%" + searchTerm + "%' or ShipmentTrackingID like '%" + searchTerm + "%' or PaymentID like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvorder.DataSource = filteredDt;
                gvorder.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblosearch.Visible = true;
                    lblosearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblosearch.Visible = false;
                    lblosearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblosearch.Visible = false;
                lblosearch.Text = "";
            }
        }

        protected void gvorder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvorder.PageIndex = e.NewPageIndex;

            bindGrid();
        }
    }
}