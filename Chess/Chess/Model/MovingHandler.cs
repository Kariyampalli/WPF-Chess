//// <copyright file="MovingHandler.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Handles "moving", setting right coordinates and calculating distances for chess pieces.
//// </summary>
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;

    /// <summary>
    /// Class to handles "moving" a chess piece to another field and calculates its distance (1:1).
    /// </summary>
    public static class MovingHandler
    {
        /// <summary>
        /// Handles the "moving" for a chess piece to another field.
        /// </summary>
        /// <param name="selection">
        /// Selected field, containing the chess piece.
        /// </param>
        /// <param name="target">
        /// Targeted field.
        /// </param>
        public static void Move(ChessBoardField selection, ChessBoardField target)
        {
            Tuple<Positions, double, double> valuesForPiece = GetNewValues(selection, target);

            if (target.Piece != null)
            {
                ChessPiece targetPiece = target.Piece;
                targetPiece.Beaten = true;
            }

            SetValues(selection, valuesForPiece, target);
        }

        /// <summary>
        /// Set the position and moving distance to the right values.
        /// </summary>
        /// <param name="selection">
        /// Selected field.
        /// </param>
        /// <param name="newValues">
        /// The new value to set to.
        /// </param>
        /// <param name="target">
        /// Target field.
        /// </param>
        private static void SetValues(ChessBoardField selection, Tuple<Positions, double, double> newValues, ChessBoardField target)
        {
            ChessPiece selectedPiece = selection.Piece;
            selectedPiece.MovingDistance.OldXAxisDistance = selectedPiece.MovingDistance.NewXAxisDistance;
            selectedPiece.MovingDistance.OldYAxisDistance = selectedPiece.MovingDistance.NewYAxisDistance;
            selectedPiece.MovingDistance.NewXAxisDistance += newValues.Item2;
            selectedPiece.MovingDistance.NewYAxisDistance += newValues.Item3;
            selectedPiece.PositionOnBoard.XPosition = target.PositionOnBoard.XPosition;
            selectedPiece.PositionOnBoard.YPosition = target.PositionOnBoard.YPosition;
            selectedPiece.FireOnMovingUpdate();

            target.Piece = selection.Piece;
            selection.Piece = null;

            selection.State = FieldState.Not_Selected;
            selection.IsSelectingAllowed = false;
            selection.FireOnSelectionUpdate();
        }

        /// <summary>
        /// Gets the moving distances.
        /// </summary>
        /// <param name="from">
        /// What field to move from.
        /// </param>
        /// <param name="to">
        /// What field to move to.
        /// </param>
        /// <returns>
        /// Returns a tuple with the the position of the target field and the x and y distance.
        /// </returns>
        private static Tuple<Positions, double, double> GetNewValues(ChessBoardField from, ChessBoardField to)
        {
            double yDistance = CalculateMovingValue(from.PositionOnBoard.YPosition, to.PositionOnBoard.YPosition);
            double xDistance = CalculateMovingValue(from.PositionOnBoard.XPosition, to.PositionOnBoard.XPosition);

            return Tuple.Create(new Positions(to.PositionOnBoard.XPosition, to.PositionOnBoard.YPosition), xDistance, yDistance);
        }

        /// <summary>
        /// Calculates the distance values from one position on a axis to another position on the same axis.
        /// </summary>
        /// <param name="posFrom">
        /// Position to move from.
        /// </param>
        /// <param name="posTo">
        /// Position to move to.
        /// </param>
        /// <returns>
        /// Returns a double with the moving amount.
        /// </returns>
        private static double CalculateMovingValue(int posFrom, int posTo)
        {
            int factor = -1;
            int amount = 0;
            if (posFrom < posTo)
            {
                factor = 1;
            }

            int abs = Math.Abs(posFrom - posTo);
            for (int i = 0; i < abs; i++)
            {
                amount += 1 * factor;
            }

            return amount;
        }
    }
}