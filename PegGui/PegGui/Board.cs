using System;
using System.Collections.Generic;

namespace BattleShipGame
{
	public class Board
	{
		List<Tuple<Coord, Coord>> history = new List<Tuple<Coord, Coord>> { };

		// 10x10 Board object
		public GridPosition[,] board_Player = new GridPosition[10, 10];
		public GridPosition[,] board_Other = new GridPosition[10, 10];

		public List<Ship> playerShips = new List<Ship>();
		public List<Ship> otherShips = new List<Ship>();

		public enum ACTION_STATE {
			ACTION_SUCCESS,
			ACTION_FAIL
		};

		


		public Board()
		{
		}

		public ACTION_STATE PlacePeg(int _X, int _Y, bool isPlayer)
        {
			return ACTION_STATE.ACTION_SUCCESS;
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