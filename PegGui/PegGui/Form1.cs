using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using static BattleShipGame.Ship;

namespace BattleShipGame
{
    public partial class BattleShipMainGUI : Form
    {
        private Board board;



        private readonly Button[,] AIbtn = new Button[Board.SIZE, Board.SIZE];
        private readonly Button[,] Playerbtn = new Button[Board.SIZE, Board.SIZE];

        int[] Ships_to_place = new int[] { 1, 0, 2, 2 };
        bool placed = false;
        bool recentShip = false;
        int OriginX;
        int OriginY;

        private readonly SoundPlayer Music;
        private SoundPlayer Sfx;

        public BattleShipMainGUI(Board b)
        {
            board = b;
            InitializeComponent();
            Music = new SoundPlayer(PegGui.Properties.Resources.music);
            Music.PlayLooping();
            for (int x = 0; x < Board.SIZE; x++)
            {
                for (int y = 0; y < Board.SIZE; y++)
                {
                    AIbtn[x, y] = new Button();
                    AIbtn[x, y].SetBounds(50 + (40 * x), 60 + (40 * y), 43, 43);
                    AIbtn[x, y].BackColor = Color.LightGoldenrodYellow;
                    AIbtn[x, y].Click += new EventHandler(this.PlayerButtonEvent_Click);
                    Controls.Add(AIbtn[x, y]);
                }
            }

            for (int x = 0; x < Board.SIZE; x++)
            {
                for (int y = 0; y < Board.SIZE; y++)
                {
                    Playerbtn[x, y] = new Button();
                    Playerbtn[x, y].SetBounds(540 + (40 * x), 60 + (40 * y), 43, 43);

                    Playerbtn[x, y].Click += new EventHandler(this.PlayerButtonEvent_Click);
                    Playerbtn[x, y].BackColor = Color.LightBlue;
                    Controls.Add(Playerbtn[x, y]);
                }
            }
            MapSetup();
        }

