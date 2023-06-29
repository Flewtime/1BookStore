using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookStore.Model;
using BookStore.Controllers;
using System.Data;
using System.Xml.Linq;

namespace BookStore.Views.Dashboard
{
    public partial class Book : System.Web.UI.Page
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
                    if (!IsPostBack)
                    {
                        bookview.Visible = true;
                        bookform.Visible = false;
                        lblbsearch.Visible = false;
                        lblError.Visible = false;

                        bindGrid();
                    }

                    if (Request["action"] != null)
                    {
                        if (Request["action"].ToString().Equals("insert"))
                        {
                            if (!IsPostBack)
                            {
                                GenreController genreController = new GenreController();
                                CategoryController categoryController = new CategoryController();
                                AuthorController authorController = new AuthorController();
                                PublisherController publisherController = new PublisherController();
                                LanguageController languageController = new LanguageController();
                                CoverTypeController coverTypeController = new CoverTypeController();

                                List<Model.Genre> genreList = genreController.getAllGenre();
                                List<Model.Category> categoryList = categoryController.getAllCategory();
                                List<Model.Author> authorList = authorController.getAllAuthor();
                                List<Model.Publisher> publisherList = publisherController.getAllPublisher();
                                List<Model.Language> languageList = languageController.getAllLanguage();
                                List<Model.CoverType> coverTypeList = coverTypeController.getAllCoverType();

                                ddlGenre.DataSource = genreList;
                                ddlGenre.DataTextField = "GenreName";
                                ddlGenre.DataValueField = "GenreID";
                                ddlGenre.DataBind();

                                ddlCategory.DataSource = categoryList;
                                ddlCategory.DataTextField = "CategoryName";
                                ddlCategory.DataValueField = "CategoryID";
                                ddlCategory.DataBind();

                                ddlAuthor.DataSource = authorList;
                                ddlAuthor.DataTextField = "AuthorName";
                                ddlAuthor.DataValueField = "AuthorID";
                                ddlAuthor.DataBind();

                                ddlPublisher.DataSource = publisherList;
                                ddlPublisher.DataTextField = "PublisherName";
                                ddlPublisher.DataValueField = "PublisherID";
                                ddlPublisher.DataBind();

                                ddlLanguage.DataSource = languageList;
                                ddlLanguage.DataTextField = "LanguageName";
                                ddlLanguage.DataValueField = "LanguageID";
                                ddlLanguage.DataBind();

                                ddlCoverType.DataSource = coverTypeList;
                                ddlCoverType.DataTextField = "CoverTypeName";
                                ddlCoverType.DataValueField = "CoverTypeID";
                                ddlCoverType.DataBind();

                                bookview.Visible = false;
                                bookform.Visible = true;
                                bookformtitle.InnerText = "Insert Book";
                                btnPost.Text = "Insert";
                                calPublishedDate.Visible = false;
                                imgBook.Visible = false;
                            }
                        }
                        else if (Request["BookID"] != null && Request["action"].ToString().Equals("edit"))
                        {
                            if (!IsPostBack)
                            {
                                BookController bookController = new BookController();
                                GenreController genreController = new GenreController();
                                CategoryController categoryController = new CategoryController();
                                AuthorController authorController = new AuthorController();
                                PublisherController publisherController = new PublisherController();
                                LanguageController languageController = new LanguageController();
                                CoverTypeController coverTypeController = new CoverTypeController();

                                List<Model.Genre> genreList = genreController.getAllGenre();
                                List<Model.Category> categoryList = categoryController.getAllCategory();
                                List<Model.Author> authorList = authorController.getAllAuthor();
                                List<Model.Publisher> publisherList = publisherController.getAllPublisher();
                                List<Model.Language> languageList = languageController.getAllLanguage();
                                List<Model.CoverType> coverTypeList = coverTypeController.getAllCoverType();

                                ddlGenre.DataSource = genreList;
                                ddlGenre.DataTextField = "GenreName";
                                ddlGenre.DataValueField = "GenreID";
                                ddlGenre.DataBind();

                                ddlCategory.DataSource = categoryList;
                                ddlCategory.DataTextField = "CategoryName";
                                ddlCategory.DataValueField = "CategoryID";
                                ddlCategory.DataBind();

                                ddlAuthor.DataSource = authorList;
                                ddlAuthor.DataTextField = "AuthorName";
                                ddlAuthor.DataValueField = "AuthorID";
                                ddlAuthor.DataBind();

                                ddlPublisher.DataSource = publisherList;
                                ddlPublisher.DataTextField = "PublisherName";
                                ddlPublisher.DataValueField = "PublisherID";
                                ddlPublisher.DataBind();

                                ddlLanguage.DataSource = languageList;
                                ddlLanguage.DataTextField = "LanguageName";
                                ddlLanguage.DataValueField = "LanguageID";
                                ddlLanguage.DataBind();

                                ddlCoverType.DataSource = coverTypeList;
                                ddlCoverType.DataTextField = "CoverTypeName";
                                ddlCoverType.DataValueField = "CoverTypeID";
                                ddlCoverType.DataBind();

                                bookview.Visible = false;
                                bookform.Visible = true;
                                bookformtitle.InnerText = "Edit Book";
                                btnPost.Text = "Update";
                                calPublishedDate.Visible = false;
                                imgBook.Visible = true;

                                int BookID = Int32.Parse(Request["BookID"].ToString());
                                var book = bookController.findBookByID(BookID);
                                if (book != null)
                                {
                                    txtISBN.Text = book.BookISBN.ToString();
                                    txtTitle.Text = book.BookTitle.ToString();
                                    txtPrice.Text = book.BookPrice.ToString();
                                    txtStock.Text = book.BookStock.ToString();
                                    txtPage.Text = book.BookPage.ToString();
                                    txtWeight.Text = book.BookWeight.ToString();
                                    txtDimension.Text = book.BookDimension.ToString();
                                    txtSynopsis.Text = book.BookSynopsis.ToString();
                                    txtPublishedDate.Text = book.BookPublishedDate.ToString("dd/MM/yyyy");
                                    imgBook.ImageUrl = "~/Assets/Books/" + book.BookImage.ToString();
                                    ddlGenre.SelectedValue = book.GenreID.ToString();
                                    ddlCategory.SelectedValue = book.CategoryID.ToString();
                                    ddlAuthor.SelectedValue = book.AuthorID.ToString();
                                    ddlPublisher.SelectedValue = book.PublisherID.ToString();
                                    ddlLanguage.SelectedValue = book.LanguageID.ToString();
                                    ddlCoverType.SelectedValue = book.CoverTypeID.ToString();
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Views/LoginPage.aspx");
                }
            }
        }

        private void bindGrid()
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
            dt.Columns.Add("GenreName");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("AuthorName");
            dt.Columns.Add("PublisherName");
            dt.Columns.Add("LanguageName");
            dt.Columns.Add("CoverTypeName");

            List<Model.Book> bookList = bookController.getAllBook();
            foreach(Model.Book book in bookList)
            {
                DataRow dr = dt.NewRow();
                dr["BookID"] = book.BookID;
                dr["BookISBN"] = book.BookISBN;
                dr["BookTitle"] = book.BookTitle;
                dr["BookPrice"] = book.BookPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("id-ID"));
                dr["BookStock"] = book.BookStock;
                dr["BookPage"] = book.BookPage + " pages";
                dr["BookWeight"] = book.BookWeight + " grams";
                dr["BookDimension"] = book.BookDimension;
                dr["BookSynopsis"] = book.BookSynopsis;
                dr["BookPublishedDate"] = book.BookPublishedDate.ToString("dd/MM/yyyy");
                dr["BookImage"] = book.BookImage;
                dr["GenreName"] = book.Genre.GenreName;
                dr["CategoryName"] = book.Category.CategoryName;
                dr["AuthorName"] = book.Author.AuthorName;
                dr["PublisherName"] = book.Publisher.PublisherName;
                dr["LanguageName"] = book.Language.LanguageName;
                dr["CoverTypeName"] = book.CoverType.CoverTypeName;
                dt.Rows.Add(dr);
            }

