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
    public class clsFavorieteVensterViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsFavorieteVensterDataService _dataService;
        

        public ObservableCollection<clsFavorieteVensterModel> FavorieteVensters { get; set; }
        private readonly Dictionary<string, Type> _viewModelMapping = new();
        public Action<clsBindableBase> OpenViewModelAction { get; set; }
        
        public ICommand cmdSave { get; }
        public ICommand cmdDelete { get; }
        public ICommand cmdOpen { get; }

        public Action<string> OpenVensterAction { get; set; }

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

        public clsFavorieteVensterViewModel()
        {
            _dataService = new clsFavorieteVensterDataService();
            FavorieteVensters = new ObservableCollection<clsFavorieteVensterModel>();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdOpen = new clsCustomCommand(Execute_OpenCommand, CanExecute_OpenCommand);

            LoadFavorieteVensters();

            clsMessenger.Default.Register<string>(this, (msg) =>
                LoadFavorieteVensters());
        }


        private void LoadFavorieteVensters()
        {
            FavorieteVensters.Clear();
            var vensters = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);
            foreach (var v in vensters)
            {
                FavorieteVensters.Add(v);
            }
        }

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
        private string VindViewModelNaam(string titel)
        {
            return _displayToVmName.TryGetValue(titel, out var vmName)
                ? vmName
                : null;

        }
        private bool CanExecute_DeleteCommand(object parameter) => SelectedVenster != null;

        private void Execute_OpenCommand(object parameter)
        {
            if (parameter is clsFavorieteVensterModel item && !string.IsNullOrWhiteSpace(item.VensterNaam))
            {
                // Gebruik VindViewModelNaam om de correcte ViewModel naam te vinden
                string viewModelNaam = VindViewModelNaam(item.VensterNaam);

                if (!string.IsNullOrWhiteSpace(viewModelNaam))
                {
                    OpenVensterAction?.Invoke(viewModelNaam);
                }
                else
                {
                    System.Windows.MessageBox.Show("U heeft geen toegang tot deze pagina");
                }
            }
        }

        private bool CanExecute_OpenCommand(object parameter) => SelectedVenster != null;
    }
}
