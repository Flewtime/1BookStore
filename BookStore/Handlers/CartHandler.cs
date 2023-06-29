using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using System.Web.UI.WebControls.WebParts;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class CartHandler
    {
        private CartRepository cartRepository;

        public CartHandler()
        {
            cartRepository = new CartRepository();
        }

        public void insertCart(int UserID, int BookID, int Qty)
        {
            cartRepository.insertCart(UserID, BookID, Qty);
        }

        public void deleteCart(int UserID, int BookID)
        {
            cartRepository.deleteCart(UserID, BookID);
        }

        public void deleteAllCartByBook(int BookID)
        {
            cartRepository.deleteAllCartByBook(BookID);
        }

        public void deleteAllCartByUser(int UserID)
        {
            cartRepository.deleteAllCartByUser(UserID);
        }

        public void updateCart(int UserID, int BookID, int Qty)
        {
            cartRepository.updateCart(UserID, BookID, Qty);
        }

        public Cart findCartByID(int UserID, int BookID)
        {
            return cartRepository.findCartByID(UserID, BookID);
        }

        public List<Cart> getAllCart()
        {
            return cartRepository.getAllCart();
        }

        public List<Cart> getAllCartByUser(int UserID)
        {
            return cartRepository.getAllCartByUser(UserID);
        }

        public List<Cart> getAllCartByBook(int BookID)
        {
            return cartRepository.getAllCartByBook(BookID);
        }
    }
}