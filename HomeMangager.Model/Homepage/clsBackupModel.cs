using HomeManager.Common;

namespace HomeManager.Model.Homepage
{
    public class clsBackupModel : clsCommonModelPropertiesBase
    {
        public override string ToString()
        {
            return Path;
        }

        private string _path = string.Empty;
        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    if (_path != null)
                    {
                        IsDirty = true;
                    }
                }
                _path = value;
                OnPropertyChanged();
            }
        }

    }
}
