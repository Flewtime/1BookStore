using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Web.UI.WebControls.WebParts;

namespace BookStore.Views
{
    public partial class CartPage : System.Web.UI.Page
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

                var u = (User)Session["sessionLogin"];
                if (u.UserRole.Equals("Customer"))
                {
                }
                else
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                }
            }

            if (!IsPostBack)
            {
                carttitle.Visible = true;

                CartController cartController = new CartController();

                var user = (User)Session["sessionLogin"];
                int UserID = user.UserID;
                List<Cart> cartList = cartController.getAllCartByUser(UserID);

                carttitle.InnerText = "All Carts (" + cartList.Count + ")";

                rcart.DataSource = cartList;
                rcart.DataBind();

                if (!cartList.Any())
                {
                    lblcnone.Visible = true;
                    btncheckout.Visible = false;
                }
                else
                {
                    lblcnone.Visible = false;
                    btncheckout.Visible = true;
                }
            }   
        }

        protected void btncdelete_Command(object sender, CommandEventArgs e)
        {
            CartController cartController = new CartController();

            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;
            int BookID = int.Parse(e.CommandArgument.ToString());
            cartController.deleteCart(UserID, BookID);

            rcart.DataSource = cartController.getAllCartByUser(UserID);
            rcart.DataBind();
        }

        protected void btnccheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ShipmentPage.aspx");
        }
    }
}