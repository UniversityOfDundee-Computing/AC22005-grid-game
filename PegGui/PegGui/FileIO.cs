using System;
using System.Collections.Generic;
using System.IO;

namespace PegGui
{
    public static class FileIO
    {
        public static Board ReadBoardFile(String name)
        {
            return new Board();
        }

        public static void WriteBoardFile(Board b, String name)
        {

        }

        public static List<Tuple<string, int>> getHighscores(String name)
        {
            return new List<Tuple<string, int>>();
        }

        public static void addScore(int score, String playerName, String mapName)
        {

        }
    }

}