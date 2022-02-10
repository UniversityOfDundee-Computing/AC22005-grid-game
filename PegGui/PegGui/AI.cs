using System;
using System.Collections.Generic;



namespace BattleShipGame
{
	

	public class AI
	{
		private bool foundAShip = false;
		private FoundShip foundShip;
		private Random random;
		private Board board;
		private DIFFICUILTY difficuilty;
		private List<Coord> shots = new List<Coord>(); 
		public AI(Board board)
		{
			this.board = board;
			this.difficuilty = board.GameDificulty;
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

		private Board.ACTION_STATE placeRandomPeg()
        {
			bool IS_PLAYER = false;
			bool pegPlaced = false;
			Board.ACTION_STATE state = Board.ACTION_STATE.ACTION_FAIL;
			while (!pegPlaced)
            {
				int randX = random.Next(0, Board.SIZE);
				int randY = random.Next(0, Board.SIZE);
				state = board.PlacePeg(randX, randY, IS_PLAYER);
				if (state != Board.ACTION_STATE.ACTION_FAIL) // not placed ontop of ship
                {
					pegPlaced = true;
                }
            }
			return state;
        }

		public bool doMove()
        {
			this.difficuilty = board.GameDificulty;
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
				placeRandomPeg();
            } else if (difficuilty == DIFFICUILTY.MEDIUM) // random hits with trailing
			{
				if (foundAShip) // just hit a ship
                {
					if (foundShip.foundDirection)
                    {
						Coord lastHit = foundShip.hits[foundShip.hits.Count -1];
						Coord direction = foundShip.direction;
						int nextX = lastHit.x + direction.x;
						int nextY = lastHit.y + direction.y;
						if (!(nextX >= 0 && nextY >= 0 && nextX < Board.SIZE && nextY < Board.SIZE))
                        { // hit edge of board, flip direction and hit that side
							foundShip.reverseDirection();
							nextX = lastHit.x + foundShip.direction.x;
							nextY = lastHit.y + foundShip.direction.y;
                        }

						Board.ACTION_STATE state = board.PlacePeg(nextX, nextY, IS_PLAYER);
						if (state == Board.ACTION_STATE.ACTION_HIT)
                        {
							foundShip.hits.Add(new Coord(nextX, nextY));
                        } else if (state == Board.ACTION_STATE.ACTION_MISS) // reached end of ship
                        {
							foundShip.reverseDirection();
                        } else if (state == Board.ACTION_STATE.ACTION_SHIP_SUNK)
                        {
							foundAShip = false;
							foundShip = null;
                        }
                    }
                } else // random shot
                {
					Board.ACTION_STATE state = placeRandomPeg();
					if (state == Board.ACTION_STATE.ACTION_HIT)
                    {
						foundAShip = true;
                    }
                }
            }
			return true;
        }
		private class FoundShip
		{
			public List<Coord> hits;
			public Coord direction;
			public bool foundDirection;
			public bool hitEnd; // to flip direction 4[3][2][1][5][6]  -  [] is ship N is hits
			public FoundShip(int x, int y)
			{
				hits = new List<Coord>();
				hits.Add(new Coord(x, y));
				hitEnd = false;
				foundDirection = false;
			}
			public void reverseDirection()
            {
				direction.x *= -1;
				direction.y *= -1;
            }
		}

	}

}

