//// <copyright file="Dimensions.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Saves the dimensions, mostly used for background fields and so on.
//// </summary>
namespace Chess.Model.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Shared;

    /// <summary>
    /// Class for defining dimensions.
    /// </summary>
    public class Dimensions
    {
        /// <summary>
        /// Stores the x-value of the dimension.
        /// </summary>
        private int x;

        /// <summary>
        /// Stores the y-value of the dimension.
        /// </summary>
        private int y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimensions"/> class.
        /// </summary>
        public Dimensions()
        {
            Tuple<int, int> t = this.GetDimensionByArgs();
            this.X = t.Item1;
            this.Y = t.Item2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimensions"/> class.
        /// </summary>
        /// <param name="x">
        /// Y-axis for the dimension.
        /// </param>
        /// <param name="y">
        /// X-axis for the dimension.
        /// </param>
        public Dimensions(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the x-axis.
        /// </summary>
        /// <value>
        /// Gets or sets an integer.
        /// </value>
        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                if (value < 0)
                {
                    value = 8;
                }

                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the x-axis.
        /// </summary>
        /// <value>
        /// Gets or sets an integer.
        /// </value>
        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                if (value < 0)
                {
                    value = 8;
                }

                this.y = value;
            }
        }

        /// <summary>
        /// Gets the dimension by checking the command line arguments first.
        /// </summary>
        /// <returns>
        /// Returns a tuple of x and y dimensions.
        /// </returns>
        private Tuple<int, int> GetDimensionByArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 3)
            {
                int xDimension = 8;
                int yDimension = 8;
                if (args[1].ToLower() == "-size")
                {
                    string[] dimensions = args[2].ToLower().Split('x');

                    if (dimensions.Length == 2 && int.TryParse(dimensions[0], out xDimension) && int.TryParse(dimensions[1], out yDimension) && xDimension >= 8 && xDimension <= 26 && yDimension >= 8 && yDimension <= 26)
                    {
                        return Tuple.Create(xDimension, yDimension);
                    }
                }
            }

            return Tuple.Create(8, 8);
        }
    }
}
