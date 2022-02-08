﻿using System;
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
        Board board;



        Button[,] AIbtn = new Button[10, 10];
        Button[,] Playerbtn = new Button[10, 10];

        int[] ship = new int[] { 1, 0, 2, 2};
        bool placed = false;
        bool recentShip = false;
        int grayX;
        int grayY;
        int NoOfShips = 0;

        public enum DIRECTION
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        };

        public PegSolitairGUI(Board b)
        {
            board = b;
            InitializeComponent();
            for (int x =0; x < 10; x++)
            {
               for (int y = 0; y < 10; y++)
                {
                    AIbtn[x, y] = new Button();
                    AIbtn[x, y].SetBounds(50 + (40 * x), 60 + (40 * y), 43, 43);
                    AIbtn[x, y].BackColor = Color.LightGoldenrodYellow;
                    AIbtn[x, y].Click += new EventHandler(this.btnEvent_Click);
                    Controls.Add(AIbtn[x, y]);
                }
            }

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Playerbtn[x, y] = new Button();
                    Playerbtn[x, y].SetBounds(540 + (40 * x), 60 + (40 * y), 43, 43);

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

            //make 7 images that disapear when ship has been placed
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



        private void FindValid(int x, int y)
        {
            bool Valid = true;
            int loop = 0;

            foreach (DIRECTION i in Enum.GetValues(typeof(DIRECTION))){
                
                x = grayX;
                y = grayY;
                Valid = true;
                loop = 0;
                while (loop <4 && Valid == true)
                {
                    while (loop <4 && ship[loop] == 3)
                    {
                        if (loop != 3 || ship[3] != 3)
                            switch (i)
                            {
                                case DIRECTION.UP:
                                    y--;
                                    break;
                                case DIRECTION.DOWN:
                                    y++;
                                    break;
                                case DIRECTION.LEFT:
                                    x--;
                                    break;
                                case DIRECTION.RIGHT:
                                    x++;
                                    break;
                            }
                        
                        loop++;
                       
                    }
                    if (loop < 4)  
                        switch (i)
                        {
                            case DIRECTION.UP:
                                y--;
                                break;
                            case DIRECTION.DOWN:
                                y++;
                                break;
                            case DIRECTION.LEFT:
                                x--;
                                break;
                            case DIRECTION.RIGHT:
                                x++;
                                break;
                        }   
                    
                    if (x <0 || x  >9 || y<0 || y>9 || Playerbtn[x, y].BackColor == Color.Gray)
                    {
                        Valid = false;
                    }
                    else
                    {
                        Playerbtn[x, y].BackColor = Color.LightGray;
                    }
                    loop++;
                }
                
           }
        }
        

        void placeShip(int x, int y)
        {
            int counter = 0;
            DIRECTION i;
           
            if(x > grayX)
            {
                i = DIRECTION.LEFT;
            }
            else if(y > grayY)
            {
                i = DIRECTION.UP;
            }
            else if (x < grayX)
            {
                i = DIRECTION.RIGHT;
            }
            else
            {
                i = DIRECTION.DOWN;
            }



            while(x > grayX || y > grayY || x < grayX || y < grayY)
            {

                counter++;
                Playerbtn[x, y].BackColor=Color.Gray;
                
                switch (i)
                {
                    case DIRECTION.UP:
                        y--;
                        break;
                    case DIRECTION.DOWN:
                        y++;
                        break;
                    case DIRECTION.LEFT:
                        x--;
                        break;
                    case DIRECTION.RIGHT:
                        x++;
                        break;
                        
                }
                
            }

            recentShip = true;
            NoOfShips++;

            switch (counter)
            {
                case 1:
                    ship[0]++;
                    break;
                case 2:
                    ship[1]++;
                    break;
                case 3:
                    ship[2]++;
                    break;
                case 4:
                    ship[3]++;
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
            
            
            if (NoOfShips == 7)
            {
                placed = true;
                DisplayAi();


            }


        }

        void btnEvent_Click(Object sender, EventArgs e)
        {
            

            if(placed == false)
            {

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        if (sender == Playerbtn[x, y])
                        {
                            if (((Button)sender).BackColor == Color.LightGray)
                            {
                                placeShip(x, y);


                            }
                            else if (((Button)sender).BackColor == Color.LightBlue)
                            {
                                
                                Playerbtn[x,y].BackColor = Color.Gray;
                                
                                grayX = x;
                                grayY = y;
                                FindValid(x, y);
                                recentShip = true;

                            }
                        }
                        

                    }
                }
               

                if (recentShip == false)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            Playerbtn[x, y].BackColor = Color.LightBlue;
                            Playerbtn[grayX, grayY].BackColor = Color.LightBlue;
                        }
                    }
                    recentShip = false;
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
