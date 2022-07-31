//// <copyright file="GameVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for the chess game class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.Model.Board;
    using Chess.Model.Games;
    using Chess.Shared;
    using Chess.ViewModel.Command;
    using static Chess.Model.Games.GameSave;

    /// <summary>
    /// View model for the game.
    /// </summary>
    public class GameVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the chess board model.
        /// </summary>
        private readonly ChessBoardVM board;

        /// <summary>
        /// Stores the current game.
        /// </summary>
        private readonly ChessGame game;

        /// <summary>
        /// Stores the played games box.
        /// </summary>
        private readonly PlayedGamesBoxVM playedGamesBoxVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameVM"/> class.
        /// </summary>
        public GameVM()
        {
            try
            {
                this.game = new ChessGame();
                this.game.OnNewRoundStarted += this.UpdateCurrentRound;
                ChessBoardModel boardModel = new ChessBoardModel(BoardCreator.GetFieldsAndChessPieces(this.game, new Dimensions()), this.game);
                this.board = new ChessBoardVM(boardModel);
                this.Board.OnDisplayMessage += this.FireOnDisplayMessage;
                this.board.OnBoardUpdate += this.UpdateBoard;
                this.playedGamesBoxVM = new PlayedGamesBoxVM(this, this.game, boardModel);
                this.PlayedGamesBox.OnDisplayMessage += this.FireOnDisplayMessage;
                this.game.BeginNewRound();
            }
            catch (Exception)
            {
                this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("An unexpected error ocurred during the game!", GameMessageType.Exception));
            }
        }

        /// <summary>
        /// Event to signal a message needs to be displayed.
        /// </summary>
        public event EventHandler<OnDisplayGameMessageEventArgs> OnDisplayMessage;

        /// <summary>
        /// Event to signal a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the played games box.
        /// </summary>
        /// <value>
        /// Gets a "PlayedGamesBoxVM" object.
        /// </value>
        public PlayedGamesBoxVM PlayedGamesBox
        {
            get
            {
                return this.playedGamesBoxVM;
            }
        }

        /// <summary>
        /// Gets the beaten pieces.
        /// </summary>
        /// <value>
        /// Gets a "BeatenPiecesVM" object.
        /// </value>
        public BeatenPiecesVM BeatenPieces
        {
            get
            {
                return new BeatenPiecesVM(this.game.BeatenChessPieces);
            }
        }

        /// <summary>
        /// Gets the current round.
        /// </summary>
        /// <value>
        /// Gets a "CurrentRoundVM" object.
        /// </value>
        public CurrentRoundVM CurrentRound
        {
            get
            {
                return new CurrentRoundVM(this.game.RoundCurrently);
            }
        }

        /// <summary>
        /// Gets the chess board.
        /// </summary>
        /// <value>
        /// Gets a "ChessBoardVM" object.
        /// </value>
        public ChessBoardVM Board
        {
            get
            {
                return this.board;
            }
        }

        /// <summary>
        /// Raises an event to signal current round has been updated.
        /// </summary>
        /// <param name="sender">
        /// Called of the method.
        /// </param>
        /// <param name="args">
        /// Arguments for the method.
        /// </param>
        public void UpdateCurrentRound(object sender, OnNewRoundStartedEventArgs args)
        {
            this.FireOnPropertyChanged(nameof(this.CurrentRound));
        }

        /// <summary>
        /// Raises an event to signal the board has been updated.
        /// </summary>
        public void UpdateBoard()
        {
            this.FireOnPropertyChanged(nameof(this.Board));
        }

        /// <summary>
        /// Raises an event to signal a property value has changed.
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

        /// <summary>
        /// Fires an event to signal if a message needs to be displayed.
        /// </summary>
        /// <param name="sender">
        /// Caller of the method.
        /// </param>
        /// <param name="args">
        /// Arguments passed to the method, to be displayed.
        /// </param>
        private void FireOnDisplayMessage(object sender, OnDisplayGameMessageEventArgs args)
        {
            if (this.OnDisplayMessage != null)
            {
                this.OnDisplayMessage.Invoke(sender, args);
            }
        }
    }
}