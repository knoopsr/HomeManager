using FluentEmail.Core;
using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Personen;
using HomeManager.Services;
using HomeManager.View;
using HomeManager.View.Personen;
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
        public ICommand cmdNewEmailAdressen { get; set; }
        public ICommand cmdEditEmailAdressen { get; set; }
        public ICommand cmdEditPersoon { get; set; }
        public ICommand cmdEditNotities { get; set; }
        public ICommand cmdNewTelefoonNummers { get; set; }
        public ICommand cmdEditTelefoonNummers { get; set; }
        public ICommand cmdDeleteEmailAdressen { get; set; }
        public ICommand cmdDeletePersoon { get; set; }
        public ICommand cmdNewAdres { get; set; }
        public ICommand cmdDeleteAdres { get; set; }
        public ICommand cmdDeleteTelefoonNummer { get; set; }
        public ICommand cmdEditAdres { get; set; }
        public ICommand cmdNewNotities { get; set; }
        public ICommand cmdDeleteNotitie { get; set; }
        public ICommand cmdSendEmail { get; set; }





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

        private clsEmailAdressenModel _selectedEmailAdres;
        public clsEmailAdressenModel SelectedEmailAdres
        {
            get { return _selectedEmailAdres; }
            set
            {
                _selectedEmailAdres = value;
                OnPropertyChanged();
            }
        }

        private clsAdressenModel _selectedAdres;
        public clsAdressenModel SelectedAdres
        {
            get { return _selectedAdres; }
            set
            {
                _selectedAdres = value;
                OnPropertyChanged();
            }
        }

        private clsTelefoonNummersModel _selectedTelefoonNummers;
        public clsTelefoonNummersModel SelectedTelefoonNummers
        {
            get { return _selectedTelefoonNummers; }
            set
            {
                _selectedTelefoonNummers = value;
                OnPropertyChanged();
            }
        }

        private clsNotitiesModel _selectedNotities;
        public clsNotitiesModel SelectedNotities
        {
            get { return _selectedNotities; }
            set
            {
                _selectedNotities = value;
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
            cmdNewEmailAdressen = new clsCustomCommand(New_EmailAdressen, CanNewEmailAdressen);
            cmdEditEmailAdressen = new clsCustomCommand(Edit_EmailAdressen, CanEditEmailAdressen);
            cmdNewAdres = new clsCustomCommand(New_Adres, CanNewAdres);
            cmdEditAdres = new clsCustomCommand(Edit_Adressen, CanEditAdres);
            cmdEditPersoon = new clsCustomCommand(Edit_Persoon, CanEditPersoon);

            cmdNewNotities = new clsCustomCommand(New_Notities, CanNewNotities);
            cmdEditNotities = new clsCustomCommand(Edit_Notities, CanEditNotities);
            cmdNewTelefoonNummers = new clsCustomCommand(New_TelefoonNummers, CanNewTelefoonNummers);
            cmdEditTelefoonNummers = new clsCustomCommand(Edit_TelefoonNummers, CanEditTelefoonNummers);

            cmdSendEmail = new clsCustomCommand(Edit_SendEmail, CanSendEmail);

            cmdDeleteEmailAdressen = new clsCustomCommand(Delete_EmailAdres, CanExecute_Delete_EmailAdres);
            cmdDeletePersoon = new clsCustomCommand(Delete_Persoon, CanExecute_Delete_Persoon);
            cmdDeleteAdres = new clsCustomCommand(Delete_Adres, CanExecute_Delete_Adres);
            cmdDeleteTelefoonNummer = new clsCustomCommand(Delete_TelefoonNummer, CanExecute_Delete_TelefoonNummer);
            cmdDeleteNotitie = new clsCustomCommand(Delete_Notitie, CanExecute_Delete_Notitie);
            //Messenger
            clsMessenger.Default.Register<clsUpdateListMessages>(this, OnUpdateListMessageReceived);


            //de searchcommand
            SearchCommand = new RelayCommand(FilterUsers);
            //de clear SearchCommand
            ClearSearchCommand = new RelayCommand(ClearSearch);

            //load data
            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
        }

        private bool CanEditNotities(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("115");
        }

        private void New_Notities(object? obj)
        {
            //OpenNotities(obj);

            clsNotitiesModel notities = new clsNotitiesModel()
            {
                PersoonID = MijnSelectedItem.PersoonID,
                NotitieID = 0,
                Onderwerp = string.Empty,
                Notitie = string.Empty,
            };
            clsMessenger.Default.Send<clsNotitiesModel>(notities);
            _DialogService.ShowDialog(new ucNotities(), "Notities");
        }

        private bool CanNewNotities(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("114");

        }

        private bool CanEditTelefoonNummers(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("112");
        }

        private bool CanNewTelefoonNummers(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("111");
        }

        private void New_TelefoonNummers(object? obj)
        {
            //OpenTelefoonNummer(obj);

            clsTelefoonNummersModel telefoonnummer = new clsTelefoonNummersModel()
            {
                PersoonID = MijnSelectedItem.PersoonID,
                TelefoonNummerID = 0,
                TelefoonTypeID = 0,
                TelefoonNummer = string.Empty
            };
            clsMessenger.Default.Send<clsTelefoonNummersModel>(telefoonnummer);
            _DialogService.ShowDialog(new ucTelefoonNummers(), "TelefoonNummers");
        }    
            
        

        private bool CanEditAdres(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("109");
        }

        private bool CanNewAdres(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("108");
        }

        private void New_Adres(object obj)
        {
            //OpenAdres(obj);

            clsAdressenModel adres = new clsAdressenModel()
            {
                PersoonID = MijnSelectedItem.PersoonID,
                AdresID = 0,
                GemeenteID = 0,
                FunctieID = 0,
                Straat = string.Empty,
                Nummer = string.Empty,
            };
            clsMessenger.Default.Send<clsAdressenModel>(adres);
            _DialogService.ShowDialog(new ucAdressen(), "Adressen");         
        }
        private void New_EmailAdressen(object obj)
        {
            //OpenEmailAdressen(obj);

            clsEmailAdressenModel email = new clsEmailAdressenModel()
            {
                PersoonID = MijnSelectedItem.PersoonID,
                EmailAdresID = 0,
                Emailadres = string.Empty,
                EmailTypeID = 0
            };
            clsMessenger.Default.Send<clsEmailAdressenModel>(email);
            _DialogService.ShowDialog(new ucEmailAdressen(), "EmailAdressen");
        }

        private bool CanSendEmail(object arg)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("107");
        }

        private bool CanNewEmailAdressen(object arg)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("104");
        }



        private bool CanEditEmailAdressen(object arg)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("105");
        }

        private bool CanEditPersoon(object arg)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("102");
        }

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();

            //collectie voor de filter
            FilteredCollectie = new ObservableCollection<clsPersoonModel>(MijnCollectie);
        }

        private void OnUpdateListMessageReceived(clsUpdateListMessages obj)
        {
            // Refresh de gegevens
            LoadData();
            _DialogService.CloseDialog(); // Sluit het dialoogvenster als dat nodig is
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


        private void Edit_TelefoonNummers(object? obj)
        {
            OpenTelefoonNummer(obj);
        }

        private void OpenTelefoonNummer(object? obj)
        {
            if (obj != null)
            {
                if (obj is clsPersoonModel)
                {
                    clsTelefoonNummersModel telefoonnummer = new clsTelefoonNummersModel()
                    {
                        PersoonID = MijnSelectedItem.PersoonID,
                        TelefoonNummerID = 0,
                        TelefoonTypeID = 0,
                        TelefoonNummer = string.Empty
                    };
                    clsMessenger.Default.Send<clsTelefoonNummersModel>(telefoonnummer);
                }
                else
                {
                    if (MijnSelectedItem != null)
                    {
                        if (obj is clsTelefoonNummersModel)
                        {
                            clsTelefoonNummersModel telefoonnummer = obj as clsTelefoonNummersModel;
                            clsMessenger.Default.Send<clsTelefoonNummersModel>(telefoonnummer);
                        }
                    }
                }
                _DialogService.ShowDialog(new ucTelefoonNummers(), "TelefoonNummers");
            }
        }

        private void Edit_Notities(object? obj)
        {
            OpenNotities(obj);
        }

        private void OpenNotities(object? obj)
        {
            if (obj != null)
            {
                if (obj is clsPersoonModel)
                {
                    clsNotitiesModel notities = new clsNotitiesModel()
                    {
                        PersoonID = MijnSelectedItem.PersoonID,
                        NotitieID = 0,
                        Onderwerp = string.Empty,
                        Notitie = string.Empty,
                    };
                    clsMessenger.Default.Send<clsNotitiesModel>(notities);
                }
                else
                {
                    if (MijnSelectedItem != null)
                    {
                        if (obj is clsNotitiesModel)
                        {
                            clsNotitiesModel notities = obj as clsNotitiesModel;
                            clsMessenger.Default.Send<clsNotitiesModel>(notities);
                        }
                    }
                }
                _DialogService.ShowDialog(new ucNotities(), "Notities");
            }
        }

        private void Edit_Adressen(object? obj)
        {
            OpenAdres(obj);
        }

        private void OpenAdres(object? obj)
        {
            if (obj != null)
            {


                if (obj is clsPersoonModel)
                {
                    clsAdressenModel adres = new clsAdressenModel()
                    {
                        PersoonID = MijnSelectedItem.PersoonID,
                        AdresID = 0,
                        GemeenteID = 0,
                        FunctieID = 0,
                        Straat = string.Empty,
                        Nummer = string.Empty,
                    };
                    clsMessenger.Default.Send<clsAdressenModel>(adres);
                }
                else
                {
                    if (MijnSelectedItem != null)
                    {
                        if (obj is clsAdressenModel)
                        {
                            clsAdressenModel adres = obj as clsAdressenModel;
                            clsMessenger.Default.Send<clsAdressenModel>(adres);
                        }
                    }
                }
                _DialogService.ShowDialog(new ucAdressen(), "Adressen");
            }
        }

        private void Edit_Persoon(object? obj)
        {
            if (obj != null)
            {
                if (obj is clsPersoonModel)
                {
                    clsPersoonModel persoon = obj as clsPersoonModel;
                    clsMessenger.Default.Send<clsPersoonModel>(persoon);
                }
            }
            else
            {
                if (MijnSelectedItem != null)
                {
                    if (obj is clsPersoonModel)
                    {
                        // Stuur het geselecteerde persoon object naar de messenger
                        clsPersoonModel persoon = obj as clsPersoonModel;
                        clsMessenger.Default.Send<clsPersoonModel>(persoon);
                    }
                }
            }

            _DialogService.ShowDialog(new ucPersoon(), "Persoon");
            
        }


        private void Edit_EmailAdressen(object obj)
        {
            OpenEmailAdressen(obj);
        }

        private void OpenEmailAdressen(object obj)
        {
            if (obj != null)
            {
                if (obj is clsPersoonModel)
                {
                    clsEmailAdressenModel email = new clsEmailAdressenModel()
                    {
                        PersoonID = MijnSelectedItem.PersoonID,
                        EmailAdresID = 0,
                        Emailadres = string.Empty,
                        EmailTypeID = 0
                    };
                    clsMessenger.Default.Send<clsEmailAdressenModel>(email);
                }
                else
                {
                    if (MijnSelectedItem != null)
                    {
                        if (obj is clsEmailAdressenModel)
                        {
                            clsEmailAdressenModel email = obj as clsEmailAdressenModel;
                            clsMessenger.Default.Send<clsEmailAdressenModel>(email);
                        }
                    }
                }
                _DialogService.ShowDialog(new ucEmailAdressen(), "EmailAdressen");
            }
        }

        private void Edit_SendEmail(object obj)
        {
            if (obj != null)
            {
                if (obj is clsEmailAdressenModel)
                {
                    clsEmailAdressenModel email = obj as clsEmailAdressenModel;
                    clsEmailVerzendenModel sendemail = new clsEmailVerzendenModel()
                    {
                        PersoonID = MijnSelectedItem.PersoonID,
                        Ontvanger = email.Emailadres
                    };
                    clsMessenger.Default.Send<clsEmailVerzendenModel>(sendemail);
                }
            }
            else
            {
                if (SelectedEmailAdres != null)
                {
                    clsEmailVerzendenModel sendemail = new clsEmailVerzendenModel()
                    {
                        PersoonID = MijnSelectedItem.PersoonID,
                        Ontvanger = SelectedEmailAdres.Emailadres
                    };
                    clsMessenger.Default.Send<clsEmailVerzendenModel>(sendemail);
                }
            }
            _DialogService.ShowDialog(new ucEmailVerzenden(), "Email Verzenden");
        }

        private void Execute_Save_Command(object? obj)
        {
            OpslaanCommando();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            return false;
        }

        private void Execute_New_Command(object? obj)
        {
            clsPersoonModel p = new clsPersoonModel
            {
                PersoonID = 0,
                Naam = string.Empty,
                Voornaam = string.Empty,
                Foto = null,
                Geboortedatum = DateOnly.FromDateTime(DateTime.Now),
                IsApplicationUser = null
            };
            clsMessenger.Default.Send<clsPersoonModel>(p);
        }

        private bool CanExecute_New_Command(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("101");
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



        //Delete commands
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


        private void Delete_Adres(object? obj)
        {
            if (obj is clsAdressenModel selectedAdres)
            {
                if (MessageBox.Show($"Wil je het adres '{selectedAdres}' verwijderen?",
                    "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnServiceAdressen.Delete(selectedAdres))
                    {
                        AdressenCollectie.Remove(selectedAdres);
                        MessageBox.Show("Het adres is succesvol verwijderd.", "Succes");
                    }
                    else
                    {
                        MessageBox.Show("Er is een fout opgetreden bij het verwijderen van het adres.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecteer een geldig adres om te verwijderen.", "Fout");
            }
        }
        private bool CanExecute_Delete_Adres(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("110");
        }


        private void Delete_EmailAdres(object? obj)
        {
            if (obj is clsEmailAdressenModel selectedEmail)
            {
                if (MessageBox.Show($"Wil je het e-mailadres '{selectedEmail}' verwijderen?",
                    "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnServiceEmailAdressen.Delete(selectedEmail))
                    {
                        EmailAdressenCollectie.Remove(selectedEmail);
                        MessageBox.Show("Het e-mailadres is succesvol verwijderd.", "Succes");
                    }
                    else
                    {
                        MessageBox.Show("Er is een fout opgetreden bij het verwijderen van het e-mailadres.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecteer een geldig e-mailadres om te verwijderen.", "Fout");
            }
        }

        private bool CanExecute_Delete_EmailAdres(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("106");
        }



        private void Delete_Persoon(object? obj)
        {
            if (MessageBox.Show("Wil je " + MijnSelectedItem + " verwijderen?", "Vewijderen?", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes) if (MijnSelectedItem != null)
                {
                    if (MijnService.Delete(MijnSelectedItem))
                    {
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Error?", MijnSelectedItem.ErrorBoodschap);
                    }
                }
        }
        private bool CanExecute_Delete_Persoon(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            if (!permissionChecker.HasPermission("103"))
            {
                return false;
            }
            if (MijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        private void Delete_TelefoonNummer(object? obj)
        {
            if (obj is clsTelefoonNummersModel selectedTelefoonNummer)
            {
                if (MessageBox.Show($"Wil je het telefoonnummer '{selectedTelefoonNummer}' verwijderen?",
                    "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnServiceTelefoonNummers.Delete(selectedTelefoonNummer))
                    {
                        TelefoonNummersCollectie.Remove(selectedTelefoonNummer);
                        MessageBox.Show("Het telefoonnummer is succesvol verwijderd.", "Succes");
                    }
                    else
                    {
                        MessageBox.Show("Er is een fout opgetreden bij het verwijderen van het telefoonnummer.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecteer een geldig telefoonnummer om te verwijderen.", "Fout");
            }
        }
        private bool CanExecute_Delete_TelefoonNummer(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("113");
        }

        private bool CanExecute_Delete_Notitie(object? obj)
        {
            clsPermissionChecker permissionChecker = new();
            return permissionChecker.HasPermission("116");
        }

        private void Delete_Notitie(object? obj)
        {
            if (obj is clsNotitiesModel selectedNotities)
            {
                if (MessageBox.Show($"Wil je het notitie '{selectedNotities}' verwijderen?",
                    "Verwijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (MijnNotitiesService.Delete(selectedNotities))
                    {
                        NotitiesCollectie.Remove(selectedNotities);
                        MessageBox.Show("Het notitie is succesvol verwijderd.", "Succes");
                    }
                    else
                    {
                        MessageBox.Show("Er is een fout opgetreden bij het verwijderen van het notitie.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecteer een geldig notitie om te verwijderen.", "Fout");
            }
        }



        // Searchbar data
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterUsers();
            }
        }

        //RelayCommand toevoegen voor de RelayCommand filters
        public class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;
            private Action<object> executeBold;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public RelayCommand(Action<object> executeBold)
            {
                this.executeBold = executeBold;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

            public void Execute(object parameter) => _execute();

            public event EventHandler CanExecuteChanged
            {
                add => CommandManager.RequerySuggested += value;
                remove => CommandManager.RequerySuggested -= value;
            }
        }

        //ObservableCollection voor de filter personen
        private ObservableCollection<clsPersoonModel> _filteredCollectie;
        public ObservableCollection<clsPersoonModel> FilteredCollectie
        {
            get => _filteredCollectie;
            set
            {
                _filteredCollectie = value;
                OnPropertyChanged();
            }
        }

        //ICommand voor mijn filters
        public ICommand SearchCommand { get; private set; }
        public ICommand ClearSearchCommand { get; private set; }

        // Methode voor filter uit te voeren
        private void FilterUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                //niet in de zoekbalk
                FilteredCollectie = new ObservableCollection<clsPersoonModel>(MijnCollectie);
            }
            else
            {
                // Filteren op naam of voornaam
                //stackflow
                var filtered = MijnCollectie
                    .Where(p => p.Naam.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                 p.Voornaam.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredCollectie = new ObservableCollection<clsPersoonModel>(filtered);
            }
        }



        // Methode voor de zoekbalk te clearen
        private void ClearSearch()
        {
            SearchText = string.Empty;
            FilterUsers();
        }
    }
}


