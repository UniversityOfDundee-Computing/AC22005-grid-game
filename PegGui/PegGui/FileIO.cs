using System;
using System.Collections.Generic;
using System.IO;

namespace PegGui
{
    public static class FileIO
    {
        public static Board ReadBoardFile(String name)
        {
            byte[] bitmapData = File.ReadAllBytes("./maps/" + name + ".bmp");

            Bitmap bmp = new Bitmap(bitmapData);
            Peg[,] p = new Peg[bmp.height, bmp.width];
            for (int _y = 0; _y < bmp.height; _y++)
            {
                for (int _x = 0; _x < bmp.width; _x++)
                {
                    PixelColour pixel = bmp.pixelData[_y, _x];
                    if (pixel.Equals(new PixelColour(0,0,0,255)))
                    {
                        p[_y, _x] = new Peg(new Coord(_x,_y),Peg.Peg_State.WALL);
                    }
                    if (pixel.Equals(new PixelColour(255,0,0,255)))
                    {
                        p[_y, _x] = new Peg(new Coord(_x, _y), Peg.Peg_State.SET);
                    }
                    if (pixel.Equals(new PixelColour(255,255,255,255)))
                    {
                        p[_y, _x] = new Peg(new Coord(_x, _y), Peg.Peg_State.EMPTY);
                    }
                }
            }


            return new Board(p);
        }

        public static List<Tuple<string, int>> getHighscores(string name)
        {
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            if (File.Exists("./maps/" + name + ".scores"))
            {
                var lines = File.ReadAllLines("./maps/" + name + ".scores");

                foreach (var line in lines)
                {
                    var data = line.Split(';');
                    int len = int.Parse(data[0]);
                    int score = int.Parse(data[1]);
                    list.Add(new Tuple<string, int>(data[2].Substring(0,len), score));
                }
            }

            list.Sort(compareScores);
            return list;
        }

        // Used for sorting by score
        private static int compareScores(Tuple<string, int> a, Tuple<string, int> b)
        {
            return a.Item2.CompareTo(b.Item2);
        }

        public static void addScore(int score, string playerName, string mapName)
        {
            // Append the name, score and name length to the scores file
            string ln = playerName.Length + ";" + score + ";" + playerName;
            var sw = File.AppendText("./maps/" + mapName + ".scores");
            sw.WriteLine(ln);
            sw.Close();
        }
    }

}