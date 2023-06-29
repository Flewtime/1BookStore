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
    public partial class RegisterPage : System.Web.UI.Page
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
            }
            else
            {
                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }

                if (Request["action"] == null && !Request["action"].ToString().Equals("edit"))
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                }
            }

            if (!IsPostBack)
            {
                cdob.Visible = false;

                imgu.Visible = false;
                lbluerror.Visible = false;
                btnuregister.Visible = true;
                btnupdate.Visible = false;

                usertitleform.InnerText = "Register";

                List<string> userGender = new List<string>();
                userGender.Add("Male");
                userGender.Add("Female");
                rblugender.DataSource = userGender;
                rblugender.DataBind();
            }

            if (Request["action"] != null && Request["action"].ToString().Equals("edit"))
            {
                if (!IsPostBack)
                {
                    usertitleform.InnerText = "Edit Profile";
                    txtupassword.TextMode = TextBoxMode.SingleLine;

                    var user = (User)Session["sessionLogin"];

                    txtuname.Text = user.UserName.ToString();
                    txtuemail.Text = user.UserEmail.ToString();
                    txtupassword.Text = user.UserPassword.ToString();
                    txtuphonenumber.Text = user.UserPhoneNumber.ToString().Replace("+62", "");
                    txtudob.Text = user.UserDOB.ToString("dd/MM/yyyy");
                    rblugender.SelectedValue = user.UserGender.ToString();
                    txtuaddress.Text = user.UserAddress.ToString();
                    imgu.ImageUrl = "~/Assets/Users/" + user.UserImage.ToString();

                    imgu.Visible = true;
                    lbluerror.Visible = false;
                    btnuregister.Visible = false;
                    btnupdate.Visible = true;
                }
            }
        }

        protected void btnuregister_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            string userName = txtuname.Text.ToString().Trim();
            string userEmail = txtuemail.Text.ToString().Trim();
            string userPassword = txtupassword.Text.ToString().Trim();
            string userPhoneNumber = "+62" + txtuphonenumber.Text.ToString().Trim();
            string userDOB = txtudob.Text.ToString().Trim();
            string userGender = rblugender.SelectedValue.ToString();
            string userAddress = txtuaddress.Text.ToString().Trim();
            string userImage = fuuimage.PostedFile.FileName.ToString();
            string userRole = "Customer";
            Boolean checkUserEmail = true;
            Boolean checkUserImage = true;

            string validateRegister = userController.insertUser(userName, userEmail, userPassword, userPhoneNumber, userDOB, userGender, userAddress, userImage, userRole, checkUserEmail, checkUserImage);
            if(validateRegister.Equals("Register Success!"))
            {
                fuuimage.SaveAs(Request.PhysicalApplicationPath + "/Assets/Users/" + userImage);

                Response.Redirect("~/Views/LoginPage.aspx");
            }
            else
            {
                lbluerror.Visible = true;
                lbluerror.Text = validateRegister;
            }
        }

        protected void cdob_SelectionChanged(object sender, EventArgs e)
        {
            txtudob.Text = cdob.SelectedDate.ToString("dd/MM/yyyy");

            cdob.Visible = false;
        }

        protected void btncalendar_Click(object sender, EventArgs e)
        {
            if(cdob.Visible == false)
            {
                cdob.Visible = true;
            }
            else
            {
                cdob.Visible = false;
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            UserController userController = new UserController();

            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;
            string userName = txtuname.Text.ToString().Trim();
            string userEmail = txtuemail.Text.ToString().Trim();
            string userPassword = txtupassword.Text.ToString().Trim();
            string userPhoneNumber = "+62" + txtuphonenumber.Text.ToString().Trim();
            string userDOB = txtudob.Text.ToString();
            string userGender = rblugender.SelectedValue.ToString();
            string userAddress = txtuaddress.Text.ToString().Trim();
            string userImage = "";
            string userRole = "Customer";
            Boolean checkUserEmail = true;
            Boolean checkUserImage = true;

            if(userEmail.Equals(user.UserEmail))
            {
                checkUserEmail = false;
            }
            else if(!userEmail.Equals(user.UserEmail))
            {
                checkUserEmail = true;
            }

            if (fuuimage.HasFile == true)
            {
                checkUserImage = true;
                userImage = fuuimage.PostedFile.FileName.ToString();
            }
            else if (fuuimage.HasFile == false)
            {
                checkUserImage = false;
                userImage = user.UserImage;
            }

            string validateEdit = userController.updateUser(UserID, userName, userEmail, userPassword, userPhoneNumber, userDOB, userGender, userAddress, userImage, userRole, checkUserEmail, checkUserImage);
            if (validateEdit.Equals("Register Success!"))
            {
                if (userImage.Equals(user.UserImage) == false)
                {
                    fuuimage.SaveAs(Request.PhysicalApplicationPath + "/Assets/Users/" + userImage);
                    System.IO.File.Delete(Request.PhysicalApplicationPath + "/Assets/Users/" + user.UserImage);
                }

                Response.Redirect("~/Views/ProfilePage.aspx");
            }
            else
            {
                lbluerror.Visible = true;
                lbluerror.Text = validateEdit;
            }
        }
    }
}