using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Factories;
using BookStore.Model;
using BookStore.Repositories;

namespace BookStore.Handlers
{
    public class UserHandler
    {
        private UserRepository userRepository;
        private CartRepository cartRepository;
        private OrderRepository orderRepository;
        private ReviewRepository reviewRepository;
        private CartHandler cartHandler;
        private OrderHandler orderHandler;
        private ReviewHandler reviewHandler;

        public UserHandler()
        {
            userRepository = new UserRepository();
            cartRepository = new CartRepository();
            orderRepository = new OrderRepository();
            reviewRepository = new ReviewRepository();
            cartHandler = new CartHandler();
            orderHandler = new OrderHandler();
            reviewHandler = new ReviewHandler();
        }

        public void insertUser(string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, DateTime UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole)
        {
            userRepository.insertUser(UserName, UserEmail, UserPassword, UserPhoneNumber, UserDOB, UserGender, UserAddress, UserImage, UserRole);
        }

        public void deleteUser(int UserID, string Path)
        {
            cartRepository.deleteAllCartByUser(UserID);
            reviewRepository.deleteAllReviewByUser(UserID);
            List<Order> orderList = orderRepository.getAllOrderByUser(UserID);
            if (orderList.Any())
            {
                foreach (Order o in orderList)
                {
                    orderHandler.deleteOrder(o.OrderID);
                }
            }

            userRepository.deleteUser(UserID, Path);
        }

        public void updateUser(int UserID, string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, DateTime UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole)
        {
            userRepository.updateUser(UserID, UserName, UserEmail, UserPassword, UserPhoneNumber, UserDOB, UserGender, UserAddress, UserImage, UserRole);
        }

        public User findUserByID(int UserID)
        {
            return userRepository.findUserByID(UserID);
        }

        public User findUserByLogin(string UserEmail, string UserPassword)
        {
            return userRepository.findUserByLogin(UserEmail, UserPassword);
        }

        public List<User> getAllUser()
        {
            return userRepository.getAllUser();
        }

        public List<User> getAllUserByRole(string UserRole)
        {
            return userRepository.getAllUserByRole(UserRole);
        }

        public User isRegistered(string UserEmail, string UserPassword)
        {
            return userRepository.isRegistered(UserEmail, UserPassword);
        }

        public Boolean isEmailRegistered(string UserEmail)
        {
            return userRepository.isEmailRegistered(UserEmail);
        }
    }
}