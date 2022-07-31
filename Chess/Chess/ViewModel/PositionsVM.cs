//// <copyright file="PositionsVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for position.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Elements.ChessPieces;

    /// <summary>
    /// Class for positions.
    /// </summary>
    public class PositionsVM
    {
        /// <summary>
        /// Stores the position it represents.
        /// </summary>
        private readonly Positions position;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionsVM"/> class.
        /// </summary>
        /// <param name="bp">
        /// The position object to be represented.
        /// </param>
        public PositionsVM(Positions bp)
        {
            this.position = bp;
        }

        /// <summary>
        /// Gets the position on the x-axis.
        /// </summary>
        /// <value>
        /// Gets an integer.
        /// </value>
        public int XPosition
        {
            get
            {
                return this.position.XPosition;
            }
        }

        /// <summary>
        /// Gets the position on the y-axis.
        /// </summary>
        /// <value>
        /// Gets an integer.
        /// </value>
        public int YPosition
        {
            get
            {
                return this.position.YPosition;
            }
        }
    }
}
