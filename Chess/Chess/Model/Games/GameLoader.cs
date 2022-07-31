//// <copyright file="GameLoader.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Loads a saved game.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Xml.Serialization;
    using Chess.Model.Board;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;

    /// <summary>
    /// Class loads game from a folder.
    /// </summary>
    public static class GameLoader
    {
        /// <summary>
        /// Load the typed in game name.
        /// </summary>
        /// <param name="fileName">
        /// Name of the game file.
        /// </param>
        /// <param name="game">
        /// Current game.
        /// </param>
        /// <returns>
        /// Returns a tuple with the necessary data to reload the game and into its last state.
        /// </returns>
        public static Tuple<Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions>, bool, GameSave> Load(string fileName, ChessGame game)
        {           
            try
            {
                GameSave savedGame;
                using (FileStream fs = new FileStream($@"..\Saved_Games\{fileName}", FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(GameSave));
                    savedGame = (GameSave)formatter.Deserialize(fs);
                    fs.Close();
                }

                return Tuple.Create(GetGame(savedGame, game), true, savedGame);
            }
            catch (Exception)
            {
                return Tuple.Create(BoardCreator.GetFieldsAndChessPieces(game, new Dimensions(8, 8)), false, new GameSave());
            }   
        }

        /// <summary>
        /// Rewinds to the loaded games last state.
        /// </summary>
        /// <param name="gameSave">
        /// Loaded game.
        /// </param>
        /// <param name="game">
        /// Current game.
        /// </param>
        /// <returns>
        /// Returns a Tuple with the necessary data to set up the board to the loaded game.
        /// </returns>
        private static Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> GetGame(GameSave gameSave, ChessGame game)
        {
            Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> t = BoardCreator.GetFieldsAndChessPieces(game, gameSave.BoardDimensions);
            GameStateRewinder.Rewind(gameSave.PlayedRounds[gameSave.PlayedRounds.Count - 1], t.Item1, gameSave.PlayedRounds, game);
            return t;
        }
    }
}
