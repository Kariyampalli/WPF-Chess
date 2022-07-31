//// <copyright file="FieldState.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Used to represent the state of a chessboard field.
//// </summary>
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enumeration for the state of a chess board field.
    /// </summary>
    public enum FieldState
    {
        /// <summary>
        /// Field has been selected.
        /// </summary>
        Selected,

        /// <summary>
        /// Field has not been selected.
        /// </summary>
        Not_Selected,

        /// <summary>
        /// Field is in danger.
        /// </summary>
        Danger
    }
}
