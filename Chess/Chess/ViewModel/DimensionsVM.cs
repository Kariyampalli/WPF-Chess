//// <copyright file="DimensionsVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for dimensions class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Board;

    /// <summary>
    /// View model class for dimensions.
    /// </summary>
    public class DimensionsVM
    {
        /// <summary>
        /// Stores the dimension it represents.
        /// </summary>
        private readonly Dimensions dimensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DimensionsVM"/> class.
        /// </summary>
        /// <param name="d">
        /// The dimensions object to represent.
        /// </param>
        public DimensionsVM(Dimensions d)
        {
            this.dimensions = d;
        }

        /// <summary>
        /// Gets x-axis of the dimension.
        /// </summary>
        /// <value>
        /// Gets an integer.
        /// </value>
        public int X
        {
            get
            {
                return this.dimensions.X;
            }
        }

        /// <summary>
        /// Gets y-axis of the dimension.
        /// </summary>
        /// <value>
        /// Gets an integer.
        /// </value>
        public int Y
        {
            get
            {
                return this.dimensions.Y;
            }
        }
    }
}
