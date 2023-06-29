using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using System.Reflection.Emit;
using BookStore.Views.Master;

namespace BookStore.Views.Dashboard
{
    public partial class Author : System.Web.UI.Page
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
                        authorview.Visible = true;
                        authorform.Visible = false;
                        lblausearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                authorview.Visible = false;
                                authorform.Visible = true;
                                authorformtitle.InnerText = "Insert New Author";
                                btnPost.Text = "Insert";
                                calDOB.Visible = false;
                                imgAuthor.Visible = false;
                            }
                        }
                        else if (Request["AuthorID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                AuthorController authorController = new AuthorController();

                                authorview.Visible = false;
                                authorform.Visible = true;
                                authorformtitle.InnerText = "Edit Author";
                                btnPost.Text = "Update";
                                calDOB.Visible = false;
                                imgAuthor.Visible = true;

                                int AuthorID = Int32.Parse(Request["AuthorID"].ToString());
                                var author = authorController.findAuthorByID(AuthorID);
                                if (author != null)
                                {
                                    txtName.Text = author.AuthorName.ToString();
                                    txtDOB.Text = author.AuthorDOB.ToString("dd/MM/yyyy");
                                    txtBiography.Text = author.AuthorBiography.ToString();
                                    imgAuthor.ImageUrl = "~/Assets/Authors/" + author.AuthorImage.ToString();
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

            gvauthor.DataSource = dt;
            gvauthor.DataBind();
        }

        protected void btnauSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtauSearch.Text.ToString().Trim();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvauthor.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("AuthorID like '%" + searchTerm + "%' or AuthorName like '%" + searchTerm + "%' or AuthorDOB like '%" + searchTerm + "%' or AuthorBiography like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvauthor.DataSource = filteredDt;
                gvauthor.DataBind();

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

        protected void gvauthor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvauthor.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void btnauInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Author.aspx?action=insert");
        }

        protected void btniCalendar_Click(object sender, ImageClickEventArgs e)
        {
            if (calDOB.Visible)
            {
                calDOB.Visible = false;
            }
            else
            {
                calDOB.Visible = true;
            }
        }

        protected void calDOB_SelectionChanged(object sender, EventArgs e)
        {
            txtDOB.Text = calDOB.SelectedDate.ToString("dd/MM/yyyy");

            calDOB.Visible = false;
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    AuthorController authorController = new AuthorController();

                    string authorName = txtName.Text.ToString().Trim();
                    string authorDOB = txtDOB.Text.ToString().Trim();
                    string authorBiography = txtBiography.Text.ToString().Trim();
                    string authorImage = fuImage.PostedFile.FileName.ToString();
                    Boolean checkAuthorName = true;

                    string insertAuthor = authorController.insertAuthor(authorName, authorDOB, authorBiography, authorImage, checkAuthorName);
                    if(insertAuthor.Equals("Insert Success!"))
                    {
                        fuImage.SaveAs(Request.PhysicalApplicationPath + "/Assets/Authors/" + authorImage);

                        Response.Redirect("~/Views/Dashboard/Author.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = insertAuthor;
                    }
                }
                else if (Request["AuthorID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    AuthorController authorController = new AuthorController();

                    int AuthorID = Int32.Parse(Request["AuthorID"].ToString());
                    string authorName = txtName.Text.ToString().Trim();
                    string authorDOB = txtDOB.Text.ToString().Trim();
                    string authorBiography = txtBiography.Text.ToString().Trim();
                    string authorImage = "";
                    Boolean checkAuthorName = true;

                    var author = authorController.findAuthorByID(AuthorID);
                    if (author.AuthorName.Equals(authorName))
                    {
                        checkAuthorName = false;
                    }

                    if (fuImage.HasFile)
                    {
                        authorImage = fuImage.PostedFile.FileName.ToString();
                    }
                    else
                    {
                        authorImage = author.AuthorImage;
                    }

                    string updateAuthor = authorController.updateAuthor(AuthorID, authorName, authorDOB, authorBiography, authorImage, checkAuthorName);
                    if(updateAuthor.Equals("Insert Success!"))
                    {
                        if (author.AuthorImage.Equals(authorImage) == false)
                        {
                            fuImage.SaveAs(Request.PhysicalApplicationPath + "/Assets/Authors/" + authorImage);
                            System.IO.File.Delete(Request.PhysicalApplicationPath + "/Assets/Authors/" + author.AuthorImage);
                        }

                        Response.Redirect("~/Views/Dashboard/Author.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = updateAuthor;
                    }
                }
            }
        }

        protected void gvauthor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            AuthorController authorController = new AuthorController();

            GridViewRow row = gvauthor.Rows[e.RowIndex];
            int AuthorID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;
            authorController.deleteAuthor(AuthorID, Path);

            Response.Redirect("~/Views/Dashboard/Author.aspx");
        }

        protected void gvauthor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvauthor.Rows[e.NewEditIndex];
            int AuthorID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/Author.aspx?AuthorID=" + AuthorID + "&action=edit");
        }
    }
}