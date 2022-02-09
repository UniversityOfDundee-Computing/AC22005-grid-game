using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
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
        int OriginX;
        int OriginY;
        int NoOfShips = 0;

        SoundPlayer Music;
        SoundPlayer Sfx;

        public PegSolitairGUI(Board b)
        {
            board = b;
            InitializeComponent();
            Music = new SoundPlayer(PegGui.Properties.Resources.music);
            Music.PlayLooping();
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

            // Commpatibility with existing code:
            Ships_to_place = new int[Enum.GetValues(typeof(SHIP_TYPE)).Length]; // create new array
            for (int i = 0; i < Ships_to_place.Length; i++) // Fill the array
                Ships_to_place[i] = 3;

            foreach (SHIP_TYPE st in def_Ships)
            {
                Ships_to_place[(int)st]--; // Remove the values to produce the inverted to be bplaced array.
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
            int[] availableLengths = GetAvailableLengths();
            foreach (DIRECTION direction in Enum.GetValues(typeof(DIRECTION)))
            {
                x = OriginX;
                y = OriginY;
                int RaytraceDistance = 0;

                while (
                    x >= 0 && x < board.board_Player.GetLength(0) && // X/Y bounds Check
                    y >= 0 && y < board.board_Player.GetLength(1) &&
                    RaytraceDistance < availableLengths[availableLengths.Length-1] // Check that the raytrace has not gone further than the longest possible ship
                    )
                {
                    // Break this direction if a ship is hit
                    if (board.board_Player[x, y].ShipIndex != -1)
                        break;

                    if (availableLengths.Contains(RaytraceDistance+1))
                        Playerbtn[x, y].BackColor = Color.LightGray;

                    RaytraceDistance++;

                    switch (direction)
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
            }
        }


        void PlaceShip(int x, int y)
        {
            int counter = 0;
            DIRECTION i;
           
            if(x > OriginX)
            {
                i = DIRECTION.RIGHT;
            }
            else if(y > OriginY)
            {
                i = DIRECTION.DOWN;
            }
            else if (x < OriginX)
            {
                i = DIRECTION.LEFT;
            }
            else
            {
                i = DIRECTION.UP;
            }



            while(x > OriginX || y > OriginY || x < OriginX || y < OriginY)
            {

                counter++;
                Playerbtn[x, y].BackColor=Color.Gray;
                
                switch (i)
                {
                    case DIRECTION.DOWN:
                        y--;
                        break;
                    case DIRECTION.UP:
                        y++;
                        break;
                    case DIRECTION.RIGHT:
                        x--;
                        break;
                    case DIRECTION.LEFT:
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
                    board.PlaceShip(SHIP_TYPE.Patrol_Boat, OriginX, OriginY, i, true);
                    break;
                case 2:
                    Ships_to_place[1]++;
                    if (Ships_to_place[1] == 1)
                    {
                       board.PlaceShip(SHIP_TYPE.Destroyer, OriginX, OriginY ,i , true);
                    }
                    board.PlaceShip(SHIP_TYPE.Submarine, OriginX, OriginY, i, true);
                    break;
                case 3:
                    Ships_to_place[2]++;
                    board.PlaceShip(SHIP_TYPE.Battleship, OriginX, OriginY, i, true);
                    break;
                case 4:
                    Ships_to_place[3]++;
                    board.PlaceShip(SHIP_TYPE.Carrier, OriginX, OriginY, i, true);
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
                                PlaceShip(x, y);


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
                        Playerbtn[OriginX, OriginY].BackColor = Color.LightBlue;
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
                                OriginX = x;
                                OriginY = y;
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.Show();
        }
    }
}
