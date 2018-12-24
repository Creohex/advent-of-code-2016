using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Problem5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculated password: " + CalculatePassword("ugkcyxxp"));
            Console.WriteLine("\nCalculated hard password: " + CalculateHardPassword("ugkcyxxp"));
            Console.ReadKey();
        }

        static string CalculatePassword(string doorID)
        {
            string result = "";
            MD5 md5 = MD5.Create();
            int index = 0;

            while (result.Length < 8)
            {
                string hexHash = byteArrayToString(md5.ComputeHash(Encoding.ASCII.GetBytes(doorID + index.ToString())), 3);

                if (hexHash[0] == '0' && hexHash[1] == '0' && hexHash[2] == '0' && hexHash[3] == '0' && hexHash[4] == '0')
                {
                    result += hexHash[5];
                    Console.WriteLine("Found password digit: " + hexHash[5].ToString() + ", hash index: " + index);
                }
                    
                index++;
            }

            return result;
        }

        static string CalculateHardPassword(string doorID)
        {
            char[] passwordArray = new char[8];
            MD5 md5 = MD5.Create();
            int index = 0;

            while (!PasswordIsComplete(passwordArray)){
                string hexHash = byteArrayToString(md5.ComputeHash(Encoding.ASCII.GetBytes(doorID + index.ToString())), 4);

                if (hexHash[0] == '0' && hexHash[1] == '0' && hexHash[2] == '0' && hexHash[3] == '0' && hexHash[4] == '0')
                {
                    Console.WriteLine("Found applicable hash: " + hexHash + "... (from string: " + (doorID + index.ToString()) + ")");
                    int passDigitID = -1;
                    if (Int32.TryParse(hexHash[5].ToString(), out passDigitID))
                    {
                        Console.WriteLine("..successfully parsed pass digit id: " + passDigitID);

                        if (passDigitID >= 0 && passDigitID < 8 && passwordArray[passDigitID] == '\0')
                        {
                            Console.WriteLine("..this pass digit id is ok to use: " + passDigitID + ".");
                            passwordArray[passDigitID] = hexHash[6];
                            Console.WriteLine("Password is now: " + charArrayToString(passwordArray));
                        }
                    }
                }

                index++;
            }

            return charArrayToString(passwordArray);
        }

        static bool PasswordIsComplete(char[] password)
        {
            foreach (var c in password)
                if (c == '\0')
                    return false;

            return true;
        }

        static string byteArrayToString(byte[] array, int amountOfBytes)
        {
            string result = "";
            for (int i = 0; i < amountOfBytes; i++)
            {
                string value = array[i].ToString("X");
                if (value.Length == 1)
                    result += "0" + value;
                else
                    result += value;
            }

            return result;
        }

        static string charArrayToString(char[] array)
        {
            string result = "";

            foreach (var c in array)
                if (c == '\0')
                    result += "?";
                else
                    result += c.ToString();

            return result;
        }
    }
}
