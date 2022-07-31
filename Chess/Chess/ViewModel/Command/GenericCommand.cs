//// <copyright file="GenericCommand.cs" company ="FH Wr.Neustadt">
//// Copyright by Christy Kariyampalli. All rights reserved
//// </copyright>
//// <summary>
//// Class for command to perform when clicked on a button.
//// </summary>
namespace Chess.ViewModel.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// Class for commands to be executed.
    /// </summary>
    public class GenericCommand : ICommand
    {
        /// <summary>
        /// Stores the action to be performed.
        /// </summary>
        private readonly Action<object> action;

        /// <summary>
        /// Stores a bool indicating if action can be performed.
        /// </summary>
        private bool isExecutable;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCommand"/> class.
        /// </summary>
        /// <param name="a">
        /// The action to be executed.
        /// </param>
        public GenericCommand(Action<object> a)
        {
            this.action = a;
            this.isExecutable = true;
        }

        /// <summary>
        /// Event to change allowance of execution.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets a bool indicating whether the action can be performed.
        /// </summary>
        /// <param name="parameter">
        /// Parameter that could be passed.
        /// </param>
        /// <returns>
        /// Returns a bool indicating whether the action can be performed.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return this.isExecutable;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <param name="parameter">
        /// Parameter for the execution.
        /// </param>
        public void Execute(object parameter)
        {
            this.action(parameter);
        }

        /// <summary>
        /// Raises can execute changed event to allow or disallow execution.
        /// </summary>
        public void FireCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.isExecutable = !this.isExecutable;
                this.CanExecuteChanged.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
