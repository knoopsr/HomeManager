using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Security;


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
            LoginModel = clsLoginModel.Instance;



        }

    }
}
