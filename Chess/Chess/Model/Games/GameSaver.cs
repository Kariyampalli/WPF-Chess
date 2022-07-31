//// <copyright file="GameSaver.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Saves the currently saved game.
//// </summary>
namespace Chess.Model.Games
{  
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Chess.Model.Board;
    using static Chess.Model.Games.GameSave;

    /// <summary>
    /// Class to save a game.
    /// </summary>
    public static class GameSaver
    {
        /// <summary>
        /// Tries to save a game.
        /// </summary>
        /// <param name="playedRounds">
        /// Played round of the game to be saved.
        /// </param>
        /// <param name="chessBoardDimensions">
        /// Dimension of the board to be saved.
        /// </param>
        /// <returns>
        /// Return a value indicating whether saving was successful.
        /// </returns>
        public static bool TrySave(List<PlayedRound> playedRounds, Dimensions chessBoardDimensions)
        {
            string folderDirectory = $@"..\Saved_Games";
            if (playedRounds != null && playedRounds.Count != 0 && chessBoardDimensions != null)
            {
                DateTime dt = DateTime.Now;
                try
                {
                    if (!Directory.Exists(folderDirectory))
                    {
                        Directory.CreateDirectory(folderDirectory);
                    }

                    XmlSerializer formatter = new XmlSerializer(typeof(GameSave));
                    using (FileStream fs = new FileStream($@"{folderDirectory}\Game_{dt.Day}-{dt.Month}-{dt.Year}_{dt.Hour}-{dt.Minute}-{dt.Second}", FileMode.Create))
                    {                    
                        formatter.Serialize(fs, new GameSave(chessBoardDimensions, playedRounds));
                        fs.Close();
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
