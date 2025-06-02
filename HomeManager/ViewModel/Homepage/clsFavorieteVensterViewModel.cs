using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeManager.ViewModel.Homepage
{
    /// <summary>
    /// ViewModel voor het beheren van de favoriete vensters van een gebruiker.
    /// De gebruiker kan snelkoppelingen naar bepaalde schermen opslaan en openen.
    /// </summary>
    public class clsFavorieteVensterViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsFavorieteVensterDataService _dataService;

        /// <summary>
        /// Lijst met alle favoriete vensters van de huidige gebruiker.
        /// </summary>
        public ObservableCollection<clsFavorieteVensterModel> FavorieteVensters { get; set; }

        /// <summary>
        /// Mapping van display-namen naar ViewModel-namen (hardcoded).
        /// Wordt gebruikt om bij openen het juiste ViewModel aan te roepen.
        /// </summary>
        private static readonly Dictionary<string, string> _displayToVmName =
            new(StringComparer.OrdinalIgnoreCase)
            {
                // Personen
                { "Persoon",            "clsPersoonViewModel" },
                { "Personen",           "clsPersonenViewModel" },
                { "Functies",           "clsFunctieViewModel" },
                { "EmailType",          "clsEmailTypeViewModel" },
                { "TelefoonType",       "clsTelefoonTypeViewModel" },
                { "Land",               "clsLandViewModel" },
                { "Provincie",          "clsProvincieViewModel" },
                { "Gemeente",           "clsGemeenteViewModel" },

                // Budget
                { "Categorie",          "clsCategorieViewModel" },
                { "Transacties",        "clsTransactieViewModel" },
                { "Domiciliëring",      "clsDomicilieringViewModel" },
                { "Begunstigden",       "clsBegunstigdenViewModel" },
                { "Frequentie",         "clsFrequentieViewModel" },

                // Todo
                { "Todo Collectie",     "clsCollectiesVM" },
                { "Todo Categoriën",    "clsCategorieënVM" },
                { "Todo Kleuren",       "clsKleurenVM" },

                // Security
                { "Account beheren",    "clsAccountViewModel" },
                { "Wachtwoorden",       "clsCredentialManagementViewModel" },
                { "Wachtwoordengroep",  "clsCredentialGroupViewModel" },
                { "Rollen",             "clsRechtenViewModel" }
            };

        /// <summary>
        /// Wordt gebruikt om een venster (ViewModel) te openen, wordt extern gekoppeld aan de MainWindow.
        /// </summary>
        public Action<string> OpenVensterAction { get; set; }

        /// <summary>
        /// Commands voor de verschillende acties in de UI.
        /// </summary>
        public ICommand cmdSave { get; }
        public ICommand cmdDelete { get; }
        public ICommand cmdOpen { get; }

        /// <summary>
        /// Het geselecteerde venster in de UI.
        /// </summary>
        private clsFavorieteVensterModel _selectedVenster;
        public clsFavorieteVensterModel SelectedVenster
        {
            get => _selectedVenster;
            set
            {
                _selectedVenster = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor: initialisatie van commando's en laden van de favoriete vensters.
        /// </summary>
        public clsFavorieteVensterViewModel()
        {
            _dataService = new clsFavorieteVensterDataService();
            FavorieteVensters = new ObservableCollection<clsFavorieteVensterModel>();

            // Commando's instellen
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdOpen = new clsCustomCommand(Execute_OpenCommand, CanExecute_OpenCommand);

            // Laad de data
            LoadFavorieteVensters();

            // Abonneer op meldingen om de lijst te verversen
            clsMessenger.Default.Register<string>(this, (msg) => LoadFavorieteVensters());
        }

        /// <summary>
        /// Laadt de favoriete vensters uit de database.
        /// </summary>
        private void LoadFavorieteVensters()
        {
            FavorieteVensters.Clear();
            var vensters = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);
            foreach (var v in vensters)
            {
                FavorieteVensters.Add(v);
            }
        }

        /// <summary>
        /// Command voor het opslaan van een favoriet venster.
        /// </summary>
        private void Execute_SaveCommand(object parameter)
        {
            if (parameter is string categorieNaam)
            {
                var venster = new clsFavorieteVensterModel
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    VensterNaam = categorieNaam
                };

                if (_dataService.Insert(venster))
                {
                    FavorieteVensters.Add(venster);
                    clsMessenger.Default.Send("RefreshFavorieteVensters");
                }
                else
                {
                    System.Windows.MessageBox.Show(" Kan venster niet toevoegen.");
                }
            }
        }

        private bool CanExecute_SaveCommand(object parameter) => true;

        /// <summary>
        /// Command voor het verwijderen van een favoriet venster.
        /// </summary>
        private void Execute_DeleteCommand(object parameter)
        {
            if (parameter is clsFavorieteVensterModel item)
            {
                if (_dataService.Delete(item))
                {
                    FavorieteVensters.Remove(item);
                    clsMessenger.Default.Send("RefreshFavorieteVensters");
                }
                else
                {
                    System.Windows.MessageBox.Show(" Kan venster niet verwijderen.");
                }
            }
        }

        private bool CanExecute_DeleteCommand(object parameter) => SelectedVenster != null;

        /// <summary>
        /// Command voor het openen van het geselecteerde venster.
        /// Bepaalt het juiste ViewModel op basis van mapping en roept extern OpenVensterAction aan.
        /// </summary>
        private void Execute_OpenCommand(object parameter)
        {
            if (parameter is clsFavorieteVensterModel item && !string.IsNullOrWhiteSpace(item.VensterNaam))
            {
                // Zoek de ViewModel naam op
                string viewModelNaam = VindViewModelNaam(item.VensterNaam);

                if (!string.IsNullOrWhiteSpace(viewModelNaam))
                {
                    // Open het venster via de MainWindow
                    OpenVensterAction?.Invoke(viewModelNaam);
                }
                else
                {
                    System.Windows.MessageBox.Show("U heeft geen toegang tot deze pagina");
                }
            }
        }

        private bool CanExecute_OpenCommand(object parameter) => SelectedVenster != null;

        /// <summary>
        /// Hulp-methode om op basis van de display-naam de ViewModel-naam te vinden.
        /// </summary>
        private string VindViewModelNaam(string titel)
        {
            return _displayToVmName.TryGetValue(titel, out var vmName)
                ? vmName
                : null;
        }
    }
}
