using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Factories;

namespace BookStore.Repositories
{
    public class UserRepository : IDisposable
    {
        private Database1Entities1 db;
        private User user;

        public UserRepository()
        {
            db = Database.getDb();
            user = new User();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void insertUser(string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, DateTime UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole)
        {
            user = UserFactory.createUser(UserName, UserEmail, UserPassword, UserPhoneNumber, UserDOB, UserGender, UserAddress, UserImage, UserRole);
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void deleteUser(int UserID, string Path)
        {
            user = findUserByID(UserID);
            if (user != null)
            {
                System.IO.File.Delete(Path + "/Assets/Users/" + user.UserImage);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public void updateUser(int UserID, string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, DateTime UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole)
        {
            user = findUserByID(UserID);
            if (user != null)
            {
                user.UserName = UserName;
                user.UserEmail = UserEmail;
                user.UserPassword = UserPassword;
                user.UserPhoneNumber = UserPhoneNumber;
                user.UserDOB = UserDOB;
                user.UserGender = UserGender;
                user.UserAddress = UserAddress;
                user.UserImage = UserImage;
                user.UserRole = UserRole;
                db.SaveChanges();
            }
        }

        public User findUserByID(int UserID)
        {
            user = db.Users.Find(UserID);
            return user;
        }

        public User findUserByLogin(string UserEmail, string UserPassword)
        {
            user = (from u in db.Users where u.UserEmail.Equals(UserEmail) && u.UserPassword.Equals(UserPassword) select u).FirstOrDefault();
            return user;
        }

        public List<User> getAllUser()
        {
            List<User> list = new List<User>();
            list = (from u in db.Users select u).ToList();
            return list;
        }

        public List<User> getAllUserByRole(string UserRole)
        {
            List<User> list = new List<User>();
            list = (from u in db.Users where u.UserRole.Equals(UserRole) select u).ToList();
            return list;
        }

        public User isRegistered(string UserEmail, string UserPassword)
        {
            user = (from userRegistered in db.Users where userRegistered.UserEmail.Equals(UserEmail) && userRegistered.UserPassword.Equals(UserPassword) select userRegistered).FirstOrDefault();
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public Boolean isEmailRegistered(string UserEmail)
        {
            var user = (from userEmail in db.Users where userEmail.UserEmail.Equals(UserEmail) select userEmail).FirstOrDefault();
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}