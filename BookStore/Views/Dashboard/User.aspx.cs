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
    public partial class User : System.Web.UI.Page
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
                        userview.Visible = true;
                        lblussearch.Visible = false;

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
            UserController userController = new UserController();

            DataTable dt = new DataTable();
            dt.Columns.Add("UserID");
            dt.Columns.Add("UserName");
            dt.Columns.Add("UserEmail");
            dt.Columns.Add("UserPassword");
            dt.Columns.Add("UserPhoneNumber");
            dt.Columns.Add("UserDOB");
            dt.Columns.Add("UserGender");
            dt.Columns.Add("UserAddress");
            dt.Columns.Add("UserImage");
            dt.Columns.Add("UserRole");

            List<Model.User> userList = userController.getAllUser();
            foreach (Model.User user in userList)
            {
                DataRow dr = dt.NewRow();
                dr["UserID"] = user.UserID;
                dr["UserName"] = user.UserName;
                dr["UserEmail"] = user.UserEmail;
                dr["UserPassword"] = user.UserPassword;
                dr["UserPhoneNumber"] = user.UserPhoneNumber;
                dr["UserDOB"] = user.UserDOB.ToString("dd/MM/yyyy");
                dr["UserGender"] = user.UserGender;
                dr["UserAddress"] = user.UserAddress;
                dr["UserImage"] = user.UserImage;
                dr["UserRole"] = user.UserRole;
                dt.Rows.Add(dr);
            }

            gvuser.DataSource = dt;
            gvuser.DataBind();
        }

        protected void btnusSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtusSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvuser.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("UserID like '%" + searchTerm + "%' or UserName like '%" + searchTerm + "%' or UserEmail like '%" + searchTerm + "%' or UserPassword like '%" + searchTerm + "%' or UserPhoneNumber like '%" + searchTerm + "%' or UserDOB like '%" + searchTerm + "%' or UserGender like '%" + searchTerm + "%' or UserAddress like '%" + searchTerm + "%' or UserImage like '%" + searchTerm + "%' or UserRole like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvuser.DataSource = filteredDt;
                gvuser.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblussearch.Visible = true;
                    lblussearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblussearch.Visible = false;
                    lblussearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblussearch.Visible = false;
                lblussearch.Text = "";
            }
        }

        protected void gvuser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvuser.PageIndex = e.NewPageIndex;

            bindGrid();
        }
    }
}