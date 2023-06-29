using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;

namespace BookStore.Views.Dashboard
{
    public partial class Language : System.Web.UI.Page
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
                        languageview.Visible = true;
                        languageform.Visible = false;
                        lbllsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                languageview.Visible = false;
                                languageform.Visible = true;
                                languageformtitle.InnerText = "Insert Book Language";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["LanguageID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                LanguageController languageController = new LanguageController();

                                languageview.Visible = false;
                                languageform.Visible = true;
                                languageformtitle.InnerText = "Edit Book Language";
                                btnPost.Text = "Update";

                                int LanguageID = Int32.Parse(Request["LanguageID"].ToString());
                                var language = languageController.findLanguageByID(LanguageID);
                                if (language != null)
                                {
                                    txtName.Text = language.LanguageName.ToString();
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

            gvlanguage.DataSource = dt;
            gvlanguage.DataBind();
        }

        protected void btnlSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtlSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvlanguage.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("LanguageID like '%" + searchTerm + "%' or LanguageName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvlanguage.DataSource = filteredDt;
                gvlanguage.DataBind();

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

        protected void gvlanguage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvlanguage.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvlanguage_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            LanguageController languageController = new LanguageController();

            GridViewRow row = gvlanguage.Rows[e.RowIndex];
            int LanguageID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;
            languageController.deleteLanguage(LanguageID, Path);

            Response.Redirect("~/Views/Dashboard/Language.aspx");
        }

        protected void gvlanguage_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvlanguage.Rows[e.NewEditIndex];
            int LanguageID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/Language.aspx?LanguageID=" + LanguageID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    LanguageController languageController = new LanguageController();

                    string languageName = txtName.Text.ToString().Trim();
                    Boolean checkLanguageName = true;

                    string validateInsert = languageController.insertLanguage(languageName, checkLanguageName);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Language.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["LanguageID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    LanguageController languageController = new LanguageController();

                    int LanguageID = Int32.Parse(Request["LanguageID"].ToString());
                    string languageName = txtName.Text.ToString().Trim();
                    Boolean checkLanguageName = true;

                    var language = languageController.findLanguageByID(LanguageID);
                    if (languageName.Equals(language.LanguageName))
                    {
                        checkLanguageName = false;
                    }

                    string validateUpdate = languageController.updateLanguage(LanguageID, languageName, checkLanguageName);
                    if (validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Language.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }

        protected void btnlInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Language.aspx?action=insert");
        }
    }
}