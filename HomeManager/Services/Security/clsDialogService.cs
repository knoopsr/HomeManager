using HomeManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeManager.Services.Security
{
    public class clsDialogService
    {
        Window _NewPassWordView = null;


        public void ShowNewPassWordView()
        {

                _NewPassWordView = new winNewPassWord();
                _NewPassWordView.Show();

        }

        public void CloseNewPassWordView()
        {
            if (_NewPassWordView != null)
            {
                _NewPassWordView.Close();
            }
        }
    }
}
