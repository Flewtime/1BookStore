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
    public partial class CoverType : System.Web.UI.Page
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
                        covertypeview.Visible = true;
                        covertypeform.Visible = false;
                        lblctsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                covertypeview.Visible = false;
                                covertypeform.Visible = true;
                                covertypeformtitle.InnerText = "Insert Book Cover Type";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["CoverTypeID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                CoverTypeController coverTypeController = new CoverTypeController();

                                covertypeview.Visible = false;
                                covertypeform.Visible = true;
                                covertypeformtitle.InnerText = "Edit Book Cover Type";
                                btnPost.Text = "Update";

                                int CoverTypeID = Int32.Parse(Request["CoverTypeID"].ToString());
                                var coverType = coverTypeController.findCoverTypeByID(CoverTypeID);
                                if (coverType != null)
                                {
                                    txtName.Text = coverType.CoverTypeName.ToString();
                                    txtMaterial.Text = coverType.CoverTypeMaterial.ToString();
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
            CoverTypeController coverTypeController = new CoverTypeController();

            DataTable dt = new DataTable();
            dt.Columns.Add("CoverTypeID");
            dt.Columns.Add("CoverTypeName");
            dt.Columns.Add("CoverTypeMaterial");

            List<Model.CoverType> coverTypeList = coverTypeController.getAllCoverType();
            foreach (Model.CoverType coverType in coverTypeList)
            {
                DataRow dr = dt.NewRow();
                dr["CoverTypeID"] = coverType.CoverTypeID;
                dr["CoverTypeName"] = coverType.CoverTypeName;
                dr["CoverTypeMaterial"] = coverType.CoverTypeMaterial;
                dt.Rows.Add(dr);
            }

            gvcovertype.DataSource = dt;
            gvcovertype.DataBind();
        }

        protected void btnctSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtctSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvcovertype.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("CoverTypeID like '%" + searchTerm + "%' or CoverTypeName like '%" + searchTerm + "%' or CoverTypeMaterial like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvcovertype.DataSource = filteredDt;
                gvcovertype.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblctsearch.Visible = true;
                    lblctsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblctsearch.Visible = false;
                    lblctsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblctsearch.Visible = false;
                lblctsearch.Text = "";
            }
        }

        protected void gvcovertype_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvcovertype.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvcovertype_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CoverTypeController coverTypeController = new CoverTypeController();

            GridViewRow row = gvcovertype.Rows[e.RowIndex];
            int CoverTypeID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;
            coverTypeController.deleteCoverType(CoverTypeID, Path);

            Response.Redirect("~/Views/Dashboard/CoverType.aspx");
        }

        protected void gvcovertype_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvcovertype.Rows[e.NewEditIndex];
            int CoverTypeID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/CoverType.aspx?CoverTypeID=" + CoverTypeID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    CoverTypeController coverTypeController = new CoverTypeController();

                    string coverTypeName = txtName.Text.ToString().Trim();
                    string coverTypeMaterial = txtMaterial.Text.ToString().Trim();
                    Boolean checkCoverTypeName = true;

                    string validateInsert = coverTypeController.insertCoverType(coverTypeName, coverTypeMaterial, checkCoverTypeName);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/CoverType.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["CoverTypeID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    CoverTypeController coverTypeController = new CoverTypeController();

                    int CoverTypeID = Int32.Parse(Request["CoverTypeID"].ToString());
                    string coverTypeName = txtName.Text.ToString().Trim();
                    string coverTypeMaterial = txtMaterial.Text.ToString().Trim();
                    Boolean checkCoverTypeName = true;

                    var coverType = coverTypeController.findCoverTypeByID(CoverTypeID);
                    if (coverType.CoverTypeName.Equals(coverTypeName))
                    {
                        checkCoverTypeName = false;
                    }

                    string validateUpdate = coverTypeController.updateCoverType(CoverTypeID, coverTypeName, coverTypeMaterial, checkCoverTypeName);
                    if (validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/CoverType.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }

        protected void btnctInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/CoverType.aspx?action=insert");
        }
    }
}