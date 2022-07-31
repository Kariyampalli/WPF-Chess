//// <copyright file="IVisitor.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Interface for the visitors.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Visitor interface for visitor pattern.
    /// </summary>
    public interface IVisitor
    {
        /// <summary>
        /// Visit a bool and get the parameter of the king.
        /// </summary>
        /// <param name="king">
        /// King chess piece. Visitor currently uses only king for check mate checking.
        /// </param>
        /// <returns>
        /// Returns a boolean value after performing the visit.
        /// </returns>
        bool Visit(King king);
    }
}
