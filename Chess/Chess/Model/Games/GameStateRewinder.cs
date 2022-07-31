//// <copyright file="GameStateRewinder.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Rewinds the game to a certain state.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Chess.Model.Games.GameSave;

    /// <summary>
    /// Class to rewinds the game to a certain state.
    /// </summary>
    public static class GameStateRewinder
    {
        /// <summary>
        /// Rewinds the game to a certain state.
        /// </summary>
        /// <param name="round">
        /// Round to rewind back to.
        /// </param>
        /// <param name="fieldsAtTheBeginningOfGame">
        /// Fields at the beginning of the game without being moved once.
        /// </param>
        /// <param name="rounds">
        /// Played rounds, needed to rewind to a certain state.
        /// </param>
        /// <param name="game">
        /// Current game.
        /// </param>
        /// <returns>
        /// Returns the chessboard field with values changed to the rewind state.
        /// </returns>
        public static IEnumerable<ChessBoardField> Rewind(this PlayedRound round, IEnumerable<ChessBoardField> fieldsAtTheBeginningOfGame, IEnumerable<PlayedRound> rounds, ChessGame game)
        {
            game.BeatenChessPieces.RemoveAllBeatenPieces();
            IEnumerator<PlayedRound> enumerator = rounds.GetEnumerator();
            while (enumerator.MoveNext() && enumerator.Current.RoundNr <= round.RoundNr)
            {
                ChessBoardField selectedField = fieldsAtTheBeginningOfGame.First(f => f.Position == enumerator.Current.From);
                ChessBoardField targetedField = fieldsAtTheBeginningOfGame.First(f => f.Position == enumerator.Current.To);

                MovingHandler.Move(selectedField, targetedField);
            }

            return fieldsAtTheBeginningOfGame;
        }
    }
}
