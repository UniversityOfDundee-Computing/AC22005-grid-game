using System;
using System.Collections.Generic;



namespace BattleShipGame
{
	public class AI
	{
		private Random random;
		private Board board;
		private DIFFICUILTY difficuilty;
		public AI(Board board, DIFFICUILTY difficuilty)
		{
			this.board = board;
			this.difficuilty = difficuilty;
			this.random = new Random(); // so a new random object isnt created every time i need a random number
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
					int randX = random.Next(0, Board.SIZE);
					int randY = random.Next(0, Board.SIZE);
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
			if (difficuilty == DIFFICUILTY.IMPOSSIBLE) // joke difficuilty, always hit ship
            {
				// loop over board finding other players ships by reading board_Player array,
				// once found ship, hit it, if already hit continue iterating over board_Player
				// array untill an unhit ship is found
				for (int i=0; i<Board.SIZE; i++)
                {
					for (int j=0; j<Board.SIZE; j++)
                    {
						// other player ship found
						if (board.board_Player[i,j].ShipIndex != -1)
                        {
							// try to hit
							if (board.PlacePeg(i,j, IS_PLAYER) != Board.ACTION_STATE.ACTION_FAIL)
                            {
								return true;
                            }
                        }
                    }
                }
            } else if (difficuilty == DIFFICUILTY.EASY) // random hits
            {
				bool pegPlaced = false;
				while (!pegPlaced)
                {
					int randX = random.Next(0, Board.SIZE);
					int randY = random.Next(0, Board.SIZE);
					if (board.PlacePeg(randX, randY, IS_PLAYER) != Board.ACTION_STATE.ACTION_FAIL)
                    {
						pegPlaced = true;
                    }
                }
				return true;
            } else if (difficuilty == DIFFICUILTY.MEDIUM) // random hits with trailing
			{

            }
			return false;

        }

	}

	
}

