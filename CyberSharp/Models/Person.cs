using CyberSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSharp.Models
{
    class Person
    {
        protected Wallet _wallet;

        public string Name { get; private set; }
        public string IpAddress { get; private set; }
        public string WalletAddress => _wallet is null ? "No wallet" : "BTC wallet address: " + _wallet.Address;
        public string WalletPassword => _wallet is null ? "No wallet" : _wallet.Password;
        public double WalletBTC => _wallet is null ? 0 : _wallet.BitCoin;

        public Person(string name)
        {
            Name = name;
            IpAddress = Generator.GetRandomIp();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder
                .Append(Name)
                .Append(" at IP address ")
                .Append(IpAddress);
            return builder.ToString();
        }
    }
}
