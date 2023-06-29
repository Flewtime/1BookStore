using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;
using BookStore.Handlers;
using BookStore.Repositories;
using System.Text.RegularExpressions;

namespace BookStore.Controllers
{
    public class UserController
    {
        private UserHandler userHandler;

        public UserController()
        {
            userHandler = new UserHandler();
        }

        public string insertUser(string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, string UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole, Boolean checkUserEmail, Boolean checkUserImage)
        {
            string validate = validateUser(UserName, UserEmail, UserPassword, UserPhoneNumber, UserDOB, UserGender, UserAddress, UserImage, checkUserEmail, checkUserImage);
            if(validate.Equals("Register Success!"))
            {
                userHandler.insertUser(UserName, UserEmail, UserPassword, UserPhoneNumber, DateTime.Parse(UserDOB), UserGender, UserAddress, UserImage, UserRole);
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deleteUser(int UserID, string Path)
        {
            userHandler.deleteUser(UserID, Path);
        }

        public string updateUser(int UserID, string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, string UserDOB, string UserGender, string UserAddress, string UserImage, string UserRole, Boolean checkEmail, Boolean checkUserImage)
        {
            string validate = validateUser(UserName, UserEmail, UserPassword, UserPhoneNumber, UserDOB, UserGender, UserAddress, UserImage, checkEmail, checkUserImage);
            if (validate.Equals("Register Success!"))
            {
                userHandler.updateUser(UserID, UserName, UserEmail, UserPassword, UserPhoneNumber, DateTime.Parse(UserDOB), UserGender, UserAddress, UserImage, UserRole);
                return validate;
            }
            else
            {
                return validate;
            }
        }

        public User findUserByID(int UserID)
        {
            return userHandler.findUserByID(UserID);
        }

        public User findUserByLogin(string UserEmail, string UserPassword)
        {
            return userHandler.findUserByLogin(UserEmail, UserPassword);
        }

        public List<User> getAllUser()
        {
            return userHandler.getAllUser();
        }

        public List<User> getAllUserByRole(string UserRole)
        {
            return userHandler.getAllUserByRole(UserRole);
        }

        public string loginUser(string UserEmail, string UserPassword)
        {
            return validateLogin(UserEmail, UserPassword);
        }

        public Boolean isEmailRegistered(string UserEmail)
        {
            return userHandler.isEmailRegistered(UserEmail);
        }

        private string validateUser(string UserName, string UserEmail, string UserPassword, string UserPhoneNumber, string UserDOB, string UserGender, string UserAddress, string UserImage, Boolean checkUserEmail, Boolean checkUserImage)
        {
            UserController userController = new UserController();

            Regex validateName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");
            Regex validateEmail = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
            Regex validatePassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%_^&*-]).{8,}$");
            Regex validatePhoneNumber = new Regex("^(\\+62)8[1-9][0-9]{6,9}$");
            Regex validateImage = new Regex("[^\\s]+(\\.(?i)(jpe?g|png|gif|bmp|jfif|))$");

            if (UserName == "" || UserEmail == "" || UserPassword == "" || UserPhoneNumber == "" || UserDOB == "" || UserGender == "" || UserAddress == "" || (UserImage == "" && checkUserImage))
            {
                return "Please Fill or Choose or Upload All The Fields!";
            }
            else if (!validateName.IsMatch(UserName))
            {
                return "Please Enter A Valid Name!";
            }
            else if ((!validateEmail.IsMatch(UserEmail) || isEmailRegistered(UserEmail)) && checkUserEmail)
            {
                if(!validateEmail.IsMatch(UserEmail))
                {
                    return "Please Enter A Valid Email!";
                }
                else if(isEmailRegistered(UserEmail))
                {
                    return "Email Has Been Registered, Please Use Another Email!";
                }
            }
            else if (!validatePassword.IsMatch(UserPassword))
            {
                return "Please Enter A Valid Password With a Minimum of 8 Characters' Length and At Least One Uppercase Letter, One Lowercase Letter, One Digit, and One Special Character!";
            }
            else if (!validatePhoneNumber.IsMatch(UserPhoneNumber))
            {
                return "Please Enter A Valid Indonesian Phone Number Without 0 In The Beginning!";
            }
            else if (!validateImage.IsMatch(UserImage))
            {
                return "Please Upload A Valid Image!";
            }

            return "Register Success!";
        }

        private string validateLogin(string UserEmail, string UserPassword)
        {
            UserController userController = new UserController();

            Regex validateEmail = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
            Regex validatePassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%_^&*-]).{8,}$");

            if (UserEmail == "" || UserPassword == "")
            {
                return "Please Fill All The Fields!";
            }
            else if (!validateEmail.IsMatch(UserEmail))
            {
                return "Please Enter A Valid Email!";
            }
            else if (!validatePassword.IsMatch(UserPassword))
            {
                return "Please Enter A Valid Password With a Minimum of 8 Characters' Length and At Least One Uppercase Letter, One Lowercase Letter, One Digit, and One Special Character!";
            }
            else
            {
                var user = userHandler.isRegistered(UserEmail, UserPassword);
                if (user == null)
                {
                    return "Email Or Password is Not Registered!";
                }
                else
                {
                    return "Login Successfully!";
                }
            }
        }
    }
}