using CyberSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSharp.Models
{
    class Wallet
    {
        public string Address { get; private set; }
        public string Password { get; private set; }
        public double BitCoin { get; set; }

        public Wallet(double amount)
        {
            BitCoin = amount;
            Address = Generator.GetRandomBtcAddress();
            Password = Generator.GetRandomPassword();
        }
    }
}
