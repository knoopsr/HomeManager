using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Personen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsHomeVM : clsBindableBase
    {

        public ICommand cmdMenu { get; }


        private clsBindableBase _currentViewModel;
        public clsBindableBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                SetProperty(ref _currentViewModel, value);
            }
        }

        private bool _isMenuVisible = true;
        public bool IsMenuVisible
        {
            get
            {
                return _isMenuVisible;
            }
            set
            {
                _isMenuVisible = value;
                OnPropertyChange();
            }
        }

        private bool _isPersonenExpanderMenu = false;
        public bool IsPersonenExpanderMenu
        {
            get
            {
                return _isPersonenExpanderMenu;
            }
            set
            {
                _isPersonenExpanderMenu = value;
                OnPropertyChange();
            }
        }

        private bool _isBudgetExpanderMenu = false;
        public bool IsBudgetExpanderMenu
        {
            get
            {
                return _isBudgetExpanderMenu;
            }
            set
            {
                _isBudgetExpanderMenu = value;
                OnPropertyChange();
            }
        }

        private bool _isTodoExpanderMenu = false;
        public bool IsTodoExpanderMenu
        {
            get
            {
                return _isTodoExpanderMenu;
            }
            set
            {
                _isTodoExpanderMenu = value;
                OnPropertyChange();
            }
        }

        private bool _isSecurityExpanderMenu = false;
        public bool IsSecurityExpanderMenu
        {
            get
            {
                return _isSecurityExpanderMenu;
            }
            set
            {
                _isSecurityExpanderMenu = value;
                OnPropertyChange();
            }
        }

        private bool _isStickyNotesExpanderMenu = false;
        public bool IsStickyNotesExpanderMenu
        {
            get
            {
                return _isStickyNotesExpanderMenu;
            }
            set
            {
                _isStickyNotesExpanderMenu = value;
                OnPropertyChange();
            }
        }





        public clsRelayCommand<string> NavCommand { get; private set; }

        public clsHomeVM()
        {
            NavCommand = new clsRelayCommand<string>(OnNav);
                    cmdMenu = new clsCustomCommand(Execute_cmdMenu_Command, CanExecute_cmdMenu_Command);

            clsMessenger.Default.Register<clsPersoonModel>(this, OnNewPersonenReceive);
        }

        private void OnNewPersonenReceive(clsPersoonModel persoon)
        {
            if (persoon != null)
            {
                if (persoon.PersoonID == 0)
                {
                    OnNav("clsPersoonViewModel");
                }
            }

    
        }

        private void Execute_cmdMenu_Command(object? obj)
        {
            IsMenuVisible = !IsMenuVisible;

            if (!IsMenuVisible)
            {
                IsPersonenExpanderMenu = false;
                IsBudgetExpanderMenu = false;
                IsTodoExpanderMenu = false;
                IsSecurityExpanderMenu = false;
                IsStickyNotesExpanderMenu = false;
            }    
        }

        private bool CanExecute_cmdMenu_Command(object? obj)
        {
            return true;    

        }

        private void OnNav(string destination)
        {
            clsPermissionChecker permissionChecker = new clsPermissionChecker();

            if (permissionChecker.PermissionViewmodel(destination))
            {
                var type = this.GetType();
                var match = type.Assembly.GetTypes().FirstOrDefault(t => t.Name == destination);
                if (match != null)
                {
                    Type t = Type.GetType(match.ToString(), true);
                    var instance = Activator.CreateInstance(t) as clsBindableBase;
                    CurrentViewModel = instance;
                }
            }
            else
            {
                MessageBox.Show("U heeft geen toegang tot deze pagina");
            }
        }
    }
}


