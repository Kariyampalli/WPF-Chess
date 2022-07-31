//// <copyright file="GameSave.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Contains the important data of a played gam to be saved and loaded again.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Board;

    /// <summary>
    /// Class for storing game data to be saved.
    /// </summary>
    [Serializable]
    public class GameSave
    {
        /// <summary>
        /// Stores the dimensions of the chessboard.
        /// </summary>
        private Dimensions boardDimensions;

        /// <summary>
        /// Stores the played rounds.
        /// </summary>
        private List<PlayedRound> playedRounds;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSave"/> class.
        /// </summary>
        public GameSave()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameSave"/> class.
        /// </summary>
        /// <param name="cbd">
        /// Dimensions of the board to be saved.
        /// </param>
        /// <param name="pr">
        /// List of played rounds to be saved.
        /// </param>
        public GameSave(Dimensions cbd, List<PlayedRound> pr)
        {
            this.boardDimensions = cbd;
            this.playedRounds = pr;
        }

        /// <summary>
        /// Gets or sets the chessboards dimensions.
        /// </summary>
        /// <value>
        /// Gets or sets a "Dimensions" object.
        /// </value>
        public Dimensions BoardDimensions
        {
            get
            {
                return this.boardDimensions;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Game save received an empty dimension!");
                }

                this.boardDimensions = value;
            }
        }

        /// <summary>
        /// Gets or sets the played rounds.
        /// </summary>
        /// <value>
        /// Gets or sets a list.
        /// </value>
        public List<PlayedRound> PlayedRounds
        {
            get
            {
                return this.playedRounds;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("Game save received an empty list of played rounds!");
                }

                this.playedRounds = value;
            }
        }

        /// <summary>
        /// Class for the data of a played round.
        /// </summary>
        public class PlayedRound
        {
            /// <summary>
            /// Stores the round number.
            /// </summary>
            private int roundNr;

            /// <summary>
            /// Stores a boolean indicating if white is on turn.
            /// </summary>
            private bool isWhiteTurn;

            /// <summary>
            /// Stores the from position of a played round based on orientation numbers/char.
            /// </summary>
            private string from;

            /// <summary>
            /// Stores the to position of a played round based on orientation numbers/char.
            /// </summary>
            private string to;

            /// <summary>
            /// Stores a boolean indicating if game has ended.
            /// </summary>
            private bool hasGameEnded;

            /// <summary>
            /// Initializes a new instance of the <see cref="PlayedRound"/> class.
            /// </summary>
            public PlayedRound()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PlayedRound"/> class.
            /// </summary>
            /// <param name="roundNumber">
            /// The round number.
            /// </param>
            /// <param name="isWhitesTurn">
            /// Boolean indicating if white is on turn.
            /// </param>
            /// <param name="fromPos">
            /// The from position of a played round based on orientation numbers/char.
            /// </param>
            /// <param name="toPos">
            ///  The to position of a played round based on orientation numbers/char.
            /// </param>
            public PlayedRound(int roundNumber, bool isWhitesTurn, string fromPos, string toPos)
            {
                this.roundNr = roundNumber;
                this.isWhiteTurn = isWhitesTurn;
                this.from = fromPos;
                this.to = toPos;
                this.HasGameEnded = false;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="PlayedRound"/> class.
            /// </summary>
            /// <param name="roundNumber">
            /// The round number.
            /// </param>
            /// <param name="isWhitesTurn">
            /// Boolean indicating if white is on turn.
            /// </param>
            /// <param name="fromPos">
            /// The from position of a played round based on orientation numbers/char.
            /// </param>
            /// <param name="toPos">
            ///  The to position of a played round based on orientation numbers/char.
            /// </param>
            /// <param name="hasChessGameEnded">
            /// Boolean indicating if game has ended.
            /// </param>
            public PlayedRound(int roundNumber, bool isWhitesTurn, string fromPos, string toPos, bool hasChessGameEnded)
            {
                this.roundNr = roundNumber;
                this.isWhiteTurn = isWhitesTurn;
                this.from = fromPos;
                this.to = toPos;
                this.HasGameEnded = hasChessGameEnded;
            }

            /// <summary>
            /// Gets or sets the round number.
            /// </summary>
            /// <value>
            /// Gets or sets an integer.
            /// </value>
            public int RoundNr
            {
                get
                {
                    return this.roundNr;
                }

                set
                {
                    if (value < 1)
                    {
                        value = 1;
                    }

                    this.roundNr = value;
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether white can play.
            /// </summary>
            /// <value>
            /// Gets or sets a boolean.
            /// </value>
            public bool IsWhiteTurn
            {
                get
                {
                    return this.isWhiteTurn;
                }

                set
                {
                    this.isWhiteTurn = value;
                }
            }

            /// <summary>
            /// Gets or sets from position of a played round based on orientation numbers/char.
            /// </summary>
            /// <value>
            /// Gets or sets a string.
            /// </value>
            public string From
            {
                get
                {
                    return this.from;
                }

                set
                {
                    if (value == null || value.Trim().Length == 0)
                    {
                        throw new ArgumentOutOfRangeException("Played round received invalid from position!");
                    }

                    this.from = value;
                }
            }

            /// <summary>
            /// Gets or sets to position of a played round based on orientation numbers/char.
            /// </summary>
            /// <value>
            /// Gets or sets a string.
            /// </value>
            public string To
            {
                get
                {
                    return this.to;
                }

                set
                {
                    if (value == null || value.Trim().Length == 0)
                    {
                        throw new ArgumentOutOfRangeException("Played round received invalid to position!");
                    }

                    this.to = value;
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether game has ended.
            /// </summary>
            /// <value>
            /// Gets or set a boolean indicating if game has ended.
            /// </value>
            public bool HasGameEnded
            {
                get
                {
                    return this.hasGameEnded;
                }

                set
                {
                    if (value != this.hasGameEnded)
                    {
                        this.hasGameEnded = value;
                    }
                }
            }
        }
    }
}