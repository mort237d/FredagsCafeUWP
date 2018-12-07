using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;

namespace FredagsCafeUWP.Models
{
    class Encrypt
    {
        List<char> alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå0123456789".ToCharArray().ToList();
        

        public string DeCrypt(string input)
        {
            string output = "";
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
            }
            return output;
        }

        public string Encrypting(string input)
        {
            string output = "";
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
            }
            return output;
        }
        public void Test()
        {
            string temp = Encrypting("Daniel");
            Debug.WriteLine(temp);
            string temp2 = DeCrypt(temp);
            Debug.WriteLine(temp2);
        }
    }
}
