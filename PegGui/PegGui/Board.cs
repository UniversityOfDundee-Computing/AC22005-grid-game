using System;
using System.Collections.Generic;

namespace BattleShipGame
{
	public class Board
	{ 
		public const int SIZE = 10;
		// 10x10 Board object¦¦¦¦¦¦¦¦¦¦¦¦
		public GridPosition[,] board_Player;
		public GridPosition[,] board_Other;

		public List<Ship> playerShips = new List<Ship>();
		public List<Ship> otherShips = new List<Ship>();

		public enum ACTION_STATE {
			ACTION_SUCCESS,
			ACTION_FAIL,
			ACTION_HIT,
			ACTION_MISS,
			ACTION_SHIP_SUNK
		};

		public Ship.SHIP_TYPE[] INITIAL_SHIPS =
		{
			Ship.SHIP_TYPE.Patrol_Boat,
			Ship.SHIP_TYPE.Patrol_Boat,
			Ship.SHIP_TYPE.Patrol_Boat,
			Ship.SHIP_TYPE.Patrol_Boat,
			Ship.SHIP_TYPE.Destroyer,
			Ship.SHIP_TYPE.Destroyer,
			Ship.SHIP_TYPE.Destroyer,
			Ship.SHIP_TYPE.Battleship,
			Ship.SHIP_TYPE.Battleship,
			Ship.SHIP_TYPE.Carrier
		};



		public Board()
		{
			board_Player = new GridPosition[10, 10];
			board_Other = new GridPosition[10, 10];

			for (int _X = 0; _X < board_Player.GetLength(0); _X++)
            {
				for (int _Y = 0; _Y < board_Player.GetLength(1); _Y++)
				{
					board_Player[_X, _Y] = new GridPosition();
					board_Other[_X, _Y] = new GridPosition();
				}
			}
		}

		public ACTION_STATE PlacePeg(int _X, int _Y, bool isPlayer)
        {
			if (!isPlayer)
            {
				if (board_Player[_X, _Y].hit)
					return ACTION_STATE.ACTION_FAIL;

				if (board_Player[_X,_Y].ShipIndex != -1)
                {
					board_Player[_X, _Y].hit = true;
					playerShips[board_Player[_X, _Y].ShipIndex].Hits++;
					if (playerShips[board_Player[_X, _Y].ShipIndex].Hits >= playerShips[board_Player[_X, _Y].ShipIndex].Length)
						return ACTION_STATE.ACTION_SHIP_SUNK;

					return ACTION_STATE.ACTION_HIT;
				}
            } else
            {
				if (board_Other[_X, _Y].hit)
					return ACTION_STATE.ACTION_FAIL;

				if (board_Other[_X, _Y].ShipIndex != -1)
				{
					board_Other[_X, _Y].hit = true;
					otherShips[board_Other[_X, _Y].ShipIndex].Hits++;
					if (otherShips[board_Other[_X, _Y].ShipIndex].Hits >= otherShips[board_Other[_X, _Y].ShipIndex].Length)
						return ACTION_STATE.ACTION_SHIP_SUNK;

					return ACTION_STATE.ACTION_HIT;
				}
			}
			return ACTION_STATE.ACTION_MISS;
        }


		public ACTION_STATE PlaceShip(Ship.SHIP_TYPE ship, int _X, int _Y, Ship.DIRECTION dir, bool isPlayer)
        {
			GridPosition[,] board_Tmp;
			if (isPlayer)
            {
				board_Tmp = board_Player;

				playerShips.Add(new Ship(ship, dir, _X, _Y));

			} else
            {
				board_Tmp = board_Other;

				otherShips.Add(new Ship(ship, dir, _X, _Y));
			}
			int xOff = 0;
			int yOff = 0;
			for (int i = 0; i < Ship.GetShipDimensions(ship); i++)
            {

				if ((_X + xOff>=0 && _X+xOff < board_Player.GetLength(1)) && (_Y + yOff >= 0 && _Y + yOff < board_Player.GetLength(0))  && board_Tmp[_X+xOff,_Y+yOff].ShipIndex != -1)
                {
					if (isPlayer)
					{
						playerShips.Remove(new Ship(ship, dir, _X, _Y));
					}
					else
					{
						otherShips.Remove(new Ship(ship, dir, _X, _Y));
					}
					return ACTION_STATE.ACTION_FAIL;
                }

				if (isPlayer)
				{
					board_Tmp[_X + xOff, _Y + yOff].ShipIndex = playerShips.Count - 1;
				}
				else
				{
					board_Tmp[_X + xOff, _Y + yOff].ShipIndex = otherShips.Count - 1;
				}

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

			if (isPlayer)
            {
				board_Player = board_Tmp;
            } else
            {
				board_Other = board_Tmp;
            }
			return ACTION_STATE.ACTION_SUCCESS;
        }

	}

}