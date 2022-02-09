using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class GridPosition
    {
        // Stores a Grid Position containing a ship index or -1 for no ship as well as weather or not a position has been hit.
        public int ShipIndex { get; set; }
        public bool hit { get; set; }

        public GridPosition()
        {
            hit = false;
            ShipIndex = -1;
        }
    }
}
