using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Problem9
{
    class Program
    {
        static Regex markerRegex = new Regex(@"\(\d+x\d+\)");
        static Regex directiveRegex = new Regex(@"[\(+|x+|\)+]+");

        static void Main(string[] args)
        {
            Console.WriteLine("Decompressed string length: " + Decompress(File.ReadAllLines("Input.txt")[0]).Length);
            Console.WriteLine("Super-decompressed string length: " + SuperDecompress(File.ReadAllLines("Input.txt")[0]));
            Console.ReadKey();
        }

        static string Decompress(string input)
        {
            string result = "";
            int count = 0;

            while (count < input.Length)
                if (input[count] == '(')
                {
                    Match marker = markerRegex.Match(input.Substring(count));
                    string[] directives = directiveRegex.Split(marker.ToString());
                    int amountOfCharacters = Int32.Parse(directives[1]);
                    for (int i = 0; i < Int32.Parse(directives[2]); i++)
                        result += input.Substring((count + marker.ToString().Length), amountOfCharacters);

                    count += marker.ToString().Length + amountOfCharacters;
                }
                else
                {
                    result += input[count];
                    count++;
                }

            return result;
        }

        static long SuperDecompress(string input)
        {
            long amount = 0;
            int count = 0;

            while (count < input.Length)
                if (input[count] == '(')
                {
                    Match marker = markerRegex.Match(input.Substring(count));
                    string[] directives = directiveRegex.Split(marker.ToString());
                    string block = input.Substring(count + marker.Length, Int32.Parse(directives[1]));
                    amount += SuperDecompress(block) * Int32.Parse(directives[2]);
                    count += marker.Length + block.Length;
                }
                else
                {
                    amount++;
                    count++;
                }

            return amount; 
        }
    }
}
