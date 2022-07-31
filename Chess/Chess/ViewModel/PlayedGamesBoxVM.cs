//// <copyright file="PlayedGamesBoxVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Used for the played games box to get the right data.
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
    using Chess.Model;
    using Chess.Model.Board;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;
    using Chess.Shared;
    using Chess.ViewModel.Command;
    using static Chess.Model.Games.GameSave;

    /// <summary>
    /// Class for storing played games box data.
    /// </summary>
    public class PlayedGamesBoxVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the command to be executed on saving.
        /// </summary>
        private readonly GenericCommand saveCommand;

        /// <summary>
        /// Stores the command to be executed on loading.
        /// </summary>
        private readonly GenericCommand loadCommand;

        /// <summary>
        /// Stores the command to be executed on restart.
        /// </summary>
        private readonly GenericCommand restartCommand;

        /// <summary>
        /// Stores the game view model.
        /// </summary>
        private readonly GameVM gameVM;

        /// <summary>
        /// Stores the chess game of the game view model.
        /// </summary>
        private readonly ChessGame game;

        /// <summary>
        /// Stores the chess board model.
        /// </summary>
        private readonly ChessBoardModel boardModel;

        /// <summary>
        /// Stores file to be loaded.
        /// </summary>
        private string fileToLoad;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayedGamesBoxVM"/> class.
        /// </summary>
        /// <param name="gVM">
        /// Game view model.
        /// </param>
        /// <param name="g">
        /// Game view model's chess game.
        /// </param>
        /// <param name="bm">
        /// Game view model's chessboard view model's chessboard model.
        /// </param>
        public PlayedGamesBoxVM(GameVM gVM, ChessGame g, ChessBoardModel bm)
        {
            this.fileToLoad = string.Empty;
            this.boardModel = bm;
            this.gameVM = gVM;
            this.game = g;
            this.game.OnRoundEnded += this.UpdatePlayedRounds;

            this.saveCommand = new GenericCommand(action =>
            {
                if (!GameSaver.TrySave(this.PlayedRounds.Select(pr => new PlayedRound(pr.RoundNr, pr.IsWiteTurn, pr.From, pr.To, pr.HasGameEnded)).ToList(), new Dimensions(gameVM.Board.Dimensions.X, gameVM.Board.Dimensions.Y)))
                {
                    this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("Couldn't save game because the game state is invalid!\n(example not enough played rounds, board error etc.)", GameMessageType.Exception));
                }
            });

            this.loadCommand = new GenericCommand(action =>
            {
                Tuple<Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions>, bool, GameSave> t = GameLoader.Load(this.FileToLoad, this.game);
                if (t.Item2)
                {
                    this.boardModel.SetUpBoard(t.Item1);
                    this.PlayedRounds = new ObservableCollection<PlayedRoundVM>(t.Item3.PlayedRounds.Select(pr => new PlayedRoundVM(pr, gameVM, game, this, this.boardModel)));
                    this.game.RoundCurrently = new CurrentRound(!t.Item3.PlayedRounds[t.Item3.PlayedRounds.Count - 1].IsWhiteTurn, t.Item3.PlayedRounds[t.Item3.PlayedRounds.Count - 1].RoundNr + 1, t.Item3.PlayedRounds[t.Item3.PlayedRounds.Count - 1].HasGameEnded);
                }
                else
                {
                    this.FireOnDisplayMessage(this, new OnDisplayGameMessageEventArgs("Couldn't load file because filename, path etc. is wrong/doesn't exist, corrupted file or some other error occured!", GameMessageType.Exception));
                }
            });

            this.restartCommand = new GenericCommand(action =>
            {
                this.game.Rounds = new List<PlayedRound>();
                this.UpdatePlayedRounds();

                Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> t = BoardCreator.GetFieldsAndChessPieces(this.game, new Dimensions(this.boardModel.ChessBoardDimensions.X, this.boardModel.ChessBoardDimensions.Y));
                this.boardModel.SetUpBoard(t);

                this.game.RoundCurrently = new CurrentRound(false, 1, false);
                this.game.BeatenChessPieces.RemoveAllBeatenPieces();
            });
        }

        /// <summary>
        /// Stores the event to signal a message has to be displayed.
        /// </summary>
        public event EventHandler<OnDisplayGameMessageEventArgs> OnDisplayMessage;

        /// <summary>
        /// Stores the event to signal a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets a file name of a saved game to be loaded.
        /// </summary>
        /// <value>
        /// Gets or set a string.
        /// </value>
        public string FileToLoad
        {
            get
            {
                return this.fileToLoad;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("File to load received a null value!");
                }

                this.fileToLoad = value;
            }
        }

        /// <summary>
        /// Gets a load command to load a saved game.
        /// </summary>
        /// <value>
        /// Gets a "GenericCommand" object.
        /// </value>
        public GenericCommand LoadCommand
        {
            get
            {
                return this.loadCommand;
            }
        }

        /// <summary>
        /// Gets a save command to save a game.
        /// </summary>
        /// <value>
        /// Gets a "GenericCommand" object.
        /// </value>
        public GenericCommand SaveCommand
        {
            get
            {
                return this.saveCommand;
            }
        }

        /// <summary>
        /// Gets a restart command to restart the game.
        /// </summary>
        /// <value>
        /// Gets a "GenericCommand" object.
        /// </value>
        public GenericCommand RestartCommand
        {
            get
            {
                return this.restartCommand;
            }
        }

        /// <summary>
        /// Gets or sets a collection of played rounds of a game.
        /// </summary>
        /// <value>
        /// Gets or set a collection of played rounds.
        /// </value>
        public ObservableCollection<PlayedRoundVM> PlayedRounds
        {
            get
            {
                return new ObservableCollection<PlayedRoundVM>(this.game.Rounds.Select(r => new PlayedRoundVM(r, this.gameVM, this.game, this, this.boardModel)));
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Tried to set played rounds to null!");
                }

                this.game.Rounds = value.Select(pr => new PlayedRound(pr.RoundNr, pr.IsWiteTurn, pr.From, pr.To, pr.HasGameEnded)).ToList();
                this.UpdatePlayedRounds();
            }
        }

        /// <summary>
        /// Updates played rounds.
        /// </summary>
        private void UpdatePlayedRounds()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(this.PlayedRounds)));
            }
        }

        /// <summary>
        /// Fires an event to signal a message need to be displayed.
        /// </summary>
        /// <param name="sender">
        /// Caller of the method.
        /// </param>
        /// <param name="args">
        /// Arguments for the message to be displayed.
        /// </param>
        private void FireOnDisplayMessage(object sender, OnDisplayGameMessageEventArgs args)
        {
            if (this.OnDisplayMessage != null)
            {
                this.OnDisplayMessage(this, args);
            }
        }
    }
}
