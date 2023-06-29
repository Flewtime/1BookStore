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
    public partial class Cart : System.Web.UI.Page
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
                        cartview.Visible = true;
                        lblcasearch.Visible = false;

                        bindGrid();
                    }
                }
                else
                {
                    Response.Redirect("~/Views/LoginPage.aspx");
                }
            }
        }

        public void bindGrid()
        {
            CartController cartController = new CartController();

            DataTable dt = new DataTable();
            dt.Columns.Add("UserID");
            dt.Columns.Add("UserName");
            dt.Columns.Add("BookID");
            dt.Columns.Add("BookTitle");
            dt.Columns.Add("Qty");

            List<Model.Cart> cartList = cartController.getAllCart();
            foreach(Model.Cart cart in cartList)
            {
                DataRow dr = dt.NewRow();
                dr["UserID"] = cart.UserID;
                dr["UserName"] = cart.User.UserName;
                dr["BookID"] = cart.BookID;
                dr["BookTitle"] = cart.Book.BookTitle;
                dr["Qty"] = cart.Qty;
                dt.Rows.Add(dr);
            }

            gvcart.DataSource = dt;
            gvcart.DataBind();
        }

        protected void btncaSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtcaSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvcart.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("UserID like '%" + searchTerm + "%' or UserName like '%" + searchTerm + "%' or BookID like '%" + searchTerm + "%' or BookTitle like '%" + searchTerm + "%' or Qty like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach(DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvcart.DataSource = filteredDt;
                gvcart.DataBind();

                if(filteredDt.Rows.Count == 0)
                {
                    lblcasearch.Visible = true;
                    lblcasearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblcasearch.Visible = false;
                    lblcasearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblcasearch.Visible = false;
                lblcasearch.Text = "";
            }
        }

        protected void gvcart_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvcart.PageIndex = e.NewPageIndex;

            bindGrid();
        }
    }
}