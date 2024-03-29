﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            AI ai = new AI(board);
            BattleShipMainGUI bsMg = new BattleShipMainGUI(board, ai);
            ai.Reset();
            ai.SetShips();
            Thread workerThread = new Thread(new ThreadStart(() => Worker(board, bsMg, ai)));
            workerThread.Start();
            Application.Run(bsMg);

        }

        private static void Worker(Board board, BattleShipMainGUI bsMg, AI ai)
        {
            while (bsMg.Visible)
            {
                if (bsMg.placed)
                {
                    while (board.GetShipsToSink(false) > 0 && board.GetShipsToSink(true) > 0)
                    {
                        if (bsMg.MoveComplete)
                        {
                            if (board.PlayerMoves > board.OtherMoves)
                            {
                                ai.DoMove(); // AI's Move
                                bsMg.UpdateGrid(); // Update the grid in the gui
                            }
                            else
                            {
                                bsMg.MoveComplete = false; // Allow the gui to make a move
                            }
                        }
                    }

                    bsMg.ShowEndScreen();
                }
            }
        }
    }
}
