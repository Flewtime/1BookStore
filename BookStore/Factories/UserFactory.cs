using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class UserFactory
    {

        public static User createUser(string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, DateTime UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole)
        {
            User user = new User();
            user.UserName = UserName;
            user.UserEmail = UserEmail;
            user.UserPassword = UserPassword;
            user.UserPhoneNumber = UserPhoneNumber;
            user.UserDOB = UserDOB;
            user.UserGender = UserGender;
            user.UserAddress = UserAddress;
            user.UserImage = UserImage;
            user.UserRole = UserRole;
            return user;
        }
    }
}