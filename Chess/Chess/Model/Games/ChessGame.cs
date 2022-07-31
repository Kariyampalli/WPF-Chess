//// <copyright file="ChessGame.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// The currently playing game.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Shared;
    using static Chess.Model.Games.GameSave;

    /// <summary>
    /// Class for the chess game.
    /// </summary>
    public class ChessGame
    {
        /// <summary>
        /// Stores the beaten chess pieces.
        /// </summary>
        private readonly BeatenPieces beatenChessPieces;

        /// <summary>
        /// Stores the current round.
        /// </summary>
        private CurrentRound currentRound;

        /// <summary>
        /// Stores the round counter to count the rounds.
        /// </summary>
        private int roundCounter;

        /// <summary>
        /// Stores the played rounds.
        /// </summary>
        private List<PlayedRound> playedRounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessGame"/> class.
        /// </summary>
        public ChessGame()
        {
            this.playedRounds = new List<PlayedRound>();
            this.beatenChessPieces = new BeatenPieces();
            this.roundCounter = 0;
        }

        /// <summary>
        /// Delegate to signal if something has been updated.
        /// </summary>
        public delegate void Update();

        /// <summary>
        /// Event to signal if a round has ended.
        /// </summary>
        public event Update OnRoundEnded;

        /// <summary>
        /// Event to signal if a new round has started.
        /// </summary>
        public event EventHandler<OnNewRoundStartedEventArgs> OnNewRoundStarted;

        /// <summary>
        /// Event to signal if a message needs to be displayed.
        /// </summary>
        public event EventHandler<OnDisplayGameMessageEventArgs> OnDisplayMessage;

        /// <summary>
        /// Gets or sets list of played rounds.
        /// </summary>
        /// <value>
        /// Gets or sets a list of played rounds of type round data.
        /// </value>
        public List<PlayedRound> Rounds
        {
            get
            {
                return this.playedRounds;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Played round was tried to be set as null!");
                }

                this.playedRounds = value;            
            }
        }

        /// <summary>
        /// Gets beaten chess pieces.
        /// </summary>
        /// <value>
        /// Gets a beaten pieces object.
        /// </value>
        public BeatenPieces BeatenChessPieces
        {
            get
            {
                return this.beatenChessPieces;
            }
        }

        /// <summary>
        /// Gets or sets the current round.
        /// </summary>
        /// <value>
        /// Gets or sets the currently playing round.
        /// </value>
        public CurrentRound RoundCurrently
        {
            get
            {
                return this.currentRound;
            }

            set
            {
                if (value == null || value.Selection != null || value.Target != null)
                {
                    throw new ArgumentOutOfRangeException("Current round was tried be set to an invalid current round!");
                }

                    this.currentRound = value;
                    this.roundCounter = this.RoundCurrently.RoundNr;
                    this.FireOnNewRoundStarted();
            }
        }

        /// <summary>
        /// Begins and sets up a new round.
        /// </summary>
        public void BeginNewRound()
        {
            CurrentRound round;

            try
            {
                if (this.playedRounds.Count == 0 && this.RoundCurrently == null)
                {
                    this.roundCounter++;
                    this.RoundCurrently = new CurrentRound(false, this.roundCounter, false);
                }
                else if (this.RoundCurrently.Selection == null || this.RoundCurrently.Target == null)
                {
                    this.FireOnDisplayMessage("Can't start a new round if current round isn't finished!");
                }
                else
                {
                    if (this.Rounds.Any(r => r.RoundNr == this.RoundCurrently.RoundNr))
                    {
                        this.RemoveCertainPlayedRounds(new PlayedRound(this.RoundCurrently.RoundNr, this.RoundCurrently.IsWhiteTurn, this.RoundCurrently.Selection.Position, this.RoundCurrently.Target.Position));
                    }

                    this.roundCounter++;

                    PlayedRound playedRound = new PlayedRound(this.RoundCurrently.RoundNr, this.RoundCurrently.IsWhiteTurn, this.RoundCurrently.Selection.Position, this.RoundCurrently.Target.Position);
                    playedRound.HasGameEnded = this.RoundCurrently.HasGameEnded;
                    this.playedRounds.Add(playedRound);
                    this.FireOnRoundEnded();
                    round = new CurrentRound(!this.RoundCurrently.IsWhiteTurn, this.roundCounter, playedRound.HasGameEnded);
                    this.RoundCurrently = round;
                }
            }
            catch (Exception ex)
            {
                this.FireOnDisplayMessage($"An unexpected error occured while trying to start a new round!\n{ex.Message}");
            }
        }

        /// <summary>
        /// Checks if selected field is selection or target.
        /// </summary>
        /// <param name="fp">
        /// Selected field.
        /// </param>
        public void SetSelectionOrTarget(ChessBoardField fp)
        {
            if (!this.CheckIsSelection(fp))
            {
                if (!this.CheckIsTarget(fp))
                {
                    this.FireOnDisplayMessage("An Error while trying to assign selected field as selection or target!");
                }
                else
                {
                    this.BeginNewRound();
                }
            }
        }

        /// <summary>
        /// Removes certain played Rounds from the played rounds list.
        /// </summary>
        /// <param name="round">
        /// Newest added round. All rounds after the passed round will be deleted.
        /// </param>
        private void RemoveCertainPlayedRounds(PlayedRound round)
        {
            List<PlayedRound> newRounds = new List<PlayedRound>();
            try
            {
                foreach (var r in this.Rounds)
                {
                    if (r.RoundNr >= round.RoundNr)
                    {
                        break;
                    }

                    newRounds.Add(r);
                }

                this.Rounds = newRounds;
            }
            catch (Exception)
            {
                this.FireOnDisplayMessage("An error occured, while trying to remove played games from played list collection!\nThere might have been no rounds in the collection.");
            }
        }

        /// <summary>
        /// Checks if selected field can be set as target.
        /// </summary>
        /// <param name="fp">
        /// Selected field.
        /// </param>
        /// <returns>
        /// Returns a value indication if selected field was set a target.
        /// </returns>
        private bool CheckIsTarget(ChessBoardField fp)
        {
            if (this.RoundCurrently.Target == null)
            {
                this.RoundCurrently.Target = fp;

                if (this.RoundCurrently.Selection.Piece != null)
                {
                    this.RoundCurrently.Selection.Piece.DeSelect();
                }

                if (this.RoundCurrently.Target.Piece != null && this.RoundCurrently.Target.Piece.PieceType == ChessPieceType.King)
                {
                    this.RoundCurrently.HasGameEnded = true;
                }

                MovingHandler.Move(this.RoundCurrently.Selection, this.RoundCurrently.Target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if selected field can be set as selection.
        /// </summary>
        /// <param name="fp">
        /// Selected field.
        /// </param>
        /// <returns>
        /// Returns a value indication if selected field was set a selection.
        /// </returns>
        private bool CheckIsSelection(ChessBoardField fp)
        {
            if (this.RoundCurrently.Selection == fp)
            {
                if (this.RoundCurrently.Selection.Piece != null)
                {
                    this.RoundCurrently.Selection.Piece.DeSelect();
                }

                this.RoundCurrently.Selection = null;
                this.RoundCurrently.Target = null;
                return true;
            }

            if (fp.Piece != null)
            {
                if (this.RoundCurrently.Selection == null)
                {
                    this.RoundCurrently.Selection = fp;
                    fp.Piece.Select();
                    return true;
                }
                else
                {
                    if (this.RoundCurrently.Selection != null && this.RoundCurrently.Selection.Piece.Team == fp.Piece.Team)
                    {
                        this.RoundCurrently.Selection.Piece.DeSelect();
                        this.RoundCurrently.Selection = fp;
                        fp.Piece.Select();
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Fires an event to signal a new round has started.
        /// </summary>
        private void FireOnNewRoundStarted()
        {
            if (this.OnNewRoundStarted != null)
            {
                this.OnNewRoundStarted.Invoke(this, new OnNewRoundStartedEventArgs(this.RoundCurrently));
            }
        }

        /// <summary>
        /// Fires an event to signal a round has ended.
        /// </summary>
        private void FireOnRoundEnded()
        {
            if (this.OnRoundEnded != null)
            {
                this.OnRoundEnded.Invoke();
            }
        }

        /// <summary>
        /// Fires an event to signal a message needs to be displayed.
        /// </summary>
        /// <param name="message">
        /// Message to be displayed.
        /// </param>
        private void FireOnDisplayMessage(string message)
        {
            if (this.OnDisplayMessage != null)
            {
                this.OnDisplayMessage(this, new OnDisplayGameMessageEventArgs(message, GameMessageType.Exception));
            }
        }
    }
}
