//// <copyright file="ChessBoard.xaml.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// View for the entire game to get its data.
//// </summary>
namespace Chess.View
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes; 
    using Chess.Shared;
    using Chess.ViewModel;

    /// <summary>
    /// Class for chess board view.
    /// </summary>
    public partial class ChessBoard : UserControl
    {
        /// <summary>
        /// Stores the views game view model.
        /// </summary>
        private readonly GameVM gameVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChessBoard"/> class.
        /// </summary>
        public ChessBoard()
        {
            try
            {
                this.InitializeComponent();
                this.gameVM = new GameVM();
                this.gameVM.OnDisplayMessage += this.DisplayMessage;
                this.DataContext = this.gameVM;
            }
            catch (Exception ex)
            {
                this.DisplayMessage(this, new OnDisplayGameMessageEventArgs($"An unexpected error occured during setting up or playing the chess game!\n{ex.Message}", GameMessageType.Exception));
            }
        }

        /// <summary>
        /// Displays a message on the screen.
        /// </summary>
        /// <param name="sender">
        /// Caller of the method.
        /// </param>
        /// <param name="args">
        /// Arguments for the method. Contains message and its type.
        /// </param>
        private void DisplayMessage(object sender, OnDisplayGameMessageEventArgs args)
        {
            MessageBox.Show(args.Message);
        }
    }
}
