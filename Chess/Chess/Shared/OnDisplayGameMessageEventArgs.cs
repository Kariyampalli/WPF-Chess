//// <copyright file="OnDisplayGameMessageEventArgs.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Used when a message needs to be displayed somewhere.
//// </summary>
namespace Chess.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Arguments class for displaying a message somewhere.
    /// </summary>
    public class OnDisplayGameMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Stores the message to be displayed.
        /// </summary>
        private readonly string message;

        /// <summary>
        /// Stores the message type.
        /// </summary>
        private readonly GameMessageType messageType;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnDisplayGameMessageEventArgs"/> class.
        /// </summary>
        /// <param name="gameMessage">
        /// Message to be displayed.
        /// </param>
        /// <param name="type">
        /// Message type.
        /// </param>
        public OnDisplayGameMessageEventArgs(string gameMessage, GameMessageType type)
        {
            this.message = gameMessage;
            this.messageType = type;
        }

        /// <summary>
        /// Gets the message to be displayed.
        /// </summary>
        /// <value>
        /// Gets a string.
        /// </value>
        public string Message
        {
            get
            {
                return this.message;
            }
        }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>
        /// Gets or sets a enumeration of type "GameMessageType".
        /// </value>
        public GameMessageType MessageType
        {
            get
            {
                return this.messageType;
            }
        }
    }
}
