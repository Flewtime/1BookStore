using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using BookStore.Views.Master;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Web.UI.HtmlControls;

namespace BookStore.Views
{
    public partial class BookDetailPage : System.Web.UI.Page
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
                addcart.Visible = false;
                reviewinsert.Visible = false;
                reviewedit.Visible = false;
            }
            else
            {
                if (Session["sessionLogin"] == null)
                {
                    int UserID = int.Parse(Request.Cookies["cookieID"].Value.ToString());
                    var user = userController.findUserByID(UserID);
                    Session["sessionLogin"] = user;
                }

                addcart.Visible = true;
                reviewinsert.Visible = true;
                reviewedit.Visible = false;

                var u = (Model.User)Session["sessionLogin"];
                if (u.UserRole.Equals("Customer"))
                {
                    addcart.Visible = true;
                    reviewinsert.Visible = true;
                    reviewedit.Visible = false;
                }
                else
                {
                    addcart.Visible = false;
                    reviewinsert.Visible = false;
                    reviewedit.Visible = false;
                }
            }

            if (!IsPostBack)
            {
                HyperLink hlbook = (HyperLink)Master.FindControl("hlbook");
                hlbook.CssClass += " active";

                BookController bookController = new BookController();
                ReviewController reviewController = new ReviewController();

                int BookID = int.Parse(Request["BookID"].ToString());
                var book = bookController.findBookByID(BookID);
                List<Book> bookList = new List<Book>();
                bookList.Add(book);

                rbookdetail.DataSource = bookList;
                rbookdetail.DataBind();

                List<Review> reviewList = reviewController.getAllReviewByBook(BookID);

                rreview.DataSource = reviewList;
                rreview.DataBind();

                if (!reviewList.Any())
                {
                    lblrnone.Visible = true;
                }
                else
                {
                    lblrnone.Visible = false;
                }
            }

            if (Request["ReviewID"] != null && Request["BookID"] != null && Request["action"] != null && Request["action"].ToString().Equals("edit"))
            {
                if (!IsPostBack)
                {
                    ReviewController reviewController = new ReviewController();

                    reviewinsert.Visible = false;
                    reviewedit.Visible = true;

                    int ReviewID = int.Parse(Request["ReviewID"].ToString());
                    Review review = reviewController.findReviewByID(ReviewID);

                    ratingOldValue(review.ReviewRating);
                    var ratingedit = FindControl("ratingedit") as HtmlInputHidden;
                    if (ratingedit != null)
                    {
                        ratingedit.Value = review.ReviewRating.ToString();
                    }
                    txtreviewedit.Text = review.ReviewComment;
                }
            }
        }

        protected void rbookdetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReviewController reviewController = new ReviewController();

                var book = (Book)e.Item.DataItem;
                int BookID = book.BookID;
                List<BookRating> bookRatingList = new List<BookRating>();
                BookRating bookRating = reviewController.getAverageRatingByBook(BookID);
                bookRatingList.Add(bookRating);

                Repeater innerRepeater = e.Item.FindControl("rrating") as Repeater;
                innerRepeater.DataSource = bookRatingList;
                innerRepeater.DataBind();
            }
        }

        protected void btnreviewinsert_Click(object sender, EventArgs e)
        {
            ReviewController reviewController = new ReviewController();

            double rating = double.Parse(Request.Form["ratinginsert"].ToString());
            string reviewComment = txtreviewinsert.Text;
            DateTime reviewDateTime = DateTime.Now;
            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;
            int BookID = int.Parse(Request["BookID"].ToString());

            reviewController.insertReview(rating, reviewComment, reviewDateTime, UserID, BookID);

            Response.Redirect("~/Views/BookDetailPage.aspx?BookID=" + BookID);
        }

        protected void btnrdelete_Command(object sender, CommandEventArgs e)
        {
            ReviewController reviewController = new ReviewController();

            int ReviewID = int.Parse(e.CommandArgument.ToString());

            reviewController.deleteReview(ReviewID);

            int BookID = int.Parse(Request["BookID"].ToString());

            Response.Redirect("~/Views/BookDetailPage.aspx?BookID=" + BookID);
        }

        protected void rreview_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btndelete = e.Item.FindControl("btnrdelete") as Button;
                Button btnedit = e.Item.FindControl("btnredit") as Button;

                var row = e.Item.FindControl("reviewrow") as HtmlTableRow;
                var review = (Review)e.Item.DataItem as Review;
                int UserIDReview = review.UserID;

                if (Session["sessionLogin"] == null && Request.Cookies["cookieID"] == null)
                {
                    btndelete.Visible = false;
                    btnedit.Visible = false;
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
                    int UserIDSession = u.UserID;

                    if (UserIDReview == UserIDSession)
                    {
                        btndelete.Visible = true;
                        btnedit.Visible = true;
                    }
                    else
                    {
                        btndelete.Visible = false;
                        btnedit.Visible = false;
                    }
                }
            }   
        }

        protected void btnredit_Command(object sender, CommandEventArgs e)
        {
            int ReviewID = int.Parse(e.CommandArgument.ToString());
            int BookID = int.Parse(Request["BookID"].ToString());

            Response.Redirect("~/Views/BookDetailPage.aspx?ReviewID=" + ReviewID + "&BookID=" + BookID + "&action=edit");
        }

        protected void btnreviewedit_Click(object sender, EventArgs e)
        {
            ReviewController reviewController = new ReviewController();

            int ReviewID = int.Parse(Request["ReviewID"].ToString());
            double rating = double.Parse(Request.Form["ratingedit"].ToString());
            string reviewComment = txtreviewedit.Text;
            DateTime reviewDateTime = DateTime.Now;
            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;
            int BookID = int.Parse(Request["BookID"].ToString());

            reviewController.updateReview(ReviewID, rating, reviewComment, reviewDateTime, UserID, BookID);

            Response.Redirect("~/Views/BookDetailPage.aspx?BookID=" + BookID);
        }

        private void ratingOldValue(double reviewRating)
        {
            if (reviewRating == 0)
            {
                staredit1.Attributes.Add("style", "color: black");
                staredit2.Attributes.Add("style", "color: black");
                staredit3.Attributes.Add("style", "color: black");
                staredit4.Attributes.Add("style", "color: black");
                staredit5.Attributes.Add("style", "color: black");
            }
            else if (reviewRating == 1)
            {
                staredit1.Attributes.Add("style", "color: gold");
                staredit2.Attributes.Add("style", "color: black");
                staredit3.Attributes.Add("style", "color: black");
                staredit4.Attributes.Add("style", "color: black");
                staredit5.Attributes.Add("style", "color: black");
            }
            else if (reviewRating == 2)
            {
                staredit1.Attributes.Add("style", "color: gold");
                staredit2.Attributes.Add("style", "color: gold");
                staredit3.Attributes.Add("style", "color: black");
                staredit4.Attributes.Add("style", "color: black");
                staredit5.Attributes.Add("style", "color: black");
            }
            else if (reviewRating == 3)
            {
                staredit1.Attributes.Add("style", "color: gold");
                staredit2.Attributes.Add("style", "color: gold");
                staredit3.Attributes.Add("style", "color: gold");
                staredit4.Attributes.Add("style", "color: black");
                staredit5.Attributes.Add("style", "color: black");
            }
            else if (reviewRating == 4)
            {
                staredit1.Attributes.Add("style", "color: gold");
                staredit2.Attributes.Add("style", "color: gold");
                staredit3.Attributes.Add("style", "color: gold");
                staredit4.Attributes.Add("style", "color: gold");
                staredit5.Attributes.Add("style", "color: black");
            }
            else if (reviewRating == 5)
            {
                staredit1.Attributes.Add("style", "color: gold");
                staredit2.Attributes.Add("style", "color: gold");
                staredit3.Attributes.Add("style", "color: gold");
                staredit4.Attributes.Add("style", "color: gold");
                staredit5.Attributes.Add("style", "color: gold");
            }
        }

        private void ratingEmpty()
        {
            starinsert1.Attributes.Add("style", "color: black");
            starinsert2.Attributes.Add("style", "color: black");
            starinsert3.Attributes.Add("style", "color: black");
            starinsert4.Attributes.Add("style", "color: black");
            starinsert5.Attributes.Add("style", "color: black");
        }

        protected void btnaddtocart_Click(object sender, EventArgs e)
        {
            CartController cartController = new CartController();
            BookController bookController = new BookController();
            ReviewController reviewController = new ReviewController();

            var user = (User)Session["sessionLogin"];
            int UserID = user.UserID;
            int BookID = int.Parse(Request["BookID"].ToString());
            int Qty = int.Parse(Request.Form["txtquantity"].ToString());

            List<Model.Cart> cartList = cartController.getAllCartByUser(UserID);
            foreach (Model.Cart c in cartList)
            {
                if (c.BookID == BookID)
                {
                    string validateUpdateCart = cartController.updateCart(UserID, BookID, Qty.ToString());
                    if (validateUpdateCart.Equals("Book Added to Cart!"))
                    {
                        lblcvalidation.Visible = true;
                        lblcvalidation.Text = validateUpdateCart;

                        List<Book> bookList = new List<Book>();
                        Book book = bookController.findBookByID(BookID);
                        bookList.Add(book);

                        rbookdetail.DataSource = bookList;
                        rbookdetail.DataBind();

                        List<Review> reviewList = reviewController.getAllReviewByBook(BookID);

                        rreview.DataSource = reviewList;
                        rreview.DataBind();

                        Response.Redirect("~/Views/CartPage.aspx");
                    }
                    else
                    {
                        lblcvalidation.Visible = true;
                        lblcvalidation.Text = validateUpdateCart;
                    }

                    return;
                }
            }

            string validateCart = cartController.insertCart(UserID, BookID, Qty.ToString());
            if(validateCart.Equals("Book Added to Cart!"))
            {
                lblcvalidation.Visible = true;
                lblcvalidation.Text = validateCart;

                List<Book> bookList = new List<Book>();
                Book book = bookController.findBookByID(BookID);
                bookList.Add(book);

                rbookdetail.DataSource = bookList;
                rbookdetail.DataBind();

                List<Review> reviewList = reviewController.getAllReviewByBook(BookID);

                rreview.DataSource = reviewList;
                rreview.DataBind();

                Response.Redirect("~/Views/CartPage.aspx");
            }
            else
            {
                lblcvalidation.Visible = true;
                lblcvalidation.Text = validateCart;
            }
        }
    }
}