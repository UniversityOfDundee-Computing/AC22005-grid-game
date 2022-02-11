using System;
using System.Collections.Generic;



namespace BattleShipGame
{

    public class AI
    {
        private bool foundAShip = false;
        private FoundShip foundShip;
        private readonly Random random;
        private readonly Board board;
        private DIFFICUILTY difficuilty;

        private Stack<Coord> queuedHits;
        public AI(Board board)
        {
            this.board = board;
            this.difficuilty = board.GameDificulty;
            this.random = new Random(); // so a new random object isnt created every time i need a random number
            this.queuedHits = new Stack<Coord>();
        }

        public enum DIFFICUILTY
        {
            EASY,
            MEDIUM,
            HARD,
            IMPOSSIBLE
        }

        public void Reset()
        {
            this.difficuilty = board.GameDificulty;
            this.foundAShip = false;
            this.foundShip = null;
            this.queuedHits = new Stack<Coord>();

        }

        public bool SetShips()
        {
            const bool IS_PLAYER = false;
            // for each ship
            for (int i = 0; i < Ship.DEFAULT_SHIPS.Length; i++)
            {

                Ship.SHIP_TYPE ship_type = Ship.DEFAULT_SHIPS[i];
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

        private Board.ACTION_STATE PlaceRandomPeg()
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

        public bool DoMove()
        {
            this.difficuilty = board.GameDificulty;
            const bool IS_PLAYER = false;
            if (difficuilty == DIFFICUILTY.IMPOSSIBLE) // joke difficuilty, always hit ship
            {
                // loop over board finding other players ships by reading board_Player array,
                // once found ship, hit it, if already hit continue iterating over board_Player
                // array untill an unhit ship is found
                for (int i = 0; i < Board.SIZE; i++)
                {
                    for (int j = 0; j < Board.SIZE; j++)
                    {
                        // other player ship found
                        if (board.board_Player[i, j].ShipIndex != -1)
                        {
                            // try to hit
                            if (board.PlacePeg(i, j, IS_PLAYER) != Board.ACTION_STATE.ACTION_FAIL)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else if (difficuilty == DIFFICUILTY.EASY) // random hits
            {
                PlaceRandomPeg();
            }
            else if (difficuilty == DIFFICUILTY.HARD) // random hits with trailing
            {
                if (foundAShip) // just hit a ship
                {
                    if (foundShip.hits.Count > 1)
                    {
                        Coord lastHit = foundShip.hits[foundShip.hits.Count - 1];
                        Coord direction = foundShip.direction;
                        int nextX = lastHit.x + direction.x;
                        int nextY = lastHit.y + direction.y;
                        if (!(nextX >= 0 && nextY >= 0 && nextX < Board.SIZE && nextY < Board.SIZE))
                        { // hit edge of board, flip direction and hit that side
                            foundShip.ReverseDirection();
                            nextX = lastHit.x + foundShip.direction.x;
                            nextY = lastHit.y + foundShip.direction.y;
                        }

                        Board.ACTION_STATE state = board.PlacePeg(nextX, nextY, IS_PLAYER);
                        if (state == Board.ACTION_STATE.ACTION_HIT)
                        {
                            foundShip.hits.Add(new Coord(nextX, nextY));
                        }
                        else if (state == Board.ACTION_STATE.ACTION_MISS) // reached end of ship
                        {
                            foundShip.ReverseDirection();
                        }
                        else if (state == Board.ACTION_STATE.ACTION_SHIP_SUNK)
                        {
                            foundAShip = false;
                            foundShip = null;
                        }
                    }
                    else
                    { // shoot around a ship to find the direction
                        if (foundShip.surroundingCoords == null)
                        {
                            foundShip.SetSurroundingCoords();
                        }
                        Coord surround = foundShip.surroundingCoords.Pop();
                        Board.ACTION_STATE state = board.PlacePeg(surround.x, surround.y, IS_PLAYER);

                        if (state == Board.ACTION_STATE.ACTION_HIT)
                        {
                            foundShip.surroundingCoords = null;
                            Coord lastHit = foundShip.GetLastHit();

                            Coord direction = new Coord((surround.x - lastHit.x), (surround.y - lastHit.y));
                            foundShip.direction = direction;
                        }
                    }
                }
                else // random shot
                {
                    bool pegPlaced = false;
                    Board.ACTION_STATE state = Board.ACTION_STATE.ACTION_FAIL;
                    int randX = -1;
                    int randY = -1;
                    while (!pegPlaced)
                    {
                        randX = random.Next(0, Board.SIZE);
                        randY = random.Next(0, Board.SIZE);
                        state = board.PlacePeg(randX, randY, IS_PLAYER);
                        if (state != Board.ACTION_STATE.ACTION_FAIL) // not placed ontop of ship
                        {
                            pegPlaced = true;
                        }
                    }
                    return state;
                    if (state == Board.ACTION_STATE.ACTION_HIT)
                    {
                        foundAShip = true;
                        foundShip = new FoundShip(randX, randY);
                    }
                }
            } else if (difficuilty == DIFFICUILTY.MEDIUM)
            {
                // place random peg, if it hits the next hits will be the 4 surrounding pegs
                Board.ACTION_STATE state = Board.ACTION_STATE.ACTION_FAIL;
                Coord placedCoord;
                while (state == Board.ACTION_STATE.ACTION_FAIL)
                {
                    if (queuedHits.Count() == 0)
                    {
                        int randX = random.Next(0, Board.SIZE);
                        int randY = random.Next(0, Board.SIZE);
                        placedCoord = new Coord(randX, randY);
                    } else
                    {
                        placedCoord = queuedHits.Pop();
                    }
                    state = board.PlacePeg(placedCoord.x, placedCoord.y, IS_PLAYER);

                    if (state == Board.ACTION_STATE.ACTION_HIT)
                    {
                        int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
                        for (int i = 0; i < 4; i++)
                        {
                            Coord newCoord = new Coord(lastHit.x + directions[i, 0], lastHit.y + directions[i, 1]);
                            if (newCoord.x >= 0 && newCoord.y >= 0 && newCoord.x < Board.SIZE && newCoord.y < Board.SIZE)
                            {
                                queuedHits.Push(newCoord);
                            }
                        }
                    }
                }
                
            }
            return true;
        }
        private class FoundShip
        {
            public List<Coord> hits;
            public Coord direction;
            public Stack<Coord> surroundingCoords;
            public FoundShip(int x, int y)
            {
                hits = new List<Coord>
                {
                    new Coord(x, y)
                };
            }

            // to flip direction 4[3][2][1][5][6]  -  [] is ship N is hits
            public void ReverseDirection()
            {
                direction.x *= -1;
                direction.y *= -1;
            }
            public Coord GetLastHit()
            {
                Coord lastHit = hits[hits.Count - 1];
                return lastHit;
            }
            public void SetSurroundingCoords()
            {
                surroundingCoords = new Stack<Coord>();
                int[,] directions = { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };
                Coord lastHit = GetLastHit();
                for (int i = 0; i < 4; i++)
                {
                    Coord newCoord = new Coord(lastHit.x + directions[i, 0], lastHit.y + directions[i, 1]);
                    if (newCoord.x >= 0 && newCoord.y >= 0 && newCoord.x < Board.SIZE && newCoord.y < Board.SIZE)
                    {
                        surroundingCoords.Push(newCoord);
                    }
                }
            }

        }

    }

}

