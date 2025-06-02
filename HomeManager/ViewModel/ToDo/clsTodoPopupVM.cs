using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.DataService.ToDo;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using HomeManager.Model.Todo;
//using HomeManager.Model.ToDo;
using HomeManager.View;
using HomeManager.Mail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using HomeManager.Model.Mail;
using System.Diagnostics;
using HomeManager.DataService.Personen;
using HomeManager.Model.Personen;
using HomeManager.Model.Budget;

namespace HomeManager.ViewModel
{
    public class clsTodoPopupVM : clsCommonModelPropertiesBase
    {
        clsTodoPopupDataService MijnService;
        clsAccountDataService MijnserviceGebruikers;
        clsCollectiesDataService MijnServiceCollecties;

        private bool NewStatus = false;
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand OpenCollectiesCommand { get; }
        public ICommand OpenAccountCommand { get; }
        //public ICommand SubmitEmailCommand { get; set; }

        private bool isSendMail = false;
        // **Fix: Declareer VerzendenService**
        private clsEmailAdressenDataService VerzendenService;

        private ObservableCollection<clsEmailAdressenModel> _MijnVerzenderEmailAdres;
        public ObservableCollection<clsEmailAdressenModel> MijnVerzenderEmailAdres
        {
            get { return _MijnVerzenderEmailAdres; }
            set
            {
                _MijnVerzenderEmailAdres = value;
                OnPropertyChanged();
            }
        }

        // **Fix: Declareer Ontvanger**
        private string _ontvanger;
        public string Ontvanger
        {
            get { return _ontvanger; }
            set
            {
                _ontvanger = value;
                OnPropertyChanged();
            }
        }


        public clsTodoPopupVM()
        {
            MijnService = new clsTodoPopupDataService();
            MijnserviceGebruikers = new clsAccountDataService();
            MijnServiceCollecties = new clsCollectiesDataService();

            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            //SubmitEmailCommand = new clsCustomCommand(Execute_SubmitEmail, CanExecute_SubmitEmail);

            //clsMessenger.Default.Register<clsTodoPopupM>(this, OnCollectiesReceived);
            OpenCollectiesCommand = new clsRelayCommand<object>(OpenCollecties);
            OpenAccountCommand = new clsRelayCommand<object>(OpenAccount);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst() ?? new clsTodoPopupM();
            VerzendenService = new clsEmailAdressenDataService();
            MijnVerzenderEmailAdres = new ObservableCollection<clsEmailAdressenModel>();
            if (MijnVerzenderEmailAdres.Count > 0)
            {
                MijnSelectedItem = MijnVerzenderEmailAdres[0];
            }

            clsMessenger.Default.Register<clsCollectiesM>(this, OnCollectiesReceived);
            clsMessenger.Default.Register<clsTodoPopupM>(this, OnUpdateListMessageReceived);

            // Initialize filtering
            FilteredMijnCollectie = CollectionViewSource.GetDefaultView(MijnCollectie);
            FilteredMijnCollectie.Filter = FilterBySelectedCollectie;
        }



        private void OnUpdateListMessageReceived(clsTodoPopupM obj)
        {
            //int GebruikerId = obj.GebruikerID;
            //Ontvanger = GebruikerId;
            int ontvanger = GebruikerID;
            MijnVerzenderEmailAdres = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            
        }

        private ICollectionView _filteredMijnCollectie;
        public ICollectionView FilteredMijnCollectie
        {
            get { return _filteredMijnCollectie; }
            set
            {
                _filteredMijnCollectie = value;
                OnPropertyChanged(nameof(FilteredMijnCollectie));
            }
        }

        private bool FilterBySelectedCollectie(object item)
        {
            if (item is clsTodoPopupM todoItem)
            {
                return MijnSelectedCollectieItem != null &&
                       todoItem.TodoCollectieID == MijnSelectedCollectieItem.ToDoCollectieID;
            }
            return false;
        }

        private void OnCollectiesReceived(clsCollectiesM obj)
        {
            MijnSelectedCollectieItem = obj;
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.TodoCollectieID = obj.ToDoCollectieID;
                OnPropertyChanged(nameof(MijnSelectedItem));
            }
        }

