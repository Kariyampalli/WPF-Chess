//// <copyright file="ChessBoardField.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Field within the chessboard a chesspiece can stand on.
//// </summary>
namespace Chess.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.Model.Board;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;

    /// <summary>
    /// Class for the chessboard fields.
    /// </summary>
    public class ChessBoardField
    {
        /// <summary>
        /// Stores the current game.
        /// </summary>
        private readonly ChessGame game;

        /// <summary>
        /// Stores the alphabet (Used for the string position in this class).
        /// </summary>
        private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Stores the position on the chessboard.
        /// </summary>
        private readonly Positions positionOnBoard;

        /// <summary>
        /// Stores position based on orientation numbers/char.
        /// </summary>
        private readonly string position;

        /// <summary>
        /// Stores the current state of the field.
        /// </summary>
        private FieldState state;

        /// <summary>
        /// Stores a boolean indicating if the field can be selected or not.
        /// </summary>
        private bool isSelectingAllowed;

        /// <summary>
        /// Stores chess piece located on the chess board.
        /// </summary>
        private ChessPiece chessPiece;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessBoardField"/> class.
        /// </summary>
        /// <param name="piece">
        /// Chess piece currently on the field.
        /// </param>
        /// <param name="bp">
        /// Field's position on the board.
        /// </param>
        /// <param name="g">
        /// Current game.
        /// </param>
        /// <param name="dimensions">
        /// Chessboard's dimensions.
        /// </param>
        public ChessBoardField(ChessPiece piece, Positions bp, ChessGame g, Dimensions dimensions)
        {
            this.Piece = piece;
            this.positionOnBoard = bp;
            this.position = (dimensions.Y - bp.YPosition) + this.alphabet.Substring(bp.XPosition, 1);
            this.State = FieldState.Not_Selected;
            this.game = g;
        }

        /// <summary>
        /// Delegate for raising update specific events.
        /// </summary>
        public delegate void Update();

        /// <summary>
        /// Event to signal the state of the field has changed.
        /// </summary>
        public event Update OnStateUpdate;

        /// <summary>
        /// Gets or sets or set a chess piece. Can be null.
        /// </summary>
        /// <value>
        /// Gets or set a chess piece.
        /// </value>
        public ChessPiece Piece
        {
            get
            {
                return this.chessPiece;
            }

            set
            {
                this.chessPiece = value;
            }
        }

        /// <summary>
        /// Gets position of board.
        /// </summary>
        /// <value>
        /// Gets or set a position.
        /// </value>
        public Positions PositionOnBoard
        {
            get
            {
                return this.positionOnBoard;
            }
        }

        /// <summary>
        /// Gets position based on orientation numbers/char.
        /// </summary>
        /// <value>
        /// Gets or set a position based on orientation numbers/char.
        /// </value>
        public string Position
        {
            get
            {
                return this.position;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether selecting is allowed.
        /// </summary>
        /// <value>
        /// Gets or sets a boolean indicating whether selecting is allowed.
        /// </value>
        public bool IsSelectingAllowed
        {
            get
            {
                return this.isSelectingAllowed;
            }

            set
            {
                if (value != this.isSelectingAllowed)
                {
                    this.isSelectingAllowed = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the state of the field.
        /// </summary>
        /// <value>
        /// Gets or sets an enumeration of type field state.
        /// </value>
        public FieldState State
        {
            get
            {
                return this.state;
            }

            set
            {
                if (value != this.state)
                {
                    this.state = value;
                    if (this.state == FieldState.Selected)
                    {
                        this.IsSelectingAllowed = true;
                    }
                    else
                    {
                        this.IsSelectingAllowed = false;
                    }
                }
            }
        }

        /// <summary>
        /// Gets current game.
        /// </summary>
        /// <value>
        /// Gets a chess game.
        /// </value>
        public ChessGame Game
        {
            get
            {
                return this.game;
            }
        }

        /// <summary>
        /// Calls a method to check if selected field is a selection or target.
        /// </summary>
        public void DoOnSelect()
        {
            this.Game.SetSelectionOrTarget(this);
        }

        /// <summary>
        /// Informs that its state has changed.
        /// </summary>
        public void FireOnSelectionUpdate()
        {
            if (this.OnStateUpdate != null)
            {
                this.OnStateUpdate.Invoke();
            }
        }
    }
}
