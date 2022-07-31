//// <copyright file="ChessBoardFieldVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for chessboard field class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model;
    using Chess.ViewModel.Command;

    /// <summary>
    /// View model for the chess board field.
    /// </summary>
    public class ChessBoardFieldVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the select command, for fields to be chosen. 
        /// </summary>
        private readonly GenericCommand selectCommand;

        /// <summary>
        /// Stores the position of the background field.
        /// </summary>
        private readonly PositionsVM boardPositionVM;

        /// <summary>
        /// Stores the chess board.
        /// </summary>
        private readonly ChessBoardField field;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessBoardFieldVM"/> class.
        /// </summary>
        /// <param name="cf">
        /// The chessboard field object to represent.
        /// </param>
        public ChessBoardFieldVM(ChessBoardField cf)
        {
            this.field = cf;
            this.field.OnStateUpdate += this.UpdateOnStateChanged;
            this.boardPositionVM = new PositionsVM(this.field.PositionOnBoard);
            this.selectCommand = new GenericCommand(obj =>
            {
                this.field.DoOnSelect();
            });
        }

        /// <summary>
        /// Event to signal property value changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the state of the field.
        /// </summary>
        /// <value>
        /// Gets a enumeration of type "FieldState".
        /// </value>
        public FieldState State
        {
            get
            {
                return this.field.State;
            }
        }

        /// <summary>
        /// Gets a value indicating whether selecting of the field allowed.
        /// </summary>
        /// <value>
        /// Gets a boolean.
        /// </value>
        public bool IsSelectingAllowed
        {
            get
            {
                return this.field.IsSelectingAllowed;
            }
        }

        /// <summary>
        /// Gets the position of the field based on orientation numbers/char.
        /// </summary>
        /// <value>
        /// Gets a string.
        /// </value>
        public string Position
        {
            get
            {
                return this.field.Position;
            }
        }

        /// <summary>
        /// Gets the selecting command, for piece when they are selected.
        /// </summary>
        /// <value>
        /// Gets a "GenericCommand" object.
        /// </value>
        public GenericCommand SelectCommand
        {
            get
            {
                return this.selectCommand;
            }
        }

        /// <summary>
        /// Gets position which can be converted into color.
        /// </summary>
        /// <value>
        /// Gets a "PositionsVM" object.
        /// </value>
        public PositionsVM PositionOnBoard
        {
            get
            {
                return this.boardPositionVM;
            }
        }

        /// <summary>
        /// Updates properties important for its state.
        /// </summary>
        private void UpdateOnStateChanged()
        {
            this.FireOnPropertyUpdate(nameof(this.IsSelectingAllowed));
            this.FireOnPropertyUpdate(nameof(this.State));
        }

        /// <summary>
        /// Fires on property update changed.
        /// </summary>
        /// <param name="name">
        /// Name of the property to be updated.
        /// </param>
        private void FireOnPropertyUpdate(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}