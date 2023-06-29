using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class CartFactory
    {
        public static Cart createCart(int UserID, int BookID, int Qty)
        {
            Cart cart = new Cart();
            cart.UserID = UserID;
            cart.BookID = BookID;
            cart.Qty = Qty;
            return cart;
        }
    }
}