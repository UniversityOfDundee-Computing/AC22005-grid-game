using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public struct GridPosition
    {
        // Stores a Grid Position containing a ship index or -1 for no ship as well as weather or not a position has been hit.
        int? shipIndex;
        public int ShipIndex { get { return shipIndex ?? -1; } set { shipIndex = value; } }
        public bool Hit { get; set; }
    }
}
