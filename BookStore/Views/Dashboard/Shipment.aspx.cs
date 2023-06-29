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
    public partial class Shipment : System.Web.UI.Page
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
                        shipmentview.Visible = true;
                        lblshsearch.Visible = false;

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
            ShipmentController shipmentController = new ShipmentController();

            DataTable dt = new DataTable();
            dt.Columns.Add("ShipmentID");
            dt.Columns.Add("ShipmentWeight");
            dt.Columns.Add("ShipmentAddress");
            dt.Columns.Add("ShipmentDate");
            dt.Columns.Add("ShipmentTime");
            dt.Columns.Add("ShipmentPrice");
            dt.Columns.Add("ShipmentStatus");
            dt.Columns.Add("ShipmentTrackingID");
            dt.Columns.Add("ShipmentTypeName");

            List<Model.Shipment> shipmentList = shipmentController.getAllShipment();
            foreach (Model.Shipment shipment in shipmentList)
            {
                DataRow dr = dt.NewRow();
                dr["ShipmentID"] = shipment.ShipmentID;
                dr["ShipmentWeight"] = shipment.ShipmentWeight;
                dr["ShipmentAddress"] = shipment.ShipmentAddress;
                dr["ShipmentDate"] = shipment.ShipmentDateTime.ToString("dd/MM/yyyy");
                dr["ShipmentTime"] = shipment.ShipmentDateTime.ToString("HH:mm:ss");
                dr["ShipmentPrice"] = shipment.ShipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["ShipmentStatus"] = shipment.ShipmentStatus;
                dr["ShipmentTrackingID"] = shipment.ShipmentTrackingID;
                dr["ShipmentTypeName"] = shipment.ShipmentType.ShipmentTypeName;
                dt.Rows.Add(dr);
            }

            gvshipment.DataSource = dt;
            gvshipment.DataBind();
        }

        protected void btnshSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtshSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvshipment.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("ShipmentID like '%" + searchTerm + "%' or ShipmentWeight like '%" + searchTerm + "%' or ShipmentAddress like '%" + searchTerm + "%' or ShipmentDate like '%" + searchTerm + "%' or ShipmentTime like '%" + searchTerm + "%' or ShipmentPrice like '%" + searchTerm + "%' or ShipmentStatus like '%" + searchTerm + "%' or ShipmentTrackingID like '%" + searchTerm + "%' or ShipmentTypeName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvshipment.DataSource = filteredDt;
                gvshipment.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblshsearch.Visible = true;
                    lblshsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblshsearch.Visible = false;
                    lblshsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblshsearch.Visible = false;
                lblshsearch.Text = "";
            }
        }

        protected void gvshipment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvshipment.PageIndex = e.NewPageIndex;

            bindGrid();
        }
    }
}