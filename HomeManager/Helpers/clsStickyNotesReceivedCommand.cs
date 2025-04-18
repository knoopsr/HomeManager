using HomeManager.ViewModel.StickyNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;

namespace HomeManager.Helpers
{
    public class clsStickyNotesReceivedCommand : clsCommonCommandBase
    {
        private readonly clsStickyNotesViewModel _stickyNotesViewModel;

        public clsStickyNotesReceivedCommand(clsStickyNotesViewModel stickyNotesViewModel)
        {
            _stickyNotesViewModel = stickyNotesViewModel;
        }

        public override void Execute(object parameter)
        {
            // Re-order here ?
        }
    }
}
