//// <copyright file="ImageConverter.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Converts chess piece team and type into coressponding image.
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
    using System.Windows.Media.Imaging;
    using Chess.Model.Elements.ChessPieces;

    /// <summary>
    /// Class that converts chess piece team and type into corresponding image.
    /// </summary>
    public class ImageConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts chess piece team and type into corresponding image.
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
        /// Return the chess piece image corresponding to its type and team.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                ChessPieceType pieceType = (ChessPieceType)values[0];
                ChessPieceTeam pieceTeam = (ChessPieceTeam)values[1];

                switch (pieceType)
                {
                    case ChessPieceType.King:
                        if (pieceTeam == ChessPieceTeam.White)
                        {
                            return new BitmapImage(new Uri(@"..\Images\kingWhite.png", UriKind.RelativeOrAbsolute));
                        }

                        return new BitmapImage(new Uri(@"..\Images\kingBlack.png", UriKind.RelativeOrAbsolute));
                    case ChessPieceType.Queen:
                        if (pieceTeam == ChessPieceTeam.White)
                        {
                            return new BitmapImage(new Uri(@"..\Images\queenWhite.png", UriKind.RelativeOrAbsolute));
                        }

                        return new BitmapImage(new Uri(@"..\Images\queenBlack.png", UriKind.RelativeOrAbsolute));
                    case ChessPieceType.Knight:
                        if (pieceTeam == ChessPieceTeam.White)
                        {
                            return new BitmapImage(new Uri(@"..\Images\horseWhite.png", UriKind.RelativeOrAbsolute));
                        }

                        return new BitmapImage(new Uri(@"..\Images\horseBlack.png", UriKind.RelativeOrAbsolute));
                    case ChessPieceType.Rook:
                        if (pieceTeam == ChessPieceTeam.White)
                        {
                            return new BitmapImage(new Uri(@"..\Images\rookWhite.png", UriKind.RelativeOrAbsolute));
                        }

                        return new BitmapImage(new Uri(@"..\Images\rookBlack.png", UriKind.RelativeOrAbsolute));
                    case ChessPieceType.Bishop:
                        if (pieceTeam == ChessPieceTeam.White)
                        {
                            return new BitmapImage(new Uri(@"..\Images\bishopWhite.png", UriKind.RelativeOrAbsolute));
                        }

                        return new BitmapImage(new Uri(@"..\Images\bishopBlack.png", UriKind.RelativeOrAbsolute));
                    case ChessPieceType.Pawn:
                        if (pieceTeam == ChessPieceTeam.White)
                        {
                            return new BitmapImage(new Uri(@"..\Images\pawnWhite.png", UriKind.RelativeOrAbsolute));
                        }

                        return new BitmapImage(new Uri(@"..\Images\pawnBlack.png", UriKind.RelativeOrAbsolute));
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts image back into type and team, but isn't needed.
        /// </summary>
        /// <param name="value">
        /// Value representing the image.
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
        /// Returns image, but isn't needed, thus it throws an exception.
        /// </returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}