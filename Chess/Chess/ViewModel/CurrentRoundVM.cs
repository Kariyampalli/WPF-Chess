//// <copyright file="CurrentRoundVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for round class.
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
    /// View model for the current round.
    /// </summary>
    public class CurrentRoundVM
    {
        /// <summary>
        /// Stores the round it represents.
        /// </summary>
        private readonly CurrentRound round;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentRoundVM"/> class.
        /// </summary>
        /// <param name="r">
        /// Round to represent.
        /// </param>
        public CurrentRoundVM(CurrentRound r)
        {
            this.round = r;
        }

        /// <summary>
        /// Gets a value indicating whether white or black can play.
        /// </summary>
        /// <value>
        /// Gets a boolean.
        /// </value>
        public bool WiteTurn
        {
            get
            {
                return this.round.IsWhiteTurn;
            }
        }
    }
}
