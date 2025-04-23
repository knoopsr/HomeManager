using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Homepage
{
    public class clsFotoCarouselModel : clsCommonModelPropertiesBase
    {
        private int _folderID;
        public int FolderID
        {
            get
            {
                return _folderID;
            }
            set
            {
                _folderID = value;
                OnPropertyChanged();
            }
        }

        private int _accountID;
        public int AccountID
        {
            get
            {
                return _accountID;
            }
            set
            {
                _accountID = value;
                OnPropertyChanged();
            }
        }
        private string _folderPath;
        public string FolderPath
        {
            get
            {
                return _folderPath;
            }
            set
            {
                _folderPath = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? ChangedOn { get; set; }
    }
}
