//// <copyright file="PlayedRoundVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for round data class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model;
    using Chess.Model.Board;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;
    using Chess.ViewModel.Command;
    using static Chess.Model.Games.GameSave;

    /// <summary>
    /// View model class for the round data.
    /// </summary>
    public class PlayedRoundVM
    {
        /// <summary>
        /// Stores the round data it represents.
        /// </summary>
        private readonly PlayedRound roundData;

        /// <summary>
        /// Stores the current chess game.
        /// </summary>
        private readonly ChessGame game;

        /// <summary>
        /// Stores the played games box.
        /// </summary>
        private readonly PlayedGamesBoxVM playedGamesBoxVM;

        /// <summary>
        /// Stores the chess board.
        /// </summary>
        private readonly ChessBoardModel boardModel;

        /// <summary>
        /// Stores the resume command, to resume to the rounds state.
        /// </summary>
        private readonly GenericCommand resumeCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayedRoundVM"/> class.
        /// </summary>
        /// <param name="rd">
        /// Round data to represent.
        /// </param>
        /// <param name="gVM">
        /// Game view model.
        /// </param>
        /// <param name="g">
        /// Game view model's chess game.
        /// </param>
        /// <param name="pgbVM">
        /// Played games box view model.
        /// </param>
        /// <param name="bm">
        /// Game view model's chessboard view model's chessboard model.
        /// </param>
        public PlayedRoundVM(PlayedRound rd, GameVM gVM, ChessGame g, PlayedGamesBoxVM pgbVM, ChessBoardModel bm)
        {
            this.boardModel = bm;
            this.roundData = rd;
            this.game = g;
            this.playedGamesBoxVM = pgbVM;

            if (this.From != null && this.To != null)
            {
                this.resumeCommand = new GenericCommand(action =>
                {
                    Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> t = BoardCreator.GetFieldsAndChessPieces(this.game, this.boardModel.ChessBoardDimensions);
                    this.roundData.Rewind(t.Item1, this.playedGamesBoxVM.PlayedRounds.Select(pr => new PlayedRound(pr.RoundNr, pr.IsWiteTurn, pr.From, pr.To, pr.HasGameEnded)), this.game);
                    this.boardModel.SetUpBoard(t);
                    CurrentRound currentRound = new CurrentRound(!this.IsWiteTurn, this.roundData.RoundNr + 1, this.HasGameEnded);
                    this.game.RoundCurrently = currentRound;
                });
            }
        }

        /// <summary>
        /// Gets a value indicating whether the game has ended.
        /// </summary>
        /// <value>
        /// Gets a boolean.
        /// </value>
        public bool HasGameEnded
        {
            get
            {
                return this.roundData.HasGameEnded;
            }
        }

        /// <summary>
        /// Gets a value indicating whether white can play.
        /// </summary>
        /// <value>
        /// Gets a boolean.
        /// </value>
        public bool IsWiteTurn
        {
            get
            {
                return this.roundData.IsWhiteTurn;
            }
        }

        /// <summary>
        /// Gets the from position of the round based on orientation numbers/char.
        /// </summary>
        /// <value>
        /// Gets a string.
        /// </value>
        public string From
        {
            get
            {
                return this.roundData.From;
            }
        }

        /// <summary>
        /// Gets the to position of the round based on orientation numbers/char.
        /// </summary>
        /// <value>
        /// Gets a string.
        /// </value>
        public string To
        {
            get
            {
                return this.roundData.To;
            }
        }

        /// <summary>
        /// Gets the round number.
        /// </summary>
        /// <value>
        /// Gets an integer.
        /// </value>
        public int RoundNr
        {
            get
            {
                return this.roundData.RoundNr;
            }
        }

        /// <summary>
        /// Gets the move of the round based on orientation numbers/char.
        /// </summary>
        /// <value>
        /// Gets a string.
        /// </value>
        public string Move
        {
            get
            {
                if (this.From != null && this.To != null)
                {
                    return $"{this.From} - > {this.To}";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the resume command, to resume to the rounds state.
        /// </summary>
        /// <value>
        /// Gets a "GenericCommand" object.
        /// </value>
        public GenericCommand ResumeCommand
        {
            get
            {
                return this.resumeCommand;
            }
        }
    }
}
