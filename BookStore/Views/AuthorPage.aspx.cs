using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Web.UI.HtmlControls;
using BookStore.Views.Master;
using System.Data;

namespace BookStore.Views
{
    public partial class AuthorPage : System.Web.UI.Page
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

                authortitle.Visible = true;
                lblnone.Visible = false;
                lblausearch.Visible = false;

                AuthorController authorController = new AuthorController();

                List<Author> authorList = authorController.getAllAuthor();

                authortitle.InnerText = "All Authors";

                bindGrid();

                if (!authorList.Any())
                {
                    lblnone.Visible = true;
                    lblnone.Text = "No Author Found!";
                }
            }
        }

        private void bindGrid()
        {
            AuthorController authorController = new AuthorController();

            DataTable dt = new DataTable();
            dt.Columns.Add("AuthorID");
            dt.Columns.Add("AuthorName");
            dt.Columns.Add("AuthorDOB");
            dt.Columns.Add("AuthorBiography");
            dt.Columns.Add("AuthorImage");

            List<Model.Author> authorList = authorController.getAllAuthor();
            foreach (Model.Author author in authorList)
            {
                DataRow dr = dt.NewRow();
                dr["AuthorID"] = author.AuthorID;
                dr["AuthorName"] = author.AuthorName;
                dr["AuthorDOB"] = author.AuthorDOB.ToString("dd/MM/yyyy");
                dr["AuthorBiography"] = author.AuthorBiography;
                dr["AuthorImage"] = author.AuthorImage;
                dt.Rows.Add(dr);
            }

            rauthor.DataSource = dt;
            rauthor.DataBind();
        }

        protected void rauthor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = e.Item.FindControl("authorrow") as HtmlTableRow;
                var drv = (DataRowView)e.Item.DataItem;
                int AuthorID = Convert.ToInt32(drv["AuthorID"]);
                if (row != null)
                {
                    row.Attributes["onclick"] = "window.location.href='AuthorDetailPage.aspx?AuthorID=" + AuthorID + "'";
                }
            }
        }

        protected void btnauSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtauSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = rauthor.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("AuthorName like '%" + searchTerm + "%' or AuthorDOB like '%" + searchTerm + "%' or AuthorBiography like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                rauthor.DataSource = filteredDt;
                rauthor.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblausearch.Visible = true;
                    lblausearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblausearch.Visible = false;
                    lblausearch.Text = "";
                }

            }
            else
            {
                bindGrid();

                lblausearch.Visible = false;
                lblausearch.Text = "";
            }
        }

        protected void ddlbooktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbooktype.SelectedValue.Equals("All Genres"))
            {
                Response.Redirect("~/Views/GenrePage.aspx");
            }
            else if (ddlbooktype.SelectedValue.Equals("All Categories"))
            {
                Response.Redirect("~/Views/CategoryPage.aspx");
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
                Response.Redirect("~/Views/AuthorPage.aspx");
            }
        }
    }
}