using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;

namespace BookStore.Views
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            if (Request.Cookies["cookieEmail"] != null && Request.Cookies["cookiePassword"] != null)
            {
                txtuemail.Text = Request.Cookies["cookieEmail"].Value.ToString();
                txtupassword.Text = Request.Cookies["cookiePassword"].Value.ToString();
            }

            if (Session["sessionLogin"] == null && Request.Cookies["cookieID"] == null)
            {
            }
            else
            {
                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }

                Response.Redirect("~/Views/HomePage.aspx");
            }

            if (!IsPostBack)
            {
                lbluerror.Visible = false;
            }
        }

        protected void btnulogin_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            string userEmail = txtuemail.Text.ToString().Trim();
            string userPassword = txtupassword.Text.ToString().Trim();

            string validateLogin = userController.loginUser(userEmail, userPassword);
            if(validateLogin.Equals("Login Successfully!"))
            {
                var user = userController.findUserByLogin(userEmail, userPassword);
                Session["sessionLogin"] = user;
                if (cbxcremember.Checked == true)
                {
                    HttpCookie cookieID = new HttpCookie("cookieID");
                    HttpCookie cookieEmail = new HttpCookie("cookieEmail");
                    HttpCookie cookiePassword = new HttpCookie("cookiePassword");
                    cookieID.Value = user.UserID.ToString();
                    cookieEmail.Value = user.UserEmail.ToString();
                    cookiePassword.Value = user.UserPassword.ToString();
                    cookieID.Expires = DateTime.Now.AddHours(2);
                    cookieEmail.Expires = DateTime.Now.AddHours(2);
                    cookiePassword.Expires = DateTime.Now.AddHours(2);
                    Response.Cookies.Add(cookieID);
                    Response.Cookies.Add(cookieEmail);
                    Response.Cookies.Add(cookiePassword);
                }

                Response.Redirect("~/Views/HomePage.aspx");
            }
            else
            {
                lbluerror.Visible = true;
                lbluerror.Text = validateLogin;
            }
        }
    }
}