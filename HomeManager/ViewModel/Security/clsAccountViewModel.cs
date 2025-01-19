using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using HomeManager.Model.Personen;
using System.Security.Cryptography;
using HomeManager.Mail;

namespace HomeManager.ViewModel
{
    public class clsAccountViewModel : clsCommonModelPropertiesBase
    {
        clsAccountDataService MijnService;
        clsPersoonDataService MijnPersoonService;
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }

        private ObservableCollection<clsAccountModel> _mijnCollectie;
        public ObservableCollection<clsAccountModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsPersoonModel> _mijnPersoonCollectie;
        public ObservableCollection<clsPersoonModel> MijnPersoonCollectie
        {
            get { return _mijnPersoonCollectie; }
            set
            {
                _mijnPersoonCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsAccountModel _mijnSelectedItem;
        public clsAccountModel MijnSelectedItem
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
                }
                _mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        private async void OpslaanCommando()
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
                  
                        //ObservableCollection<clsEmailAdressenModel> emailAdressen = new ObservableCollection<clsEmailAdressenModel>();
                        //clsEmailAdressenDataService emailAdressenService = new clsEmailAdressenDataService();

                        //emailAdressen = emailAdressenService.GetById(_mijnSelectedItem.PersoonID);

                        //string[] emailAdressenArray = new string[emailAdressen.Count];
                        //foreach (var email in emailAdressen)
                        //{

                        //    clsMailModel mailModel = new clsMailModel
                        //    {
                        //        MailToName = "HomeManager Admin",
                        //        MailFromEmail = "admin@HomeManager.be",
                        //        MailToEmail = "johndoe@example.com",
                        //        Subject = "Backup Gemaakt",
                        //        Body = "U kan het volgende wachtwoord gebruiken om in te loggen" + Environment.NewLine + "Wachtwoord: " + _mijnSelectedItem.Wachtwoord
                        //    };

                        //    bool emailVerzonden = await clsMail.SendEmail(mailModel);

                        //    if (emailVerzonden)
                        //    {
                        //        emailAdressenArray.Append(email.Emailadres);
                        //    }
                        //};

                        //MessageBox.Show("E-mail succesvol verzonden naar " + emailAdressenArray);

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

        public clsAccountViewModel()
        {
            MijnService = new clsAccountDataService();
            MijnPersoonService = new clsPersoonDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);

            LoadData();

            MijnSelectedItem = MijnService.GetFirst();
            MijnPersoonCollectie = MijnPersoonService.GetAllApplicationUser();
        }

        private async void Execute_Save_Command(object? obj)
        {
            OpslaanCommando();
            MijnPersoonCollectie = MijnPersoonService.GetAllApplicationUser();
        }

        private bool CanExecute_Save_Command(object? obj)
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
        private string GenereerWachtwoord(int lengte)
        {
            const string geldigTekens = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            char[] wachtwoord = new char[lengte];
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[lengte];
                rng.GetBytes(randomBytes);
                for (int i = 0; i < lengte; i++)
                {
                    wachtwoord[i] = geldigTekens[randomBytes[i] % geldigTekens.Length];
                }
            }
            return new string(wachtwoord);
        }

        private void Execute_New_Command(object? obj)
        {

            // Stel dat PersoonId de identifier is voor personen in beide collecties.
            var gefilterdePersoonCollectie = MijnPersoonCollectie
                .Where(persoon => !MijnCollectie.Any(mijn => mijn.PersoonID == persoon.PersoonID))
                .ToList();
            MijnPersoonCollectie.Clear();

            foreach (var persoon in gefilterdePersoonCollectie)
            {
                MijnPersoonCollectie.Add(persoon);
            }




            clsAccountModel _itemToInsert = new clsAccountModel()
            {
                AccountID = 0,
                RolID = 0,
                Wachtwoord = GenereerWachtwoord(10),
                Login = string.Empty,
                PersoonID = 0,
                IsNew = true,
                IsLock = false,
                CountFailLogins = 0,

            };
            MijnSelectedItem = _itemToInsert;
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            MijnPersoonCollectie = MijnPersoonService.GetAllApplicationUser();
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

        private bool CanExecute_SelectionChangedCommand(object obj)
        {
            return true;
        }
    }
}
