using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaAahwanam.Utility
{
    public class RandomPassword
    {
        public string GenerateString()
        {
            int size = 8;
            Random rand = new Random();
            string Alphabet = "abcdefghijklmnopqrstuvwyxz$@&*0123456789$@&*";
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }
    }
}
