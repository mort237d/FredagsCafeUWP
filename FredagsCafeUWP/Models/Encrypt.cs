using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Documents;

namespace FredagsCafeUWP.Models
{
    class Encrypt
    {
        List<char> alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå0123456789".ToCharArray().ToList();
        List<char> beta = new List<char>();
        public void Encrypting()
        {
            foreach (char cha in alpha)
            {

            switch (cha)
            {
                case 'a': beta.Add('b');
                    break;
                case 'b':
                    beta.Add('c');
                    break;
                case 'c':
                    beta.Add('d');
                    break;
                case 'd':
                    beta.Add('e');
                    break;
                case 'e':
                    beta.Add('f');
                    break;
                case 'f':
                    beta.Add('g');
                    break;
                case 'g':
                    beta.Add('h');
                    break;
                case 'h':
                    beta.Add('i');
                    break;
                case 'i':
                    beta.Add('j');
                    break;
                case 'j':
                    beta.Add('k');
                    break;
                case 'k':
                    beta.Add('l');
                    break;
                case 'l':
                    beta.Add('m');
                    break;
                case 'm':
                    beta.Add('n');
                    break;
                case 'n':
                    beta.Add('o');
                    break;
                case 'o':
                    beta.Add('p');
                    break;
                case 'p':
                    beta.Add('q');
                    break;
                case 'q':
                    beta.Add('r');
                    break;
                case 'r':
                    beta.Add('s');
                    break;
                case 's':
                    beta.Add('t');
                    break;
                case 't':
                    beta.Add('u');
                    break;
                case 'u':
                    beta.Add('v');
                    break;
                case 'v':
                    beta.Add('w');
                    break;
                case 'w':
                    beta.Add('x');
                    break;
                case 'x':
                    beta.Add('y');
                    break;
                case 'y':
                    beta.Add('z');
                    break;
                case 'z':
                    beta.Add('æ');
                    break;
                case 'æ':
                    beta.Add('ø');
                    break;
                case 'ø':
                    beta.Add('å');
                    break;
                case 'å':
                    beta.Add('a');
                    break;

                    case 'A':
                        beta.Add('B');
                        break;
                    case 'B':
                        beta.Add('C');
                        break;
                    case 'C':
                        beta.Add('D');
                        break;
                    case 'D':
                        beta.Add('E');
                        break;
                    case 'E':
                        beta.Add('F');
                        break;
                    case 'F':
                        beta.Add('G');
                        break;
                    case 'G':
                        beta.Add('H');
                        break;
                    case 'H':
                        beta.Add('I');
                        break;
                    case 'I':
                        beta.Add('J');
                        break;
                    case 'J':
                        beta.Add('K');
                        break;
                    case 'K':
                        beta.Add('L');
                        break;
                    case 'L':
                        beta.Add('M');
                        break;
                    case 'M':
                        beta.Add('N');
                        break;
                    case 'N':
                        beta.Add('O');
                        break;
                    case 'O':
                        beta.Add('P');
                        break;
                    case 'P':
                        beta.Add('Q');
                        break;
                    case 'Q':
                        beta.Add('R');
                        break;
                    case 'R':
                        beta.Add('S');
                        break;
                    case 'S':
                        beta.Add('T');
                        break;
                    case 'T':
                        beta.Add('U');
                        break;
                    case 'U':
                        beta.Add('V');
                        break;
                    case 'V':
                        beta.Add('W');
                        break;
                    case 'W':
                        beta.Add('X');
                        break;
                    case 'X':
                        beta.Add('Y');
                        break;
                    case 'Y':
                        beta.Add('Z');
                        break;
                    case 'Z':
                        beta.Add('Æ');
                        break;
                    case 'Æ':
                        beta.Add('Ø');
                        break;
                    case 'Ø':
                        beta.Add('Å');
                        break;
                    case 'Å':
                        beta.Add('A');
                        break;
                case '0':
                    beta.Add('1');
                    break;
                case '1':
                    beta.Add('2');
                    break;
                case '2':
                    beta.Add('3');
                    break;
                case '3':
                    beta.Add('4');
                    break;
                case '4':
                    beta.Add('5');
                    break;
                case '5':
                    beta.Add('6');
                    break;
                case '6':
                    beta.Add('7');
                    break;
                case '7':
                    beta.Add('8');
                    break;
                case '8':
                    beta.Add('9');
                    break;
                case '9':
                    beta.Add('0');
                    break;
                }
            }
        }
    }
}
