using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Problem3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Answer: " + CalculatePossibleTriangles(FactorizeInput(File.ReadAllLines("Input.txt"))));
            Console.WriteLine("Answer (vertical): " + CalculatePossibleTriangles(FactorizeInputVertically(File.ReadAllLines("Input.txt"))));
            Console.ReadKey();
        }

        static int CalculatePossibleTriangles(List<Triangle> input)
        {
            int possibleTriangles = 0;

            foreach(Triangle t in input)
                if (TriangleIsPossible(t)) possibleTriangles++;

            return possibleTriangles;
        }

        static bool TriangleIsPossible(Triangle t)
        {
            return (t.L1 + t.L2 <= t.L3 || t.L1 + t.L3 <= t.L2 || t.L2 + t.L3 <= t.L1) ? false : true;
        }

        static List<Triangle> FactorizeInput(string[] input)
        {
            List<Triangle> result = new List<Triangle>();

            for (int i = 0; i < input.Length; i++)
            {
                string[] words = Regex.Split(input[i], @"\s+");
                result.Add(new Triangle(Int32.Parse(words[1]), Int32.Parse(words[2]), Int32.Parse(words[3])));
            }
            return result;
        }

        static List<Triangle> FactorizeInputVertically(string[] input)
        {
            List<Triangle> result = new List<Triangle>();

            for (int i = 0; i < input.Length; i += 3)
            {
                List<string[]> group = new List<string[]>();

                for (int j = 0; j < 3; j++)
                    group.Add(Regex.Split(input[i + j], @"\s+"));

                string[] x = group[0];
                string[] y = group[1];
                string[] z = group[2];
                result.Add(new Triangle(Int32.Parse(x[1]), Int32.Parse(y[1]), Int32.Parse(z[1])));
                result.Add(new Triangle(Int32.Parse(x[2]), Int32.Parse(y[2]), Int32.Parse(z[2])));
                result.Add(new Triangle(Int32.Parse(x[3]), Int32.Parse(y[3]), Int32.Parse(z[3])));
            }

            return result;
        }
     
        class Triangle
        {
            public int L1, L2, L3;
            public Triangle() { }
            public Triangle(int l1, int l2, int l3)
            {
                this.L1 = l1;
                this.L2 = l2;
                this.L3 = l3;
            }
        }   
    }
}
