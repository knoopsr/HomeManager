using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Security;
using HomeManager.Helpers;
using HomeManager.Common;

namespace HomeManager.ViewModel
{
    public class clsTitlePersonViewModel : clsCommonModelPropertiesBase
    {
        private clsLoginModel _loginModel;

        // Public property die toegankelijk is voor binding
        public clsLoginModel LoginModel
        {
            get { return _loginModel; }
            set
            {
                if (_loginModel != value)
                {
                    _loginModel = value;
                    OnPropertyChanged();
                }
            }
        }

       

        public clsTitlePersonViewModel()
        {
            clsMessenger.Default.Register<clsLoginModel>(this, OnUpdateTitlePersonReceived);
        }

        private void OnUpdateTitlePersonReceived(clsLoginModel model)
        {
            LoginModel = model;
        }

    }
}
