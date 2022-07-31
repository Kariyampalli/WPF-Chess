//// <copyright file="Rook.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for rook piece.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Board;
    using Chess.Model.Games;

    /// <summary>
    /// Class for queen chess piece.
    /// </summary>
    public class Rook : ChessPiece
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rook"/> class.
        /// </summary>
        /// <param name="color">
        /// Chess piece color it represents.
        /// </param>
        /// <param name="bp">
        /// Chess piece position on the board.
        /// </param>
        /// <param name="b">
        ///  Represents the chessboard as a two dimensional array.
        /// </param>
        /// <param name="g">
        /// The current chess game.
        /// </param>
        /// <param name="d">
        /// Dimensions of the chessboard.
        /// </param>
        public Rook(ChessPieceTeam color, Positions bp, ChessBoardField[,] b, ChessGame g, Dimensions d) : base(color, bp, b, g, d, ChessPieceType.Rook)
        {
        }

        /// <summary>
        /// Gets the fields that can be selected.
        /// </summary>
        /// <param name="avoidOwnColor">
        /// If chess pieces from its own team should be avoided, if not, then it will be selected but the direction to be searched won't be continued (Need as well for king piece).
        /// </param>
        /// <returns>
        /// Returns selectable fields.
        /// </returns>
        public override IEnumerable<ChessBoardField> GetSelectableFields(bool avoidOwnColor)
        {
            Tuple<bool, bool> t;
            bool searchRight = true;
            bool searchLeft = true;
            bool searchDown = true;
            bool searchUp = true;
            bool keepSearching = true;

            int x = 0;
            int y = 0;
            while (keepSearching)
            {
                if (searchRight && x != 0)
                {
                    t = this.ValidateField(this.PositionOnBoard.XPosition + x, this.PositionOnBoard.YPosition, MovementType.MoveAndKill, avoidOwnColor);
                    if (t.Item1)
                    {
                        yield return this.Board[this.PositionOnBoard.YPosition, this.PositionOnBoard.XPosition + x];
                    }

                    if (!t.Item2)
                    {
                        searchRight = false;
                    }
                }

                if (searchLeft && x != 0)
                {
                    t = this.ValidateField(this.PositionOnBoard.XPosition - x, this.PositionOnBoard.YPosition, MovementType.MoveAndKill, avoidOwnColor);
                    if (t.Item1)
                    {
                        yield return this.Board[this.PositionOnBoard.YPosition, this.PositionOnBoard.XPosition - x];
                    }

                    if (!t.Item2)
                    {
                        searchLeft = false;
                    }
                }

                if (searchUp && y != 0)
                {
                    t = this.ValidateField(this.PositionOnBoard.XPosition, this.PositionOnBoard.YPosition - y, MovementType.MoveAndKill, avoidOwnColor);
                    if (t.Item1)
                    {
                        yield return this.Board[this.PositionOnBoard.YPosition - y, this.PositionOnBoard.XPosition];
                    }

                    if (!t.Item2)
                    {
                        searchUp = false;
                    }
                }

                if (searchDown && x != 0)
                {
                    t = this.ValidateField(this.PositionOnBoard.XPosition, this.PositionOnBoard.YPosition + y, MovementType.MoveAndKill, avoidOwnColor);
                    if (t.Item1)
                    {
                        yield return this.Board[this.PositionOnBoard.YPosition + y, this.PositionOnBoard.XPosition];
                    }

                    if (!t.Item2)
                    {
                        searchDown = false;
                    }
                }

                x++;
                y++;
                if (x > this.ChessBoardDimension.X && y > this.ChessBoardDimension.Y)
                {
                    keepSearching = false;
                }
                else
                {
                    keepSearching =
                searchRight ||
                searchLeft ||
                searchDown ||
                searchUp;
                }
            }
        }
    }
}