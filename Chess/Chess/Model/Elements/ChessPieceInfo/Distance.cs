//// <copyright file="Distance.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Distances for the selected piece.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for distances.
    /// </summary>
    public class Distance
    {
        /// <summary>
        /// Stores the new distance on the x-axis.
        /// </summary>
        private double newXAxisDistance;

        /// <summary>
        /// Stores the new distance on the y-axis.
        /// </summary>
        private double newYAxisDistance;

        /// <summary>
        /// Stores the old distance on the x-axis.
        /// </summary>
        private double oldXAxisDistance;

        /// <summary>
        /// Stores the old distance on the y-axis.
        /// </summary>
        private double oldYAxisDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Distance"/> class.
        /// </summary>
        public Distance()
        {
            this.OldXAxisDistance = 0;
            this.OldYAxisDistance = 0;
            this.NewXAxisDistance = 0;
            this.NewYAxisDistance = 0;
        }

        /// <summary>
        /// Gets or sets the new distance on the x-axis.
        /// </summary>
        /// <value>
        /// Gets or sets an double.
        /// </value>
        public double NewXAxisDistance
        {
            get
            {
                return this.newXAxisDistance;
            }

            set
            {
                if (value != this.newXAxisDistance)
                {
                    this.newXAxisDistance = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the new distance on the y-axis.
        /// </summary>
        /// <value>
        /// Gets or sets an double.
        /// </value>
        public double NewYAxisDistance
        {
            get
            {
                return this.newYAxisDistance;
            }

            set
            {
                if (value != this.newYAxisDistance)
                {
                    this.newYAxisDistance = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the old distance on the x-axis.
        /// </summary>
        /// <value>
        /// Gets or sets an double.
        /// </value>
        public double OldXAxisDistance
        {
            get
            {
                return this.oldXAxisDistance;
            }

            set
            {
                if (value != this.oldXAxisDistance)
                {
                    this.oldXAxisDistance = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the old distance on the y-axis.
        /// </summary>
        /// <value>
        /// Gets or sets an double.
        /// </value>
        public double OldYAxisDistance
        {
            get
            {
                return this.oldYAxisDistance;
            }

            set
            {
                if (value != this.oldYAxisDistance)
                {
                    this.oldYAxisDistance = value;
                }
            }
        }
    }
}