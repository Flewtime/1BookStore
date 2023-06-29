using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using BookStore.Dataset;
using BookStore.Report;

namespace BookStore.Views.Dashboard
{
    public partial class TransactionReport : System.Web.UI.Page
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
                    TransactionHeaderController transactionHeaderController = new TransactionHeaderController();

                    Report.TransactionReport transactionReport = new Report.TransactionReport();
                    CrystalReportViewer1.ReportSource = transactionReport;
                    List<TransactionHeader> transactionHeaderList = transactionHeaderController.getAllTransactionHeader();
                    DataSet1 data = reportDataSet(transactionHeaderList);
                    transactionReport.SetDataSource(data);
                }
                else
                {
                    Response.Redirect("~/Views/LoginPage.aspx");
                }
            }
        }

        private DataSet1 reportDataSet(List<TransactionHeader> transactionHeaderList)
        {
            BookController bookController = new BookController();

            DataSet1 data = new DataSet1();
            var headerTable = data.TransactionHeader;
            var detailTable = data.TransactionDetail;
            foreach (var th in transactionHeaderList)
            {
                int totalWeight = 0;
                int totalBookPrice = 0;
                foreach (var td in th.TransactionDetails)
                {
                    var dRow = detailTable.NewRow();
                    dRow["TransactionID"] = td.TransactionID;
                    dRow["BookID"] = td.BookID;
                    dRow["Quantity"] = td.Qty;
                    dRow["TransactionSubTotal"] = String.Format("{0:C2}", td.Book.BookPrice * td.Qty, System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                    detailTable.Rows.Add(dRow);
                    totalWeight = totalWeight + (td.Book.BookWeight * td.Qty);
                    totalBookPrice = totalBookPrice + (td.Book.BookPrice * td.Qty);
                }

                int shipmentPrice = th.Shipment.ShipmentType.ShipmentTypePrice * totalWeight;

                var hRow = headerTable.NewRow();
                hRow["TransactionID"] = th.TransactionID;
                hRow["TransactionDate"] = th.TransactionDateTime.ToString("dd/MM/yyyy");
                hRow["TransactionTime"] = th.TransactionDateTime.ToString("HH:mm");
                hRow["UserID"] = th.UserID;
                hRow["PaymentID"] = th.PaymentID;
                hRow["ShipmentID"] = th.ShipmentID;
                hRow["TotalBookPrice"] = totalBookPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                hRow["ShipmentPrice"] = shipmentPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                hRow["PaymentFee"] = th.Payment.PaymentMethod.PaymentMethodFee.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                hRow["TransactionGrandTotal"] = th.TransactionPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                hRow["PaymentStatus"] = th.Payment.PaymentStatus;
                hRow["ShipmentStatus"] = th.Shipment.ShipmentStatus;
                headerTable.Rows.Add(hRow);
            }

            return data;
        }
    }
}