using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problem6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Error-corrected message (most frequent): " + ErrorCorrect(File.ReadAllLines("Input.txt"), true));
            Console.WriteLine("Error-corrected message (less frequent): " + ErrorCorrect(File.ReadAllLines("Input.txt"), false));
            Console.ReadKey();
        }

        static string ErrorCorrect(string[] input, bool getMostFrequent)
        {
            Dictionary<int, Dictionary<char, int>> dict = new Dictionary<int, Dictionary<char, int>>();

            foreach (string word in input)
                for (int j = 0; j < word.Length; j++)
                    if (dict.ContainsKey(j))
                        if (dict[j].ContainsKey(word[j]))
                            dict[j][word[j]]++;
                        else
                            dict[j].Add(word[j], 1);
                    else
                        dict.Add(j, new Dictionary<char, int>() { { word[j], 1 } });

            string result = "";
            foreach (Dictionary<char, int> subDict in dict.Values)
                foreach(var p in getMostFrequent ? subDict.OrderByDescending(pair => pair.Value).Take(1) : subDict.OrderBy(pair => pair.Value).Take(1))
                    result += p.Key;

            return result;
        }
    }
}
