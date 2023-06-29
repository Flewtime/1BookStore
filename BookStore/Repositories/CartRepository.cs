using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class CartRepository : IDisposable
    {
        private Database1Entities1 db;
        private Cart cart;
        private BookRepository bookRepository;

        public CartRepository()
        {
            db = Database.getDb();
            cart = new Cart();
            bookRepository = new BookRepository();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertCart(int UserID, int BookID, int Qty)
        {
            bookRepository.reduceStock(BookID, Qty);
            cart = CartFactory.createCart(UserID, BookID, Qty);
            db.Carts.Add(cart);
            db.SaveChanges();
        }

        public void deleteCart(int UserID, int BookID)
        {
            cart = findCartByID(UserID, BookID);
            int Qty = cart.Qty;
            bookRepository.addStock(BookID, Qty);
            if (cart != null)
            {
                db.Carts.Remove(cart);
                db.SaveChanges();
            }
        }

        public void deleteAllCartByBook(int BookID)
        {
            List<Cart> list = getAllCartByBook(BookID);
            if (list.Any())
            {
                foreach (Cart c in list)
                {
                    db.Carts.Remove(c);
                    db.SaveChanges();
                }
            }
        }
        
        public void deleteAllCartByUser(int UserID)
        {
            List<Cart> list = getAllCartByUser(UserID);
            if (list.Any())
            {
                foreach (Cart c in list)
                {
                    db.Carts.Remove(c);
                    db.SaveChanges();
                }
            }
        }   

        public void updateCart(int UserID, int BookID, int Qty)
        {
            cart = findCartByID(UserID, BookID);
            if (cart != null)
            {
                bookRepository.reduceStock(BookID, Qty);
                cart.Qty = cart.Qty + Qty;
                db.SaveChanges();
            }
        }

        public Cart findCartByID(int UserID, int BookID)
        {
            cart = (from c in db.Carts where c.UserID == UserID && c.BookID == BookID select c).FirstOrDefault();
            return cart;
        }

        public List<Cart> getAllCart()
        {
            List<Cart> list = new List<Cart>();
            list = (from c in db.Carts select c).ToList();
            return list;
        }

        public List<Cart> getAllCartByUser(int UserID)
        {
            List <Cart> list = new List<Cart>();
            list = (from c in db.Carts where c.UserID == UserID select c).ToList();
            return list;
        }

        public List<Cart> getAllCartByBook(int BookID)
        {
            List<Cart> list = new List<Cart>();
            list = (from c in db.Carts where c.BookID == BookID select c).ToList();
            return list;
        }
    }
}