        public void SetDefaultCollectieItem(clsCollectiesM defaultItem)
        {
            MijnSelectedCollectieItem = defaultItem;
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.TodoCollectieID = defaultItem.ToDoCollectieID;
                OnPropertyChanged(nameof(MijnSelectedItem));
            }
        }

        private void OpenCollecties(object parameter)
        {
            // Logic to open ucCollecties.xaml
            var collectiesWindow = new Window
            {
                Content = new ucCollecties(),
                Title = "Collecties",
                Width = 800,
                Height = 450
            };
            collectiesWindow.ShowDialog();
        }

        private void OpenAccount(object parameter)
        {
            // Logic to open ucAccount.xaml
            var accountWindow = new Window
            {
                Content = new ucAccount(),
                Title = "Account",
                Width = 800,
                Height = 450
            };
            accountWindow.ShowDialog();
        }

        private ObservableCollection<clsTodoPopupM> _MijnCollectie;
        public ObservableCollection<clsTodoPopupM> MijnCollectie
        {
            get
            {
                return _MijnCollectie;
            }
            set
            {
                _MijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private clsTodoPopupM _MijnSelectedItem;
        public clsTodoPopupM MijnSelectedItem
        {
            get
            {
                return _MijnSelectedItem;
            }
            set
            {

                if (value != null)
                {
                    if (_MijnSelectedItem != null && _MijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("wil je " + _MijnSelectedItem + " Opslaan? ", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                    // Koppel de juiste gebruiker aan MijnSelectedGebruiker
                    if (value.GebruikerID > 0 && MijnCollectieGebruikers != null)
                    {
                        MijnSelectedGebruiker = MijnCollectieGebruikers.FirstOrDefault(g => g.AccountID == value.GebruikerID);
                    }
                }
                _MijnSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private void OpslaanCommando()
        {
            if (MijnSelectedItem != null)
            {
                // Zorg ervoor dat TodoCollectieID en Volgorde niet null zijn
                if (MijnSelectedItem.TodoCollectieID == null)
                {
                    MessageBox.Show("TodoCollectieID mag niet null zijn.", "Error");
                    return;
                }

                if (MijnSelectedItem.Volgorde == null)
                {
                    MijnSelectedItem.Volgorde = 0; // Of een andere standaardwaarde
                }

                if (NewStatus)
                {
                    if (MijnService.Insert(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
                else
                {
                    if (MijnService.Update(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
                    }
                }
            }
        }

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
            MijnCollectieGebruikers = MijnserviceGebruikers.GetAll();
            MijnCollectieCollecties = MijnServiceCollecties.GetAll();
        }

        private bool CanExecute_CloseCommand(object obj)
        {
            return true;
        }
        private void Execute_CloseCommand(object obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {

                if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
                {
                    if (MessageBox.Show(MijnSelectedItem.ToString().ToUpper() +
                        " is nog niet opgeslagen, wil je opslaan?", "Opslaan of sluiten?",
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

            // clsMessenger.Default.Send<clsUpdateListMessages>(new clsUpdateListMessages());
        }

        private bool CanExecute_CancelCommand(object obj)
        {
            return NewStatus;
        }

        private void Execute_CancelCommand(object obj)
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

        private bool CanExecute_NewCommand(object obj)
        {
            return !NewStatus;
        }

        public void Execute_NewCommand(object obj)
        {
            clsTodoPopupM ItemToInsert = new clsTodoPopupM()
            {
                TodoID = 0,
                Onderwerp = string.Empty,
                Detail = string.Empty,
                //GebruikerID = 0, // Of een standaardwaarde als dat nodig is
                //GebruikerID = clsLoginModel.Instance.AccountID,
                GebruikerID = MijnSelectedGebruiker?.AccountID ?? clsLoginModel.Instance.AccountID, // Gebruik de geselecteerde gebruiker of de ingelogde gebruiker als fallback
                Belangrijk = false,
                TodoCollectieID = MijnSelectedCollectieItem?.ToDoCollectieID, // Of een standaardwaarde als dat nodig is
                TodoCategorieID = null, // Of een standaardwaarde als dat nodig is
                TodoColorID = null, // Of een standaardwaarde als dat nodig is
                IsKlaar = false,
                Volgorde = null // Of een standaardwaarde als dat nodig is
            };
            MijnSelectedGebruiker = MijnCollectieGebruikers?.FirstOrDefault(g => g.AccountID == ItemToInsert.GebruikerID);
            ItemToInsert.IsDirty = true;

            MijnSelectedItem = ItemToInsert;

            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_DeleteCommand(object obj)
        {
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

        private void Execute_DeleteCommand(object obj)
        {
            if (MessageBox.Show("wil je " + MijnSelectedItem + " verwijderen?", "Vewijderen?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

            }
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

        private bool CanExecute_SaveCommand(object obj)
        {
            if (MijnSelectedItem != null &&
            MijnSelectedItem.Error == null &&
            MijnSelectedItem.IsDirty == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool _sendEmailOnSave = true;
        public bool SendEmailOnSave
        {
            get { return _sendEmailOnSave; }
            set { _sendEmailOnSave = value; OnPropertyChanged(); }
        }

        private void Execute_SaveCommand(object obj)
        {
            // Forceer binding update van ComboBox (indien nodig)
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(Application.Current.MainWindow), null);

            OpslaanCommando();
            Execute_SubmitEmail();
        }


        private int _gebruikerID;
        public int GebruikerID
        {
            get
            {
                return _gebruikerID;
            }
            set
            {
                _gebruikerID = clsLoginModel.Instance.PersoonID;
            }

        }

        private ObservableCollection<clsAccountModel> _MijnCollectieGebruikers;
        public ObservableCollection<clsAccountModel> MijnCollectieGebruikers
        {
            get
            {
                return _MijnCollectieGebruikers;
            }
            set
            {
                _MijnCollectieGebruikers = value;
                OnPropertyChanged();
            }
        }

        //Trigger voor het veranderen van de gebruiker in de popup.
        private clsAccountModel _MijnSelectedGebruiker;
        public clsAccountModel MijnSelectedGebruiker
        {
            get
            {
                return _MijnSelectedGebruiker;
            }
            set
            {
                _MijnSelectedGebruiker = value;

                if (value != null && MijnSelectedItem != null)
                {
                    MijnSelectedItem.GebruikerID = value.AccountID;
                }

                OnPropertyChanged();
            }
        }

        //PaperCut.exe runnen  -> http://localhost:5000/ 
        private async void Execute_SubmitEmail()
        {
            // Fallback: selecteer de gebruiker van het todo-item als MijnSelectedGebruiker null is
            if (MijnSelectedGebruiker == null && MijnSelectedItem != null && MijnCollectieGebruikers != null)
            {
                MijnSelectedGebruiker = MijnCollectieGebruikers
                    .FirstOrDefault(g => g.AccountID == MijnSelectedItem.GebruikerID);
            }

            bool bestaatGebruiker = MijnSelectedGebruiker != null
                && !string.IsNullOrWhiteSpace(MijnSelectedGebruiker.Login);

            if (bestaatGebruiker)
            {
                isSendMail = true;

                // Ophalen van het e-mailadres van de verzender via de dataservice
                var verzenderEmailAdressen = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);
                string mailFromEmail = verzenderEmailAdressen?.FirstOrDefault()?.Emailadres ?? "noreply@homemanager.com";

                // Haal het e-mailadres van de ontvanger op via de dataservice
                var ontvangerEmailAdressen = VerzendenService.GetByPersoonID(MijnSelectedGebruiker.PersoonID);
                string mailToEmail = ontvangerEmailAdressen?.FirstOrDefault()?.Emailadres
                    ?? $"{MijnSelectedGebruiker.Login}@homemanager.com"; // fallback indien geen e-mailadres gevonden

                var mailModel = new clsMailModel
                {
                    MailFromEmail = mailFromEmail,
                    MailToEmail = mailToEmail,
                    MailToName = MijnSelectedGebruiker.Login,
                    Subject = MijnSelectedItem.Onderwerp,
                    Body = MijnSelectedItem.Detail
                };
                bool emailVerzonden = await clsMail.SendEmail(mailModel);
                MessageBox.Show("Email verzonden");
            }
            isSendMail = false;
        }


        private ObservableCollection<clsCollectiesM> _MijnCollectieCollecties;
        public ObservableCollection<clsCollectiesM> MijnCollectieCollecties
        {
            get
            {
                return _MijnCollectieCollecties;
            }
            set
            {
                _MijnCollectieCollecties = value;
                OnPropertyChanged();
            }
        }

        private clsCollectiesM _MijnSelectedCollectieItem;
        public clsCollectiesM MijnSelectedCollectieItem
        {
            get { return _MijnSelectedCollectieItem; }
            set
            {
                _MijnSelectedCollectieItem = value;
                OnPropertyChanged(nameof(MijnSelectedCollectieItem));

                // Refresh the filter when the selected collection changes
                FilteredMijnCollectie?.Refresh();
            }
        }
    }
}
