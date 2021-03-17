using System;
using System.Collections.Generic;
using System.Text;

namespace Market.Verification
{
    public interface IVerification
    {
        public string SendMessage(string phoneNumber);
    }
}
