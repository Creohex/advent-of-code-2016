using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Problem4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sum of the sector ID's of the real rooms: " + CalculateRealRoomSums());
            Console.WriteLine("Sector ID of the room with North Pole objects: " + CalculateNorthPoleRoomSectorID());
            Console.ReadKey();
        }

        static int CalculateRealRoomSums()
        {
            List<RoomData> input = FormalizeInput(File.ReadAllLines("Input.txt"));
            int sum = 0;

            foreach (RoomData room in input)
                if (room.IsReal)
                    sum += room.ID;

            return sum;
        }

        static int CalculateNorthPoleRoomSectorID()
        {
            List<RoomData> input = FormalizeInput(File.ReadAllLines("Input.txt"));

            foreach (RoomData room in input)
                if (room.Decrypt().Contains("north"))
                    return room.ID;

            return 0;
        }

        static List<RoomData> FormalizeInput(string[] input)
        {
            List<RoomData> result = new List<RoomData>();
            char[] separators = new char[] { '-', '[', ']' };

            for (int i = 0; i < input.Length; i++)
            {
                string[] rawData = input[i].Split(separators);
                string name = "";
                int id = 0;
                string checksum = "";

                for (int j = 0; j < rawData.Length; j++)
                {
                    if (!Int32.TryParse(rawData[j], out id))
                        name += rawData[j];
                    else
                    {
                        checksum = rawData[j + 1];
                        break;
                    }
                }

                result.Add(new RoomData(input[i], name, id, checksum));
            }
            return result;
        }

        class RoomData
        {
            public string Raw;
            public string Name;
            public int ID;
            public string InitialChecksum;
            public bool IsReal
            {
                get { return this.GetCheckSum() == InitialChecksum; }
            }

            public RoomData() { }
            public RoomData(string rawString, string name, int id, string checksum)
            {
                this.Raw = rawString;
                this.Name = name;
                this.ID = id;
                this.InitialChecksum = checksum;
            }

            public override string ToString()
            {
                return Name + " " + InitialChecksum + " " + ID.ToString();
            }
            public string GetCheckSum()
            {
                string result = "";
                Dictionary<char, int> dict = new Dictionary<char, int>();

                for (int i = 0; i < Name.Length; i++)
                    if (dict.ContainsKey(Name[i]))
                        dict[Name[i]]++;
                    else
                        dict.Add(Name[i], 1);

                foreach (var pair in dict.OrderBy(pair => pair.Key).OrderByDescending(pair => pair.Value).Take(5))
                    result += pair.Key;

                return result;
            }
            public string Decrypt()
            {
                string result = "";
                int shift = ID % 26;

                foreach (var c in Name.ToArray())
                    result += ((c + shift) > 122)
                        ? (char)(c + (char)(shift - 26))
                        : (char)(c + (char)shift);

                return result;
            }
        }
    }
}
