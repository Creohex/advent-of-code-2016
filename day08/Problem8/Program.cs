using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Problem8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Amount of lit pixels: " + CalculateLitPixels());
            Console.ReadKey();
        }

        static int CalculateLitPixels()
        {
            Screen screen = new Screen(50, 6);
            List<Command> commands = new List<Command>();
            foreach (var str in File.ReadAllLines("Input.txt"))
                commands.Add(new Command(str));

            foreach (var cmd in commands)
                screen.ApplyCommand(cmd);

            return screen.LitPixels;
        }

        class Screen
        {
            int Width;
            int Height;
            bool[][] Pixels;
            public int LitPixels
            {
                get
                {
                    int count = 0;
                    for (int i = 0; i < Height; i++)
                        for (int j = 0; j < Width; j++)
                            if (Pixels[i][j])
                                count++;
                    return count;
                }
            }

            public Screen(int w, int h)
            {
                Width = w;
                Height = h;
                Pixels = new bool[h][];
                for (int i = 0; i < Pixels.Length; i++)
                    Pixels[i] = new bool[w];
            }

            public void ApplyCommand(Command cmd)
            {
                Console.WriteLine("\nApplying command " + cmd.ToString() + ":");
                if (cmd.Type == Command.CommandType.Rect)
                {
                    for (int i = 0; i < cmd.X; i++)
                        for (int j = 0; j < cmd.Y; j++)
                            Pixels[j][i] = true;
                }
                else if (cmd.Type == Command.CommandType.RotateColumn)
                {
                    bool[] tempArray = new bool[Height];
                    for (int i = 0; i < Height; i++)
                        tempArray[i] = Pixels[i][cmd.X];
                    for (int i = 0; i < Height; i++)
                        Pixels[(i + cmd.Y) % Height][cmd.X] = tempArray[i];
                }
                else if (cmd.Type == Command.CommandType.RotateRow)
                {
                    bool[] tempArray = new bool[Width];
                    for (int i = 0; i < Width; i++)
                        tempArray[i] = Pixels[cmd.Y][i];
                    for (int i = 0; i < Width; i++)
                        Pixels[cmd.Y][(i + cmd.X) % Width] = tempArray[i];
                }
                DrawScreen();
            }
            public void DrawScreen()
            {
                for (int i = 0; i < Height; i++)
                {
                    string line = "";
                    for (int j = 0; j < Width; j++)
                        line += Pixels[i][j] == true ? "#" : ".";
                    Console.WriteLine(line);
                }

            }
        }

        class Command
        {
            public int X;
            public int Y;
            public CommandType Type;

            public Command(string raw)
            {
                Regex rx = new Regex(@"\sby\s|=|\s");
                string[] elements = rx.Split(raw);

                if (elements[0] == "rect")
                {
                    Type = CommandType.Rect;
                    string[] values = elements[1].Split(new char[] { 'x' });
                    X = Int32.Parse(values[0]);
                    Y = Int32.Parse(values[1]);
                }
                else if (elements[0] == "rotate")
                {
                    Type = (elements[1] == "row") ? CommandType.RotateRow : CommandType.RotateColumn;
                    X = (elements[2] == "x") ? Int32.Parse(elements[3]) : Int32.Parse(elements[4]);
                    Y = (elements[2] == "y") ? Int32.Parse(elements[3]) : Int32.Parse(elements[4]);
                }
            }

            public override string ToString()
            {
                return Type.ToString() + " " + X + " " + Y;
            }
            public enum CommandType { Rect=0, RotateRow, RotateColumn }
        }
    }
}
