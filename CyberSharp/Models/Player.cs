using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSharp.Models
{
    class Player : Person
    {
        private int _hackingSkill;
        private int _criminalityLevel;

        // Clamp the hacking skill
        public int HackingSkill
        {
            get => _hackingSkill;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _hackingSkill = value;
                }
            }
        }
        // Clamp the crminality level
        public int CriminalityLevel
        {
            get => _criminalityLevel;
            set
            {
                if (value >= 0 && value <= 5)
                {
                    _criminalityLevel = value;
                }
            }
        }

        public Player(string name) : base(name)
        {
            HackingSkill = 26;
            CriminalityLevel = 0;
            _wallet = new Wallet(0.05);
        }

        // Check if the wanted amount is payable and if so then pay that amount
        // return the success of that action
        public bool Pay(double amount)
        {
            if (_wallet.BitCoin < amount)
            {
                return false;
            }

            _wallet.BitCoin -= amount;

            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder
                .Append("Criminality Level: ")
                .Append(CriminalityLevel)
                .Append(", Hacking Skill: ")
                .Append(HackingSkill.ToString())
                .Append(", BTC amount: ")
                .Append(_wallet.BitCoin);

            return builder.ToString();
        }

        // Check for winning condition
        public bool Win => _wallet.BitCoin >= 5;
    }
}
