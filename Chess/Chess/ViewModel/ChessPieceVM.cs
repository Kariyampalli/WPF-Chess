//// <copyright file="ChessPieceVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for chesspiece class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;

    /// <summary>
    /// Class for the chess piece view model.
    /// </summary>
    public class ChessPieceVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the chess piece.
        /// </summary>
        private readonly ChessPiece chessPiece;

        /// <summary>
        /// Stores the positions to move.
        /// </summary>
        private readonly DistanceVM movingDistance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessPieceVM"/> class.
        /// </summary>
        /// <param name="piece">
        /// The piece object to represent.
        /// </param>
        public ChessPieceVM(ChessPiece piece)
        {
            this.chessPiece = piece;
            this.chessPiece.OnMovingUpdate += this.UpdateOnMove;
            this.chessPiece.OnBeaten += this.UpdateOnBeaten;
            this.movingDistance = new DistanceVM(this.chessPiece.MovingDistance);
        }

        /// <summary>
        /// Stores an event to signal when a property changed its value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the type of the chess piece.
        /// </summary>
        /// <value>
        /// Gets an enumeration of type "ChessPieceType".
        /// </value>
        public ChessPieceType PieceType
        {
            get
            {
                return this.chessPiece.PieceType;
            }
        }

        /// <summary>
        /// Gets the chess piece team color.
        /// </summary>
        /// <value>
        /// Gets an enumeration of type "ChessPieceTeam".
        /// </value>
        public ChessPieceTeam PieceTeam
        {
            get
            {
                return this.chessPiece.Team;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the chess piece has been beaten.
        /// </summary>
        /// <value>
        /// Gets a boolean.
        /// </value>
        public bool Beaten
        {
            get
            {
                return this.chessPiece.Beaten;
            }
        }

        /// <summary>
        /// Gets the positions to move.
        /// </summary>
        /// <value>
        /// Gets a "MovingDistance" object.
        /// </value>
        public DistanceVM MovingDistance
        {
            get
            {
                return this.movingDistance;
            }
        }

        /// <summary>
        /// Raises an event to signal beaten property value has changed.
        /// </summary>
        private void UpdateOnBeaten()
        {
            this.FireOnPropertyChanged(nameof(this.Beaten));
        }

        /// <summary>
        /// Raises an event to signal chess piece has moved.
        /// </summary>
        private void UpdateOnMove()
        {
            this.FireOnPropertyChanged(nameof(this.MovingDistance));
        }

        /// <summary>
        /// Fires an event to signal a property value has changed.
        /// </summary>
        /// <param name="name">
        /// Name of the property to be updated.
        /// </param>
        private void FireOnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
