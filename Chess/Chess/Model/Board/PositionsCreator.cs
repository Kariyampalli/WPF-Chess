//// <copyright file="PositionsCreator.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Creates background fields in a chessboard background design.
//// </summary>
namespace Chess.Model.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Shared;

    /// <summary>
    /// Class to create positions.
    /// </summary>
    public static class PositionsCreator
    {
        /// <summary>
        /// Creates positions.
        /// </summary>
        /// <param name="dimensions">
        /// Dimension used for the amount to create positions.
        /// </param>
        /// <returns>
        /// Return a list of positions.
        /// </returns>
        public static List<Positions> Create(Dimensions dimensions)
        {
            List<Positions> positions = new List<Positions>();
            for (int y = 0; y < dimensions.Y; y++)
            {
                for (int x = 0; x < dimensions.X; x++)
                {
                    positions.Add(new Positions(x, y));
                }
            }

            return positions;
        }
    }
}
