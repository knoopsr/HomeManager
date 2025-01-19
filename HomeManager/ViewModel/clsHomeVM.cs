using HomeManager.Common;
using HomeManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeManager.ViewModel
{
    public class clsHomeVM : clsBindableBase
    {
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

        public clsRelayCommand<string> NavCommand { get; private set; }

        public clsHomeVM()
        {
            NavCommand = new clsRelayCommand<string>(OnNav);
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


