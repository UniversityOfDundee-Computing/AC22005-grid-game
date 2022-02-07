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

        Ship.SHIP_TYPE[] ship = new Ship.SHIP_TYPE[] { Ship.SHIP_TYPE.Patrol_Boat, Ship.SHIP_TYPE.Submarine, Ship.SHIP_TYPE.Destroyer, Ship.SHIP_TYPE.Battleship, Ship.SHIP_TYPE.Carrier };
        bool placed = false;
        bool recentShip = false;
        int grayX;
        int grayY;
        int NoOfShips = 0;

        public PegSolitairGUI(Board b)
        {
            board = b;
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



        private void FindValid(int x, int y)
        {
           
           int loop = 0;
           
           bool xPluss = false;
           bool yPluss = false;
           bool xMinus = false;
           bool yMinus = false;

            while (loop != 5 && placed == false)
            {   
                
                
             
             


                if (x + Ship.GetShipDimensions(ship[loop])-1 <= 9 && Playerbtn[x + Ship.GetShipDimensions(ship[loop])-1, y].BackColor != Color.Gray && xPluss == false)
                {
                    Playerbtn[x + Ship.GetShipDimensions(ship[loop]) - 1, y].BackColor = Color.LightGray;
                       
                }
               
                if (y + Ship.GetShipDimensions(ship[loop]) - 1 <= 9 && Playerbtn[x, y + Ship.GetShipDimensions(ship[loop]) - 1].BackColor != Color.Gray && yPluss == false)
                {
                    Playerbtn[x, y + Ship.GetShipDimensions(ship[loop]) - 1].BackColor = Color.LightGray;
                       
                }
                
                if (x - Ship.GetShipDimensions(ship[loop]) + 1 >= 0 && Playerbtn[x - Ship.GetShipDimensions(ship[loop]) + 1, y].BackColor != Color.Gray && xMinus == false)
                {
                    Playerbtn[x - Ship.GetShipDimensions(ship[loop]) + 1, y].BackColor = Color.LightGray;
                       
                }
              
                
                if (y - Ship.GetShipDimensions(ship[loop]) + 1 >= 0 && Playerbtn[x, y - Ship.GetShipDimensions(ship[loop]) + 1].BackColor != Color.Gray && yMinus == false)
                {
                    Playerbtn[x, y - Ship.GetShipDimensions(ship[loop]) + 1].BackColor = Color.LightGray;
                       
                }
                


                loop++;
                
            }
      
        }

        void placeShip(int x, int y)
        {
            int counter = 0;
            bool loop = true;
            if (x > grayX && y == grayY)
            {
                while (loop == true)
                {
                    if (x == grayX)
                    {
                        loop = false;
                        recentShip = true;
                        board.PlaceShip(ship[counter], grayX, grayY, Ship.DIRECTION.LEFT, true);
                        NoOfShips++;
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
                        board.PlaceShip(ship[counter], grayX, grayY, Ship.DIRECTION.DOWN, true);
                        NoOfShips++;
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
                        board.PlaceShip(ship[counter], grayX, grayY, Ship.DIRECTION.RIGHT, true);
                        NoOfShips++;
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
                        board.PlaceShip(ship[counter], grayX, grayY, Ship.DIRECTION.UP, true);
                        NoOfShips++;
                    }
                    else
                    {
                        Playerbtn[x, y].BackColor = Color.Gray;
                        y++;
                        counter++;
                    }
                }
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
            
            
            if (NoOfShips == 5)
            {
                placed = true;
                DisplayAi();


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
                                FindValid(x, y);
                               

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
