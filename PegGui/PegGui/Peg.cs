using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    /**
     * Class that represents a single peg, this stores the state of a peg as well as it's coordinates.
     * 
     * **DEPRICATED**
     */
    public class Peg
    {
        Coord coors { get; set; }
        public enum Peg_State { EMPTY, SET, AVAILABLE, WALL };
        Peg_State state { get; set; }

        public Peg(Coord coors, Peg_State state)
        {
            this.coors = coors;
            this.state = state;
        }
    }
}
