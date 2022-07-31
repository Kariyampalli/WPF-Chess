//// <copyright file="ChessPieceType.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Type for the chesspiece classes.
//// </summary>

namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enumeration for chess piece types.
    /// </summary>
    public enum ChessPieceType
    {
        /// <summary>
        /// Chess piece is type of king.
        /// </summary>
        King,

        /// <summary>
        /// Chess piece is type of queen.
        /// </summary>
        Queen,

        /// <summary>
        /// Chess piece is type of knight.
        /// </summary>
        Knight,

        /// <summary>
        /// Chess piece is type of rook.
        /// </summary>
        Rook,

        /// <summary>
        /// Chess piece is type of bishop.
        /// </summary>
        Bishop,

        /// <summary>
        /// Chess piece is type of pawn.
        /// </summary>
        Pawn
    }
}
