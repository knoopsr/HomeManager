namespace HomeManager.Common
{
    /// <summary>
    /// Basisklasse voor models die ondersteuning bieden voor statusbewaking, foutmeldingen en UI-gerelateerde eigenschappen.
    /// </summary>
    public class clsCommonModelPropertiesBase : clsObservable
    {
        #region Status Eigenschappen

        /// <summary>
        /// Geeft aan of het object gewijzigd is sinds de laatste opslag of reset.
        /// </summary>
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                OnPropertyChanged();
            }
        }
        private bool _isDirty = false;

        /// <summary>
        /// Index voor selectie in bijvoorbeeld een ComboBox.
        /// </summary>
        public int MijnSelectedIndex
        {
            get => _mijnSelectedIndex;
            set
            {
                _mijnSelectedIndex = value;
                OnPropertyChanged();
            }
        }
        private int _mijnSelectedIndex;

        #endregion

        #region Visibility

        /// <summary>
        /// Integer voor binding aan Visibility converters.
        /// </summary>
        public int MyVisibility
        {
            get => _myVisibility;
            set
            {
                _myVisibility = value;
                OnPropertyChanged();
            }
        }
        private int _myVisibility;

        /// <summary>
        /// Tegengestelde waarde van visibility voor inverse bindings.
        /// </summary>
        public int MyVisibility_Contrary
        {
            get => _myVisibility_Contrary;
            set
            {
                _myVisibility_Contrary = value;
                OnPropertyChanged();
            }
        }
        private int _myVisibility_Contrary = 1;

        #endregion

        #region UI Focus

        /// <summary>
        /// Bepaalt of het element gefocust moet worden.
        /// </summary>
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                _isFocused = value;
                OnPropertyChanged();
            }
        }
        private bool _isFocused = false;

        /// <summary>
        /// Wordt gefocust direct na aanmaak van een nieuw item.
        /// </summary>
        public bool IsFocusedAfterNew
        {
            get => _isFocusedAfterNew;
            set
            {
                _isFocusedAfterNew = value;
                OnPropertyChanged();
            }
        }
        private bool _isFocusedAfterNew = false;

        #endregion

        #region Concurrency / Database

        /// <summary>
        /// Wordt gebruikt voor rowversion / concurrency controle.
        /// </summary>
        public object ControlField
        {
            get => _controlField;
            set
            {
                _controlField = value;
                OnPropertyChanged();
            }
        }
        private object _controlField;

        #endregion

        #region Validatie en Foutmeldingen

        /// <summary>
        /// Boodschap die getoond kan worden bij foutafhandeling.
        /// </summary>
        public string ErrorBoodschap { get; set; }

        /// <summary>
        /// Lijst van validatiefouten of andere foutmeldingen.
        /// </summary>
        public List<string> ErrorList
        {
            get => _errorList;
            set => _errorList = value;
        }
        private List<string> _errorList = new List<string>();

        /// <summary>
        /// Retourneert "NOK" indien er fouten zijn, anders null.
        /// Wordt vaak gebruikt in bindings voor validatie.
        /// </summary>
        public string Error => ErrorList.Count > 0 ? "NOK" : null;

        #endregion
    }
}
