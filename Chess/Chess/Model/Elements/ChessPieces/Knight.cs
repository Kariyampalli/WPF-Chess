//// <copyright file="Knight.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for knight piece.
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
    /// Class for knight chess piece.
    /// </summary>
    public class Knight : ChessPiece
    {
        /// <summary>
        /// Stores list of positional values around the knight he could move to.
        /// </summary>
        private readonly List<Tuple<int, int>> positionValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="Knight"/> class.
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
        public Knight(ChessPieceTeam color, Positions bp, ChessBoardField[,] b, ChessGame g, Dimensions d) : base(color, bp, b, g, d, ChessPieceType.Knight)
        {
            this.positionValues = new List<Tuple<int, int>> 
            {
                Tuple.Create(1, 2),
                Tuple.Create(-1, 2),
                Tuple.Create(1, -2),
                Tuple.Create(-1, -2),
                Tuple.Create(2, 1),
                Tuple.Create(2, -1),
                Tuple.Create(-2, 1),
                Tuple.Create(-2, -1)
            };
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
            foreach (var values in this.positionValues)
            {
                Tuple<bool, bool> t = this.ValidateField(this.PositionOnBoard.XPosition + values.Item1, this.PositionOnBoard.YPosition + values.Item2, MovementType.MoveAndKill, avoidOwnColor);
                if (t.Item1)
                {
                    yield return this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1];
                }
            }
        }
    }
}
