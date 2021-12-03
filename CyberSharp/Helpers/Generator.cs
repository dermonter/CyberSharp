using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace CyberSharp.Helpers
{
    public static class Generator
    {
        private static readonly string filename = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}" +
            $"Data{Path.DirectorySeparatorChar}gameData.csv";
        private static readonly List<string> lines = File.ReadLines(filename).ToList();
        private static readonly char delimiter = ',';
        private static readonly Random rand = new Random();

        public static string GetRandomName() => GetRandomElement(0);
        public static string GetRandomIp() => GetRandomElement(1);
        public static string GetRandomBtcAddress() => GetRandomElement(2);
        public static string GetRandomPassword() => GetRandomElement(3);
        private static string GetRandomElement(int position)
        {
            return lines[rand.Next(1, lines.Count)].Split(delimiter)[position];
        }
    }
}
