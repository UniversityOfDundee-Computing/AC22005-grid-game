using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PegGui
{
    public partial class PegSolitairGUI : Form
    {
        Button[,] btn = new Button[9, 9];  
        public PegSolitairGUI()
        {
            InitializeComponent();
            for (int x =0; x < 9; x++)
            {
               for (int y = 0; y < 9; y++)
                {
                    btn[x, y] = new Button();
                    btn[x, y].SetBounds(120 + (40 * x), 60 + (40 * y), 40, 40);
                    btn[x, y].BackColor = Color.Red;
                    btn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(btn[x, y]);
                }
            }
            MapSetup(1);
        }

        private void MapSetup(int mapSelect)
        {
            switch (mapSelect)
            {
                case 1:

                    
                    for (int map = 0; map < 2; map++)
                    {
                        for (int x = 0; x < 9; x++)
                        {
                            for (int y = 0; y < 9; y++)
                            {

                                btn[x, y].BackColor = Color.Gray;
                                
                                if (y == 2)
                                {
                                    y = y +3;
                                }
                                if (x == 3)
                                {
                                    x = x + 3;
                                }
                                
                            }
                        }
                    }
                    btn[4, 4].BackColor = Color.Gray;
                    btn[3, 0].BackColor = Color.Red; 
                    btn[6, 0].BackColor = Color.Gray;
                    break;
            }
        }
        void btnEvent_Click(Object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
