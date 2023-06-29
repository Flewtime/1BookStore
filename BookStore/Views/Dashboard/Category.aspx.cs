using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using System.Web.Configuration;

namespace BookStore.Views.Dashboard
{
    public partial class Category : System.Web.UI.Page
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
                        categoryview.Visible = true;
                        categoryform.Visible = false;
                        lblcsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                categoryview.Visible = false;
                                categoryform.Visible = true;
                                categoryformtitle.InnerText = "Insert Book Category";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["CategoryID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                CategoryController categoryController = new CategoryController();

                                categoryview.Visible = false;
                                categoryform.Visible = true;
                                categoryformtitle.InnerText = "Edit Book Category";
                                btnPost.Text = "Update";

                                int CategoryID = Int32.Parse(Request["CategoryID"].ToString());
                                var category = categoryController.findCategoryByID(CategoryID);
                                if (category != null)
                                {
                                    txtName.Text = category.CategoryName.ToString();
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

            gvcategory.DataSource = dt;
            gvcategory.DataBind();
        }

        protected void btncSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtcSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvcategory.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("CategoryID like '%" + searchTerm + "%' or CategoryName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvcategory.DataSource = filteredDt;
                gvcategory.DataBind();

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

        protected void gvcategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvcategory.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvcategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CategoryController categoryController = new CategoryController();

            GridViewRow row = gvcategory.Rows[e.RowIndex];
            int CategoryID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;
            categoryController.deleteCategory(CategoryID, Path);

            Response.Redirect("~/Views/Dashboard/Category.aspx");
        }

        protected void gvcategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvcategory.Rows[e.NewEditIndex];
            int CategoryID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/Category.aspx?CategoryID=" + CategoryID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    CategoryController categoryController = new CategoryController();

                    string categoryName = txtName.Text.ToString().Trim();
                    Boolean checkCategoryName = true;

                    string validateInsert = categoryController.insertCategory(categoryName, checkCategoryName);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Category.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["CategoryID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    CategoryController categoryController = new CategoryController();

                    int CategoryID = Int32.Parse(Request["CategoryID"].ToString());
                    string categoryName = txtName.Text.ToString().Trim();
                    Boolean checkCategoryName = true;

                    var category = categoryController.findCategoryByID(CategoryID);
                    if (categoryName.Equals(category.CategoryName))
                    {
                        checkCategoryName = false;
                    }

                    string validateUpdate = categoryController.updateCategory(CategoryID, categoryName, checkCategoryName);
                    if(validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Category.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }

        protected void btncInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Category.aspx?action=insert");
        }
    }
}