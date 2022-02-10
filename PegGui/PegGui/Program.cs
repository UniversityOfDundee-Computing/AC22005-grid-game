using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipGame
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Board board = new Board();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BattleShipMainGUI bsMg = new BattleShipMainGUI(board);
            Application.Run(bsMg);

            AI ai = new AI(board);

            while(board.GetShipsToSink(false) > 0 && board.GetShipsToSink(true) > 0)
            {
                if (bsMg.MoveComplete)
                {
                    if (board.playerMoves > board.otherMoves)
                    {
                        ai.doMove(); // AI's Move
                        bsMg.updateGrid(); // Update the grid in the gui
                    }
                    else
                    {
                       // bsMg.PlayMove(); // Allow the gui to make a move
                    }
                }
            }
        }
    }
}
