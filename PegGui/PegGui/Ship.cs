using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class Ship
    {
		public enum SHIP_TYPE
		{
			Carrier,
			Battleship,
			Destroyer,
			Submarine,
			Patrol_Boat
		};
		public enum DIRECTION
		{
			UP,
			DOWN,
			LEFT,
			RIGHT
		};

		public SHIP_TYPE Type { get; set; }
		public DIRECTION Direction { get; set; }
		public int _X { get; set; }
		public int _Y { get; set; }
		public int Length { get; set; }
		public int Hits { get; set; }

		public Ship(SHIP_TYPE t, DIRECTION d, int x, int y)
        {
			this.Type = t;
			this.Direction = d;
			this._X = x;
			this._Y = y;
			this.Hits = 0;

			switch (t)
            {
				case SHIP_TYPE.Carrier:
					this.Length = 5;
					break;
				case SHIP_TYPE.Battleship:
					this.Length = 4;
					break;
				case SHIP_TYPE.Destroyer:
				case SHIP_TYPE.Submarine:
					this.Length = 3;
					break;
				case SHIP_TYPE.Patrol_Boat:
					this.Length = 2;
					break;
            }
        }
	}
}
