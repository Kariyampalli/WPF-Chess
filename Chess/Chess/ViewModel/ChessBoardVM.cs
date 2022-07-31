//// <copyright file="ChessBoardVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for chessboard class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model;
    using Chess.Model.Board;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;

    using Chess.Shared;

    /// <summary>
    /// View model class for the chess board.
    /// </summary>
    public class ChessBoardVM
    {
        /// <summary>
        /// Stores the chess board model.
        /// </summary>
        private readonly ChessBoardModel board;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessBoardVM"/> class.
        /// </summary>
        /// <param name="boardModel">
        /// The chessboard object to represent.
        /// </param>
        public ChessBoardVM(ChessBoardModel boardModel)
        {
            this.board = boardModel;
            this.board.OnDisplayMessage += this.FireOnError;
            this.board.OnBoardUpdate += this.FireOnBoardUpdate;
        }
        
        /// <summary>
        /// Delegate to signal something has updated.
        /// </summary>
        public delegate void Update();

        /// <summary>
        /// Event to signal if board has been updated.
        /// </summary>
        public event Update OnBoardUpdate;

        /// <summary>
        /// Event to signal a message has to be displayed.
        /// </summary>
        public event EventHandler<OnDisplayGameMessageEventArgs> OnDisplayMessage;

        /// <summary>
        /// Gets the orientation numbers.
        /// </summary>
        /// <value>
        /// Gets a list.
        /// </value>
        public List<string> OrientationNumbers
        {
            get
            {
                return this.board.OrientationNumbers;
            }
        }

        /// <summary>
        /// Gets the orientation chars.
        /// </summary>
        /// <value>
        /// Gets a list.
        /// </value>
        public List<char> OrientationChars
        {
            get
            {
                return this.board.OrientationChars;
            }
        }

        /// <summary>
        /// Gets the dimensions of the chessboard.
        /// </summary>
        /// <value>
        /// Gets a "DimensionsVM" object.
        /// </value>
        public DimensionsVM Dimensions
        {
            get
            {
                return new DimensionsVM(this.board.ChessBoardDimensions);
            }
        }

        /// <summary>
        /// Gets the chessboard fields.
        /// </summary>
        /// <value>
        /// Gets an "ObservableCollection".
        /// </value>
        public ObservableCollection<ChessBoardFieldVM> ChessBoardFields
        {
            get
            {
                return new ObservableCollection<ChessBoardFieldVM>(this.board.ClickableFields.Select(cf => new ChessBoardFieldVM(cf)));
            }
        }

        /// <summary>
        /// Gets the chess pieces.
        /// </summary>
        /// <value>
        /// Gets an "ObservableCollection".
        /// </value>
        public ObservableCollection<ChessPieceVM> ChessPieces
        {
            get
            {
                return new ObservableCollection<ChessPieceVM>(this.SetElements());
            }
        }

        /// <summary>
        /// Get a chess pieces as chess pieces view model and null values.
        /// </summary>
        /// <returns>
        /// Returns chess pieces and null values to be displayed on the view.
        /// </returns>
        private IEnumerable<ChessPieceVM> SetElements()
        {
            foreach (var element in this.board.ChessPieces)
            {
                if (element != null)
                {
                    yield return new ChessPieceVM(element);
                }
                else
                {
                    yield return null;
                }
            }
        }

        /// <summary>
        /// Raises an event to signal the board has been updated.
        /// </summary>
        private void FireOnBoardUpdate()
        {
            if (this.OnBoardUpdate != null)
            {
                this.OnBoardUpdate.Invoke();
            }
        }

        /// <summary>
        /// Raises an event to signal a message needs to be displayed.
        /// </summary>
        /// <param name="sender">
        /// Caller of the method.
        /// </param>
        /// <param name="args">
        /// Arguments for displaying the message.
        /// </param>
        private void FireOnError(object sender, OnDisplayGameMessageEventArgs args)
        {
            if (this.OnDisplayMessage != null)
            {
                this.OnDisplayMessage.Invoke(this, args);
            }
        }
    }
}
