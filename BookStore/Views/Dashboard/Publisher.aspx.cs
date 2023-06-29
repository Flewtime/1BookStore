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
    public partial class Publisher : System.Web.UI.Page
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
                        publisherview.Visible = true;
                        publisherform.Visible = false;
                        lblpbsearch.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                publisherview.Visible = false;
                                publisherform.Visible = true;
                                publisherformtitle.InnerText = "Insert Publisher";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["PublisherID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                PublisherController publisherController = new PublisherController();

                                publisherview.Visible = false;
                                publisherform.Visible = true;
                                publisherformtitle.InnerText = "Edit Publisher";
                                btnPost.Text = "Update";

                                int PublisherID = Int32.Parse(Request["PublisherID"].ToString());
                                var publisher = publisherController.findPublisherByID(PublisherID);
                                if (publisher != null)
                                {
                                    txtCode.Text = publisher.PublisherCode.ToString();
                                    txtName.Text = publisher.PublisherName.ToString();
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
            PublisherController publisherController = new PublisherController();

            DataTable dt = new DataTable();
            dt.Columns.Add("PublisherID");
            dt.Columns.Add("PublisherCode");
            dt.Columns.Add("PublisherName");

            List<Model.Publisher> publisherList = publisherController.getAllPublisher();
            foreach (Model.Publisher publisher in publisherList)
            {
                DataRow dr = dt.NewRow();
                dr["PublisherID"] = publisher.PublisherID;
                dr["PublisherCode"] = publisher.PublisherCode;
                dr["PublisherName"] = publisher.PublisherName;
                dt.Rows.Add(dr);
            }

            gvpublisher.DataSource = dt;
            gvpublisher.DataBind();
        }

        protected void btnpbSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtpbSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvpublisher.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("PublisherID like '%" + searchTerm + "%' or PublisherCode like '%" + searchTerm + "%' or PublisherName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvpublisher.DataSource = filteredDt;
                gvpublisher.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblpbsearch.Visible = true;
                    lblpbsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblpbsearch.Visible = false;
                    lblpbsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblpbsearch.Visible = false;
                lblpbsearch.Text = "";
            }
        }

        protected void gvpublisher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpublisher.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvpublisher_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            PublisherController publisherController = new PublisherController();

            GridViewRow row = gvpublisher.Rows[e.RowIndex];
            int PublisherID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;

            publisherController.deletePublisher(PublisherID, Path);

            Response.Redirect("~/Views/Dashboard/Publisher.aspx");
        }

        protected void gvpublisher_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvpublisher.Rows[e.NewEditIndex];
            int PublisherID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/Publisher.aspx?PublisherID=" + PublisherID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    PublisherController publisherController = new PublisherController();

                    string publisherCode = txtCode.Text.ToString().Trim();
                    string publisherName = txtName.Text.ToString().Trim();

                    publisherController.insertPublisher(publisherCode, publisherName);

                    Response.Redirect("~/Views/Dashboard/Publisher.aspx");
                }
                else if (Request["PublisherID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    PublisherController publisherController = new PublisherController();

                    int PublisherID = Int32.Parse(Request["PublisherID"].ToString());
                    string publisherCode = txtCode.Text.ToString().Trim();
                    string publisherName = txtName.Text.ToString().Trim();

                    var publisher = publisherController.findPublisherByID(PublisherID);

                    publisherController.updatePublisher(PublisherID, publisherCode, publisherName);

                    Response.Redirect("~/Views/Dashboard/Publisher.aspx");
                }
            }
        }

        protected void btnpbInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Publisher.aspx?action=insert");
        }
    }
}