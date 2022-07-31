//// <copyright file="BoardCreator.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Creates the important data for the chessboard.
//// </summary>
namespace Chess.Model.Board
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;

    /// <summary>
    /// Class creating the board/ its important data.
    /// </summary>
    public static class BoardCreator
    {
        /// <summary>
        /// Creates the important data for the chessboard.
        /// </summary>
        /// <param name="game">
        /// Current chess game.
        /// </param>
        /// <param name="dimensions">
        /// Chessboard dimension.
        /// </param>
        /// <returns>
        /// Return a tuple containing all important data to set up the chessboard.
        /// </returns>
        public static Tuple<List<ChessBoardField>, List<ChessPiece>, ChessBoardField[,], List<string>, List<char>, Dimensions> GetFieldsAndChessPieces(ChessGame game, Dimensions dimensions)
        {
            ChessBoardField[,] board = new ChessBoardField[dimensions.Y, dimensions.X];
            List<ChessBoardField> fields = new List<ChessBoardField>();
            List<ChessPiece> pieces = new List<ChessPiece>();

            Rook rookBlack = new Rook(ChessPieceTeam.Black, new Positions(0, 0), board, game, dimensions);
            pieces.Add(rookBlack);
            fields.Add(new ChessBoardField(rookBlack, new Positions(0, 0), game, dimensions));

            Knight knightBlack = new Knight(ChessPieceTeam.Black, new Positions(1, 0), board, game, dimensions);
            pieces.Add(knightBlack);
            fields.Add(new ChessBoardField(knightBlack, new Positions(1, 0), game, dimensions));

            Bishop bishopBlack = new Bishop(ChessPieceTeam.Black, new Positions(2, 0), board, game, dimensions);
            pieces.Add(bishopBlack);
            fields.Add(new ChessBoardField(bishopBlack, new Positions(2, 0), game, dimensions));

            Queen queenBlack = new Queen(ChessPieceTeam.Black, new Positions(3, 0), board, game, dimensions);
            pieces.Add(queenBlack);
            fields.Add(new ChessBoardField(queenBlack, new Positions(3, 0), game, dimensions));

            King kingBlack = new King(ChessPieceTeam.Black, new Positions(4, 0), board, game, dimensions, pieces, fields);
            pieces.Add(kingBlack);
            fields.Add(new ChessBoardField(kingBlack, new Positions(4, 0), game, dimensions));

            Bishop bishopBlack2 = new Bishop(ChessPieceTeam.Black, new Positions(5, 0), board, game, dimensions);
            pieces.Add(bishopBlack2);
            fields.Add(new ChessBoardField(bishopBlack2, new Positions(5, 0), game, dimensions));

            Knight knightBlack2 = new Knight(ChessPieceTeam.Black, new Positions(6, 0), board, game, dimensions);
            pieces.Add(knightBlack2);
            fields.Add(new ChessBoardField(knightBlack2, new Positions(6, 0), game, dimensions));

            Rook rookBlack2 = new Rook(ChessPieceTeam.Black, new Positions(7, 0), board, game, dimensions);
            pieces.Add(rookBlack2);
            fields.Add(new ChessBoardField(rookBlack2, new Positions(7, 0), game, dimensions));
            AddChessBoardFields(fields, pieces, 8, 0, 1, game, dimensions);

            for (int i = 0; i < 8; i++)
            {
                Pawn pawnBlack = new Pawn(ChessPieceTeam.Black, new Positions(i, 1), board, game, dimensions);
                pieces.Add(pawnBlack);
                fields.Add(new ChessBoardField(pawnBlack, new Positions(i, 1), game, dimensions));
            }

            AddChessBoardFields(fields, pieces, 8, 1, dimensions.Y - 2, game, dimensions);

            for (int i = 0; i < 8; i++)
            {
                Pawn pawnWhite = new Pawn(ChessPieceTeam.White, new Positions(i, dimensions.Y - 2), board, game, dimensions);
                pieces.Add(pawnWhite);
                fields.Add(new ChessBoardField(pawnWhite, new Positions(i, dimensions.Y - 2), game, dimensions));
            }

            AddChessBoardFields(fields, pieces, 8, dimensions.Y - 2, dimensions.Y - 1, game, dimensions);

            Rook rookWhite = new Rook(ChessPieceTeam.White, new Positions(0, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(rookWhite);
            fields.Add(new ChessBoardField(rookWhite, new Positions(0, dimensions.Y - 1), game, dimensions));

            Knight knightWhite = new Knight(ChessPieceTeam.White, new Positions(1, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(knightWhite);
            fields.Add(new ChessBoardField(knightWhite, new Positions(1, dimensions.Y - 1), game, dimensions));

            Bishop bishopWhite = new Bishop(ChessPieceTeam.White, new Positions(2, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(bishopWhite);
            fields.Add(new ChessBoardField(bishopWhite, new Positions(2, dimensions.Y - 1), game, dimensions));

            Queen queenWhite = new Queen(ChessPieceTeam.White, new Positions(3, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(queenWhite);
            fields.Add(new ChessBoardField(queenWhite, new Positions(3, dimensions.Y - 1), game, dimensions));

            King kingWhite = new King(ChessPieceTeam.White, new Positions(4, dimensions.Y - 1), board, game, dimensions, pieces, fields);
            pieces.Add(kingWhite);
            fields.Add(new ChessBoardField(kingWhite, new Positions(4, dimensions.Y - 1), game, dimensions));

            Bishop bishopWhite2 = new Bishop(ChessPieceTeam.White, new Positions(5, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(bishopWhite2);
            fields.Add(new ChessBoardField(bishopWhite2, new Positions(5, dimensions.Y - 1), game, dimensions));

            Knight knightWhite2 = new Knight(ChessPieceTeam.White, new Positions(6, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(knightWhite2);
            fields.Add(new ChessBoardField(knightWhite2, new Positions(6, dimensions.Y - 1), game, dimensions));

            Rook rookWhite2 = new Rook(ChessPieceTeam.White, new Positions(7, dimensions.Y - 1), board, game, dimensions);
            pieces.Add(rookWhite2);
            fields.Add(new ChessBoardField(rookWhite2, new Positions(7, dimensions.Y - 1), game, dimensions));

            AddChessBoardFields(fields, pieces, 8, dimensions.Y - 1, dimensions.Y, game, dimensions);
            return Tuple.Create(fields, pieces, FillBoard(board, fields), GetOrientationNumbers(dimensions), GetOrientationChars(dimensions), dimensions);
        }

        /// <summary>
        /// Adds chessboard fields to a list with the correct data and empty pieces to the piece list.
        /// </summary>
        /// <param name="fields">
        /// List of chessboard fields.
        /// </param>
        /// <param name="pieces">
        /// List of chess pieces.
        /// </param>
        /// <param name="xMinAmount">
        /// X-minimum amount to start.
        /// </param>
        /// <param name="yMinAmount">
        ///  Y-minimum amount to start.
        ///  </param>
        /// <param name="yMaxAmount">
        ///  Y-maximum amount to end.
        /// </param>
        /// <param name="game">
        /// Current chess game.
        /// </param>
        /// <param name="dimensions">
        /// Chessboard dimensions.
        /// </param>
        private static void AddChessBoardFields(List<ChessBoardField> fields, List<ChessPiece> pieces, int xMinAmount, int yMinAmount, int yMaxAmount, ChessGame game, Dimensions dimensions)
        {
            for (int y = yMinAmount; y < yMaxAmount; y++)
            {
                for (int x = xMinAmount; x < dimensions.X; x++)
                {
                    pieces.Add(null);
                    fields.Add(new ChessBoardField(null, new Positions(x, y), game, dimensions));
                }

                xMinAmount = 0;
            }
        }

        /// <summary>
        /// Fills the two dimensional chessboard array with chessboard fields.
        /// </summary>
        /// <param name="board">
        /// Board to be filled.
        /// </param>
        /// <param name="fields">
        /// Fields to fill into the board.
        /// </param>
        /// <returns>
        /// Return the filled board.
        /// </returns>
        private static ChessBoardField[,] FillBoard(ChessBoardField[,] board, List<ChessBoardField> fields)
        {
            foreach (var fp in fields)
            {
                board[fp.PositionOnBoard.YPosition, fp.PositionOnBoard.XPosition] = fp;
            }

            return board;
        }

        /// <summary>
        /// Create the orientation numbers of the chessboard.
        /// </summary>
        /// <param name="dimensions">
        /// Dimensions of the chessboard.
        /// </param>
        /// <returns>
        /// Returns a list of the orientation numbers.
        /// </returns>
        private static List<string> GetOrientationNumbers(Dimensions dimensions)
        {
            List<string> sn = new List<string>();
            for (int i = dimensions.Y; i >= 1; i--)
            {
                sn.Add(i.ToString("00"));
            }

            return sn;
        }

        /// <summary>
        /// Create the orientation chars of the chessboard.
        /// </summary>
        /// <param name="dimensions">
        /// Dimensions of the chessboard.
        /// </param>
        /// <returns>
        /// Returns a list of the orientation chars.
        /// </returns>
        private static List<char> GetOrientationChars(Dimensions dimensions)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return alphabet.ToCharArray(0, dimensions.X).ToList();
        }
    }
}
