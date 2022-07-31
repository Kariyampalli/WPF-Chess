//// <copyright file="MovementType.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Used to get the fields and pieces a piece can kill/move to.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enumeration for the movement type of a chess piece.
    /// </summary>
    public enum MovementType
    {
        /// <summary>
        /// Allowed to move and kill on move.
        /// </summary>
        MoveAndKill,

        /// <summary>
        /// Only allowed to kill on move.
        /// </summary>
        Kill,

        /// <summary>
        /// Only allowed to move on move.
        /// </summary>
        Move
    }
}
