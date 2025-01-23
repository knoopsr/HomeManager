using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Agenda.ViewModel
{
    public class clsVMLocator
    {
        public clsAgendaViewModel AgendaViewModel
        {
            get
            {
                return new clsAgendaViewModel();
            }
        }


    }
}
