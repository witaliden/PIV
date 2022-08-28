using System;
using System.Linq;

namespace Project_2.dao
{
    public class Randomizer
    {
        private static Random _random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        public static string RandomGender()
        {
            const string chars = "MK";
            return new string(Enumerable.Repeat(chars, 1)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
