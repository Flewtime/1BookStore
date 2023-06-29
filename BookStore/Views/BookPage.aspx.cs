using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Web.UI.HtmlControls;
using System.Security.Policy;
using System.Data;

namespace BookStore.Views
{
    public partial class BookPage : System.Web.UI.Page
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

                ddlbooktype.Visible = true;
            }

            if (Request["GenreID"] != null)
            {
                if (!IsPostBack)
                {
                    booktitle.Visible = true;
                    lblnone.Visible = false;
                    lblbsearch.Visible = false;

                    BookController bookController = new BookController();
                    GenreController genreController = new GenreController();

                    int GenreID = int.Parse(Request["GenreID"].ToString());
                    var genre = genreController.findGenreByID(GenreID);
                    List<Model.Book> bookList = bookController.getAllBookByGenre(GenreID);

                    booktitle.InnerText = "All Books By Genre: " + genre.GenreName;

                    bindGrid(GenreID, -1, -1, -1, -1);

                    if (!bookList.Any())
                    {
                        lblnone.Visible = true;
                        lblnone.Text = "There is No Book In The " + genre.GenreName + " Genre!";
                    }
                }
            }
            else if (Request["CategoryID"] != null)
            {
                if (!IsPostBack)
                {
                    booktitle.Visible= true;
                    lblnone.Visible = false;
                    lblbsearch.Visible = false;

                    BookController bookController = new BookController();
                    CategoryController categoryController = new CategoryController();

                    int CategoryID = int.Parse(Request["CategoryID"].ToString());
                    var category = categoryController.findCategoryByID(CategoryID);
                    List<Model.Book> bookList = bookController.getAllBookByCategory(CategoryID);

                    booktitle.InnerText = "All Books By Category: " + category.CategoryName;

                    bindGrid(-1, CategoryID, -1, -1, -1);

                    if (!bookList.Any())
                    {
                        lblnone.Visible = true;
                        lblnone.Text = "There is No Book In The " + category.CategoryName + " Category!";
                    }
                }
            }
            else if (Request["AuthorID"] != null)
            {
                if (!IsPostBack)
                {
                    booktitle.Visible = true;
                    lblnone.Visible = false;
                    lblbsearch.Visible = false;

                    BookController bookController = new BookController();
                    AuthorController authorController = new AuthorController();

                    int AuthorID = int.Parse(Request["AuthorID"].ToString());
                    var author = authorController.findAuthorByID(AuthorID);
                    List<Model.Book> bookList = bookController.getAllBookByAuthor(AuthorID);

                    booktitle.InnerText = "All Books By Author: " + author.AuthorName;

                    bindGrid(-1, -1, AuthorID, -1, -1);

                    if (!bookList.Any())
                    {
                        lblnone.Visible = true;
                        lblnone.Text = "There is No " + author.AuthorName + "'s Book!";
                    }
                }
            }
            else if (Request["LanguageID"] != null)
            {
                if (!IsPostBack)
                {
                    booktitle.Visible = true;
                    lblnone.Visible = false;
                    lblbsearch.Visible = false;

                    BookController bookController = new BookController();
                    LanguageController languageController = new LanguageController();

                    int LanguageID = int.Parse(Request["LanguageID"].ToString());
                    var language = languageController.findLanguageByID(LanguageID);
                    List<Model.Book> bookList = bookController.getAllBookByLanguage(LanguageID);

                    booktitle.InnerText = "All Books By Language: " + language.LanguageName;

                    bindGrid(-1, -1, -1, LanguageID, -1);

                    if (!bookList.Any())
                    {
                        lblnone.Visible = true;
                        lblnone.Text = "There is No Book In The " + language.LanguageName + " Language!";
                    }
                }
            }
            else
            {
                if (!IsPostBack)
                {
                    booktitle.Visible = true;
                    lblnone.Visible = false;
                    lblbsearch.Visible = false;

                    BookController bookController = new BookController();

                    List<Model.Book> bookList = bookController.getAllBook();

                    booktitle.InnerText = "All Books";

                    bindGrid(-1, -1, -1, -1, 8);

                    if (!bookList.Any())
                    {
                        lblnone.Visible = true;
                        lblnone.Text = "There is No Book!";
                    }
                }
            }
        }

        private void bindGrid(int GenreID, int CategoryID, int AuthorID, int LanguageID, int BookID)
        {
            BookController bookController = new BookController();

            DataTable dt = new DataTable();
            dt.Columns.Add("BookID");
            dt.Columns.Add("BookISBN");
            dt.Columns.Add("BookTitle");
            dt.Columns.Add("BookPrice");
            dt.Columns.Add("BookStock");
            dt.Columns.Add("BookPage");
            dt.Columns.Add("BookWeight");
            dt.Columns.Add("BookDimension");
            dt.Columns.Add("BookSynopsis");
            dt.Columns.Add("BookPublishedDate");
            dt.Columns.Add("BookImage");
            dt.Columns.Add("GenreID");
            dt.Columns.Add("CategoryID");
            dt.Columns.Add("AuthorID");
            dt.Columns.Add("PublisherID");
            dt.Columns.Add("LanguageID");
            dt.Columns.Add("CoverTypeID");
            dt.Columns.Add("GenreName");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("AuthorName");
            dt.Columns.Add("PublisherName");
            dt.Columns.Add("LanguageName");
            dt.Columns.Add("CoverTypeName");

            List<Model.Book> bookList = new List<Model.Book>();
            if(GenreID != -1 && CategoryID == -1 && AuthorID == -1 && LanguageID == -1 && BookID == -1)
            {
                bookList = bookController.getAllBookByGenre(GenreID);
            }
            else if (GenreID == -1 && CategoryID != -1 && AuthorID == -1 && LanguageID == -1 && BookID == -1)
            {
                bookList = bookController.getAllBookByCategory(CategoryID);
            }
            else if (GenreID == -1 && CategoryID == -1 && AuthorID != -1 && LanguageID == -1 && BookID == -1)
            {
                bookList = bookController.getAllBookByAuthor(AuthorID);
            }
            else if (GenreID == -1 && CategoryID == -1 && AuthorID == -1 && LanguageID != -1 && BookID == -1)
            {
                bookList = bookController.getAllBookByLanguage(LanguageID);
            }
            else if (GenreID == -1 && CategoryID == -1 && AuthorID == -1 && LanguageID == -1 && BookID != -1)
            {
                bookList = bookController.getAllBook();
            }

            foreach (Model.Book book in bookList)
            {
                DataRow dr = dt.NewRow();
                dr["BookID"] = book.BookID;
                dr["BookISBN"] = book.BookISBN;
                dr["BookTitle"] = book.BookTitle;
                dr["BookPrice"] = book.BookPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["BookStock"] = book.BookStock;
                dr["BookPage"] = book.BookPage;
                dr["BookWeight"] = book.BookWeight;
                dr["BookDimension"] = book.BookDimension;
                dr["BookSynopsis"] = book.BookSynopsis;
                dr["BookPublishedDate"] = book.BookPublishedDate.ToString("dd/MM/yyyy");
                dr["BookImage"] = book.BookImage;
                dr["GenreID"] = book.GenreID;
                dr["CategoryID"] = book.CategoryID;
                dr["AuthorID"] = book.AuthorID;
                dr["PublisherID"] = book.PublisherID;
                dr["LanguageID"] = book.LanguageID;
                dr["CoverTypeID"] = book.CoverTypeID;
                dr["GenreName"] = book.Genre.GenreName;
                dr["CategoryName"] = book.Category.CategoryName;
                dr["AuthorName"] = book.Author.AuthorName;
                dr["PublisherName"] = book.Publisher.PublisherName;
                dr["LanguageName"] = book.Language.LanguageName;
                dr["CoverTypeName"] = book.CoverType.CoverTypeName;
                dt.Rows.Add(dr);
            }

            rbook.DataSource = dt;
            rbook.DataBind();
        }

        protected void rbook_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ReviewController reviewController = new ReviewController();

                var drv = (DataRowView) e.Item.DataItem;
                int BookID = Convert.ToInt32(drv["BookID"]);
                List<BookRating> bookRatingList = new List<BookRating>();
                BookRating bookRating = reviewController.getAverageRatingByBook(BookID);
                bookRatingList.Add(bookRating);

                Repeater innerRepeater = e.Item.FindControl("rrating") as Repeater;
                innerRepeater.DataSource = bookRatingList;
                innerRepeater.DataBind();

                var row = e.Item.FindControl("bookrow") as HtmlTableRow;
                if (row != null)
                {
                    row.Attributes["onclick"] = "window.location.href='BookDetailPage.aspx?BookID=" + BookID + "'";
                }
            }
        }

        protected void btnbSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtbSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                if (Request["GenreID"] != null)
                {
                    int GenreID = int.Parse(Request["GenreID"].ToString());
                    
                    bindGrid(GenreID, -1, -1, -1, -1);
                }
                else if (Request["CategoryID"] != null)
                {
                    int CategoryID = int.Parse(Request["CategoryID"].ToString());
                    
                    bindGrid(-1, CategoryID, -1, -1, -1);
                }
                else if (Request["AuthorID"] != null)
                {
                    int AuthorID = int.Parse(Request["AuthorID"].ToString());
                    
                    bindGrid(-1, -1, AuthorID, -1, -1);
                }
                else if (Request["LanguageID"] != null)
                {
                    int LanguageID = int.Parse(Request["LanguageID"].ToString());

                    bindGrid(-1, -1, -1, LanguageID, -1);  
                }
                else
                {
                    bindGrid(-1, -1, -1, -1, 8);
                }

                DataTable dt = rbook.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("BookISBN like '%" + searchTerm + "%' or BookTitle like '%" + searchTerm + "%' or BookPrice like '%" + searchTerm + "%' or BookStock like '%" + searchTerm + "%' or BookPage like '%" + searchTerm + "%' or BookWeight like '%" + searchTerm + "%' or BookDimension like '%" + searchTerm + "%' or BookSynopsis like '%" + searchTerm + "%' or BookPublishedDate like '%" + searchTerm + "%' or GenreName like '%" + searchTerm + "%' or CategoryName like '%" + searchTerm + "%' or AuthorName like '%" + searchTerm + "%' or PublisherName like '%" + searchTerm + "%' or LanguageName like '%" + searchTerm + "%' or CoverTypeName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach (DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                rbook.DataSource = filteredDt;
                rbook.DataBind();

                if (filteredDt.Rows.Count == 0)
                {
                    lblbsearch.Visible = true;
                    lblbsearch.Text = "No results were found for '" + searchTerm + "'!";
                }
                else
                {
                    lblbsearch.Visible = false;
                    lblbsearch.Text = "";
                }
            }
            else
            {
                if (Request["GenreID"] != null)
                {
                    int GenreID = int.Parse(Request["GenreID"].ToString());

                    bindGrid(GenreID, -1, -1, -1, -1);
                }
                else if (Request["CategoryID"] != null)
                {
                    int CategoryID = int.Parse(Request["CategoryID"].ToString());

                    bindGrid(-1, CategoryID, -1, -1, -1);
                }
                else if (Request["AuthorID"] != null)
                {
                    int AuthorID = int.Parse(Request["AuthorID"].ToString());

                    bindGrid(-1, -1, AuthorID, -1, -1);
                }
                else if (Request["LanguageID"] != null)
                {
                    int LanguageID = int.Parse(Request["LanguageID"].ToString());

                    bindGrid(-1, -1, -1, LanguageID, -1);
                }
                else
                {
                    bindGrid(-1, -1, -1, -1, 8);
                }

                lblbsearch.Visible = false;
                lblbsearch.Text = "";
            }
        }

        protected void ddlbooktype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlbooktype.SelectedValue.Equals("All Genres"))
            {
                Response.Redirect("~/Views/GenrePage.aspx");
            }
            else if(ddlbooktype.SelectedValue.Equals("All Categories"))
            {
                Response.Redirect("~/Views/CategoryPage.aspx");
            }
            else if(ddlbooktype.SelectedValue.Equals("All Authors"))
            {
                Response.Redirect("~/Views/AuthorPage.aspx");
            }
            else if(ddlbooktype.SelectedValue.Equals("All Languages"))
            {
                Response.Redirect("~/Views/LanguagePage.aspx");
            }
            else
            {
                Response.Redirect("~/Views/BookPage.aspx");
            }
        }
    }
}