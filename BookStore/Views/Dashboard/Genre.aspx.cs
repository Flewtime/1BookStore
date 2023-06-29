using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using BookStore.Views.Dashboard;

namespace BookStore.Views
{
    public partial class Genre : System.Web.UI.Page
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
                        genreview.Visible = true;
                        genreform.Visible = false;
                        lblgsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                genreview.Visible = false;
                                genreform.Visible = true;
                                genreformtitle.InnerText = "Insert Book Genre";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["GenreID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                GenreController genreController = new GenreController();

                                genreview.Visible = false;
                                genreform.Visible = true;
                                genreformtitle.InnerText = "Edit Book Genre";
                                btnPost.Text = "Update";

                                int GenreID = Int32.Parse(Request["GenreID"].ToString());
                                var genre = genreController.findGenreByID(GenreID);
                                if (genre != null)
                                {
                                    txtName.Text = genre.GenreName.ToString();
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

            gvgenre.DataSource = dt;
            gvgenre.DataBind();
        }

        protected void btngSearch_Click1(object sender, EventArgs e)
        {
            string searchTerm = txtgSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvgenre.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("GenreID like '%" + searchTerm + "%' or GenreName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvgenre.DataSource = filteredDt;
                gvgenre.DataBind();

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

        protected void gvgenre_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvgenre.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvgenre_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GenreController genreController = new GenreController();

            GridViewRow row = gvgenre.Rows[e.RowIndex];
            int GenreID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;
            genreController.deleteGenre(GenreID, Path);

            Response.Redirect("~/Views/Dashboard/Genre.aspx");
        }

        protected void gvgenre_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvgenre.Rows[e.NewEditIndex];
            int GenreID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/Genre.aspx?GenreID=" + GenreID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    GenreController genreController = new GenreController();

                    string genreName = txtName.Text.ToString().Trim();
                    Boolean checkGenreName = true;

                    string validateInsert = genreController.insertGenre(genreName, checkGenreName);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Genre.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["GenreID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    GenreController genreController = new GenreController();

                    int GenreID = Int32.Parse(Request["GenreID"].ToString());
                    string genreName = txtName.Text.ToString().Trim();
                    Boolean checkGenreName = true;

                    var genre = genreController.findGenreByID(GenreID);
                    if (genreName.Equals(genre.GenreName))
                    {
                        checkGenreName = false;
                    }

                    string validateUpdate = genreController.updateGenre(GenreID, genreName, checkGenreName);
                    if(validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Genre.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }

        protected void btngInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Genre.aspx?action=insert");
        }
    }
}