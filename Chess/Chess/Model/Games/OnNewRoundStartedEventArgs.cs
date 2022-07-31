//// <copyright file="OnNewRoundStartedEventArgs.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Contains the args to be passed through an event when a new round has started.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Arguments class to be passed on new round started event.
    /// </summary>
    public class OnNewRoundStartedEventArgs
    {
        /// <summary>
        /// Stores the newly started round.
        /// </summary>
        private readonly CurrentRound round;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnNewRoundStartedEventArgs"/> class.
        /// </summary>
        /// <param name="newRound">
        /// Newly started round.
        /// </param>
        public OnNewRoundStartedEventArgs(CurrentRound newRound)
        {
            this.round = newRound;
        }

        /// <summary>
        /// Gets the newly started round.
        /// </summary>
        /// <value>
        /// Gets a "CurrentRound" object.
        /// </value>
        public CurrentRound Round
        {
            get
            {
                return this.round;
            }
        }
    }
}
