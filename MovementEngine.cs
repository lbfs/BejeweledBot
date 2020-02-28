using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BejeweledBot
{
    class MoveUnit
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public MoveUnit(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(MoveUnit A, MoveUnit B)
        {
            if (A.X == B.X && A.Y == B.Y) return true;
            return false;
        }

        public static bool operator !=(MoveUnit A, MoveUnit B)
        {
            if (A.X == B.X && A.Y == B.Y) return false;
            return true;
        }
    }

    class Move
    {
        public MoveUnit A { get; private set; }
        public MoveUnit B { get; private set; }

        public Move(MoveUnit a, MoveUnit b)
        {
            this.A = a;
            this.B = b;
        }

        public bool IsIdenticalMove(Move move)
        {
            if ((this.A == move.A && this.B == move.B) || (this.A == move.B && this.B == move.A)) return true;
            return false;
        }

        public override string ToString()
        {
            return String.Format("({0},{1}) -> ({2}, {3})", this.A.X, this.A.Y, this.B.X, this.B.Y);
        }
    }

    class MovementEngine
    {
        List<Move> _validMoves = new List<Move>();
        List<Move> _potentialMoves;
        Gem[,] _board;
        Move previousMove = null;

        public MovementEngine(Gem[,] board)
        {
            this._board = board;
            this._potentialMoves = PregeneratePotentialMoves(this._board);
        }

        private List<Move> PregeneratePotentialMoves(Gem[,] board)
        {
            List<Move> potentialMoves = new List<Move>();
            int width = board.GetLength(0);
            int height = board.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x + 1 < width)
                    {
                        potentialMoves.Add(new Move(new MoveUnit(x, y), new MoveUnit(x + 1, y)));
                    }

                    if (y + 1 < height)
                    {
                        potentialMoves.Add(new Move(new MoveUnit(x, y), new MoveUnit(x, y + 1)));
                    }
                }
            }
            return potentialMoves;
        }

        //Refactor out or change logic, should not rely on Gem
        private bool SwapMove(MoveUnit a, MoveUnit b)
        {
            Gem gem = _board[a.X, a.Y];
            Gem otherGem = _board[b.X, b.Y];

            if (otherGem.Type == Gemtype.Unknown || gem.Type == Gemtype.Unknown)
                return false;

            int count = 0;
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                if (otherGem.X == x)
                {
                    count += 1;
                }
                else if (gem.X == x)
                {
                    count = 0;
                }
                else if (_board[x, otherGem.Y].Type == gem.Type)
                {
                    count += 1;
                }
                else
                {
                    count = 0;
                }

                if (count >= 3)
                {
                    return true;
                }
            }

            count = 0;
            for (int y = 0; y < _board.GetLength(1); y++)
            {
                if (otherGem.Y == y)
                {
                    count += 1;
                }
                else if (gem.Y == y)
                {
                    count = 0;
                }
                else if (_board[otherGem.X, y].Type == gem.Type)
                {
                    count += 1;
                }
                else
                {
                    count = 0;
                }

                if (count >= 3)
                {
                    return true;
                }
            }

            return false;
        }

        private bool TestValidMove(Move move)
        {
            if (SwapMove(move.A, move.B)) return true;
            if (SwapMove(move.B, move.A)) return true;
            return false;
        }

        List<Move> _previousMoves = new List<Move>();
        List<Move> _copyList = new List<Move>();
        public List<Move> GenerateValidMoves()
        {
            _validMoves.Clear();

            if (_previousMoves.Count > 0)
            {
                foreach (Move move in _previousMoves)
                {
                    if (TestValidMove(move)) _validMoves.Add(move);
                }
            }

            if (_previousMoves.Count <= 2)
            {
                _validMoves.Clear();
                foreach (Move move in _potentialMoves)
                {
                    if (TestValidMove(move)) _validMoves.Add(move);
                }
            }

            _previousMoves = new List<Move>(_validMoves);

            _copyList.Clear();
            _copyList.AddRange(_validMoves);
            List<Move> temp = _previousMoves;
            _previousMoves = _copyList;
            _copyList = temp;
            return _validMoves;

        }
    }
}
