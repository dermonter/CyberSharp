using CyberSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSharp.Models
{
    class Target : Person
    {
        private int _defendIndex = 0;
        private readonly int[] _defendValues;
        private static readonly int[] DEFEND_COMMON = new int[] { 0, 10 };
        private static readonly int[] DEFEND_RARE = new int[] { 15 };
        private static readonly int[] DEFEND_EPIC = new int[] { 10, 15, 20 };

        // iterate over all possible defend values and return the correct number
        public int Defend() => _defendValues[_defendIndex++ % _defendValues.Length];

        public Target(PersonType personType) : base(Generator.GetRandomName())
        {
            // Set the correct parameters based on the PersonType
            double coins;
            switch (personType)
            {
                case PersonType.COMMON:
                    _defendValues = DEFEND_COMMON;
                    int walletChance = Game.RNG.Next(4);
                    if (walletChance != 0)
                    {
                        coins = Game.RNG.NextDouble() * 0.5;
                        _wallet = new Wallet(coins);
                    }
                    break;
                case PersonType.RARE:
                    _defendValues = DEFEND_RARE;
                    coins = Game.RNG.NextDouble() + 0.5;
                    _wallet = new Wallet(coins);
                    break;
                case PersonType.EPIC:
                    _defendValues = DEFEND_EPIC;
                    coins = Game.RNG.NextDouble() * 1.5 + 1;
                    _wallet = new Wallet(coins);
                    break;
                default:
                    break;
            }
        }
    }

    public enum PersonType
    {
        COMMON,
        RARE,
        EPIC,
        PLAYER
    }
}
