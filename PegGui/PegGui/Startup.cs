using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BattleShipGame.Ship;

namespace BattleShipGame
{
    public partial class Battleship : Form
    {
        private readonly Board board;
        public Battleship(Board b)
        {
            board = b;
            InitializeComponent();
        }

        private void BtnEasy_Click(object sender, EventArgs e)
        {
            AI.DIFFICUILTY Difficulty = (AI.DIFFICUILTY)int.Parse((string)((Button)sender).Tag);
            board.GameDificulty = Difficulty;
            Close();
        }
    }
}
