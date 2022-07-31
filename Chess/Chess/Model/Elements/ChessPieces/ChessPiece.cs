//// <copyright file="ChessPiece.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Upper class for all the chess pieces.
//// </summary>

namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Board;
    using Chess.Model.Elements;
    using Chess.Model.Elements.ChessPieces;
    using Chess.Model.Games;

    /// <summary>
    /// Upper class for chess pieces.
    /// </summary>
    public abstract class ChessPiece
    {
        /// <summary>
        /// Stores the type of the chess piece.
        /// </summary>
        private readonly ChessPieceType pieceType;

        /// <summary>
        /// Stores the team of the chess piece.
        /// </summary>
        private readonly ChessPieceTeam team;

        /// <summary>
        /// Stores the current game.
        /// </summary>
        private readonly ChessGame game;

        /// <summary>
        /// Stores the dimensions of the chessBoard.
        /// </summary>
        private readonly Dimensions chessBoardDimension;

        /// <summary>
        /// Stores the chessboard as a two dimensional array.
        /// </summary>
        private readonly ChessBoardField[,] board;

        /// <summary>
        /// Stores its position on the chessboard.
        /// </summary>
        private readonly Positions positionOnBoard;

        /// <summary>
        /// Stores position it needs to move.
        /// </summary>
        private readonly Distance positionToMove;

        /// <summary>
        /// Stores its targeted fields.
        /// </summary>
        private List<ChessBoardField> targetedFields;

        /// <summary>
        /// Stores a boolean indicating whether it has been beaten or not.
        /// </summary>
        private bool beaten;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessPiece"/> class.
        /// </summary>
        /// <param name="pieceTeam">
        /// Chess piece team it represents.
        /// </param>
        /// <param name="bp">
        /// Chess piece position on the board.
        /// </param>
        /// <param name="b">
        ///  Represents the chessboard as a two dimensional array.
        /// </param>
        /// <param name="g">
        /// The current chess game.
        /// </param>
        /// <param name="d">
        /// Dimensions of the chessboard.
        /// </param>
        /// <param name="type">
        /// Chess piece type.
        /// </param>
        public ChessPiece(ChessPieceTeam pieceTeam, Positions bp, ChessBoardField[,] b, ChessGame g, Dimensions d, ChessPieceType type)
        {
            this.pieceType = type;
            this.game = g;
            this.positionOnBoard = bp;
            this.team = pieceTeam;
            this.board = b;
            this.chessBoardDimension = d;
            this.targetedFields = new List<ChessBoardField>();
            this.positionToMove = new Distance();
        }

        /// <summary>
        /// Delegate for informing something has updated.
        /// </summary>
        public delegate void Update();

        /// <summary>
        /// Event to signal moving positions has been updated.
        /// </summary>
        public event Update OnMovingUpdate;

        /// <summary>
        /// Event to signal a chess piece has been beaten.
        /// </summary>
        public event Update OnBeaten;

        /// <summary>
        /// Gets the chessboard dimensions for the chess piece.
        /// </summary>
        /// <value>
        /// Gets the dimensions.
        /// </value>
        public Dimensions ChessBoardDimension
        {
            get
            {
                return this.chessBoardDimension;
            }
        }

        /// <summary>
        /// Gets the type for the chess piece.
        /// </summary>
        /// <value>
        /// Gets the chess piece type.
        /// </value>
        public ChessPieceType PieceType
        {
            get
            {
                return this.pieceType;
            }
        }

        /// <summary>
        /// Gets the current game.
        /// </summary>
        /// <value>
        /// Gets the game.
        /// </value>
        public ChessGame Game
        {
            get
            {
                return this.game;
            }
        }

        /// <summary>
        /// Gets the position to move.
        /// </summary>
        /// <value>
        /// Gets the moving position.
        /// </value>
        public Distance MovingDistance
        {
            get
            {
                return this.positionToMove;
            }
        }

        /// <summary>
        /// Gets the position on the board.
        /// </summary>
        /// <value>
        /// Gets the positions on the board.
        /// </value>
        public Positions PositionOnBoard
        {
            get
            {
                return this.positionOnBoard;
            }
        }

        /// <summary>
        /// Gets the targeted field for the chess piece.
        /// </summary>
        /// <value>
        /// Gets a list of chessboard fields (targets).
        /// </value>
        public List<ChessBoardField> TargetedFields
        {
            get
            {
                return this.targetedFields;
            }
        }

        /// <summary>
        /// Gets the current chessboard as two dimensional array.
        /// </summary>
        /// <value>
        /// Gets the board.
        /// </value>
        public ChessBoardField[,] Board
        {
            get
            {
                return this.board;
            }
        }

        /// <summary>
        /// Gets the color of the chess piece.
        /// </summary>
        /// <value>
        /// Gets the team of the chess piece.
        /// </value>
        public ChessPieceTeam Team
        {
            get
            {
                return this.team;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a chess piece has been beaten.
        /// </summary>
        /// <value>
        /// Gets or set a boolean indicating whether chess piece has been beaten..
        /// </value>
        public bool Beaten
        {
            get
            {
                return this.beaten;
            }

            set
            {
                if (value != this.beaten)
                {
                    this.beaten = value;
                    this.Game.BeatenChessPieces.AddBeatenPiece(this);
                    this.FireOnBeaten();
                }
            }
        }

        /// <summary>
        /// Selects the fields the piece could move to.
        /// </summary>
        public void Select()
        {
            IEnumerator<ChessBoardField> enumerator = this.GetSelectableFields(true).GetEnumerator();
            while (enumerator.MoveNext())
            {
                this.TargetedFields.Add(enumerator.Current);
                enumerator.Current.State = FieldState.Selected;
                enumerator.Current.FireOnSelectionUpdate();
            }
        }

        /// <summary>
        /// Gets the fields that can be selected.
        /// </summary>
        /// <param name="avoidOwnColor">
        /// If chess pieces from its own team should be avoided, if not, then it will be selected but the direction to be searched won't be continued (Need as well for king piece).
        /// </param>
        /// <returns>
        /// Returns selectable fields.
        /// </returns>
        public abstract IEnumerable<ChessBoardField> GetSelectableFields(bool avoidOwnColor);

        /// <summary>
        /// Gets the fields critical for the enemies king.
        /// </summary>
        /// <returns>
        /// Returns the fields critical for the enemies king.
        /// </returns>
        public virtual IEnumerable<ChessBoardField> GetCriticalFieldsForEnemyKing()
        {
            return this.GetSelectableFields(false);
        }

        /// <summary>
        /// Validates the field to be selected on different factors.
        /// </summary>
        /// <param name="x">
        /// X-position on the board.
        /// </param>
        /// <param name="y">
        /// Y-position on the board.
        /// </param>
        /// <param name="selectionType">
        /// Movement type of the chess piece.
        /// </param>
        /// <param name="avoidOwnColor">
        /// Should own color be avoided (Need for king piece).
        /// </param>
        /// <returns>
        /// Return a tuple indicating if field can be used and if next field can be put into validation.
        /// </returns>
        public Tuple<bool, bool> ValidateField(int x, int y, MovementType selectionType, bool avoidOwnColor)
        {
            if (x < this.ChessBoardDimension.X && y < this.ChessBoardDimension.Y && x >= 0 && y >= 0)
            {
                switch (selectionType)
                {
                    case MovementType.MoveAndKill:
                        if (x < this.ChessBoardDimension.X && y < this.ChessBoardDimension.Y && x >= 0 && y >= 0)
                        {
                            if (this.Board[y, x].Piece != null)
                            {
                                if (avoidOwnColor && this.Board[y, x].Piece.Team != this.Team)
                                {
                                    return Tuple.Create(true, false);
                                }
                                else if (!avoidOwnColor)
                                {
                                    return Tuple.Create(true, false);
                                }

                                return Tuple.Create(false, false);
                            }

                            return Tuple.Create(true, true);
                        }

                        return Tuple.Create(false, false);

                    case MovementType.Kill:
                        if (this.Board[y, x].Piece != null)
                        {
                            ChessPiece piece = this.Board[y, x].Piece;
                            if (avoidOwnColor && piece.Team != this.Team)
                            {
                                return Tuple.Create(true, false);
                            }
                            else if (!avoidOwnColor)
                            {
                                return Tuple.Create(true, false);
                            }

                            return Tuple.Create(false, false);
                        }

                        return Tuple.Create(false, false);

                    case MovementType.Move:
                        if (this.Board[y, x].Piece == null)
                        {
                            return Tuple.Create(true, true);
                        }

                        return Tuple.Create(false, false);
                }
            }

            return Tuple.Create(false, false);
        }

        /// <summary>
        /// Deselects all targeted fields.
        /// </summary>
        public void DeSelect()
        {
            if (this.TargetedFields != null)
            {
                foreach (var tf in this.TargetedFields)
                {
                    tf.State = FieldState.Not_Selected;
                    tf.FireOnSelectionUpdate();
                }

                this.TargetedFields.Clear();
            }
        }

        /// <summary>
        /// Fires an event if moving position has been updated.
        /// </summary>
        public void FireOnMovingUpdate()
        {
            if (this.OnMovingUpdate != null)
            {
                this.OnMovingUpdate.Invoke();
            }
        }

        /// <summary>
        /// Fires an event if chess piece hass been beaten.
        /// </summary>
        public void FireOnBeaten()
        {
            if (this.OnBeaten != null)
            {
                this.OnBeaten.Invoke();
            }
        }

        /// <summary>
        /// Accepts the visitor, returns automatically false, if it is not needed (Need to override).
        /// </summary>
        /// <param name="visitor">
        /// Visitor to visit, if needed.
        /// </param>
        /// <returns>
        /// Returns automatically false, if it is not needed.
        /// </returns>
        public virtual bool Accept(IVisitor visitor)
        {
            return false;
        }
    }
}