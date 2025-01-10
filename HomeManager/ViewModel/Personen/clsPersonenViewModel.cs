using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Personen;
using HomeManager.Services;
using HomeManager.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsPersonenViewModel : clsCommonModelPropertiesBase
    {
        clsPersoonDataService MijnService;
        clsPersoonDataService MijnServicePersoon;
        clsEmailAdressenDataService MijnServiceEmailAdressen;
        clsAdressenDataService MijnServiceAdressen;
        clsNotitiesDataService MijnNotitiesService;
        clsTelefoonNummersDataService MijnServiceTelefoonNummers;

        private clsDialogService _DialogService;

        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdEditEmailAdressen { get; set; }
        public ICommand cmdEditAdressen { get; set; }
        public ICommand cmdEditPersoon { get; set; }
        public ICommand cmdEditNotities { get; set; }
        public ICommand cmdEditTelefoonNummers { get; set; }
        public ICommand cmdDeleteEmailAdressen { get; set; }



        private ObservableCollection<clsPersoonModel> _mijnCollectie;
        public ObservableCollection<clsPersoonModel> MijnCollectie
        {
            get
            {
                return _mijnCollectie;
            }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsPersoonModel> _persoonCollectie;
        public ObservableCollection<clsPersoonModel> PersoonCollectie
        {
            get
            {
                return _persoonCollectie;
            }
            set
            {
                _persoonCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsNotitiesModel> _notitiesCollectie;
        public ObservableCollection<clsNotitiesModel> NotitiesCollectie
        {
            get
            {
                return _notitiesCollectie;
            }
            set
            {
                _notitiesCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsEmailAdressenModel> _emailAdressenCollectie;
        public ObservableCollection<clsEmailAdressenModel> EmailAdressenCollectie
        {
            get
            {
                return _emailAdressenCollectie;
            }
            set
            {
                _emailAdressenCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsAdressenModel> _adressenCollectie;
        public ObservableCollection<clsAdressenModel> AdressenCollectie
        {
            get
            {
                return _adressenCollectie;
            }
            set
            {
                _adressenCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsTelefoonNummersModel> _telefoonnummersCollectie;
        public ObservableCollection<clsTelefoonNummersModel> TelefoonNummersCollectie
        {
            get
            {
                return _telefoonnummersCollectie;
            }
            set
            {
                _telefoonnummersCollectie = value;
                OnPropertyChanged();
            }
        }






        private clsPersoonModel _mijnSelectedItem;
        public clsPersoonModel MijnSelectedItem
        {
            get { return _mijnSelectedItem; }
            set
            {
                if (value != null)
                {
                    if (_mijnSelectedItem != null && _mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wilt je " + _mijnSelectedItem + " opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                    NotitiesCollectie = MijnNotitiesService.GetByPersoonID(value.PersoonID);
                    EmailAdressenCollectie = MijnServiceEmailAdressen.GetByPersoonID(value.PersoonID);
                    AdressenCollectie = MijnServiceAdressen.GetByPersoonID(value.PersoonID);
                    TelefoonNummersCollectie = MijnServiceTelefoonNummers.GetByPersoonID(value.PersoonID);
                    PersoonCollectie = MijnServicePersoon.GetByPersoonID(value.PersoonID);
                }

                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }



        private void OpslaanCommando()
        {
            if (_mijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnService.Insert(_mijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(_mijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        public clsPersonenViewModel()
        {
            MijnService = new clsPersoonDataService();
            MijnServiceEmailAdressen = new clsEmailAdressenDataService();
            MijnServiceAdressen = new clsAdressenDataService();
            MijnNotitiesService = new clsNotitiesDataService();
            MijnServiceTelefoonNummers = new clsTelefoonNummersDataService();
            MijnServicePersoon = new clsPersoonDataService();


            _DialogService = new clsDialogService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            //Messages
            cmdEditEmailAdressen = new clsCustomCommand(Edit_EmailAdressen, CanExecute_Edit_EmailAdressen);
            cmdEditAdressen = new clsCustomCommand(Edit_Adressen, CanExecute_Edit_Adressen);
            cmdEditPersoon = new clsCustomCommand(Edit_Persoon, CanExecute_Edit_Persoon);
            cmdEditNotities = new clsCustomCommand(Edit_Notities, CanExecute_Edit_Notities);
            cmdEditTelefoonNummers = new clsCustomCommand(Edit_TelefoonNummers, CanExecute_Edit_TelefoonNummers);
            cmdDeleteEmailAdressen = new clsCustomCommand(Delete_EmailAdres, CanExecute_Delete_EmailAdres);

            //messenger
            clsMessenger.Default.Register<clsUpdateListMessages>(this, OnUpdateListMessageReceived);


            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        private void Delete_EmailAdres(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Delete_EmailAdres(object? obj)
        {
            return true;
        }



        private bool CanExecute_Edit_TelefoonNummers(object? obj)
        {
            return true;
        }

        private void Edit_TelefoonNummers(object? obj)
        {
            int persoonID = MijnSelectedItem.PersoonID;

            if (MijnSelectedItem != null)
            {
                clsTelefoonNummersModel nummers = new clsTelefoonNummersModel()
                {
                    PersoonID = MijnSelectedItem.PersoonID,
                    TelefoonTypeID = 0,
                    TelefoonNummer = string.Empty
                };
                clsMessenger.Default.Send<clsTelefoonNummersModel>(nummers);
                _DialogService.ShowDialog(new ucTelefoonNummers(), "TelefoonNummers");
            }
        }

        private bool CanExecute_Edit_Notities(object? obj)
        {
            return true;
        }

        private void Edit_Notities(object? obj)
        {
            int persoonID = MijnSelectedItem.PersoonID;

            if (MijnSelectedItem != null)
            {
                clsNotitiesModel notities = new clsNotitiesModel()
                {
                    PersoonID = MijnSelectedItem.PersoonID,
                    Onderwerp = string.Empty,
                    Notitie = string.Empty
                };
                clsMessenger.Default.Send<clsNotitiesModel>(notities);
                _DialogService.ShowDialog(new ucNotities(), "Notities");
            }
        }

        private bool CanExecute_Edit_Persoon(object? obj)
        {
            return true;
        }




        private bool CanExecute_Edit_Adressen(object? obj)
        {
            return true;
        }

        private void Edit_Adressen(object? obj)
        {
            //_DialogService.ShowDialog(new ucAdressen(), "Adressen");

            int persoonID = MijnSelectedItem.PersoonID;

            if (MijnSelectedItem != null)
            {
                clsAdressenModel adres = new clsAdressenModel()
                {
                    PersoonID = MijnSelectedItem.PersoonID,
                    Straat = string.Empty,
                    Nummer = string.Empty,
                    GemeenteID = 0,
                    FunctieID = 0
                };
                clsMessenger.Default.Send<clsAdressenModel>(adres);
                _DialogService.ShowDialog(new ucAdressen(), "Adressen");
            }
        }

        private void OnUpdateListMessageReceived(clsUpdateListMessages obj)
        {
            //refresh
            LoadData();
            _DialogService.CloseDialog();
        }

        private bool CanExecute_Edit_EmailAdressen(object? obj)
        {
            return true;
        }

        private void Edit_Persoon(object? obj)
        {
            //_DialogService.ShowDialog(new ucPersoon(), "Persoon");
            int persoonID = MijnSelectedItem.PersoonID;

            if (MijnSelectedItem != null)
            {

                clsPersoonModel p = new clsPersoonModel
                {
                    PersoonID = MijnSelectedItem.PersoonID,
                    Naam = string.Empty,
                    Voornaam = string.Empty,
                    Foto = MijnSelectedItem.Foto,
                    Geboortedatum = MijnSelectedItem.Geboortedatum,
                    IsApplicationUser = MijnSelectedItem.IsApplicationUser
                };
                clsMessenger.Default.Send<clsPersoonModel>(p);
                _DialogService.ShowDialog(new ucPersoon(), "Persoon");
            }
        }

        private void Edit_EmailAdressen(object? obj)
        {

            //_DialogService.ShowDialog(new ucEmailAdressen(), "EmailAdressen");

            int persoonID = MijnSelectedItem.PersoonID;

            if (MijnSelectedItem != null)
            {
                clsEmailAdressenModel model = new clsEmailAdressenModel()
                {
                    PersoonID = MijnSelectedItem.PersoonID,
                    EmailAdresID = 0,
                    Emailadres = string.Empty,
                    EmailTypeID = 0
                };
                clsMessenger.Default.Send<clsEmailAdressenModel>(model);
                _DialogService.ShowDialog(new ucEmailAdressen(), "EmailAdressen");
            }
        }

        private void Execute_Save_Command(object? obj)
        {
            OpslaanCommando();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            return false;
        }

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show(
                "Wil je " + MijnSelectedItem + " verwijderen?",
                "Verwijderen?",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnSelectedItem != null)
                {
                    if (MijnService.Delete(MijnSelectedItem))
                    {
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                    }
                }
            }
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return false;
        }

        private void Execute_New_Command(object? obj)
        {
            clsMessenger.Default.Send<clsNewPersoonMessage>(new clsNewPersoonMessage());
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            MijnSelectedItem = MijnService.GetFirst();
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MijnSelectedIndex = 0;
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            }
            NewStatus = false;
            IsFocusedAfterNew = false;
            IsFocused = true;
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            return NewStatus;
        }

        private void Execute_Close_Command(object? obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {
                if (MijnSelectedItem != null && MijnSelectedItem.IsDirty == true && MijnSelectedItem.Error == null)
                {
                    if (MessageBox.Show(
                        MijnSelectedItem.ToString().ToUpper() + " is nog niet opgeslagen, wil je opslaan?",
                        "Opslaan of sluiten?",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        OpslaanCommando();
                        clsHomeVM vm2 = (clsHomeVM)HomeWindow.DataContext;
                        vm2.CurrentViewModel = null;
                    }
                }
                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }
    }
}


