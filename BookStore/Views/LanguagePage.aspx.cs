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
    public partial class LanguagePage : System.Web.UI.Page
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

                languagetitle.Visible = true;
                lblnone.Visible = false;
                lbllsearch.Visible = false;

                LanguageController languageController = new LanguageController();

                List<Language> languageList = languageController.getAllLanguage();

                languagetitle.InnerText = "All Languages";

                bindGrid();

                if(!languageList.Any())
                {
                    lblnone.Visible = true;
                    lblnone.Text = "No Language Found!";
                }
            }
        }

        private void bindGrid()
        {
            LanguageController languageController = new LanguageController();

            DataTable dt = new DataTable();
            dt.Columns.Add("LanguageID");
            dt.Columns.Add("LanguageName");

            List<Model.Language> languageList = languageController.getAllLanguage();
            foreach (Model.Language language in languageList)
            {
                DataRow dr = dt.NewRow();
                dr["LanguageID"] = language.LanguageID;
                dr["LanguageName"] = language.LanguageName;
                dt.Rows.Add(dr);
            }

            rlanguage.DataSource = dt;
            rlanguage.DataBind();
        }

        protected void rlanguage_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = e.Item.FindControl("languagerow") as HtmlTableRow;
                var drv = (DataRowView)e.Item.DataItem;
                int LanguageID = Convert.ToInt32(drv["LanguageID"]);
                if (row != null)
                {
                    row.Attributes["onclick"] = "window.location.href='BookPage.aspx?LanguageID=" + LanguageID + "'";
                }
            }
        }

        protected void btnlSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtlSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = rlanguage.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("LanguageName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                rlanguage.DataSource = filteredDt;
                rlanguage.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lbllsearch.Visible = true;
                    lbllsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lbllsearch.Visible = false;
                    lbllsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lbllsearch.Visible = false;
                lbllsearch.Text = "";
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
            else if (ddlbooktype.SelectedValue.Equals("All Genres"))
            {
                Response.Redirect("~/Views/GenrePage.aspx");
            }
            else
            {
                Response.Redirect("~/Views/LanguagePage.aspx");
            }
        }
    }
}