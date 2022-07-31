//// <copyright file="CurrentRound.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Contains the data of a currently playing round.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for the currently playing round.
    /// </summary>
    public class CurrentRound
    {
        /// <summary>
        /// Stores the round number.
        /// </summary>
        private readonly int roundNr;

        /// <summary>
        /// Stores a boolean indicating if white can play.
        /// </summary>
        private readonly bool isWhiteTurn;

        /// <summary>
        /// Stores the selected field.
        /// </summary>
        private ChessBoardField selection;

        /// <summary>
        /// Stores the targeted field.
        /// </summary>
        private ChessBoardField target;

        /// <summary>
        /// Stores a boolean indicating whether the game has ended.
        /// </summary>
        private bool hasGameEnded;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentRound"/> class.
        /// </summary>
        /// <param name="isWhitesTurn">
        /// Is white's turn.
        /// </param>
        /// <param name="roundNumber">
        /// Round's number.
        /// </param>
        /// <param name="hasGameEnded">
        /// If the game has already ended.
        /// </param>
        public CurrentRound(bool isWhitesTurn, int roundNumber, bool hasGameEnded)
        {
            this.isWhiteTurn = isWhitesTurn;
            this.roundNr = roundNumber;
            this.HasGameEnded = hasGameEnded;
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
                return this.roundNr;
            }
        }

        /// <summary>
        /// Gets a value indicating whether white or black can play.
        /// </summary>
        /// <value>
        /// Gets a boolean.
        /// </value>
        public bool IsWhiteTurn
        {
            get
            {
                return this.isWhiteTurn;
            }
        }

        /// <summary>
        /// Gets or sets a selected field. Can be null.
        /// </summary>
        /// <value>
        /// Gets or sets a "ChessBoardField" object.
        /// </value>
        public ChessBoardField Selection
        {
            get
            {
                return this.selection;
            }

            set
            {
                this.selection = value;
            }
        }

        /// <summary>
        /// Gets or sets a targeted field. Can be null.
        /// </summary>
        /// <value>
        /// Gets or sets a "ChessBoardField" object.
        /// </value>
        public ChessBoardField Target
        {
            get
            {
                return this.target;
            }

            set
            {
                this.target = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the game has ended.
        /// </summary>
        /// <value>
        /// Gets a boolean.
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
