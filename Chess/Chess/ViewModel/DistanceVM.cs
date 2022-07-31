//// <copyright file="DistanceVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for the moving position class.
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
    /// View model for distances.
    /// </summary>
    public class DistanceVM
    {
        /// <summary>
        /// Distance it represents.
        /// </summary>
        private readonly Distance distance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceVM"/> class.
        /// </summary>
        /// <param name="d">
        /// The moving position object to represent.
        /// </param>
        public DistanceVM(Distance d)
        {
            this.distance = d;
        }

        /// <summary>
        /// Gets the old distance on the x-axis.
        /// </summary>
        /// <value>
        /// Gets a double.
        /// </value>
        public double OldXAxisDistance
        {
            get
            {
                return this.distance.OldXAxisDistance;
            }
        }

        /// <summary>
        /// Gets the old distance on the y-axis.
        /// </summary>
        /// <value>
        /// Gets a double.
        /// </value>
        public double OldYAxisDistance
        {
            get
            {
                return this.distance.OldYAxisDistance;
            }
        }

        /// <summary>
        ///  Gets the new distance on the x-axis.
        /// </summary>
        /// <value>
        /// Gets a double.
        /// </value>
        public double NewXAxisDistance
        {
            get
            {
                return this.distance.NewXAxisDistance;
            }
        }

        /// <summary>
        ///  Gets the old distance on the y-axis.
        /// </summary>
        /// <value>
        /// Gets a double.
        /// </value>
        public double NewYAxisDistance
        {
            get
            {
                return this.distance.NewYAxisDistance;
            }
        }
    }
}