using System;
using System.Collections.Generic;



namespace BattleShipGame
{
	public class AI
	{
		private Board board;
		private DIFFICUILTY difficuilty;
		private List<Coord> shots = new List<Coord>(); 
		public AI(Board board, DIFFICUILTY difficuilty)
		{
			this.board = board;
			this.difficuilty = difficuilty;
		}
		public enum DIFFICUILTY
        {
			EASY,
			MEDIUM,
			HARD,
			IMPOSSIBLE
        }

		public bool setShips()
		{
			const bool IS_PLAYER = false;
			Random random = new Random();
			// for each ship
			for (int i = 0; i < board.INITIAL_SHIPS.Length; i++)
            {

				Ship.SHIP_TYPE ship_type = board.INITIAL_SHIPS[i];
				// get random direction
				Ship.DIRECTION randDir = (Ship.DIRECTION)random.Next(0, 4);
				bool placed = false;
				// get random coords, try to place, if not fits, try again
				while (!placed)
                {
					int randX = random.Next(0, 10);
					int randY = random.Next(0, 10);
					Board.ACTION_STATE res = board.PlaceShip(ship_type, randX, randY, randDir, IS_PLAYER);
					if (res == Board.ACTION_STATE.ACTION_SUCCESS)
                    {
						placed = true;
                    }
                }

            }
			return true;

		}

		public bool doMove()
        {
			const bool IS_PLAYER = false;
			if (difficuilty == DIFFICUILTY.IMPOSSIBLE) // joke difficuilty
            {
				for (int i=0; i<board.SIZE; i++)
                {
					for (int j=0; j<board.SIZE; j++)
                    {
						if (board.board_player[i][j].ShipIndex != -1)
                        {
							if (!shots.Contains(new Coord(i,j)))
                            {
								board.PlacePeg(i,j, IS_PLAYER);
								return true;
                            }
                        }
                    }
                }
            } else
            {
				///
            }
			return false;

        }

	}

	
}

