using System;

namespace BattleShipGame
{
	public class AI
	{
		public AI()
		{
		}

		public void setShips(Board board)
		{
			cosnt bool IS_PLAYER = false;
			Random random = new Random();
			// for each ship
			for (int i = 0; i < board.INITIAL_SHIPS.Length; i++)
            {

				Ship.SHIP_TYPE ship_type = board.INITIAL_SHIPS[i];
				// get random direction
				Ship.DIRECTION randDir = (Ship.DIRECTION)random.Next(0, 4);
				bool placed = false;
				// get random coords try to place, if not fits, try again
				while (!placed)
                {
					int randX = random.Next(0, 9);
					int randY = random.Next(0, 9);
					board.ACTION_STATE res = board.PlaceShip(ship_type, randX, randY, randDir, IS_PLAYER);
					if (res == board.ACTION_STATE.SUCCESS)
                    {
						placed = true;
                    }
                }

            }

		}

	}

	
}

