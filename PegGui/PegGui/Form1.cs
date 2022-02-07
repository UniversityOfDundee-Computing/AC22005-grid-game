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
    public partial class PegSolitairGUI : Form
    {
        Button[,] AIbtn = new Button[10, 10];
        Button[,] Playerbtn = new Button[10, 10];

        int[] ship = new int[] { 1, 2, 2, 3, 4 };
        bool placed = false;
        bool recentShip = false;
        int grayX;
        int grayY;
        public PegSolitairGUI()
        {
            InitializeComponent();
            for (int x =0; x < 10; x++)
            {
               for (int y = 0; y < 10; y++)
                {
                    AIbtn[x, y] = new Button();
                    AIbtn[x, y].SetBounds(120 + (40 * x), 60 + (40 * y), 43, 43);
                    AIbtn[x, y].BackColor = Color.Red;
                    AIbtn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(AIbtn[x, y]);
                }
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Playerbtn[x, y] = new Button();
                    Playerbtn[x, y].SetBounds(520 + (40 * x), 60 + (40 * y), 43, 43);

                    Playerbtn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Playerbtn[x, y].BackColor = Color.LightBlue;
                    Controls.Add(Playerbtn[x, y]);
                }
            }
            MapSetup();
        }

        public void MapSetup()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    AIbtn[x, y].Visible = false;

                }
            }
            Button BtnCarrier = new Button();
            Button BtnWarShip = new Button();
            Button BtnSub = new Button();
            Button BtnShip = new Button();
            Button BtnFisher = new Button();

            BtnCarrier.SetBounds(120 , 200 , 43, 215);
            BtnWarShip.SetBounds(170, 200, 43, 172);
            BtnSub.SetBounds(220, 200, 43, 129);
            BtnShip.SetBounds(270, 200, 43, 129);
            BtnFisher.SetBounds(320, 200, 43, 86);

            BtnCarrier.BackColor = Color.Gray;
            BtnWarShip.BackColor = Color.Gray;
            BtnSub.BackColor = Color.Gray;
            BtnShip.BackColor = Color.Gray;
            BtnFisher.BackColor = Color.Gray;

            

            Controls.Add(BtnCarrier);
            Controls.Add(BtnWarShip);
            Controls.Add(BtnSub);
            Controls.Add(BtnShip);
            Controls.Add(BtnFisher);
        }

        private void DisplayAi()
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    AIbtn[x, y].Visible = true;

                }
            }
        }



        private void FindValid()
        {
           int x = grayX;
           int y = grayY;
           int loop = 0;
           int NoOfShips = 0; 
           bool xPluss = false;
           bool yPluss = false;
           bool xMinus = false;
           bool yMinus = false;

            while (loop != 5 && placed == false)
            {   
                
                
                // this bit doesnt work
                if (ship[loop] == 0)
                {

                    NoOfShips++;
                    if (NoOfShips == 5)
                    {
                        placed = true;
                        DisplayAi();
                    }
                }
             
                if (x + ship[loop] <= 8 && Playerbtn[x + ship[loop], y].BackColor != Color.Gray && xPluss == false)
                {
                    Playerbtn[x + ship[loop], y].BackColor = Color.LightGray;
                       
                }
                if (y + ship[loop] <= 8 && Playerbtn[x, y + ship[loop]].BackColor != Color.Gray && yPluss == false)
                {
                    Playerbtn[x, y + ship[loop]].BackColor = Color.LightGray;
                       
                }
                if (x - ship[loop] >= 0 && Playerbtn[x - ship[loop], y].BackColor != Color.Gray && xMinus == false)
                {
                    Playerbtn[x - ship[loop], y].BackColor = Color.LightGray;
                       
                }
                if (y - ship[loop] >= 0 && Playerbtn[x, y - ship[loop]].BackColor != Color.Gray && yMinus == false)
                {
                    Playerbtn[x, y - ship[loop]].BackColor = Color.LightGray;
                       
                }

                if (ship[loop] == 0)
                {

                    NoOfShips++;
                    if (NoOfShips == 5)
                    {
                        placed = true;
                        DisplayAi();
                    }
                }
                loop++;
                
            }
      
        }

        void placeShip(int x, int y)
        {
            int counter = 1;
            bool loop = true;
            if (x > grayX && y == grayY)
            {
                while (loop == true)
                {
                    if (x == grayX)
                    {
                        loop = false;
                        recentShip = true;
                        
                    }
                    else
                    {
                        Playerbtn[x, y].BackColor = Color.Gray;
                        x--;
                        counter++;

                    }
                }


            }
            else if (y > grayY && x == grayX)
            {
                while (loop == true)
                {
                    if (y == grayY)
                    {
                        loop = false;
                        recentShip = true;

                    }
                    else
                    {
                        Playerbtn[x, y].BackColor = Color.Gray;
                        y--;
                        counter++;
                    }
                }
            }
            else if (x < grayX && y == grayY)
            {
                while (loop == true)
                {
                    if (x == grayX)
                    {
                        loop = false;
                        recentShip = true;

                    }
                    else
                    {
                        Playerbtn[x, y].BackColor = Color.Gray;
                        x++;
                        counter++;
                    }
                }
            }
            else if (y < grayY && x == grayX)
            {
                while (loop == true)
                {
                    if (y == grayY)
                    {
                        loop = false;
                        recentShip = true;

                    }
                    else
                    {
                        Playerbtn[x, y].BackColor = Color.Gray;
                        y++;
                        counter++;
                    }
                }
            }

            switch (counter)
            {
                
                case 2:
                    ship[0] = 0;

                    break;
                case 3:
                    
                    if (ship[1] == 0)
                    {
                        ship[2] = 0;
                    }
                    ship[1] = 0;
                    break;
                case 4:
                    ship[3] = 0;
                    break;
                case 5:
                    ship[4] = 0;
                    
                    break;
                
             
                
            }

            for (x = 0; x < 10; x++)
            {
                for (y = 0; y < 10; y++)
                {
                    if (Playerbtn[x, y].BackColor == Color.LightGray)
                    {

                        Playerbtn[x, y].BackColor = Color.LightBlue;

                    }

                }
            }


        }

        void btnEvent_Click(Object sender, EventArgs e)
        {
            

            if(placed == false)
            {
                if( ((Button)sender).BackColor == Color.LightGray )
                {
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            if (sender == Playerbtn[x, y])
                            {
                                placeShip(x, y);


                            }

                        }
                    }
                }
                else {
                    if (recentShip == false)
                    {

                        for (int x = 0; x < 10; x++)
                        {
                            for (int y = 0; y < 10; y++)
                            {
                                if (Playerbtn[x, y].BackColor == Color.LightGray)
                                {

                                    Playerbtn[x, y].BackColor = Color.LightBlue;

                                }

                            }
                        }
                        Playerbtn[grayX,grayY].BackColor = Color.LightBlue;
                    }
                    else
                    {
                        recentShip = false;
                    }
                    ((Button)sender).BackColor = Color.Gray;
                    
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            if (sender == Playerbtn[x, y])
                            {
                                grayX = x;
                                grayY = y;
                                FindValid();
                               

                            }

                        }
                    }
                   
                }   
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    

                     Playerbtn[x, y].BackColor = Color.LightBlue;

                    

                }
            }
            placed = false;
            ship[0] = 1;
            ship[1] = 2;
            ship[2] = 2;
            ship[3] = 3;
            ship[4] = 4;
            MapSetup();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
