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
    public class PaymentMethodController
    {
        private PaymentMethodHandler paymentMethodHandler;

        public PaymentMethodController()
        {
            paymentMethodHandler = new PaymentMethodHandler();
        }

        public string insertPaymentMethod(string PaymentMethodName, int PaymentMethodFee, Boolean checkPaymentMethodName)
        {
            string validate = validatePaymentMethod(PaymentMethodName, PaymentMethodFee.ToString(), checkPaymentMethodName);
            if(validate.Equals("Insert Success!"))
            {
                paymentMethodHandler.insertPaymentMethod(PaymentMethodName, PaymentMethodFee);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public void deletePaymentMethod(int PaymentMethodID)
        {
            paymentMethodHandler.deletePaymentMethod(PaymentMethodID);
        }

        public string updatePaymentMethod(int PaymentMethodID, string PaymentMethodName, int PaymentMethodFee, Boolean checkPaymentMethodName)
        {
            string validate = validatePaymentMethod(PaymentMethodName, PaymentMethodFee.ToString(), checkPaymentMethodName);
            if(validate.Equals("Insert Success!"))
            {
                paymentMethodHandler.updatePaymentMethod(PaymentMethodID, PaymentMethodName, PaymentMethodFee);

                return validate;
            }
            else
            {
                return validate;
            }
        }

        public PaymentMethod findPaymentMethodByID(int PaymentMethodID)
        {
            return paymentMethodHandler.findPaymentMethodByID(PaymentMethodID);
        }

        public List<PaymentMethod> getAllPaymentMethod()
        {
            return paymentMethodHandler.getAllPaymentMethod();
        }

        private string validatePaymentMethod(string PaymentMethodName, string PaymentMethodFee, Boolean checkPaymentMethodName)
        {
            Regex validatePaymentMethodName = new Regex("^[a-zA-Z]+(([',_. -][a-zA-Z ])?[a-zA-Z]*)*$");
            Regex validatePaymentMethodFee = new Regex("^\\d+$");

            Boolean isPaymentMethodNameExist = false;
            List<PaymentMethod> paymentMethodList = getAllPaymentMethod();
            foreach (var paymentMethod in paymentMethodList)
            {
                if (paymentMethod.PaymentMethodName.Equals(PaymentMethodName))
                {
                    isPaymentMethodNameExist = true;
                    break;
                }
            }

            if(PaymentMethodName == "" || PaymentMethodFee == "")
            {
                return "Please Fill All The Fields!";
            }
            else if(PaymentMethodName.Length < 2)
            {
                return "Payment Method Name Must be More Than 2 Characters!";
            }
            else if(!validatePaymentMethodName.IsMatch(PaymentMethodName))
            {
                return "Please Enter A Valid Payment Method Name!";
            }
            else if (checkPaymentMethodName && isPaymentMethodNameExist)
            {
                return "Payment Method Name Already Exist, Please Enter Another Payment Method Name!";
            }
            else if(!validatePaymentMethodFee.IsMatch(PaymentMethodFee))
            {
                return "Please Enter A Valid Payment Method Fee That Only Contains Numbers!";
            }

            return "Insert Success!";
        }
    }
}