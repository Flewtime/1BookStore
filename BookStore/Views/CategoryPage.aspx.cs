using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Web.UI.HtmlControls;
using System.Data;

namespace BookStore.Views
{
    public partial class CategoryPage : System.Web.UI.Page
    {
        void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["sessionLogin"] == null && Request.Cookies["cookieID"] == null)
            {
                this.MasterPageFile = "~/Views/Master/Guest.Master";
            }
            else
            {
                UserController userController = new UserController();

                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }

                var u = (User)Session["sessionLogin"];
                if (u.UserRole.Equals("Admin"))
                {
                    this.MasterPageFile = "~/Views/Master/Admin.Master";
                }
                else
                {
                    this.MasterPageFile = "~/Views/Master/Customer.Master";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            if (Session["sessionLogin"] == null && Request.Cookies["cookieID"] == null)
            {
            }
            else
            {
                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }
            }

            if (!IsPostBack)
            {
                HyperLink hlbook = (HyperLink)Master.FindControl("hlbook");
                hlbook.CssClass += " active";

                categorytitle.Visible = true;
                lblnone.Visible = false;
                lblcsearch.Visible = false;

                CategoryController categoryController = new CategoryController();

                List<Category> categoryList = categoryController.getAllCategory();

                categorytitle.InnerText = "All Categories";

                bindGrid();

                if(!categoryList.Any())
                {
                    lblnone.Visible = true;
                    lblnone.Text = "No Category Found!";
                }
            }
        }

        private void bindGrid()
        {
            CategoryController categoryController = new CategoryController();

            DataTable dt = new DataTable();
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("CategoryName");

            List<Model.Category> categoryList = categoryController.getAllCategory();
            foreach (Model.Category category in categoryList)
            {
                DataRow dr = dt.NewRow();
                dr["CategoryID"] = category.CategoryID;
                dr["CategoryName"] = category.CategoryName;
                dt.Rows.Add(dr);
            }

            rcategory.DataSource = dt;
            rcategory.DataBind();
        }

        protected void rcategory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = e.Item.FindControl("categoryrow") as HtmlTableRow;
                var drv = (DataRowView)e.Item.DataItem;
                int CategoryID = Convert.ToInt32(drv["CategoryID"]);
                if (row != null)
                {
                    row.Attributes["onclick"] = "window.location.href='BookPage.aspx?CategoryID=" + CategoryID + "'";
                }
            }
        }

        protected void btncSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtcSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = rcategory.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("CategoryName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                rcategory.DataSource = filteredDt;
                rcategory.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblcsearch.Visible = true;
                    lblcsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblcsearch.Visible = false;
                    lblcsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblcsearch.Visible = false;
                lblcsearch.Text = "";
            }
        }

        protected void ddlbooktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbooktype.SelectedValue.Equals("All Genres"))
            {
                Response.Redirect("~/Views/GenrePage.aspx");
            }
            else if (ddlbooktype.SelectedValue.Equals("All Authors"))
            {
                Response.Redirect("~/Views/AuthorPage.aspx");
            }
            else if (ddlbooktype.SelectedValue.Equals("All Books"))
            {
                Response.Redirect("~/Views/BookPage.aspx");
            }
            else if (ddlbooktype.SelectedValue.Equals("All Languages"))
            {
                Response.Redirect("~/Views/LanguagePage.aspx");
            }
            else
            {
                Response.Redirect("~/Views/CategoryPage.aspx");
            }
        }
    }
}