        public void MapSetup()
        {
            for (int x = 0; x < Board.SIZE; x++)
            {
                for (int y = 0; y < Board.SIZE; y++)
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

            BtnCarrier.SetBounds(120, 200, 43, 215);
            BtnWarShip.SetBounds(170, 200, 43, 172);
            BtnSub.SetBounds(220, 200, 43, 129);
            BtnShip.SetBounds(270, 200, 43, 129);
            BtnFisher.SetBounds(320, 200, 43, 86);

            BtnCarrier.BackColor = Color.Transparent;
            BtnCarrier.BackgroundImageLayout = ImageLayout.Zoom;
            BtnCarrier.BackgroundImage = PegGui.Properties.Resources.Carrier;
            BtnWarShip.BackColor = Color.Transparent;
            BtnWarShip.BackgroundImageLayout = ImageLayout.Zoom;
            BtnWarShip.BackgroundImage = PegGui.Properties.Resources.Battleship;
            BtnSub.BackColor = Color.Transparent;
            BtnSub.BackgroundImageLayout = ImageLayout.Zoom;
            BtnSub.BackgroundImage = PegGui.Properties.Resources.Submarine;
            BtnShip.BackColor = Color.Transparent;
            BtnShip.BackgroundImageLayout = ImageLayout.Zoom;
            BtnShip.BackgroundImage = PegGui.Properties.Resources.Destroyer;
            BtnFisher.BackColor = Color.Transparent;
            BtnFisher.BackgroundImageLayout = ImageLayout.Zoom;
            BtnFisher.BackgroundImage = PegGui.Properties.Resources.Patrol_Boat;



            Controls.Add(BtnCarrier);
            Controls.Add(BtnWarShip);
            Controls.Add(BtnSub);
            Controls.Add(BtnShip);
            Controls.Add(BtnFisher);
        }

        private void DisplayAi()
        {
            for (int x = 0; x < Board.SIZE; x++)
            {
                for (int y = 0; y < Board.SIZE; y++)
                {
                    AIbtn[x, y].Visible = true;

                }
            }
        }

        // Get all the available ships to place
        private List<SHIP_TYPE> GetAvailableShips()
        {
            List<SHIP_TYPE> def_Ships = new List<SHIP_TYPE>(Ship.DEFAULT_SHIPS);

            // Itterate over the ships that the player has already placed
            foreach (Ship s in board.playerShips)
            {
                // Itterate over the default ships, if the ship from the player's ship list is in the default ships list, remove it
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
                Ships_to_place[(int)st]--; // Remove the values to produce the inverted to be placed array.
            }

            return def_Ships;
        }


        // From the available ships list, get all the lengths, make it unique, and sort it numerically
        private int[] GetAvailableLengths()
        {
            List<SHIP_TYPE> avail_Ships = GetAvailableShips();
            List<int> lengths = new List<int>();

            // Itterate over the available ships, get the length, if it is not contained in the list, add it
            foreach (SHIP_TYPE st in avail_Ships)
            {
                int l = Ship.GetShipDimensions(st);
                if (!lengths.Contains(l))
                    lengths.Add(l);
            }

            // Sort the list, and return as an array
            lengths.Sort();
            return lengths.ToArray();
        }



        private void FindValid()
        {
            int[] availableLengths = GetAvailableLengths();
            foreach (DIRECTION direction in Enum.GetValues(typeof(DIRECTION)))
            {
                int x = OriginX;
                int y = OriginY;
                int RaytraceDistance = 0;

                while (
                    x >= 0 && x < board.board_Player.GetLength(0) && // X/Y bounds Check
                    y >= 0 && y < board.board_Player.GetLength(1) &&
                    RaytraceDistance < availableLengths[availableLengths.Length - 1] // Check that the raytrace has not gone further than the longest possible ship
                    )
                {
                    // Break this direction if a ship is hit
                    if (board.board_Player[x, y].ShipIndex != -1)
                        break;

                    if (availableLengths.Contains(RaytraceDistance + 1))
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
            int shipLength = 1;
            DIRECTION direction;

            // Determine the direction of the ship
            if (x > OriginX)
            {
                direction = DIRECTION.RIGHT;
            }
            else if (y > OriginY)
            {
                direction = DIRECTION.DOWN;
            }
            else if (x < OriginX)
            {
                direction = DIRECTION.LEFT;
            }
            else
            {
                direction = DIRECTION.UP;
            }


            // Walk the direction of the ship from the last coordinate to the origin, measureing the length and coloring the cell
            while (x > OriginX || y > OriginY || x < OriginX || y < OriginY)
            {
                shipLength++;
                Playerbtn[x, y].BackColor = Color.Gray;

                switch (direction)
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

            // Itterating over the ships to try to find an appropriate ship that matches the measured length, and then placing it in the datamodel
            foreach (SHIP_TYPE st in GetAvailableShips())
            {
                if (shipLength == Ship.GetShipDimensions(st))
                {
                    if (board.PlaceShip(st, OriginX, OriginY, direction, true) == Board.ACTION_STATE.ACTION_SUCCESS)
                    {
                        Sfx = new SoundPlayer(PegGui.Properties.Resources.drop);
                        Sfx.Play();
                    }
                    break;
                }
            }

            // Color the remaining cells blue
            for (x = 0; x < Board.SIZE; x++)
            {
                for (y = 0; y < Board.SIZE; y++)
                {
                    if (Playerbtn[x, y].BackColor == Color.LightGray)
                    {
                        Playerbtn[x, y].BackColor = Color.LightBlue;
                    }
                }
            }

            // End the cycle if all available ships have been placed
            if (board.playerShips.Count >= Ship.DEFAULT_SHIPS.Length)
            {
                placed = true;
                DisplayAi();
            }


        }

        void PlayerButtonEvent_Click(Object sender, EventArgs e)
        {
            if (!placed)
            {
                if (((Button)sender).BackColor == Color.LightGray)
                {
                    for (int x = 0; x < Board.SIZE; x++)
                        for (int y = 0; y < Board.SIZE; y++)
                            if (sender == Playerbtn[x, y])
                                PlaceShip(x, y);
                }
                else
                {
                    if (!recentShip)
                    {
                        for (int x = 0; x < Board.SIZE; x++)
                        {
                            for (int y = 0; y < Board.SIZE; y++)
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

                    for (int x = 0; x < Board.SIZE; x++)
                    {
                        for (int y = 0; y < Board.SIZE; y++)
                        {
                            if (sender == Playerbtn[x, y])
                            {
                                OriginX = x;
                                OriginY = y;
                                FindValid();
                            }
                        }
                    }
                }
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            board.InitialiseBoard();
            for (int x = 0; x < Board.SIZE; x++)
                for (int y = 0; y < Board.SIZE; y++)
                    Playerbtn[x, y].BackColor = Color.LightBlue;
            placed = false;

            MapSetup();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.Show();
        }
    }
}
