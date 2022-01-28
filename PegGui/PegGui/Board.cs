using System;
using System.Collections.Generic;

namespace BattleShipGame
{
	public class Board
	{
		List<Tuple<Coord, Coord>> history = new List<Tuple<Coord, Coord>> { };

		// 10x10 Board object
		public Peg[,] board_Player = new Peg[10, 10];
		public Peg[,] board_PlayerHits = new Peg[10, 10];
		public Peg[,] board_Other = new Peg[10, 10];
		public Peg[,] board_OtherHits = new Peg[10, 10];

		public Ship[] playerShips = new Ship[10];
		public Ship[] otherShips = new Ship[10];

		public enum ACTION_STATE {
			ACTION_SUCCESS,
			ACTION_FAIL
		};

		


		public Board()
		{
		}

		public ACTION_STATE PlacePlayerShip(Ship.SHIP_TYPE ship, int _X, int _Y, Ship.DIRECTION dir)
        {

			return ACTION_STATE.ACTION_SUCCESS;
        }

	}

}