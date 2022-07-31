//// <copyright file="King.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for king piece.
//// </summary>
namespace Chess.Model.Elements.ChessPieces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using Chess.Model.Board;
    using Chess.Model.Games;

    /// <summary>
    /// Class for king chess piece.
    /// </summary>
    public class King : ChessPiece
    {
        /// <summary>
        /// Stores list of values king can move to.
        /// </summary>
        private readonly List<Tuple<int, int>> positionValues;

        /// <summary>
        /// Stores chess pieces.
        /// </summary>
        private readonly IEnumerable<ChessPiece> chessPieces;

        /// <summary>
        /// Stores chessboard fields.
        /// </summary>
        private readonly IEnumerable<ChessBoardField> fields;

        /// <summary>
        /// Initializes a new instance of the <see cref="King"/> class.
        /// </summary>
        /// <param name="color">
        /// Chess piece color it represents.
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
        /// <param name="pieces">
        /// Chess pieces, needed for calculations.
        /// </param>
        /// <param name="fields">
        ///  Chessboard fields, needed for calculations.
        /// </param>
        public King(ChessPieceTeam color, Positions bp, ChessBoardField[,] b, ChessGame g, Dimensions d, IEnumerable<ChessPiece> pieces, IEnumerable<ChessBoardField> fields) : base(color, bp, b, g, d, ChessPieceType.King)
        {
            this.fields = fields;
            this.chessPieces = pieces;
            this.positionValues = new List<Tuple<int, int>> 
            {
                Tuple.Create(1, 0),
                Tuple.Create(-1, 0),
                Tuple.Create(0, 1),
                Tuple.Create(0, -1),
                Tuple.Create(1, 1),
                Tuple.Create(-1, 1),
                Tuple.Create(1, -1),
                Tuple.Create(-1, -1)
            };
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
        public override IEnumerable<ChessBoardField> GetSelectableFields(bool avoidOwnColor)
        {
            bool myTurn = (this.Game.RoundCurrently.IsWhiteTurn && this.Team == ChessPieceTeam.White) || (!this.Game.RoundCurrently.IsWhiteTurn && this.Team == ChessPieceTeam.Black);
            IEnumerable<ChessBoardField> fields = this.GetFields(avoidOwnColor, myTurn);
            return fields;
        }

        /// <summary>
        /// Checks for checkmate.
        /// </summary>
        /// <returns>
        /// Returns a boolean indicating if a king has been checkmated.
        /// </returns>
        public bool CheckForCheckMate()
        {
            if (!this.DoFirstClauseCheck())
            {
                return false;
            }
            else
            {
                return this.DoSecondClauseCheck();
            }
        }

        /// <summary>
        /// Accepts a visitor (Used for check mate checking).
        /// </summary>
        /// <param name="visitor">
        /// Visitor to visit.
        /// </param>
        /// <returns>
        /// Returns a boolean after the visitors visit.
        /// </returns>
        public override bool Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

        /// <summary>
        /// Extra conditions needs to be checked for the king before it can get the fields to move to. Gets the selectable fields.
        /// </summary>
        /// <param name="avoidOwnColor">
        /// If own color should be avoided or not.
        /// </param>
        /// <param name="myTurn">
        /// Indicating if its king teams turn.
        /// </param>
        /// <returns>
        /// Return fields to be selected.
        /// </returns>
        private IEnumerable<ChessBoardField> GetFields(bool avoidOwnColor, bool myTurn)
        {
            Dictionary<ChessPiece, IEnumerable<ChessBoardField>> h = new Dictionary<ChessPiece, IEnumerable<ChessBoardField>>();
            if (myTurn)
            {
                h = this.GetCriticalFields();
            }

            foreach (var values in this.positionValues)
            {
                Tuple<bool, bool> t = this.ValidateField(this.PositionOnBoard.XPosition + values.Item1, this.PositionOnBoard.YPosition + values.Item2, MovementType.MoveAndKill, avoidOwnColor);
                if (t.Item1)
                {
                    if (myTurn && !h.Values.Any(fields => fields.Any(field => field == this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1])))
                    {
                        yield return this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1];
                    }
                    else if (!myTurn)
                    {
                        yield return this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1];
                    }
                }
            }
        }

        /// <summary>
        /// First clause check checks if king is in danger, if true, it looks for a friendly piece that could save him. if not it does the second clause.
        /// </summary>
        /// <returns>
        /// Returns a boolean indicating if the second clause should be performed.
        /// </returns>
        private bool DoFirstClauseCheck()
        {
            Tuple<bool, ChessPiece, IEnumerable<ChessBoardField>> t = this.CheckIfKingIsInDanger();
            bool look = true;
            if (t.Item1)
            {
                IEnumerable<ChessBoardField> enemyFieldsEnumerator = this.GetEnemyFields(t.Item3, t.Item2);
                IEnumerator<ChessPiece> piecesEnumerator = this.chessPieces.GetEnumerator();

                while (piecesEnumerator.MoveNext() && look)
                {
                    if (piecesEnumerator.Current != null && !(piecesEnumerator.Current.PieceType == ChessPieceType.King))
                    {
                        ChessPiece piece = piecesEnumerator.Current;
                        if (piece.Team == this.Team && !piece.Beaten)
                        {
                            IEnumerator<ChessBoardField> friendlyPiecesFieldsEnumerator = piece.GetSelectableFields(true).GetEnumerator();
                            while (friendlyPiecesFieldsEnumerator.MoveNext() && look)
                            {
                                if (enemyFieldsEnumerator.Any(ef => ef == friendlyPiecesFieldsEnumerator.Current) || (t.Item2.PositionOnBoard.XPosition == friendlyPiecesFieldsEnumerator.Current.PositionOnBoard.XPosition && t.Item2.PositionOnBoard.YPosition == friendlyPiecesFieldsEnumerator.Current.PositionOnBoard.YPosition))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The second clause checks if the king can move, else it checkmate.
        /// </summary>
        /// <returns>
        /// Returns a boolean indicating if king is checkmated.
        /// </returns>
        private bool DoSecondClauseCheck()
        {
            bool myTurn = (this.Game.RoundCurrently.IsWhiteTurn && this.Team == ChessPieceTeam.White) || (!this.Game.RoundCurrently.IsWhiteTurn && this.Team == ChessPieceTeam.Black);
            IEnumerable<ChessBoardField> fields = this.GetFields(true, myTurn);
            if (fields.Count() == 0 && myTurn)
            {
                foreach (var values in this.positionValues)
                {
                    Tuple<bool, bool> t = this.ValidateField(this.PositionOnBoard.XPosition + values.Item1, this.PositionOnBoard.YPosition + values.Item2, MovementType.MoveAndKill, false);
                    if (t.Item1)
                    {
                        ChessPiece piece = this.Board[this.PositionOnBoard.YPosition + values.Item2, this.PositionOnBoard.XPosition + values.Item1].Piece;
                        if (piece != null)
                        {
                            if (piece.Team != this.Team)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a specific enemies selectable field between the pos of king and the enemy.
        /// </summary>
        /// <param name="enemyFields">
        /// Selectable fields of the enemy.
        /// </param>
        /// <param name="enemy">
        /// Enemy chess piece.
        /// </param>
        /// <returns>
        /// Returns specific enemies selectable field between the pos of king and the enemy.
        /// </returns>
        private IEnumerable<ChessBoardField> GetEnemyFields(IEnumerable<ChessBoardField> enemyFields, ChessPiece enemy)
        {
            int xFactor = 1;
            int yFactor = 1;
            if (this.PositionOnBoard.XPosition > enemy.PositionOnBoard.XPosition)
            {
                xFactor = -1;
            }

            if (this.PositionOnBoard.YPosition > enemy.PositionOnBoard.YPosition)
            {
                yFactor = -1;
            }

            for (int y = this.PositionOnBoard.YPosition; y != enemy.PositionOnBoard.YPosition; y += yFactor)
            {
                for (int x = this.PositionOnBoard.XPosition; x != enemy.PositionOnBoard.XPosition; x += xFactor)
                {
                    IEnumerator<ChessBoardField> enumerator = enemyFields.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current.PositionOnBoard.XPosition == x && enumerator.Current.PositionOnBoard.YPosition == y)
                        {
                            if ((enumerator.Current.Piece != null && enumerator.Current.Piece.PieceType != ChessPieceType.King) || enumerator.Current.Piece == null)
                            {
                                yield return enumerator.Current;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the king is in danger.
        /// </summary>
        /// <returns>
        /// Returns a Tuple with a boolean indicating if king is in danger, danger by which enemy chess piece, and the enemies selectable fields.
        /// </returns>
        private Tuple<bool, ChessPiece, IEnumerable<ChessBoardField>> CheckIfKingIsInDanger()
        {
            Dictionary<ChessPiece, IEnumerable<ChessBoardField>> h = this.GetCriticalFields();
            ChessPiece piece = new Pawn(ChessPieceTeam.Black, new Positions(0, 0), new ChessBoardField[0, 0], new ChessGame(), new Dimensions(0, 0));
            IEnumerable<ChessBoardField> enumerable = new List<ChessBoardField>();
            bool danger = false;
            foreach (var dic in h)
            {
                IEnumerator<ChessBoardField> enumerator1 = dic.Value.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    if (enumerator1.Current.Piece == this)
                    {
                        danger = true;
                        piece = dic.Key;
                        enumerable = dic.Value;
                        break;
                    }
                }
            }

            IEnumerator<ChessBoardField> enumerator = this.fields.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Piece == this)
                {
                    if (danger)
                    {
                        enumerator.Current.State = FieldState.Danger;
                    }
                    else
                    {
                        enumerator.Current.State = FieldState.Not_Selected;
                    }

                    enumerator.Current.FireOnSelectionUpdate();
                    break;
                }
            }

            return Tuple.Create(danger, piece, enumerable);
        }

        /// <summary>
        /// Gets the critical fields the king cant move on.
        /// </summary>
        /// <returns>
        /// Return a dictionary with the key being an enemy chess piece and the value being the enemies chess piece selectable fields.
        /// </returns>
        private Dictionary<ChessPiece, IEnumerable<ChessBoardField>> GetCriticalFields()
        {
            Dictionary<ChessPiece, IEnumerable<ChessBoardField>> h = new Dictionary<ChessPiece, IEnumerable<ChessBoardField>>();
            IEnumerator<ChessPiece> enumerator = this.chessPieces.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    ChessPiece piece = enumerator.Current;
                    if (piece.Team != this.Team && !piece.Beaten)
                    {
                        piece.GetCriticalFieldsForEnemyKing().GetEnumerator();
                        h.Add(piece, piece.GetCriticalFieldsForEnemyKing());
                    }
                }
            }

            return h;
        }
    }
}