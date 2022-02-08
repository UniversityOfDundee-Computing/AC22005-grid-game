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

		public SHIP_TYPE[] SHIPS =
		{
			SHIP_TYPE.Patrol_Boat,
			SHIP_TYPE.Patrol_Boat,
			SHIP_TYPE.Patrol_Boat,
			SHIP_TYPE.Patrol_Boat,
			SHIP_TYPE.Destroyer,
			SHIP_TYPE.Destroyer,
			SHIP_TYPE.Destroyer,
			SHIP_TYPE.Battleship,
			SHIP_TYPE.Battleship,
			SHIP_TYPE.Carrier
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
			this.Length = GetShipDimensions(t);

		}

		public static int GetShipDimensions(SHIP_TYPE t)
        {
			switch (t)
			{
				case SHIP_TYPE.Carrier:
					return 5;
				case SHIP_TYPE.Battleship:
					return 4;
				case SHIP_TYPE.Destroyer:
				case SHIP_TYPE.Submarine:
					return 3;
				case SHIP_TYPE.Patrol_Boat:
					return 2;
				default:
					throw new Exception("Unkonw Ship type - '" + Enum.GetName(typeof(SHIP_TYPE), t) + "'");
			}
		}
	}
}
