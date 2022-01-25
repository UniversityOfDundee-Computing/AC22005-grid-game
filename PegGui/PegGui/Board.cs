using System;
using System.Collections.Generic;

namespace PegGui
{
	public class Board
	{
		List<Tuple<Coord, Coord>> history = new List<Tuple<Coord, Coord>> { };
		public Peg[,] board = new Peg[9, 9];

		public Board()
		{
		}

		public Board(Peg[,] grid)
		{
			board = grid;
		}
	}

}