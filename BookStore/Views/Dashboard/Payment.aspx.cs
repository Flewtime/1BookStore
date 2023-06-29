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
    public partial class Payment : System.Web.UI.Page
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
                        paymentview.Visible = true;
                        lblpdsearch.Visible = false;

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
            PaymentController paymentController = new PaymentController();

            DataTable dt = new DataTable();
            dt.Columns.Add("PaymentID");
            dt.Columns.Add("PaymentTotal");
            dt.Columns.Add("PaymentDate");
            dt.Columns.Add("PaymentTime");
            dt.Columns.Add("PaymentStatus");
            dt.Columns.Add("PaymentMethodName");

            List<Model.Payment> paymentList = paymentController.getAllPayment();
            foreach (Model.Payment payment in paymentList)
            {
                DataRow dr = dt.NewRow();
                dr["PaymentID"] = payment.PaymentID;
                dr["PaymentTotal"] = payment.PaymentTotal.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["PaymentDate"] = payment.PaymentDateTime.ToString("dd/MM/yyyy");
                dr["PaymentTime"] = payment.PaymentDateTime.ToString("HH:mm:ss");
                dr["PaymentStatus"] = payment.PaymentStatus;
                dr["PaymentMethodName"] = payment.PaymentMethod.PaymentMethodName;
                dt.Rows.Add(dr);
            }

            gvpaymentdetail.DataSource = dt;
            gvpaymentdetail.DataBind();
        }

        protected void btnpdSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtpdSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvpaymentdetail.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("PaymentID like '%" + searchTerm + "%' or PaymentTotal like '%" + searchTerm + "%' or PaymentDate like '%" + searchTerm + "%' or PaymentTime like '%" + searchTerm + "%' or PaymentStatus like '%" + searchTerm + "%' or PaymentMethodName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvpaymentdetail.DataSource = filteredDt;
                gvpaymentdetail.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblpdsearch.Visible = true;
                    lblpdsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblpdsearch.Visible = false;
                    lblpdsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblpdsearch.Visible = false;
                lblpdsearch.Text = "";
            }
        }

        protected void gvpaymentdetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpaymentdetail.PageIndex = e.NewPageIndex;

            bindGrid();
        }
    }
}