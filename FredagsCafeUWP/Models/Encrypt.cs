using System.Collections.Generic;
using System.Linq;

namespace FredagsCafeUWP.Models
{
    class Encrypt
    {
        List<char> alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå0123456789".ToCharArray().ToList();
        

        private string DeCrypt(string input)
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

        private string Encrypting(string input)
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

        public void EncryptUsers()
        {
            foreach (var user in Administration.Instance.Users)
            {
                user.Name = Encrypting(user.Name);
                user.Admin = Encrypting(user.Admin);
                user.Education = Encrypting(user.Education);
                user.Email = Encrypting(user.Email);
                user.Grade = Encrypting(user.Grade);
                user.Password = Encrypting(user.Password);
                user.TelephoneNumber = Encrypting(user.TelephoneNumber);
                user.UserName = Encrypting(user.UserName);
                user.ImageSource = Encrypting(user.ImageSource);
            }
        }

        public void DecryptUsers()
        {
            foreach (var user in Administration.Instance.Users)
            {
                user.Name = DeCrypt(user.Name);
                user.Admin = DeCrypt(user.Admin);
                user.Education = DeCrypt(user.Education);
                user.Email = DeCrypt(user.Email);
                user.Grade = DeCrypt(user.Grade);
                user.Password = DeCrypt(user.Password);
                user.TelephoneNumber = DeCrypt(user.TelephoneNumber);
                user.UserName = DeCrypt(user.UserName);
                user.ImageSource = DeCrypt(user.ImageSource);
            }
        }
    }
}
