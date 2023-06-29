using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Model;

namespace BookStore.Factories
{
    public class PaymentMethodFactory
    {
        public static PaymentMethod createPaymentMethod(string PaymentMethodName, int PaymentMethodFee)
        {
            PaymentMethod paymentMethod = new PaymentMethod();
            paymentMethod.PaymentMethodName = PaymentMethodName;
            paymentMethod.PaymentMethodFee = PaymentMethodFee;
            return paymentMethod;
        }
    }
}