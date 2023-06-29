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
    public partial class Review : System.Web.UI.Page
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
                        reviewview.Visible = true;
                        lblrvsearch.Visible = false;

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
            ReviewController reviewController = new ReviewController();

            DataTable dt = new DataTable();
            dt.Columns.Add("ReviewID");
            dt.Columns.Add("ReviewRating");
            dt.Columns.Add("ReviewComment");
            dt.Columns.Add("ReviewDate");
            dt.Columns.Add("ReviewTime");
            dt.Columns.Add("UserName");
            dt.Columns.Add("BookTitle");

            List<Model.Review> reviewList = reviewController.getAllReview();
            foreach (Model.Review review in reviewList)
            {
                DataRow dr = dt.NewRow();
                dr["ReviewID"] = review.ReviewID;
                dr["ReviewRating"] = review.ReviewRating;
                dr["ReviewComment"] = review.ReviewComment;
                dr["ReviewDate"] = review.ReviewDateTime.ToString("dd/MM/yyyy");
                dr["ReviewTime"] = review.ReviewDateTime.ToString("HH:mm:ss");
                dr["UserName"] = review.User.UserName;
                dr["BookTitle"] = review.Book.BookTitle;
                dt.Rows.Add(dr);
            }

            gvreview.DataSource = dt;
            gvreview.DataBind();
        }

        protected void btnrvSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtrvSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvreview.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("ReviewID like '%" + searchTerm + "%' or ReviewRating like '%" + searchTerm + "%' or ReviewComment like '%" + searchTerm + "%' or ReviewDate like '%" + searchTerm + "%' or ReviewTime like '%" + searchTerm + "%' or UserName like '%" + searchTerm + "%' or BookTitle like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvreview.DataSource = filteredDt;
                gvreview.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblrvsearch.Visible = true;
                    lblrvsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblrvsearch.Visible = false;
                    lblrvsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblrvsearch.Visible = false;
                lblrvsearch.Text = "";
            }
        }

        protected void gvreview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvreview.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvreview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ReviewController reviewController = new ReviewController();

            GridViewRow row = gvreview.Rows[e.RowIndex];
            int reviewID = int.Parse(row.Cells[1].Text.ToString());

            reviewController.deleteReview(reviewID);
        }
    }
}