//// <copyright file="MovingConverter.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class converts the distances into valuable distances multiplying it with the size of the chess piece.
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

    /// <summary>
    /// Converts the distances into valuable distances multiplying it with the size of the chess piece.
    /// </summary>
    public class MovingConverter : IValueConverter
    {
        /// <summary>
        /// Converts the distances into valuable distances multiplying it with the size of the chess piece.
        /// </summary>
        /// <param name="value">
        /// Distance value of an axis.
        /// </param>
        /// <param name="targetType">
        /// Targeted type.
        /// </param>
        /// <param name="parameter">
        /// Optional parameter.
        /// </param>
        /// <param name="culture">
        /// Culture info.
        /// </param>
        /// <returns>
        /// Returns a valuable distance by matching it value with the chess piece size.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double distance = 0;
            double.TryParse(value.ToString(), out distance);

            return distance * 50;
        }

        /// <summary>
        /// Converts it back to a value (Not used).
        /// </summary>
        /// <param name="value">
        /// Distance value.
        /// </param>
        /// <param name="targetType">
        /// Target type.
        /// </param>
        /// <param name="parameter">
        /// Optional parameter.
        /// </param>
        /// <param name="culture">
        /// Culture info.
        /// </param>
        /// <returns>
        /// Returns nothing. Throws an exception.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
