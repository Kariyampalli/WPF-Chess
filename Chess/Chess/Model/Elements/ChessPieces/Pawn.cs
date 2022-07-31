//// <copyright file="Pawn.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for pawn piece.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using Chess.Model.Board;
    using Chess.Model.Games;

    /// <summary>
    /// Class for pawn chess piece.
    /// </summary>
    public class Pawn : ChessPiece
    {
        /// <summary>
        /// Stores the factor which is value needed for pawn to know which direction to move.
        /// </summary>
        private readonly int factor;

        /// <summary>
        /// Stores values to be used to look if pawn can kill on certain field.
        /// </summary>
        private readonly List<Tuple<int, int>> positionValues;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pawn"/> class.
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
        public Pawn(ChessPieceTeam color, Positions bp, ChessBoardField[,] b, ChessGame g, Dimensions d) : base(color, bp, b, g, d, ChessPieceType.Pawn)
        {
            this.factor = 1;
            if (this.Team == ChessPieceTeam.White)
            {
                this.factor = -1;
            }

            this.positionValues = new List<Tuple<int, int>> 
            {
                Tuple.Create(1, this.factor),
                Tuple.Create(-1, this.factor)
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
            Tuple<bool, bool> t;
            for (int y = 1; y <= 1; y++)
            {
                t = this.ValidateField(this.PositionOnBoard.XPosition, this.PositionOnBoard.YPosition + (y * this.factor), MovementType.Move, avoidOwnColor);
                if (t.Item1)
                {
                    yield return this.Board[this.PositionOnBoard.YPosition + (y * this.factor), this.PositionOnBoard.XPosition];
                }

                if (!t.Item2)
                {
                    break;
                }
            }

            foreach (var values in this.positionValues)
            {
                t = this.ValidateField(this.PositionOnBoard.XPosition + values.Item1, this.PositionOnBoard.YPosition + values.Item2, MovementType.Kill, avoidOwnColor);
                if (t.Item1)
                {
                    yield return this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1];
                }
            }
        }

        /// <summary>
        /// Gets the critical fields for the enemies king.
        /// </summary>
        /// <returns>
        /// Returns critical fields.
        /// </returns>
        public override IEnumerable<ChessBoardField> GetCriticalFieldsForEnemyKing()
        {
            Tuple<bool, bool> t;
            foreach (var values in this.positionValues)
            {
                t = this.ValidateField(this.PositionOnBoard.XPosition + values.Item1, this.PositionOnBoard.YPosition + values.Item2, MovementType.MoveAndKill, false);
                if (t.Item1)
                {
                    yield return this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1];
                }
            }
        }
    }
}