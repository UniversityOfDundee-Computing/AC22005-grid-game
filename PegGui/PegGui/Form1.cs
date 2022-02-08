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
    public partial class PegSolitairGUI : Form
    {
        Board board;



        Button[,] AIbtn = new Button[10, 10];
        Button[,] Playerbtn = new Button[10, 10];

        int[] Ships_to_place = new int[] { 1, 0, 2, 2};
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

        private List<SHIP_TYPE> GetAvailableShips()
        {
            List<SHIP_TYPE> def_Ships = new List<SHIP_TYPE>(Ship.DEFAULT_SHIPS);
            foreach (Ship s in board.playerShips)
            {

                for (int i = 0; i < def_Ships.Count; i++)
                {
                    SHIP_TYPE st = def_Ships[i];
                    if (st == s.Type)
                    {
                        def_Ships.RemoveAt(i);
                        break;
                    }
                }
            }
            return def_Ships;
        }
        private int[] GetAvailableLengths()
        {
            List<SHIP_TYPE> avail_Ships = GetAvailableShips();
            List<int> lengths = new List<int>();
            foreach (SHIP_TYPE st in avail_Ships)
            {
                int l = Ship.GetShipDimensions(st);
                if (!lengths.Contains(l))
                    lengths.Add(l);
            }
            lengths.Sort();
            return lengths.ToArray();
        }

        private void FindValid(int x, int y)
        {

            int[] ShipsToBePlaced = GetAvailableLengths();
            int itterator;
            int xOff;
            int yOff;
            foreach (DIRECTION dir in Enum.GetValues(typeof(DIRECTION)))
            {
                xOff = 0;
                yOff = 0;
                itterator = 0;
                while (
                    itterator < ShipsToBePlaced[ShipsToBePlaced.Length-1] &&
                    x+xOff >= 0 && x+xOff < board.board_Player.GetLength(0) &&
                    y+yOff >= 0 && y+yOff < board.board_Player.GetLength(1)
                    )
                {
                    if (board.board_Player[x + xOff, y + yOff].ShipIndex != -1)
                        break;

                    Playerbtn[x + xOff, y + yOff].BackColor = Color.LightGray;

                    itterator++;

                    switch (dir)
                    {
                        case DIRECTION.UP:
                            yOff--;
                            break;
                        case DIRECTION.DOWN:
                            yOff++;
                            break;
                        case DIRECTION.LEFT:
                            xOff--;
                            break;
                        case DIRECTION.RIGHT:
                            xOff++;
                            break;
                    }
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
                    Ships_to_place[0]++;
                    break;
                case 2:
                    Ships_to_place[1]++;
                    break;
                case 3:
                    Ships_to_place[2]++;
                    break;
                case 4:
                    Ships_to_place[3]++;
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


            if (placed == false)
            {
                if (((Button)sender).BackColor == Color.LightGray)
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
                else
                {
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
                        Playerbtn[grayX, grayY].BackColor = Color.LightBlue;
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
