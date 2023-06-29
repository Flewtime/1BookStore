using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;

namespace BookStore.Views.Dashboard
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            if (Session["sessionLogin"] != null || Request.Cookies["cookieID"] != null)
            {
                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }
            }

            if (!IsPostBack)
            {
                var user = (Model.User)Session["sessionLogin"];
                lbladminname.Text = user.UserName.ToString();
                lbladminemail.Text = user.UserEmail.ToString();
                imgadmin.ImageUrl = "~/Assets/Users/" + user.UserImage.ToString();
            }
        }

        protected void lblogout_Click(object sender, EventArgs e)
        {
            string[] cookies = Request.Cookies.AllKeys;
            foreach (string cookie in cookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }

            Session.Remove("sessionLogin");

            Response.Redirect("~/Views/LoginPage.aspx");
        }
    }
}