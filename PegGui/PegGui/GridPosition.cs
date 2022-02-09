﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class GridPosition
    {
        public int ShipIndex { get; set; }
        public bool Hit { get; set; }

        public GridPosition()
        {
            Hit = false;
            ShipIndex = -1;
        }
    }
}
