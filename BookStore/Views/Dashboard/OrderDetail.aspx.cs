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
    public partial class OrderDetail : System.Web.UI.Page
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
                        orderdetailview.Visible = true;
                        lblodsearch.Visible = false;

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
            OrderDetailController orderDetailController = new OrderDetailController();

            DataTable dt = new DataTable();
            dt.Columns.Add("OrderID");
            dt.Columns.Add("BookTitle");
            dt.Columns.Add("Qty");

            List<Model.OrderDetail> orderDetailList = orderDetailController.getAllOrderDetail();
            foreach (Model.OrderDetail orderDetail in orderDetailList)
            {
                DataRow dr = dt.NewRow();
                dr["OrderID"] = orderDetail.OrderID;
                dr["BookTitle"] = orderDetail.Book.BookTitle;
                dr["Qty"] = orderDetail.Qty;
                dt.Rows.Add(dr);
            }

            gvorderdetail.DataSource = dt;
            gvorderdetail.DataBind();
        }

        protected void btnodSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtodSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvorderdetail.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("OrderID like '%" + searchTerm + "%' or BookTitle like '%" + searchTerm + "%' or Qty like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvorderdetail.DataSource = filteredDt;
                gvorderdetail.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblodsearch.Visible = true;
                    lblodsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblodsearch.Visible = false;
                    lblodsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblodsearch.Visible = false;
                lblodsearch.Text = "";
            }
        }

        protected void gvorderdetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvorderdetail.PageIndex = e.NewPageIndex;

            bindGrid();
        }
    }
}