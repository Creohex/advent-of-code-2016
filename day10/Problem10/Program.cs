using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Problem10
{
    class Program
    {
        static Regex rx = new Regex(@"\s");

        static void Main(string[] args)
        {
            Console.WriteLine("Bot that is responsible for comparing chips 17 and 61 is: " +  CalculateBotChips(17, 61));
            Console.ReadKey();
        }

        static int CalculateBotChips(int chipID1, int chipID2)
        {
            Dictionary<int, List<int>> bots = new Dictionary<int, List<int>>();
            List<Command> commands = new List<Command>();
            Dictionary<int, List<int>> bin = new Dictionary<int, List<int>>();

            foreach (string directive in File.ReadAllLines("Input.txt"))
            {
                string[] elements = rx.Split(directive);
                if (elements[0] == "value")
                {
                    int chipID = Int32.Parse(elements[1]);
                    int botID = Int32.Parse(elements[5]);

                    if (bots.ContainsKey(botID))
                        bots[botID].Add(chipID);
                    else
                        bots.Add(botID, new List<int>() { chipID });
                }
                else if (elements[0] == "bot")
                    commands.Add(
                        new Command(
                            Int32.Parse(elements[1]),
                            elements[5] == "bot",
                            Int32.Parse(elements[6]),
                            elements[10] == "bot",
                            Int32.Parse(elements[11])));
            }

            while (commands.Count > 0)
            {
                var readyBot = bots.First(x => x.Value.Count == 2);

                int minChip = readyBot.Value.Min();
                int maxChip = readyBot.Value.Max();

                if (minChip == chipID1 && maxChip == chipID2)
                    return readyBot.Key;

                Command cmd = commands.Find(x => x.BotID == readyBot.Key);
                if (cmd != null)
                {
                    if (cmd.LowToBot)
                        PassChip(bots, readyBot, cmd.LowID, minChip);
                    else
                        BinChip(readyBot, bin, minChip, cmd.LowID);

                    if (cmd.HighToBot)
                        PassChip(bots, readyBot, cmd.HighID, maxChip);
                    else
                        BinChip(readyBot, bin, maxChip, cmd.HighID);

                    commands.Remove(cmd);
                }
            }

            return -1;
        }

        static void PassChip(Dictionary<int, List<int>> bots, KeyValuePair<int, List<int>> fromBot, int toBotID, int chipID)
        {
            if (bots.Keys.Contains(toBotID))
                bots[toBotID].Add(chipID);
            else
                bots.Add(toBotID, new List<int>() { chipID });

            fromBot.Value.Remove(chipID);
        }

        static void BinChip(KeyValuePair<int, List<int>> fromBot, Dictionary<int, List<int>> bin, int chipID, int binID)
        {
            if (bin.ContainsKey(binID))
                bin[binID].Add(chipID);
            else
                bin.Add(binID, new List<int>() { chipID });

            fromBot.Value.Remove(chipID);
        }

        class Command
        {
            public int BotID;
            public bool LowToBot;
            public int LowID;
            public bool HighToBot;
            public int HighID;

            public Command() { }
            public Command(int botID, bool lowToBot, int lowID, bool highToBot, int highID)
            {
                this.BotID = botID;
                this.LowToBot = lowToBot;
                this.LowID = lowID;
                this.HighToBot = highToBot;
                this.HighID = highID;
            }

            public override string ToString()
            {
                return "bot " + BotID + " " + (LowToBot ? "gives low to bot " : "gives low to output ") + LowID + " and high to " + (HighToBot ? "bot " : "output ") + HighID; 
            }
        }
    }
}
