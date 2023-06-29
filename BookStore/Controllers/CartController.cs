using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;
using BookStore.Controllers;
using BookStore.Views.Master;

namespace BookStore.Controllers
{
    public class CartController
    {
        private CartHandler cartHandler;

        public CartController()
        {
            cartHandler = new CartHandler();
        }

        public string insertCart(int UserID, int BookID, string Qty)
        {
            string validate = validateCart(BookID, Qty);
            if (validate.Equals("Book Added to Cart!"))
            {
                cartHandler.insertCart(UserID, BookID, int.Parse(Qty));
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteCart(int UserID, int BookID)
        {
            cartHandler.deleteCart(UserID, BookID);
        }

        public void deleteAllCartByBook(int BookID)
        {
            cartHandler.deleteAllCartByBook(BookID);
        }

        public void deleteAllCartByUser(int UserID)
        {
            cartHandler.deleteAllCartByUser(UserID);
        }

        public string updateCart(int UserID, int BookID, string Qty)
        {
            string validate = validateCart(BookID, Qty);
            if (validate.Equals("Book Added to Cart!"))
            {
                cartHandler.updateCart(UserID, BookID, int.Parse(Qty));
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public Cart findCartByID(int UserID, int BookID)
        {
            return cartHandler.findCartByID(UserID, BookID);
        }

        public List<Cart> getAllCart()
        {
            return cartHandler.getAllCart();
        }

        public List<Cart> getAllCartByUser(int UserID)
        {
            return cartHandler.getAllCartByUser(UserID);
        }

        public List<Cart> getAllCartByBook(int BookID)
        {
            return cartHandler.getAllCartByBook(BookID);
        }

        private string validateCart(int BookID, string Qty)
        {
            BookController bookController = new BookController();

            var book = bookController.findBookByID(BookID);
            if (Qty.Length == 0 || int.Parse(Qty) > book.BookStock || int.Parse(Qty) <= 0)
            {
                if (Qty.ToString().Length == 0)
                {
                    return "Quantity Must be Filled!";
                }
                else if (int.Parse(Qty) > book.BookStock)
                {
                    return "Quantity Can't be More than Book Stock!";
                }
                else if (int.Parse(Qty) <= 0)
                {
                    return "Quantity Must be More than 0!";
                }
            }

            return "Book Added to Cart!";
        }   
    }
}