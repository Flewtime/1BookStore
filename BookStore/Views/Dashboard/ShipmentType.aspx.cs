using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace BookStore.Views
{
    public partial class ShipmentType : System.Web.UI.Page
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
                        shipmenttypeview.Visible = true;
                        shipmenttypeform.Visible = false;
                        lblstsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                shipmenttypeview.Visible = false;
                                shipmenttypeform.Visible = true;
                                shipmenttypeformtitle.InnerText = "Insert Shipment Type";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["ShipmentTypeID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

                                shipmenttypeview.Visible = false;
                                shipmenttypeform.Visible = true;
                                shipmenttypeformtitle.InnerText = "Edit Shipment Type";
                                btnPost.Text = "Update";

                                int ShipmentTypeID = Int32.Parse(Request["ShipmentTypeID"].ToString());
                                var shipmentType = shipmentTypeController.findShipmentTypeByID(ShipmentTypeID);
                                if (shipmentType != null)
                                {
                                    txtName.Text = shipmentType.ShipmentTypeName.ToString();
                                    txtDeliveryTime.Text = shipmentType.ShipmentTypeDeliveryTime.ToString();
                                    txtPrice.Text = shipmentType.ShipmentTypePrice.ToString();
                                }
                            }
                        }
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
            ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

            DataTable dt = new DataTable();
            dt.Columns.Add("ShipmentTypeID");
            dt.Columns.Add("ShipmentTypeName");
            dt.Columns.Add("ShipmentTypeDeliveryTime");
            dt.Columns.Add("ShipmentTypePrice");

            List<Model.ShipmentType> shipmentTypeList = shipmentTypeController.getAllShipmentType();
            foreach (Model.ShipmentType shipmentType in shipmentTypeList)
            {
                DataRow dr = dt.NewRow();
                dr["ShipmentTypeID"] = shipmentType.ShipmentTypeID;
                dr["ShipmentTypeName"] = shipmentType.ShipmentTypeName;
                dr["ShipmentTypeDeliveryTime"] = shipmentType.ShipmentTypeDeliveryTime + " days";
                dr["ShipmentTypePrice"] = shipmentType.ShipmentTypePrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dt.Rows.Add(dr);
            }

            gvshipmenttype.DataSource = dt;
            gvshipmenttype.DataBind();
        }

        protected void btnstSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtstSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvshipmenttype.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("ShipmentTypeID like '%" + searchTerm + "%' or ShipmentTypeName like '%" + searchTerm + "%' or ShipmentTypeDeliveryTime like '%" + searchTerm + "%' or ShipmentTypePrice like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvshipmenttype.DataSource = filteredDt;
                gvshipmenttype.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblstsearch.Visible = true;
                    lblstsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblstsearch.Visible = false;
                    lblstsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblstsearch.Visible = false;
                lblstsearch.Text = "";
            }
        }

        protected void gvshipmenttype_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvshipmenttype.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvshipmenttype_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

            GridViewRow row = gvshipmenttype.Rows[e.RowIndex];
            int ShipmentTypeID = Int32.Parse(row.Cells[1].Text.ToString());

            shipmentTypeController.deleteShipmentType(ShipmentTypeID);

            Response.Redirect("~/Views/Dashboard/ShipmentType.aspx");
        }

        protected void gvshipmenttype_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvshipmenttype.Rows[e.NewEditIndex];
            int ShipmentTypeID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/ShipmentType.aspx?ShipmentTypeID=" + ShipmentTypeID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

                    string shipmentTypeName = txtName.Text.ToString().Trim();
                    int shipmentTypeDeliveryTime = Int32.Parse(txtDeliveryTime.Text.ToString().Trim());
                    int shipmentTypePrice = Int32.Parse(txtPrice.Text.ToString().Trim());
                    Boolean checkShipmentTypeName = true;

                    string validateInsert = shipmentTypeController.insertShipmentType(shipmentTypeName, shipmentTypeDeliveryTime, shipmentTypePrice, checkShipmentTypeName);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/ShipmentType.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["ShipmentTypeID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    ShipmentTypeController shipmentTypeController = new ShipmentTypeController();

                    int ShipmentTypeID = Int32.Parse(Request["ShipmentTypeID"].ToString());
                    string shipmentTypeName = txtName.Text.ToString().Trim();
                    int shipmentTypeDeliveryTime = Int32.Parse(txtDeliveryTime.Text.ToString().Trim());
                    int shipmentTypePrice = Int32.Parse(txtPrice.Text.ToString().Trim());
                    Boolean checkShipmentTypeName = true;

                    var shipmentType = shipmentTypeController.findShipmentTypeByID(ShipmentTypeID);
                    if(shipmentTypeName.Equals(shipmentType.ShipmentTypeName))
                    {
                        checkShipmentTypeName = false;
                    }

                    string validateUpdate = shipmentTypeController.updateShipmentType(ShipmentTypeID, shipmentTypeName, shipmentTypeDeliveryTime, shipmentTypePrice, checkShipmentTypeName);
                    if (validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/ShipmentType.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }

        protected void btnstInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/ShipmentType.aspx?action=insert");
        }
    }
}