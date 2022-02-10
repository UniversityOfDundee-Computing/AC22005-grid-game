using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegGui
{
    class Scoreboard
    {
        // Add score to scoreboard
        public static void SubmitScore(string Name, int Time)
        {
            // Read in current scores (this is not safe if multiple instances)
            String[] Lines = File.ReadAllLines(".scores");
            
            // Build the datastructure from the lines
            List<Tuple<int, string>> Scores = new List<Tuple<int, string>>();
            foreach (string ln in Lines)
            {
                string[] d = ln.Split(';');
                Scores.Add(new Tuple<int, string>(int.Parse(d[0]), d[1]));
            }

            // Add new score
            Scores.Add(new Tuple<int, string>(Time, Name));

            // Sort it by best score
            Scores.Sort((first, second) => first.Item1.CompareTo(second.Item1));
            
            // Build new file
            List<string> lines = new List<string>();
            foreach (Tuple<int, string> score in Scores)
            {
                lines.Add(score.Item1 + ";" + score.Item2);
            }

            // Write the file out
            File.WriteAllLines(".scores", lines.ToArray());
        }

        public static List<Tuple<int, string>> GetScores()
        {
            // Read in current scores (this is not safe if multiple instances)
            String[] Lines = File.ReadAllLines(".scores");

            // Build the datastructure from the lines
            List<Tuple<int, string>> Scores = new List<Tuple<int, string>>();
            foreach (string ln in Lines)
            {
                string[] d = ln.Split(';');
                Scores.Add(new Tuple<int, string>(int.Parse(d[0]), d[1]));
            }
            return Scores;
        }
    }
}
