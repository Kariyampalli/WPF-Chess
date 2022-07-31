//// <copyright file="Positions.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Position on the chessboard.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for Positions.
    /// </summary>
    public class Positions
    {
        /// <summary>
        /// Stores x-position.
        /// </summary>
        private int xPosition;

        /// <summary>
        /// Stores y-position.
        /// </summary>
        private int yPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Positions"/> class.
        /// </summary>
        /// <param name="x">
        /// Position on the x-axis.
        /// </param>
        /// <param name="y">
        /// Position on the y-axis.
        /// </param>
        public Positions(int x, int y)
        {
            this.XPosition = x;
            this.YPosition = y;
        }

        /// <summary>
        /// Gets or sets x-position.
        /// </summary>
        /// <value>
        /// Gets or set a integer.
        /// </value>
        public int XPosition
        {
            get
            {
                return this.xPosition;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("X-position was tryied to be set to a negative number!");
                }

                this.xPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets y-position.
        /// </summary>
        /// <value>
        /// Gets or set a integer.
        /// </value>
        public int YPosition
        {
            get
            {
                return this.yPosition;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Y-position was tryied to be set to a negative number!");
                }

                this.yPosition = value;
            }
        }
    }
}