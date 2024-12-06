using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Security
{
    public class clsMailModel : clsCommonModelPropertiesBase
    {
        private string _mailToEmail;
        public string MailToEmail
        {
            get { return _mailToEmail; }
            set
            {
                _mailToEmail = value;
                OnPropertyChanged();
            }
        }

        private string _mailToName;
        public string MailToName
        {
            get { return _mailToName; }
            set
            {
                _mailToName = value;
                OnPropertyChanged();
            }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set
            {
                _subject = value;
                OnPropertyChanged();
            }
        }

        private string _body;
        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                OnPropertyChanged();
            }
        }

    }
}
