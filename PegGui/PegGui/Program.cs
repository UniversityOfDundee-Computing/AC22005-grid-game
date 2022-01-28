using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PegGui
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Board b = FileIO.ReadBoardFile("cross"); // Testing code
            FileIO.addScore(10, "Adam", "cross"); // Testing code
            FileIO.getHighscores("cross"); // Testing code

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PegSolitairGUI());
        }
    }
}
