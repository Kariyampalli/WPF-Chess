//// <copyright file="CheckMateVisitor.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for check mate visitor.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Check mate visitor to check for check mate.
    /// </summary>
    public class CheckMateVisitor : IVisitor
    {
        /// <summary>
        /// Visits checkmate of king.
        /// </summary>
        /// <param name="king">
        /// King to check check mate.
        /// </param>
        /// <returns>
        /// Returns a boolean indicating whether king has been checkmated.
        /// </returns>
        public bool Visit(King king)
        {
            return king.CheckForCheckMate();
        }
    }
}