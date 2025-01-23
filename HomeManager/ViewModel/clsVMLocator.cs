using HomeManager.Agenda.ViewModel;
using HomeManager.ViewModel.AddOns;
using HomeManager.ViewModel.StickyNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel
{
    public class clsVMLocator
    {


        private static clsPersoonViewModel _persoonViewModel = new clsPersoonViewModel();

        public clsPersoonViewModel PersoonViewModel
        {
            get
            {
                return _persoonViewModel;
            }
        }
        public clsFunctieViewModel FunctieViewModel
        {
            get
            {
                return new clsFunctieViewModel();
            }
        }

        public clsEmailTypeViewModel EmailTypeViewModel
        {
            get
            {
                return new clsEmailTypeViewModel();
            }
        }

        public clsTelefoonTypeViewModel TelefoonTypeViewModel
        {
            get
            {
                return new clsTelefoonTypeViewModel();
            }
        }

        public clsLandViewModel LandViewModel
        {
            get
            {
                return new clsLandViewModel();
            }
        }

        public clsProvincieViewModel ProvincieViewModel
        {
            get
            {
                return new clsProvincieViewModel();
            }
        }

        public clsGemeenteViewModel GemeenteViewModel
        {
            get
            {
                return new clsGemeenteViewModel();
            }
        }
        private static clsEmailAdressenViewModel _EmailAdressen = new clsEmailAdressenViewModel();
        public clsEmailAdressenViewModel EmailAdressenViewModel
        {
            get
            {
                return _EmailAdressen;
                //return new clsEmailAdressenViewModel();
            }
        }

        public clsPersonenViewModel PersonenViewModel
        {
            get
            {
                return new clsPersonenViewModel();
            }
        }

        private static clsAdressenViewModel _Adressen = new clsAdressenViewModel();

        public clsAdressenViewModel AdressenViewModel
        {
            get
            {
                return _Adressen;
                //return new clsAdressenViewModel();
            }
        }


        private static clsTelefoonNummersViewModel _TelefoonNummers = new clsTelefoonNummersViewModel();
        public clsTelefoonNummersViewModel TelefoonNummersViewModel
        {
            get
            {
                return _TelefoonNummers;
                //return new clsTelefoonNummersViewModel();
            }
        }

        private static clsNotitiesViewModel _Notities = new clsNotitiesViewModel();
        public clsNotitiesViewModel NotitiesViewModel
        {
            get
            {
                return _Notities;
                //return new clsNotitiesViewModel();
            }
        }

        #region Security
        public clsTitlePersonViewModel TitlePersonViewModel
        {
            get
            {
                return new clsTitlePersonViewModel();
            }
        }


        public clsRechtenViewModel RechtenViewModel
        {
            get
            {
                return new clsRechtenViewModel();
            }
        }

        public clsCredentialGroupViewModel CredentialGroupViewModel
        {
            get
            {
                return new clsCredentialGroupViewModel();
            }
        }

        public clsAccountViewModel AccountViewModel
        {
            get
            {
                return new clsAccountViewModel();
            }
        }
        private clsCredentialManagementViewModel _credentialManagementViewModel = new clsCredentialManagementViewModel();
        public clsCredentialManagementViewModel CredentialManagementViewModel
        {
            get
            {
                return _credentialManagementViewModel;
            }
        }
        
        public clsLogin LoginViewModel
        {
            get
            {
                return new clsLogin();
            }
        }

        private static clsNewPassViewModel _newPassViewModel = new clsNewPassViewModel();
        public clsNewPassViewModel NewPassViewModel
        {
            get
            {
                return _newPassViewModel;
            }
        }

        #endregion

        #region Agenda

        private static clsAgendaViewModel _agendaViewModel = new clsAgendaViewModel();
        public clsAgendaViewModel AgendaViewModel
        {
            get
            {
                return _agendaViewModel;
            }
        }


        #endregion

        #region Notes

        private static clsNoteViewModel _noteViewModel = new clsNoteViewModel();
        public clsNoteViewModel NoteViewModel
        {
            get
            {
                return _noteViewModel;
            }
        }
        #endregion

        private static clsHomeVM _homeViewModel = new clsHomeVM();
        public clsHomeVM HomeViewModel
        {
            get
            {
                return _homeViewModel;
            }
        }

        private static clsComputersViewModel _computersViewModel = new clsComputersViewModel();

        public clsComputersViewModel ComputersViewModel
        {
            get
            {
                return _computersViewModel;
            }
        }

    }
}
