using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace Problem11
{
    class Program
    {
        static void Main(string[] args)
        {
            AssembleGenerators();
            Console.ReadKey();
        }

        static void AssembleGenerators()
        {
            Area area = new Area(File.ReadAllLines("TestInput.txt"));
            area.Render();
        }

        class Area
        {
            public int Floors;
            public Dictionary<string, int> Generators;
            public Dictionary<string, int> Chips;
            public Lift Elevator;

            public Area(string[] input)
            {
                Floors = input.Length;
                Elevator = new Lift();
                Generators = new Dictionary<string, int>();
                Chips = new Dictionary<string, int>();
                Regex rx = new Regex(@"(\w+\sgenerator)|(\w+-compatible)");
                for (int i = 0; i < input.Length; i++)
                    foreach (Match match in rx.Matches(input[i]))
                        if (match.Value.Contains("-"))
                            Chips.Add(match.Value.Split(new char[] { '-' })[0], i + 1);
                        else
                            Generators.Add(match.Value.Split((char[])null)[0], i + 1);
            }

            public void Render()
            {
                for (int i = Floors; i > 0; i--)
                {
                    string str = "F" + i;
                    str += (Elevator.CurrentFloor == i) ? " E" : " .";
                    foreach (var generator in Generators)
                    {
                        str += (generator.Value == i) ? ("\t" + generator.Key[0] + generator.Key[1]) : "\t.";
                        var chip = Chips.First(x => x.Key == generator.Key);
                        str += (chip.Value == i) ? ("\t" + chip.Key[0] + chip.Key[1]) : "\t.";
                    }
                    Console.WriteLine(str);
                }
            }

            public class Lift
            {
                public int CurrentFloor;
                public Dictionary<CargoType, string> Cargo;

                public int ItemsCount
                {
                    get { return Cargo.Keys.Count; }
                }
                public bool CanRelocate
                {
                    get { return ItemsCount > 0; }
                }

                public Lift()
                {
                    CurrentFloor = 1;
                    Cargo = new Dictionary<CargoType, string>() { };
                }

                public enum CargoType { Generator=0, Chip }
            }
        }
    }
}
