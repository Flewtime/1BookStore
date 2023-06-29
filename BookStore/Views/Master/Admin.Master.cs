using BookStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStore.Views.Master
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
                hlprofile.Text = user.UserName.ToString();
            }
        }
    }
}