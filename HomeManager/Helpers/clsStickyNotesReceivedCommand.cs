using HomeManager.ViewModel.StickyNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Helpers
{
    /// <summary>
    /// Command class that handles actions related to received sticky notes.
    /// This class is responsible for executing logic related to sticky notes when triggered.
    /// </summary>
    public class clsStickyNotesReceivedCommand : clsCommonCommandBase
    {
        private readonly clsStickyNotesViewModel _stickyNotesViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="clsStickyNotesReceivedCommand"/> class.
        /// </summary>
        /// <param name="stickyNotesViewModel">The view model associated with sticky notes, used to interact with sticky notes data.</param>
        public clsStickyNotesReceivedCommand(clsStickyNotesViewModel stickyNotesViewModel)
        {
            _stickyNotesViewModel = stickyNotesViewModel;
        }

        /// <summary>
        /// Executes the command logic for handling received sticky notes.
        /// This method will define the specific behavior of the command, such as re-ordering sticky notes.
        /// </summary>
        /// <param name="parameter">The parameter passed to the command, which could be used for further processing.</param>
        public override void Execute(object parameter)
        {
            // Re-order logic for received sticky notes COULD ALSO be implemented here.
        }
    }
}
