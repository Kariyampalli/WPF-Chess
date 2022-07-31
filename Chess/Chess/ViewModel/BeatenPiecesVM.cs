//// <copyright file="BeatenPiecesVM.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View model for beaten pieces class.
//// </summary>
namespace Chess.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Chess.Model.Board;
    using Chess.Model.Games;

    /// <summary>
    /// View mode Class for beaten pieces model.
    /// </summary>
    public class BeatenPiecesVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the beaten piece model it represents.
        /// </summary>
        private readonly BeatenPieces beatenPieces;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeatenPiecesVM"/> class.
        /// </summary>
        /// <param name="bp">
        /// The beaten piece object to be represented.
        /// </param>
        public BeatenPiecesVM(BeatenPieces bp)
        {
            this.beatenPieces = bp;
            this.beatenPieces.UpdateBeatenList += this.DoOnBeatePiecesListUpdate;
        }

        /// <summary>
        /// Event to signal property value changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the beaten black pieces.
        /// </summary>
        /// <value>
        /// Gets a "ObservableCollection".
        /// </value>
        public ObservableCollection<ChessPieceVM> BeatenBlackPieces
        {
            get
            {
                return new ObservableCollection<ChessPieceVM>(this.beatenPieces.BeatenBlackPieces.Select(p => new ChessPieceVM(p)));
            }
        }

        /// <summary>
        /// Gets the beaten white pieces.
        /// </summary>
        /// <value>
        /// Gets a "ObservableCollection".
        /// </value>
        public ObservableCollection<ChessPieceVM> BeatenWhitePieces
        {
            get
            {
                return new ObservableCollection<ChessPieceVM>(this.beatenPieces.BeatenWhitePieces.Select(p => new ChessPieceVM(p)));
            }
        }

        /// <summary>
        /// Gets the beaten pieces background fields.
        /// </summary>
        /// <value>
        /// Gets a list.
        /// </value>
        public List<PositionsVM> BackgroundFields
        {
            get
            {
                return PositionsCreator.Create(new Dimensions(2, 8)).Select(p => new PositionsVM(p)).ToList();
            }
        }

        /// <summary>
        /// Updates one of the beaten pieces list.
        /// </summary>
        /// <param name="updateBeatenBlackPieces">
        /// Boolean indicating which one of the beaten piece list needs to be updated.
        /// </param>
        private void DoOnBeatePiecesListUpdate(bool updateBeatenBlackPieces)
        {
            if (updateBeatenBlackPieces)
            {
                this.FireOnPropertyChanged(nameof(this.BeatenBlackPieces));
            }
            else
            {
                this.FireOnPropertyChanged(nameof(this.BeatenWhitePieces));
            }
        }

        /// <summary>
        /// Raises an event to notify a property value has changed.
        /// </summary>
        /// <param name="name">
        /// Name of the property to be updated.
        /// </param>
        private void FireOnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
