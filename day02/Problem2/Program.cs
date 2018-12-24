using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Problem2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Answer: " + CalculateAnswer());
            Console.WriteLine("Answer: " + CalculateAnswerAdvanced());
            Console.ReadKey();
        }

        #region lvl1

        static string CalculateAnswer()
        {
            string result = "";
            string[] inputArray = File.ReadAllLines("Input.txt");
            Dictionary<Point, int> dictionary = GetDictionary();
            Point currentPosition = new Point(2, 2);

            for (int i = 0; i < inputArray.Length; i++)
            {
                currentPosition = CalculateSingleDigit(dictionary, currentPosition, inputArray[i]);
                result += dictionary[currentPosition].ToString();
            }

            return result;
        }

        static Point CalculateSingleDigit(Dictionary<Point, int> dictionary, Point startingPosition, string sequence)
        {
            Point currentPosition = new Point(startingPosition.X, startingPosition.Y);
            for (int i = 0; i < sequence.Length; i++)
                currentPosition = MoveSelection(currentPosition, ResolveDirection(sequence[i]));
            return currentPosition;
        }

        static Point MoveSelection(Point currentPosition, Direction movingDirection)
        {
            Point newPosition = new Point(currentPosition.X, currentPosition.Y);
            switch ((int)movingDirection)
            {
                case (int)Direction.Up:
                    newPosition.Y++;
                    if (newPosition.Y > 3) newPosition.Y = 3;
                    break;
                case (int)Direction.Right:
                    newPosition.X++;
                    if (newPosition.X > 3) newPosition.X = 3;
                    break;
                case (int)Direction.Down:
                    newPosition.Y--;
                    if (newPosition.Y < 1) newPosition.Y = 1;
                    break;
                case (int)Direction.Left:
                    newPosition.X--;
                    if (newPosition.X < 1) newPosition.X = 1;
                    break;
            }
            return newPosition;
        }

        static Dictionary<Point, int> GetDictionary()
        {
            Dictionary<Point, int> dict = new Dictionary<Point, int>(9);
            dict.Add(new Point(1, 1), 7);
            dict.Add(new Point(1, 2), 4);
            dict.Add(new Point(1, 3), 1);
            dict.Add(new Point(2, 1), 8);
            dict.Add(new Point(2, 2), 5);
            dict.Add(new Point(2, 3), 2);
            dict.Add(new Point(3, 1), 9);
            dict.Add(new Point(3, 2), 6);
            dict.Add(new Point(3, 3), 3);

            return dict;
        }

        #endregion

        #region lvl2

        static string CalculateAnswerAdvanced()
        {
            string result = "";
            string[] inputArray = File.ReadAllLines("Input.txt");
            Dictionary<Point, int> dictionary = GetAdvancedDictionary();
            Point currentPosition = new Point(1, 3);

            for (int i = 0; i < inputArray.Length; i++)
            {
                currentPosition = CalculateSingleDigitAdvanced(dictionary, currentPosition, inputArray[i]);
                result += dictionary[currentPosition].ToString("X");
            }

            return result;
        }

        static Point CalculateSingleDigitAdvanced(Dictionary<Point, int> dictionary, Point startingPosition, string sequence)
        {
            Point currentPosition = new Point(startingPosition.X, startingPosition.Y);
            for (int i = 0; i < sequence.Length; i++)
                currentPosition = MoveSelectionAdvanced(currentPosition, ResolveDirection(sequence[i]));
            return currentPosition;
        }

        static Point MoveSelectionAdvanced(Point currentPosition, Direction movingDirection)
        {
            Point newPosition = new Point(currentPosition.X, currentPosition.Y);
            switch ((int)movingDirection)
            {
                case (int)Direction.Up:
                    newPosition.Y++;
                    switch (newPosition.X)
                    {
                        case 1:
                        case 5:
                            newPosition.Y = 3; break;
                        case 2:
                        case 4:
                            if (newPosition.Y > 4) newPosition.Y = 4; break;
                        case 3:
                            if (newPosition.Y > 5) newPosition.Y = 5; break;
                    }
                    break;
                case (int)Direction.Right:
                    newPosition.X++;
                    switch (newPosition.Y)
                    {
                        case 1:
                        case 5:
                            newPosition.X = 3; break;
                        case 2:
                        case 4:
                            if (newPosition.X > 4) newPosition.X = 4; break;
                        case 3:
                            if (newPosition.X > 5) newPosition.X = 5; break;
                    }
                    break;
                case (int)Direction.Down:
                    newPosition.Y--;
                    switch (newPosition.X)
                    {
                        case 1:
                        case 5:
                            newPosition.Y = 3; break;
                        case 2:
                        case 4:
                            if (newPosition.Y < 2) newPosition.Y = 2; break;
                        case 3:
                            if (newPosition.Y < 1) newPosition.Y = 1; break;
                    }
                    break;
                case (int)Direction.Left:
                    newPosition.X--;
                    switch (newPosition.Y)
                    {
                        case 1:
                        case 5:
                            newPosition.X = 3; break;
                        case 2:
                        case 4:
                            if (newPosition.X < 2) newPosition.X = 2; break;
                        case 3:
                            if (newPosition.X < 1) newPosition.X = 1; break;
                    }
                    break;
            }
            return newPosition;
        }

        static Dictionary<Point, int> GetAdvancedDictionary()
        {
            Dictionary<Point, int> dict = new Dictionary<Point, int>(13);
            dict.Add(new Point(1, 3), 5);
            dict.Add(new Point(2, 2), 10);
            dict.Add(new Point(2, 3), 6);
            dict.Add(new Point(2, 4), 2);
            dict.Add(new Point(3, 1), 13);
            dict.Add(new Point(3, 2), 11);
            dict.Add(new Point(3, 3), 7);
            dict.Add(new Point(3, 4), 3);
            dict.Add(new Point(3, 5), 1);
            dict.Add(new Point(4, 2), 12);
            dict.Add(new Point(4, 3), 8);
            dict.Add(new Point(4, 4), 4);
            dict.Add(new Point(5, 3), 9);

            return dict;
        }

        #endregion

        static Direction ResolveDirection(char c)
        {
            if (c.ToString() == "U") return Direction.Up;
            else if (c.ToString() == "R") return Direction.Right;
            else if (c.ToString() == "D") return Direction.Down;
            else return Direction.Left;
        }

        enum Direction { Up=0, Right, Down, Left }
    }
}
