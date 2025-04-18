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

            RegistreerViewModels();
            LoadFavorieteVensters();

            
            clsMessenger.Default.Register<string>(this, (msg) =>
                LoadFavorieteVensters());
        }
        private void RegistreerViewModels()
        {
            var viewModelTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.StartsWith("cls") && t.Name.EndsWith("ViewModel") && t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var vmType in viewModelTypes)
            {
                string key = vmType.Name.Replace("cls", "").Replace("ViewModel", "");
                _viewModelMapping[key] = vmType;
            }

            Debug.WriteLine(" Geregistreerde ViewModels: " + string.Join(", ", _viewModelMapping.Keys));
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

        private bool CanExecute_DeleteCommand(object parameter) => SelectedVenster != null;

        private void Execute_OpenCommand(object parameter)
        {
            if (parameter is clsFavorieteVensterModel item && !string.IsNullOrWhiteSpace(item.VensterNaam))
            {
                Debug.WriteLine($" Open venster: {item.VensterNaam}");

                if (_viewModelMapping.TryGetValue(item.VensterNaam, out var vmType))
                {
                    var vm = Activator.CreateInstance(vmType) as clsBindableBase;
                    if (vm != null)
                    {
                        OpenViewModelAction?.Invoke(vm);
                        Debug.WriteLine($" Geopend via favoriet: {vmType.FullName}");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Kan venster niet openen.");
                    Debug.WriteLine($" Geen ViewModel gevonden voor: {item.VensterNaam}");
                }
            }
        }

        private bool CanExecute_OpenCommand(object parameter) => SelectedVenster != null;
    }
}
