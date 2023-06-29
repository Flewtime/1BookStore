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
    public partial class GenrePage : System.Web.UI.Page
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

                genretitle.Visible = true;
                lblnone.Visible = false;
                lblgsearch.Visible = false;

                GenreController genreController = new GenreController();

                List<Model.Genre> genreList = genreController.getAllGenre();

                genretitle.InnerText = "All Genres";

                bindGrid();

                if(!genreList.Any())
                {
                    lblnone.Visible = true;
                    lblnone.Text = "No Genre Found!";
                }
            }
        }

        private void bindGrid()
        {
            GenreController genreController = new GenreController();

            DataTable dt = new DataTable();
            dt.Columns.Add("GenreID");
            dt.Columns.Add("GenreName");

            List<Model.Genre> genreList = genreController.getAllGenre();
            foreach (Model.Genre genre in genreList)
            {
                DataRow dr = dt.NewRow();
                dr["GenreID"] = genre.GenreID;
                dr["GenreName"] = genre.GenreName;
                dt.Rows.Add(dr);
            }

            rgenre.DataSource = dt;
            rgenre.DataBind();
        }

        protected void rgenre_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = e.Item.FindControl("genrerow") as HtmlTableRow;
                var drv = (DataRowView)e.Item.DataItem;
                int GenreID = Convert.ToInt32(drv["GenreID"]);
                if (row != null)
                {
                    row.Attributes["onclick"] = "window.location.href='BookPage.aspx?GenreID=" + GenreID + "'";
                }
            }
        }

        protected void btngSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtgSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = rgenre.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("GenreName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                rgenre.DataSource = filteredDt;
                rgenre.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblgsearch.Visible = true;
                    lblgsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblgsearch.Visible = false;
                    lblgsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblgsearch.Visible = false;
                lblgsearch.Text = "";
            }
        }

        protected void ddlbooktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbooktype.SelectedValue.Equals("All Categories"))
            {
                Response.Redirect("~/Views/CategoryPage.aspx");
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
                Response.Redirect("~/Views/GenrePage.aspx");
            }
        }
    }
}