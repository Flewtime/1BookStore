using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using BookStore.Views.Master;
using System.Web.UI.HtmlControls;

namespace BookStore.Views
{
    public partial class ProfilePage : System.Web.UI.Page
    {
        void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["sessionLogin"] == null && Request.Cookies["cookieID"] == null)
            {
                this.MasterPageFile = "~/Views/Master/Guest.Master";
            }
            else
            {
                UserController userController = new UserController();

                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }

                var u = (User)Session["sessionLogin"];
                if (u.UserRole.Equals("Admin"))
                {
                    this.MasterPageFile = "~/Views/Master/Admin.Master";
                }
                else
                {
                    this.MasterPageFile = "~/Views/Master/Customer.Master";
                }
            }
        }

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
            }

            if (!IsPostBack)
            {
                profiletitle.Visible = true;

                List<User> userList = new List<User>();

                var user = (User)Session["sessionLogin"];
                userList.Add(user);

                profiletitle.InnerText = user.UserName + "'s Profile";

                rprofile.DataSource = userList;
                rprofile.DataBind();
            }
        }

        protected void btntogglepassword_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem item = btn.NamingContainer as RepeaterItem;
            var user = (User)Session["sessionLogin"];
            HtmlGenericControl passwordText = item.FindControl("passwordtext") as HtmlGenericControl;
            if (btn.Text == "Show")
            {
                passwordText.InnerHtml = user.UserPassword;
                btn.Text = "Hide";
            }
            else
            {
                passwordText.InnerHtml = new string('*', user.UserPassword.Length);
                btn.Text = "Show";
            }
        }

        protected void btncdelete_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;
            string Path = Request.PhysicalApplicationPath;
            userController.deleteUser(UserID, Path);

            string[] cookies = Request.Cookies.AllKeys;
            foreach (string cookie in cookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Remove("sessionLogin");

            Response.Redirect("~/Views/LoginPage.aspx");
        }

        protected void btncupdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/RegisterPage.aspx?action=edit");
        }

        protected void rprofile_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var user = e.Item.DataItem as User;
                HtmlGenericControl passwordText = e.Item.FindControl("passwordtext") as HtmlGenericControl;
                passwordText.InnerHtml = new string('*', user.UserPassword.Length);
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
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