//// <copyright file="BeatenPieces.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Keeps track of the beaten pieces.
//// </summary>
namespace Chess.Model.Games
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Board;
    using Chess.Model.Elements.ChessPieces;

    /// <summary>
    /// Class to keep track of the beaten pieces.
    /// </summary>
    public class BeatenPieces
    {
        /// <summary>
        /// Stores the beaten black chess pieces.
        /// </summary>
        private readonly List<ChessPiece> beatenBlackPieces;

        /// <summary>
        /// Stores the beaten white chess pieces.
        /// </summary>
        private readonly List<ChessPiece> beatenWhitePieces;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeatenPieces"/> class.
        /// </summary>
        public BeatenPieces()
        {
            this.beatenBlackPieces = new List<ChessPiece>();
            this.beatenWhitePieces = new List<ChessPiece>();
        }

        /// <summary>
        /// Delegate for updating a beaten chess piece list.
        /// </summary>
        /// <param name="updateBeatenBlackPieces">
        /// A boolean indicating which beaten chess piece list should be updated.
        /// </param>
        public delegate void Update(bool updateBeatenBlackPieces);

        /// <summary>
        /// Event to signal a beaten chess piece list has been updated.
        /// </summary>
        public event Update UpdateBeatenList;

        /// <summary>
        /// Gets beaten black chess pieces.
        /// </summary>
        /// <value>
        /// Gets an "IEnumerable".
        /// </value>
        public IEnumerable<ChessPiece> BeatenBlackPieces 
        {
            get 
            {
                return this.beatenBlackPieces;
            }
        }

        /// <summary>
        /// Gets beaten white chess pieces.
        /// </summary>
        /// <value>
        /// Gets an "IEnumerable".
        /// </value>
        public IEnumerable<ChessPiece> BeatenWhitePieces
        {
            get 
            {
                return this.beatenWhitePieces;
            }
        }

        /// <summary>
        /// Adds a beaten chess piece to one of the beaten chess piece lists.
        /// </summary>
        /// <param name="piece">
        /// Beaten chess piece to be added to one of the beaten chess piece lists.
        /// </param>
        public void AddBeatenPiece(ChessPiece piece)
        {
            if (piece != null)
            {
                switch (piece.Team)
                {
                    case ChessPieceTeam.Black:
                        this.beatenBlackPieces.Add(piece);
                        this.FireOnUpdateBeatenList(true);
                        break;
                    case ChessPieceTeam.White:
                        this.beatenWhitePieces.Add(piece);
                        this.FireOnUpdateBeatenList(false);
                        break;
                }
            }
        }

        /// <summary>
        /// Remove all beaten chess pieces from one the beaten chess piece lists.
        /// </summary>
        public void RemoveAllBeatenPieces()
        {
            this.beatenBlackPieces.Clear();
            this.beatenWhitePieces.Clear();
            this.FireOnUpdateBeatenList(true);
            this.FireOnUpdateBeatenList(false);
        }

        /// <summary>
        /// Raise event to signal one of the beaten chess piece list has been updated.
        /// </summary>
        /// <param name="updateBeatenBlackPieces">
        /// A boolean indicating which of the beaten chess piece list has been updated.
        /// </param>
        private void FireOnUpdateBeatenList(bool updateBeatenBlackPieces)
        {
            if (this.UpdateBeatenList != null)
            {
                this.UpdateBeatenList.Invoke(updateBeatenBlackPieces);
            }
        }
    }
}
