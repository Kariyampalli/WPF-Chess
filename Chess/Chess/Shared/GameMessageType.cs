//// <copyright file="GameMessageType.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Used to differ message type.
//// </summary>
namespace Chess.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Enumeration for the message type.
    /// </summary>
    public enum GameMessageType
    {
        /// <summary>
        /// Normal game message.
        /// </summary>
        Game,

        /// <summary>
        /// Exception message.
        /// </summary>
        Exception,
    }
}
