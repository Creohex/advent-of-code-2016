using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problem7
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateABBA();
            CalculateABA();
            Console.ReadKey();
        }

        static void CalculateABBA()
        {
            int count = 0;
            foreach (IPv7 ip in FactorizeInput(File.ReadAllLines("Input.txt")))
                if (ip.SupportsABBA)
                    count++;

            Console.WriteLine("Amount of IPs that support ABBA: " + count);
        }

        static void CalculateABA()
        {
            int count = 0;
            foreach (var ip in FactorizeInput(File.ReadAllLines("Input.txt")))
                if (ip.SupportsABA)
                    count++;

            Console.WriteLine("Amount of IPs that support ABA: " + count);
        }

        static List<IPv7> FactorizeInput(string[] input)
        {
            List<IPv7> result = new List<IPv7>();

            foreach (var str in input)
            {
                List<string> addr = new List<string>();
                List<string> seq = new List<string>();

                string[] words = str.Split(new char[] { '[', ']' });
                for (int i = 0; i < words.Length; i++)
                    if (i % 2 == 0)
                        addr.Add(words[i]);
                    else
                        seq.Add(words[i]);

                result.Add(new IPv7(addr, seq));
            }

            return result;
        }

        class IPv7
        {
            public List<string> SuperSequences;
            public List<string> HyperSequences;
            public bool SupportsABBA
            {
                get { return (StringsSupportABBA(SuperSequences) && !StringsSupportABBA(HyperSequences)); }
            }
            public bool SupportsABA
            {
                get
                {
                    List<string> abas = GetPalindromesOf3(SuperSequences);
                    List<string> babs = GetPalindromesOf3(HyperSequences);
                    foreach (var aba in abas)
                        foreach (var bab in babs)
                            if (aba[0] == bab[1] && aba[1] == bab[0])
                                return true;

                    return false;
                }
            }

            public IPv7() { }
            public IPv7(List<string> addr, List<string> seq)
            {
                this.SuperSequences = addr;
                this.HyperSequences = seq;
            }

            public static bool StringsSupportABBA(List<string> list)
            {
                foreach (var s in list)
                    for (int i = 0; i < (s.Length - 3); i++)
                        if (s[i] != s[i + 1] && s[i] == s[i + 3] && s[i + 1] == s[i + 2])
                            return true;

                return false;
            }
            public static List<string> GetPalindromesOf3(List<string> list)
            {
                List<string> result = new List<string>();
                foreach (string s in list)
                    for (int i = 0; i < (s.Length - 2); i++)
                        if (s[i] == s[i + 2] && s[i] != s[i + 1])
                            result.Add(s.Substring(i, 3));

                return result;
            }
        }
    }
}
