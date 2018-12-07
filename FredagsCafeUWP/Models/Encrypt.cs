using System.Collections.Generic;
using System.Linq;

namespace FredagsCafeUWP.Models
{
    class Encrypt
    {
        List<char> alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå0123456789".ToCharArray().ToList();
        

        public string DeCrypt(string input)
        {
            string output = "";

            if (!string.IsNullOrEmpty(input))
            {
                foreach (var inputChar in input)
                {
                    foreach (var character in alpha)
                    {
                        if (inputChar == character)
                        {
                            if (character == alpha.First()) output += alpha.Last();
                            else output += alpha[alpha.IndexOf(character) - 1];
                        }
                    }
                    if (!alpha.Contains(inputChar))
                    {
                        output += inputChar;
                    }
                }
            }
            
            return output;
        }

        public string Encrypting(string input)
        {
            string output = "";

            if (!string.IsNullOrEmpty(input))
            {
                foreach (var inputChar in input)
                {
                    foreach (var character in alpha)
                    {
                        if (inputChar == character)
                        {
                            if (character == alpha.Last()) output += alpha.First();
                            else output += alpha[alpha.IndexOf(character) + 1];
                        }
                    }
                    if (!alpha.Contains(inputChar))
                    {
                        output += inputChar;
                    }
                }
            }
            
            return output;
        }
    }
}
