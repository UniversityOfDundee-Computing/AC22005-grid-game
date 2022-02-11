using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShipGame
{
    public partial class Closing : Form
    {
        private readonly int RunningTime;
        private readonly bool PlayerWon;
        private readonly Board bOard;
        private readonly List<Tuple<int, int, bool>> HighScores;

        public Closing(int v, bool playerWinner, Board board, List<Tuple<int, int, bool>> lists)
        {
            this.RunningTime = v;
            this.PlayerWon = playerWinner;
            this.bOard = board;
            this.HighScores = lists;
            InitializeComponent();

            LblWinner.Text = PlayerWon ? "YOU" : "AI";
            LblScore.Text = bOard.PlayerMoves.ToString();
            LblScore.Text = RunningTime.ToString();
            LblHighScore.Text = HighScores[0].Item2.ToString();
            LblHighTime.Text = HighScores[0].Item1.ToString();
        }

        private void Closing_Load(object sender, EventArgs e)
        {

        }
        private void LblWinner_Click(object sender, EventArgs e)
        {

        }

        private void LblScore_Click(object sender, EventArgs e)
        {

        }

        private void LblTime_Click(object sender, EventArgs e)
        {

        }

        private void LblHighScore_Click(object sender, EventArgs e)
        {

        }

        private void LblHighTime_Click(object sender, EventArgs e)
        {

        }
    }
}
