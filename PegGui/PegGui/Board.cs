using System;
using System.Collections.Generic;

namespace BattleShipGame
{
    public class Board
    {
        // 10x10 Board object

        // Global Properties for storing current state
        public const int SIZE = 10;
        public GridPosition[,] board_Player;
        public GridPosition[,] board_Other;

        public List<Ship> playerShips = new List<Ship>();
        public List<Ship> otherShips = new List<Ship>();

        // Counters to help with scoring
        public int PlayerMoves { get; set; }
        public int OtherMoves { get; set; }

        public AI.DIFFICUILTY GameDificulty { get; set; }

        // Enums allowing for more legible code and allowing callers to easily know return state
        public enum ACTION_STATE
        {
            ACTION_SUCCESS,
            ACTION_FAIL,
            ACTION_HIT,
            ACTION_MISS,
            ACTION_SHIP_SUNK
        };

        // Main Constructor, call the inisialiser
        public Board()
        {
            InitialiseBoard();
        }

        // PlacePeg method, allows user (ai or player) to place a peg on the board and returns if it was a hit, a miss, or a fail (if a peg was already present there)
        public ACTION_STATE PlacePeg(int _X, int _Y, bool isPlayer)
        {
            GridPosition[,] current_grid;
            List<Ship> current_ship_list;
            // Allocate correct current grid and list - helps remove duplicated code
            if (!isPlayer)
            {
                current_grid = board_Player;
                current_ship_list = playerShips;
            }
            else
            {
                current_grid = board_Other;
                current_ship_list = otherShips;
            }

            // If the cell is already hit - fail
            if (current_grid[_X, _Y].Hit)
                return ACTION_STATE.ACTION_FAIL;

            // If the cell contains a ship - hit
            if (current_grid[_X, _Y].ShipIndex != -1)
            {
                if (isPlayer)
                    this.PlayerMoves++;
                else
                    this.OtherMoves++;

                current_grid[_X, _Y].Hit = true;
                current_ship_list[current_grid[_X, _Y].ShipIndex].Hits++;
                // Check if ship is sunk - sunk
                if (current_ship_list[current_grid[_X, _Y].ShipIndex].Hits >= current_ship_list[current_grid[_X, _Y].ShipIndex].Length)
                    return ACTION_STATE.ACTION_SHIP_SUNK;

                return ACTION_STATE.ACTION_HIT;
            }

            // Else - miss
            return ACTION_STATE.ACTION_MISS;
        }


        // Method validating and placing ships on the appropriate board
        public ACTION_STATE PlaceShip(Ship.SHIP_TYPE ship, int _X, int _Y, Ship.DIRECTION dir, bool isPlayer)
        {
            GridPosition[,] board_Tmp; // Reusable temp board
            List<Ship> ships; // Reusable ships list
            Ship shipObject = new Ship(ship, dir, _X, _Y); // New Ship object

            // Variables used for incrementing directions
            int xOff = 0;
            int yOff = 0;

            // Set the temp board and add the ship to the relavent arrays (this is not TS but given this app is single threaded its fine)
            if (isPlayer)
            {
                board_Tmp = (GridPosition[,])board_Player.Clone();
                ships = playerShips;

            }
            else
            {
                board_Tmp = (GridPosition[,])board_Other.Clone();
                ships = otherShips;
            }

            ships.Add(shipObject);

            // Itterate over length of the selected ship in the correct direction checking the poition is valid, if invalid remove it from the list and return fail
            for (int i = 0; i < Ship.GetShipDimensions(ship); i++)
            {

                // Bounds checking and collision checking for a placed ship
                if (
                    !(_X + xOff >= 0 && _X + xOff < board_Player.GetLength(0)) ||
                    !(_Y + yOff >= 0 && _Y + yOff < board_Player.GetLength(1)) ||
                    !(board_Tmp[_X + xOff, _Y + yOff].ShipIndex == -1)
                    )
                {
                    // Remove the ship from the list and fail if collision with edge or other ship
                    ships.Remove(shipObject);

                    return ACTION_STATE.ACTION_FAIL;
                }

                // Set the relavent cells to contain the appropriate ship index
                board_Tmp[_X + xOff, _Y + yOff].ShipIndex = ships.Count - 1;


                // based on ship direction increment the appropriate axis
                switch (dir)
                {
                    case Ship.DIRECTION.UP:
                        yOff--;
                        break;
                    case Ship.DIRECTION.DOWN:
                        yOff++;
                        break;
                    case Ship.DIRECTION.LEFT:
                        xOff--;
                        break;
                    case Ship.DIRECTION.RIGHT:
                        xOff++;
                        break;
                }
            }

            // Update the gamewide boards
            if (isPlayer)
            {
                board_Player = (GridPosition[,])board_Tmp.Clone();
            }
            else
            {
                board_Other = (GridPosition[,])board_Tmp.Clone();
            }
            return ACTION_STATE.ACTION_SUCCESS;
        }

        // Get number of ships still to sink
        public int GetShipsToSink(bool isPlayer)
        {
            List<Ship> shipsList; // reusable ships list
            int counter = 0; // Ship Counter

            // Set the reusable list
            if (isPlayer)
                shipsList = playerShips;
            else
                shipsList = otherShips;

            // Itterate over still floating ships
            foreach (Ship s in shipsList)
                if (s.Hits < s.Length)
                    counter++;

            return counter;
        }

        // setting up the GridPositions and initialising them correctly
        public void InitialiseBoard()
        {
            PlayerMoves = 0;
            OtherMoves = 0;

            playerShips = new List<Ship>();
            otherShips = new List<Ship>();

            board_Player = new GridPosition[Board.SIZE, Board.SIZE];
            board_Other = new GridPosition[Board.SIZE, Board.SIZE];

            for (int _X = 0; _X < board_Player.GetLength(0); _X++)
            {
                for (int _Y = 0; _Y < board_Player.GetLength(1); _Y++)
                {
                    board_Player[_X, _Y] = new GridPosition();
                    board_Other[_X, _Y] = new GridPosition();
                }
            }
        }

    }
}