using System;
using System.Collections.Generic;
using System.Text;

namespace Market.PaymentMethods
{
    public interface IPayment
    {
        public bool ToPay(decimal Moneyamount);

    }
}
