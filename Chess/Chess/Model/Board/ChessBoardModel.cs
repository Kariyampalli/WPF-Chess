//// <copyright file="ChessBoardModel.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Model for the chessboard.
//// </summary>
namespace Chess.Model.Board
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;
    using Chess.Shared;

    /// <summary>
    /// Class for the chess board.
    /// </summary>
    public class ChessBoardModel
    {
        /// <summary>
        /// Stores the visitor for the checkmate clause.
        /// </summary>
        private readonly CheckMateVisitor checkMateVisitor;

        /// <summary>
        /// Stores the current chess game.
        /// </summary>
        private readonly ChessGame chessGame;

        /// <summary>
        /// Stores the chess board dimensions.
        /// </summary>
        private Dimensions chessBoardDimensions;

        /// <summary>
        /// Stores the chessboard fields.
        /// </summary>
        private IEnumerable<ChessBoardField> clickableFields;

        /// <summary>
        /// Stores the chess pieces.
        /// </summary>
        private IEnumerable<ChessPiece> chessPieces;

        /// <summary>
        /// Stores a two dimensional array of the chess board.
        /// </summary>
        private ChessBoardField[,] board;

        /// <summary>
        /// Stores a list of orientation numbers.
        /// </summary>
        private List<string> orientationNumbers;

        /// <summary>
        /// Stores a list of orientation chars.
        /// </summary>
        private List<char> orientationChars;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessBoardModel"/> class.
        /// </summary>
        /// <param name="t">
        /// Data the chessboard needs to be set.
        /// </param>
        /// <param name="game">
        /// Current game.
        /// </param>
        public ChessBoardModel(Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> t, ChessGame game)
        {
            this.checkMateVisitor = new CheckMateVisitor();
            this.chessGame = game;
            this.chessGame.OnNewRoundStarted += this.DoOnNewRound;
            this.SetUpBoard(t);
        }

        /// <summary>
        /// Delegate for signal an update of a property.
        /// </summary>
        public delegate void Update();

        /// <summary>
        /// Event to signal a message needs to be displayed.
        /// </summary>
        public event EventHandler<OnDisplayGameMessageEventArgs> OnDisplayMessage;

        /// <summary>
        /// Event to signal the board has been updated.
        /// </summary>
        public event Update OnBoardUpdate;

        /// <summary>
        /// Gets the dimensions of the chessboard.
        /// </summary>
        /// <value>
        /// Gets or set a dimensions object.
        /// </value>
        public Dimensions ChessBoardDimensions
        {
            get
            {
                return this.chessBoardDimensions;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Board model received empty dimensions!");
                }

                this.chessBoardDimensions = value;
            }
        }

        /// <summary>
        /// Gets a list of the orientation numbers for the chessboard.
        /// </summary>
        /// <value>
        /// Gets or set a list of the orientation numbers for the chessboard.
        /// </value>
        public List<string> OrientationNumbers
        {
            get
            {
                return this.orientationNumbers;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Board model received empty orientation numbers!");
                }

                this.orientationNumbers = value;                
            }
        }

        /// <summary>
        /// Gets a list of the orientation chars for the chessboard.
        /// </summary>
        /// <value>
        /// Gets or set a list of the orientation chars for the chessboard.
        /// </value>
        public List<char> OrientationChars
        {
            get
            {
                return this.orientationChars;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Board model received empty orientation chars!");
                }

                this.orientationChars = value;
            }
        }

        /// <summary>
        /// Gets a two dimensional chess board array.
        /// </summary>
        /// <value>
        /// Gets or set a two dimensional chess board array.
        /// </value>
        public ChessBoardField[,] Board
        {
            get
            {
                return this.board;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Board model received empty board!");
                }

                this.board = value;
            }
        }

        /// <summary>
        /// Gets chessboard fields.
        /// </summary>
        /// <value>
        /// Gets or set chessboard fields.
        /// </value>
        public IEnumerable<ChessBoardField> ClickableFields
        {
            get
            {
                return this.clickableFields;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Board model received empty chess board fields of type IEnumerable!");
                }

                this.clickableFields = value;
            }
        }

        /// <summary>
        /// Gets chess pieces.
        /// </summary>
        /// <value>
        /// Gets or set chess pieces.
        /// </value>
        public IEnumerable<ChessPiece> ChessPieces
        {
            get
            {
                return this.chessPieces;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Board model received empty chess piece of type IEnumerable!");
                }

                this.chessPieces = value;
            }
        }

        /// <summary>
        /// Sets value of the chess board to one passed to the method.
        /// </summary>
        /// <param name="t">
        /// Tuple containing the important data for the chessboard.
        /// </param>
        public void SetUpBoard(Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> t)
        {
            try
            {
                this.ClickableFields = t.Item1;
                this.ChessPieces = t.Item2;
                this.Board = t.Item3;
                this.OrientationNumbers = t.Item4;
                this.OrientationChars = t.Item5;
                this.ChessBoardDimensions = t.Item6;
                this.FireOnBoardUpdate();
            }
            catch (NullReferenceException)
            {
                this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("An error occured trying to set up the board by an empty value!", GameMessageType.Exception));
            }
            catch (Exception ex)
            {
                this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs($"An unexpected error occured trying to set up the board!{ex.Message}", GameMessageType.Exception));
            }
        }

        /// <summary>
        /// Selects and unselects the right fields on new round started.
        /// </summary>
        /// <param name="sender">
        /// Object sender of the caller.
        /// </param>
        /// <param name="args">
        /// Arguments for the method.
        /// </param>
        public void DoOnNewRound(object sender, OnNewRoundStartedEventArgs args)
        {
            bool unselectAll = false;
            try
            {
                if (args.Round.HasGameEnded)
                {
                    unselectAll = true;
                }
                else if (this.chessGame.Rounds.Count != 0 && args.Round.RoundNr < this.chessGame.Rounds[this.chessGame.Rounds.Count - 1].RoundNr)
                {
                    unselectAll = args.Round.HasGameEnded;
                }
                else if (this.chessGame.Rounds.Count != 0 && this.CheckKingsState(args.Round.IsWhiteTurn))
                {
                    this.chessGame.Rounds[this.chessGame.Rounds.Count - 1].HasGameEnded = true;
                    unselectAll = true;
                }

                IEnumerator<ChessBoardField> enumerator = this.ClickableFields.GetEnumerator();
                if (!unselectAll)
                {
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current.Piece != null)
                        {
                            ChessPiece piece = enumerator.Current.Piece;
                            if (args.Round.IsWhiteTurn)
                            {
                                if (piece.Team == ChessPieceTeam.White)
                                {
                                    enumerator.Current.IsSelectingAllowed = true;
                                    enumerator.Current.FireOnSelectionUpdate();
                                }
                                else
                                {
                                    enumerator.Current.IsSelectingAllowed = false;
                                    enumerator.Current.FireOnSelectionUpdate();
                                }
                            }
                            else
                            {
                                if (piece.Team == ChessPieceTeam.White)
                                {
                                    enumerator.Current.IsSelectingAllowed = false;
                                    enumerator.Current.FireOnSelectionUpdate();
                                }
                                else
                                {
                                    enumerator.Current.IsSelectingAllowed = true;
                                    enumerator.Current.FireOnSelectionUpdate();
                                }
                            }
                        }
                    }
                }
                else
                {
                    while (enumerator.MoveNext())
                    {
                        enumerator.Current.IsSelectingAllowed = false;
                        enumerator.Current.FireOnSelectionUpdate();
                    }

                    if (args.Round.IsWhiteTurn)
                    {
                        this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("Check Mate!\nBlack won.", GameMessageType.Game));
                    }
                    else
                    {
                        this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("Check Mate!\nWhite won.", GameMessageType.Game));
                    }
                }
            }
            catch (ArgumentNullException)
            {
                this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("Couldn't deselect/selecet pieces on each team on new round started due to an error caused by a missing value or an unexessible value!", GameMessageType.Exception));
            }
            catch (Exception)
            {
                this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("Couldn't deselect/selecet pieces on each team on new round started due to an unexpected error!", GameMessageType.Exception));
            }
        }

        /// <summary>
        /// Chess the kings state after a round. 
        /// </summary>
        /// <param name="isWhiteTurn">
        /// Boolean indicating if now is white's turn.
        /// </param>
        /// <returns>
        /// Return a boolean indicating if king has been checkmated.
        /// </returns>
        private bool CheckKingsState(bool isWhiteTurn)
        {
            IEnumerator<ChessBoardField> enumerator = this.ClickableFields.GetEnumerator();
            bool isCheckMate = false;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Piece != null && enumerator.Current.Piece.PieceType == ChessPieceType.King)
                {
                    isCheckMate = enumerator.Current.Piece.Accept(this.checkMateVisitor);
                }
            }

            return isCheckMate;
        }

        /// <summary>
        /// Fires an event if the board has been updated.
        /// </summary>
        private void FireOnBoardUpdate()
        {
            if (this.OnBoardUpdate != null)
            {
                this.OnBoardUpdate.Invoke();
            }
        }

        /// <summary>
        /// Fires an event if a message needs to be displayed.
        /// </summary>
        /// <param name="sender">
        /// Object sender / caller of the method.
        /// </param>
        /// <param name="args">
        /// Arguments for the method.
        /// </param>
        private void FireOnDisplayMessage(object sender, OnDisplayGameMessageEventArgs args)
        {
            if (this.OnDisplayMessage != null)
            {
                this.OnDisplayMessage.Invoke(this, args);
            }
        }
    }
}