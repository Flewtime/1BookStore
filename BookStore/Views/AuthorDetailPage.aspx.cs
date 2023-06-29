using BookStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using System.Web.UI.HtmlControls;

namespace BookStore.Views
{
    public partial class AuthorDetailPage : System.Web.UI.Page
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
            }

            if (!IsPostBack)
            {
                HyperLink hlbook = (HyperLink)Master.FindControl("hlbook");
                hlbook.CssClass += " active";

                bookviewtitle.Visible = true;
                lblnone.Visible = false;

                AuthorController authorController = new AuthorController();
                BookController bookController = new BookController();

                List<Author> authorList = new List<Author>();
                int AuthorID = int.Parse(Request["AuthorID"].ToString());
                Author author = authorController.findAuthorByID(AuthorID);
                authorList.Add(author);

                rauthordetail.DataSource = authorList;
                rauthordetail.DataBind();

                List<Model.Book> bookList = bookController.getAllBookByAuthor(AuthorID);

                bookviewtitle.InnerText = "All Book By " + author.AuthorName;

                rbook.DataSource = bookList;
                rbook.DataBind();

                if (!bookList.Any())
                {
                    lblnone.Visible = true;
                    lblnone.Text = "There is No Book By " + author.AuthorName;
                }
            }
        }

        protected void rbook_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReviewController reviewController = new ReviewController();

                var book = (Model.Book)e.Item.DataItem;
                int BookID = book.BookID;
                List<BookRating> bookRatingList = new List<BookRating>();
                BookRating bookRating = reviewController.getAverageRatingByBook(BookID);
                bookRatingList.Add(bookRating);

                Repeater innerRepeater = e.Item.FindControl("rrating") as Repeater;
                innerRepeater.DataSource = bookRatingList;
                innerRepeater.DataBind();

                var row = e.Item.FindControl("bookrow") as HtmlTableRow;
                var b = e.Item.DataItem as Model.Book;
                int BID = b.BookID;
                if (row != null)
                {
                    row.Attributes["onclick"] = "window.location.href='BookDetailPage.aspx?BookID=" + BID + "'";
                }
            }
        }
    }
}