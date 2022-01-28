using System;
using System.Collections.Generic;

namespace PegGui
{
	public class Board
	{
		List<Tuple<Coord, Coord>> history = new List<Tuple<Coord, Coord>> { };

		// 10x10 Board object
		public Peg[,] board_Player = new Peg[10, 10];
		public Peg[,] board_PlayerHits = new Peg[10, 10];
		public Peg[,] board_Other = new Peg[10, 10];
		public Peg[,] board_OtherHits = new Peg[10, 10];

		public enum ACTION_STATE {
			ACTION_SUCCESS,
			ACTION_FAIL
		};

		public enum SHIP_TYPE {
			Carrier,
			Battleship,
			Destroyer,
			Submarine,
			Patrol_Boat
		};
		public enum DIRECTION {
			UP,
			DOWN,
			LEFT,
			RIGHT
		};


		public Board()
		{
		}

		public ACTION_STATE PlacePlayerShip(SHIP_TYPE ship, int _X, int _Y, DIRECTION dir)
        {

			return ACTION_STATE.ACTION_SUCCESS;
        }

	}

}