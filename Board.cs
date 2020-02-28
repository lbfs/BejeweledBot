using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BejeweledBot
{
    class Board
    {
        Gem[,] _board = new Gem[8, 8];
        GametypeConfiguration _config;
        MovementEngine _movementEngine;
        IGameInterface _gameInterface;
        Point _screenLocation;
        public bool Playing { get; set; }
        public bool AutoResetGame { get; set; }

        private Board() { }

        public Board(IGameInterface gameInterface)
        {
            for (int x = 0; x < _board.GetLength(0); x++)
            {
                for (int y = 0; y < _board.GetLength(1); y++)
                {
                    _board[x, y] = new Gem(Gemtype.Unknown, x, y);
                }
            }

            this._config = new GametypeConfiguration(Gametype.Classic);
            this._gameInterface = gameInterface;
            this._screenLocation = gameInterface.FetchGameWindowCoordinates();
            this._movementEngine = new MovementEngine(_board);
            this.Playing = false;
            this.AutoResetGame = false;
        }

        public void ChangeGametype(Gametype gametype)
        {
            this._config = new GametypeConfiguration(gametype);
        }

        Point GetWindowCoordinates(Gem gem)
        {
            return new Point((gem.X * _config.BlockSize.Width) + _config.BoardLocation.X, (gem.Y * _config.BlockSize.Height) + _config.BoardLocation.Y);
        }

        private void ClassifyGems(Bitmap bitmap) // TODO: Refactor out graphics edit
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                foreach (Gem gem in _board)
                {
                    Point coordinates = GetWindowCoordinates(gem);
                    Color centerColor = bitmap.GetPixel(coordinates.X + (_config.BlockSize.Width / 2), coordinates.Y + (_config.BlockSize.Height / 2));
                    gem.Type = ColorClassifier.FindClosestTypeFromColor(centerColor);

                    using (SolidBrush brush = new SolidBrush(centerColor))
                    {
                        graphics.FillRectangle(brush, coordinates.X, coordinates.Y, _config.BlockSize.Width, _config.BlockSize.Height);
                    }

                }
            }
        }

        private void DrawMoves(Bitmap bitmap, List<Move> moves)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen blackPen = new Pen(Color.Black, 5))
                {
                    foreach (Move move in moves)
                    {
                        Point coordinates = GetWindowCoordinates(_board[move.A.X, move.A.Y]);
                        Point otherCoordinates = GetWindowCoordinates(_board[move.B.X, move.B.Y]);

                        coordinates.X += (_config.BlockSize.Height / 2);
                        coordinates.Y += (_config.BlockSize.Width / 2);

                        otherCoordinates.X += (_config.BlockSize.Height / 2);
                        otherCoordinates.Y += (_config.BlockSize.Width / 2);

                        graphics.DrawLine(blackPen, coordinates, otherCoordinates);
                    }
                }
            }
        }

        int moveShuffler = 0;
        public void ProcessGametick(Bitmap window)
        {
            if (!Playing) return;

            Color retrievedColor = window.GetPixel(_config.ResetGame.Location.X, _config.ResetGame.Location.Y);
            if (AutoResetGame && retrievedColor == _config.ResetGame.Color) // fix these comparisons to match
            {
                for (int i = 0; i < 10; i++)
                {
                    MouseOperations.SetCursorPosition(_screenLocation.X + _config.ResetGame.Location.X, _screenLocation.Y + _config.ResetGame.Location.Y);
                }

                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
            }


            retrievedColor = window.GetPixel(_config.ActiveGame.Location.X, _config.ActiveGame.Location.Y);
            if (retrievedColor != _config.ActiveGame.Color)
            {
                return;
            }

            ClassifyGems(window);
            List<Move> moves = _movementEngine.GenerateValidMoves();
            DrawMoves(window, moves);

            if (moves.Count > 0)
            {
                Move move = moves[moveShuffler % moves.Count];
                Point coordinates = GetWindowCoordinates(_board[move.A.X, move.A.Y]);
                Point otherCoordinates = GetWindowCoordinates(_board[move.B.X, move.B.Y]);

                coordinates.X += _screenLocation.X;
                coordinates.Y += _screenLocation.Y;

                otherCoordinates.X += _screenLocation.X;
                otherCoordinates.Y += _screenLocation.Y;

                coordinates.X += (_config.BlockSize.Height / 2);
                coordinates.Y += (_config.BlockSize.Width / 2);

                otherCoordinates.X += (_config.BlockSize.Height / 2);
                otherCoordinates.Y += (_config.BlockSize.Width / 2);

                MouseOperations.SetCursorPosition(coordinates.X, coordinates.Y);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);

                MouseOperations.SetCursorPosition(otherCoordinates.X, otherCoordinates.Y);
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);

                _gameInterface.PushGameMessage(String.Format("Performing Move: {0}", move.ToString()));
                moveShuffler += 1;
            }

            _gameInterface.UpdateGameImage(window);

            MouseOperations.SetCursorPosition(_screenLocation.X, _screenLocation.Y);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

    }


    
}
