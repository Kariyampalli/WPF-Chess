//// <copyright file="BackgroundFieldColorConverter.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Converts positions into background colors.
//// </summary>
namespace Chess.View.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// Converts positions into background colors in a chessboard stylish way.
    /// </summary>
    public class BackgroundFieldColorConverter : IMultiValueConverter
    {
        /// <summary>
        /// Stores a color to be converted into.
        /// </summary>
        private readonly SolidColorBrush color1;

        /// <summary>
        /// Stores a color to be converted into.
        /// </summary>
        private readonly SolidColorBrush color2;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundFieldColorConverter"/> class.
        /// </summary>
        public BackgroundFieldColorConverter()
        {
            this.color1 = Brushes.Gray;
            this.color2 = Brushes.White;
        }

        /// <summary>
        /// Converts received x- and y-position into corresponding color.
        /// </summary>
        /// <param name="values">
        /// Contains the positions.
        /// </param>
        /// <param name="targetType">
        /// Target type value.
        /// </param>
        /// <param name="parameter">
        /// Optional parameter.
        /// </param>
        /// <param name="culture">
        /// Culture info.
        /// </param>
        /// <returns>
        /// Returns a color to its corresponding position.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int y = 0;
            int x = 0;

            if (int.TryParse(values[0].ToString(), out y) && int.TryParse(values[1].ToString(), out x))
            {
                if (this.CheckIsColor1(x, y))
                {
                    return this.color1;
                }
                else
                {
                    return this.color2;
                }
            }

            return Brushes.Transparent;
        }

        /// <summary>
        /// Converts color back into positions, but isn't needed.
        /// </summary>
        /// <param name="value">
        /// Value representing the color.
        /// </param>
        /// <param name="targetTypes">
        /// Targets for the conversion.
        /// </param>
        /// <param name="parameter">
        /// Optional parameter.
        /// </param>
        /// <param name="culture">
        /// Culture info.
        /// </param>
        /// <returns>
        /// Return positions, but isn't needed, thus it throws an exception.
        /// </returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the positions match criteria to return the first color.
        /// </summary>
        /// <param name="x">
        /// Position on the x-axis.
        /// </param>
        /// <param name="y">
        /// Position on the y-axis.
        /// </param>
        /// <returns>
        /// Returns a boolean indicating whether the positions match the criteria to return the first color.
        /// </returns>
        private bool CheckIsColor1(int x, int y)
        {
            if (y == 0)
            {
                if (x == 0 || x % 2 == 0)
                {
                    return true;
                }
            }

            if (y % 2 != 0 && x % 2 != 0)
            {
                return true;
            }

            if (y % 2 == 0)
            {
                if (x % 2 == 0 || x == 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}