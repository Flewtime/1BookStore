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
    public partial class PaymentMethod : System.Web.UI.Page
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
                        paymentmethodview.Visible = true;
                        paymentmethodform.Visible = false;
                        lblpmsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                paymentmethodview.Visible = false;
                                paymentmethodform.Visible = true;
                                paymentmethodformtitle.InnerText = "Insert Payment Method";
                                btnPost.Text = "Insert";
                            }
                        }
                        else if (Request["PaymentMethodID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                PaymentMethodController paymentMethodController = new PaymentMethodController();

                                paymentmethodview.Visible = false;
                                paymentmethodform.Visible = true;
                                paymentmethodformtitle.InnerText = "Edit Payment Method";
                                btnPost.Text = "Update";

                                int PaymentMethodID = Int32.Parse(Request["PaymentMethodID"].ToString());
                                var paymentMethod = paymentMethodController.findPaymentMethodByID(PaymentMethodID);
                                if (paymentMethod != null)
                                {
                                    txtName.Text = paymentMethod.PaymentMethodName.ToString();
                                    txtFee.Text = paymentMethod.PaymentMethodFee.ToString();
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
            PaymentMethodController paymentMethodController = new PaymentMethodController();

            DataTable dt = new DataTable();
            dt.Columns.Add("PaymentMethodID");
            dt.Columns.Add("PaymentMethodName");
            dt.Columns.Add("PaymentMethodFee");

            List<Model.PaymentMethod> paymentMethodList = paymentMethodController.getAllPaymentMethod();
            foreach (Model.PaymentMethod paymentMethod in paymentMethodList)
            {
                DataRow dr = dt.NewRow();
                dr["PaymentMethodID"] = paymentMethod.PaymentMethodID;
                dr["PaymentMethodName"] = paymentMethod.PaymentMethodName;
                dr["PaymentMethodFee"] = paymentMethod.PaymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dt.Rows.Add(dr);
            }

            gvpaymentmethod.DataSource = dt;
            gvpaymentmethod.DataBind();
        }

        protected void btnpmSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtpmSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvpaymentmethod.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("PaymentMethodID like '%" + searchTerm + "%' or PaymentMethodName like '%" + searchTerm + "%' or PaymentMethodFee like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvpaymentmethod.DataSource = filteredDt;
                gvpaymentmethod.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblpmsearch.Visible = true;
                    lblpmsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblpmsearch.Visible = false;
                    lblpmsearch.Text = "";
                }
            }
            else
            {
                bindGrid();

                lblpmsearch.Visible = false;
                lblpmsearch.Text = "";
            }
        }

        protected void gvpaymentmethod_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpaymentmethod.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void gvpaymentmethod_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            PaymentMethodController paymentMethodController = new PaymentMethodController();

            GridViewRow row = gvpaymentmethod.Rows[e.RowIndex];
            int PaymentMethodID = Int32.Parse(row.Cells[1].Text.ToString());
            paymentMethodController.deletePaymentMethod(PaymentMethodID);

            Response.Redirect("~/Views/Dashboard/PaymentMethod.aspx");
        }

        protected void gvpaymentmethod_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvpaymentmethod.Rows[e.NewEditIndex];
            int PaymentMethodID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/PaymentMethod.aspx?PaymentMethodID=" + PaymentMethodID + "&action=edit");
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    PaymentMethodController paymentMethodController = new PaymentMethodController();

                    string paymentMethodName = txtName.Text.ToString().Trim();
                    int paymentMethodFee = Int32.Parse(txtFee.Text.ToString().Trim());
                    Boolean checkPaymentMethodName = true;

                    string validateInsert = paymentMethodController.insertPaymentMethod(paymentMethodName, paymentMethodFee, checkPaymentMethodName);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/PaymentMethod.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["PaymentMethodID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    PaymentMethodController paymentMethodController = new PaymentMethodController();

                    int PaymentMethodID = Int32.Parse(Request["PaymentMethodID"].ToString());
                    string paymentMethodName = txtName.Text.ToString().Trim();
                    int paymentMethodFee = Int32.Parse(txtFee.Text.ToString().Trim());
                    Boolean checkPaymentMethodName = true;

                    var paymentMethod = paymentMethodController.findPaymentMethodByID(PaymentMethodID);
                    if(paymentMethodName.Equals(paymentMethod.PaymentMethodName))
                    {
                        checkPaymentMethodName = false;
                    }

                    string validateUpdate = paymentMethodController.updatePaymentMethod(PaymentMethodID, paymentMethodName, paymentMethodFee, checkPaymentMethodName);
                    if (validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/PaymentMethod.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }

        protected void btnpmInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/PaymentMethod.aspx?action=insert");
        }
    }
}