            gvbook.DataSource = dt;
            gvbook.DataBind();
        }

        protected void btnbSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtbSearch.Text.ToString().Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bindGrid();
                DataTable dt = gvbook.DataSource as DataTable;
                DataRow[] filteredRows = dt.Select("BookID like '%" + searchTerm + "%' or BookISBN like '%" + searchTerm + "%' or BookTitle like '%" + searchTerm + "%' or BookPrice like '%" + searchTerm + "%' or BookStock like '%" + searchTerm + "%' or BookPage like '%" + searchTerm + "%' or BookWeight like '%" + searchTerm + "%' or BookDimension like '%" + searchTerm + "%' or BookSynopsis like '%" + searchTerm + "%' or BookPublishedDate like '%" + searchTerm + "%' or BookImage like '%" + searchTerm + "%' or GenreName like '%" + searchTerm + "%' or CategoryName like '%" + searchTerm + "%' or AuthorName like '%" + searchTerm + "%' or PublisherName like '%" + searchTerm + "%' or LanguageName like '%" + searchTerm + "%' or CoverTypeName like '%" + searchTerm + "%'");
                DataTable filteredDt = dt.Clone();
                foreach(DataRow row in filteredRows)
                {
                    filteredDt.ImportRow(row);
                }

                gvbook.DataSource = filteredDt;
                gvbook.DataBind();

                if(filteredDt.Rows.Count == 0)
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
                bindGrid();

                lblbsearch.Visible = false;
                lblbsearch.Text = "";
            }
        }

        protected void gvbook_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvbook.PageIndex = e.NewPageIndex;

            bindGrid();
        }

        protected void btnbInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Dashboard/Book.aspx?action=insert");
        }

        protected void gvbook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            BookController bookController = new BookController();

            GridViewRow row = gvbook.Rows[e.RowIndex];
            int BookID = Int32.Parse(row.Cells[1].Text.ToString());
            string Path = Request.PhysicalApplicationPath;
            bookController.deleteBook(BookID, Path);

            Response.Redirect("~/Views/Dashboard/Book.aspx");
        }

        protected void gvbook_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvbook.Rows[e.NewEditIndex];
            int BookID = Int32.Parse(row.Cells[1].Text.ToString());

            Response.Redirect("~/Views/Dashboard/Book.aspx?BookID=" + BookID + "&action=edit");
        }

        protected void calPublishedDate_SelectionChanged(object sender, EventArgs e)
        {
            txtPublishedDate.Text = calPublishedDate.SelectedDate.ToString("dd/MM/yyyy");

            calPublishedDate.Visible = false;
        }

        protected void btnCalendar_Click1(object sender, ImageClickEventArgs e)
        {
            if (calPublishedDate.Visible)
            {
                calPublishedDate.Visible = false;
            }
            else
            {
                calPublishedDate.Visible = true;
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (Request["action"] != null)
            {
                if (Request["action"].ToString().Equals("insert"))
                {
                    BookController bookController = new BookController();

                    string bookISBN = txtISBN.Text.ToString().Trim();
                    string bookTitle = txtTitle.Text.ToString().Trim();
                    int bookPrice = Int32.Parse(txtPrice.Text.ToString().Trim());
                    int bookStock = Int32.Parse(txtStock.Text.ToString().Trim());
                    int bookPage = Int32.Parse(txtPage.Text.ToString().Trim());
                    int bookWeight = Int32.Parse(txtWeight.Text.ToString().Trim());
                    string bookDimension = txtDimension.Text.ToString().Trim();
                    string bookSynopsis = txtSynopsis.Text.ToString().Trim();
                    DateTime bookPublishedDate = DateTime.Parse(txtPublishedDate.Text.ToString());
                    string bookImage = fuImage.PostedFile.FileName.ToString();
                    int GenreID = Int32.Parse(ddlGenre.SelectedValue.ToString());
                    int CategoryID = Int32.Parse(ddlCategory.SelectedValue.ToString());
                    int AuthorID = Int32.Parse(ddlAuthor.SelectedValue.ToString());
                    int PublisherID = Int32.Parse(ddlPublisher.SelectedValue.ToString());
                    int LanguageID = Int32.Parse(ddlLanguage.SelectedValue.ToString());
                    int CoverTypeID = Int32.Parse(ddlCoverType.SelectedValue.ToString());
                    Boolean checkBookISBN = true;

                    fuImage.SaveAs(Request.PhysicalApplicationPath + "/Assets/Books/" + bookImage);

                    string validateInsert = bookController.insertBook(bookISBN, bookTitle, bookPrice, bookStock, bookPage, bookWeight, bookDimension, bookSynopsis, bookPublishedDate, bookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID, checkBookISBN);
                    if(validateInsert.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Book.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateInsert;
                    }
                }
                else if (Request["BookID"] != null && Request["action"].ToString().Equals("edit"))
                {
                    BookController bookController = new BookController();

                    int BookID = Int32.Parse(Request["BookID"].ToString());
                    string bookISBN = txtISBN.Text.ToString().Trim();
                    string bookTitle = txtTitle.Text.ToString().Trim();
                    int bookPrice = Int32.Parse(txtPrice.Text.ToString().Trim());
                    int bookStock = Int32.Parse(txtStock.Text.ToString().Trim());
                    int bookPage = Int32.Parse(txtPage.Text.ToString().Trim());
                    int bookWeight = Int32.Parse(txtWeight.Text.ToString().Trim());
                    string bookDimension = txtDimension.Text.ToString().Trim();
                    string bookSynopsis = txtSynopsis.Text.ToString().Trim();
                    DateTime bookPublishedDate = DateTime.Parse(txtPublishedDate.Text.ToString());
                    string bookImage = "";
                    int GenreID = Int32.Parse(ddlGenre.SelectedValue.ToString());
                    int CategoryID = Int32.Parse(ddlCategory.SelectedValue.ToString());
                    int AuthorID = Int32.Parse(ddlAuthor.SelectedValue.ToString());
                    int PublisherID = Int32.Parse(ddlPublisher.SelectedValue.ToString());
                    int LanguageID = Int32.Parse(ddlLanguage.SelectedValue.ToString());
                    int CoverTypeID = Int32.Parse(ddlCoverType.SelectedValue.ToString());
                    Boolean checkBookISBN = true;

                    var book = bookController.findBookByID(BookID);
                    if (fuImage.HasFile)
                    {
                        bookImage = fuImage.PostedFile.FileName.ToString();
                    }
                    else
                    {
                        bookImage = book.BookImage;
                    }

                    if (!book.BookImage.Equals(bookImage))
                    {
                        fuImage.SaveAs(Request.PhysicalApplicationPath + "/Assets/Books/" + bookImage);
                        System.IO.File.Delete(Request.PhysicalApplicationPath + "/Assets/Books/" + book.BookImage);
                    }

                    if (bookISBN.Equals(book.BookISBN))
                    {
                        checkBookISBN = false;
                    }

                    string validateUpdate = bookController.updateBook(BookID, bookISBN, bookTitle, bookPrice, bookStock, bookPage, bookWeight, bookDimension, bookSynopsis, bookPublishedDate, bookImage, GenreID, CategoryID, AuthorID, PublisherID, LanguageID, CoverTypeID, checkBookISBN);
                    if(validateUpdate.Equals("Insert Success!"))
                    {
                        Response.Redirect("~/Views/Dashboard/Book.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = validateUpdate;
                    }
                }
            }
        }
    }
}