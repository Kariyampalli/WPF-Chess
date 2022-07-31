//// <copyright file="IVisitable.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Interface for visitors to be "accepted".
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Visitable interface for visitors.
    /// </summary>
    public interface IVisitable
    {
        /// <summary>
        /// Accepts the visitor and returns the bool value from performing the visitors visit.
        /// </summary>
        /// <param name="visitor">
        /// Returns the bool value from performing the visitors visit.
        /// </param>
        /// <returns>
        /// Returns a boolean value after performing the visitors visit.
        /// </returns>
        bool Accept(IVisitor visitor);
    }
}
