using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    class Scoreboard
    {
        // Add score to scoreboard
        public static void SubmitScore(int Moves, int Time, bool playerWon)
        {
            String[] Lines;
            if (!File.Exists(".scores"))
                Lines = new string[0];
            else
                // Read in current scores (this is not safe if multiple instances)
                Lines = File.ReadAllLines(".scores");

            // Build the datastructure from the lines
            List<Tuple<int, int, bool>> Scores = new List<Tuple<int, int, bool>>();
            foreach (string ln in Lines)
            {
                string[] d = ln.Split(';');
                Scores.Add(new Tuple<int, int, bool>(int.Parse(d[0]), int.Parse(d[1]), bool.Parse(d[2])));
            }

            // Add new score
            Scores.Add(new Tuple<int, int, bool>(Time, Moves, playerWon));

            // Sort it by best score
            Scores.Sort((first, second) => first.Item1.CompareTo(second.Item1));

            // Build new file
            List<string> lines = new List<string>();
            foreach (Tuple<int, int, bool> score in Scores)
            {
                lines.Add(score.Item1 + ";" + score.Item2 + ";" + score.Item3);
            }

            // Write the file out
            File.WriteAllLines(".scores", lines.ToArray());
        }

        public static List<Tuple<int, int, bool>> GetScores()
        {
            if (!File.Exists(".scores"))
                return new List<Tuple<int, int, bool>>();
            // Read in current scores (this is not safe if multiple instances)
            String[] Lines = File.ReadAllLines(".scores");

            // Build the datastructure from the lines
            List<Tuple<int, int, bool>> Scores = new List<Tuple<int, int, bool>>();
            foreach (string ln in Lines)
            {
                string[] d = ln.Split(';');
                Scores.Add(new Tuple<int, int, bool>(int.Parse(d[0]), int.Parse(d[1]), bool.Parse(d[2])));
            }
            return Scores;
        }
    }
}
