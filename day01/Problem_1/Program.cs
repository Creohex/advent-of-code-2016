using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Problem_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Destination: " + FindDestination());
            Console.WriteLine("Read Destination: " + FindRealDestination());
            Console.ReadKey();
        }

        static string readInput(string filePath)
        {
            string result = "";
            string[] array = File.ReadAllLines(filePath);

            foreach (string s in array)
                result += s;

            return result;
        }

        static List<Directions> FactorizeInput(string input)
        {
            List<Directions> result = new List<Directions>();
            Regex rx = new Regex(@"\d");
            char turnChar = new char();
            string stepsStr = "";

            foreach (char c in input)
            {
                if (c.ToString() == "R" || c.ToString() == "L")
                    turnChar = c;

                if (rx.IsMatch(c.ToString()))
                    stepsStr += c;

                if (c.ToString() == ",")
                {
                    result.Add(
                        new Directions(
                            turnChar.ToString() == "L" ? TurnDirection.Left : TurnDirection.Right,
                            Int32.Parse(stepsStr)));

                    turnChar = new char();
                    stepsStr = "";
                }
            }

            return result;
        }

        static FacingDirection CalculateNewFacingDirection(FacingDirection facingDirection, TurnDirection turnDirection)
        {
            int facingDirectionAsInt = (int)facingDirection;
            if (turnDirection == TurnDirection.Right)
            {
                facingDirectionAsInt++;
                if (facingDirectionAsInt > 3) facingDirectionAsInt = 0;
            }
            else
            {
                facingDirectionAsInt--;
                if (facingDirectionAsInt < 0) facingDirectionAsInt = 3;
            }

            return (FacingDirection)facingDirectionAsInt;
        }

        static Point Walk(Point currentPosition, FacingDirection facingDirection, int amountOfSteps)
        {
            switch ((int)facingDirection)
            {
                case ((int)FacingDirection.North):
                    currentPosition.Y += amountOfSteps;
                    break;
                case ((int)FacingDirection.East):
                    currentPosition.X += amountOfSteps;
                    break;
                case ((int)FacingDirection.South):
                    currentPosition.Y -= amountOfSteps;
                    break;
                case ((int)FacingDirection.West):
                    currentPosition.X -= amountOfSteps;
                    break;
            }

            return currentPosition;
        }

        static Point WalkOneStep(Point currentPosition, FacingDirection facingDirection)
        {
            switch ((int)facingDirection)
            {
                case ((int)FacingDirection.North):
                    currentPosition.Y += 1;
                    break;
                case ((int)FacingDirection.East):
                    currentPosition.X += 1;
                    break;
                case ((int)FacingDirection.South):
                    currentPosition.Y -= 1;
                    break;
                case ((int)FacingDirection.West):
                    currentPosition.X -= 1;
                    break;
            }

            return currentPosition;
        }

        static int GetDistance(Point position)
        {
            return Math.Abs(position.X) + Math.Abs(position.Y);
        }

        static int FindDestination()
        {
            List<Directions> input = FactorizeInput(readInput("Input.txt"));
            FacingDirection currentFacingDirection = FacingDirection.North;
            Point currentPosition = new Point(0, 0);

            foreach (Directions d in input)
            {
                currentFacingDirection = CalculateNewFacingDirection(currentFacingDirection, d.TurnDirection);
                currentPosition = Walk(currentPosition, currentFacingDirection, d.Steps);
            }

            return GetDistance(currentPosition);
        }

        static int FindRealDestination()
        {
            List<Directions> input = FactorizeInput(readInput("Input.txt"));
            FacingDirection currentFacingDirection = FacingDirection.North;
            Point currentPosition = new Point(0, 0);
            List<Point> previousLocations = new List<Point>();

            foreach (Directions d in input)
            {
                currentFacingDirection = CalculateNewFacingDirection(currentFacingDirection, d.TurnDirection);
                for (int i = 0; i < d.Steps; i++)
                {
                    currentPosition = WalkOneStep(currentPosition, currentFacingDirection);

                    if (previousLocations.Contains(currentPosition))
                        return GetDistance(currentPosition);

                    previousLocations.Add(new Point(currentPosition.X, currentPosition.Y));
                }
            }

            return GetDistance(currentPosition);
        }

        enum TurnDirection { Left=0, Right }
        enum FacingDirection { North=0, East, South, West }
        class Directions
        {
            public TurnDirection TurnDirection;
            public int Steps;

            public Directions() { }
            public Directions(TurnDirection d, int steps)
            {
                this.TurnDirection = d;
                this.Steps = steps;
            }

            public override string ToString()
            {
                return TurnDirection.ToString() + " " + Steps;
            }
        }
    }
}
