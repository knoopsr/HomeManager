using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsRechtenViewModel : clsCommonModelPropertiesBase
    {
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdIsChecked { get; set; }


        private bool NewStatus = false;
   


            
        clsRechtenDataService MijnRechtenService;
        clsRechtenCatogorieDataService MijnRechtenCatogorieService;
        clsRollenDataService MijnRollenService;


        private ObservableCollection<clsRechtenModel> _mijnRechtenCollectie;
        public ObservableCollection<clsRechtenModel> MijnRechtenCollectie
        {
            get { return _mijnRechtenCollectie; }
            set
            {
                _mijnRechtenCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsRechtenCatogorieModel> _mijnRechtenCatogorieCollectie;
        public ObservableCollection<clsRechtenCatogorieModel> MijnRechtenCatogorieCollectie
        {
            get
            {
                return _mijnRechtenCatogorieCollectie;
            }
            set
            {
                _mijnRechtenCatogorieCollectie = value;
                OnPropertyChanged();

                foreach (var item in _mijnRechtenCatogorieCollectie)
                {
                    foreach (var item2 in _mijnRechtenCollectie)
                    {
                        if (item.RechtenCatogorieID == item2.RechtenCatogorieID)
                        {
                            item.Rechten.Add(item2);
                        }
                    }
                }



            }
        }

        private ObservableCollection<clsRollenModel> _mijnRollenCollectie;
        public ObservableCollection<clsRollenModel> MijnRollenCollectie
        {
            get { return _mijnRollenCollectie; }
            set
            {
                _mijnRollenCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsRollenModel _mijnSelectedItem;
        public clsRollenModel MijnSelectedItem
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
                if (_mijnSelectedItem != null)
                {
                    if (_mijnSelectedItem.RolName == "Admin")
                    {
                       MijnSelectedItem.IsTextBoxEnabled = false;
                    }
                    else
                    {
                        MijnSelectedItem.IsTextBoxEnabled = true;
                    }


                    SetCheckedChildren(_mijnSelectedItem.Rechten);
                }
            }
        }

        private void LoadData()
        {
            SetCheckedChildren("");
            MijnRollenCollectie = MijnRollenService.GetAll();
        }
        private void OpslaanCommando()
        {
            if (_mijnSelectedItem != null)
            {
                if (NewStatus)
                {
                    if (MijnRollenService.Insert(MijnSelectedItem))
                    {
                        MijnSelectedItem.IsDirty = false;
                        MijnSelectedItem.MijnSelectedIndex = 0;
                        MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                        NewStatus = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show(_mijnSelectedItem.ErrorBoodschap, "Error??");
                    }
                }
                else
                {
                    if (MijnRollenService.Update(MijnSelectedItem))
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



        public clsRechtenViewModel()
        {
            MijnRechtenService = new clsRechtenDataService();
            MijnRechtenCatogorieService = new clsRechtenCatogorieDataService();
            MijnRollenService = new clsRollenDataService();

            cmdIsChecked = new clsCustomCommand(Execute_cmdIsChecked_Command, CanExecute_cmdIsChecked_Command);
            cmdSave = new clsCustomCommand(Execute_cmdSave_Command, CanExecute_cmdSave_Command);
            cmdNew = new clsCustomCommand(Execute_cmdNew_Command, CanExecute_cmdNew_Command);
            cmdDelete = new clsCustomCommand(Execute_cmdDelete_Command, CanExecute_cmdDelete_Command);
            cmdCancel = new clsCustomCommand(Execute_cmdCancel_Command, CanExecute_cmdCancel_Command);
            cmdClose = new clsCustomCommand(Execute_cmdClose_Command, CanExecute_cmdClose_Command);

            LoadRoles();
            LoadData();

            MijnSelectedItem = MijnRollenService.GetFirst();

        }

        private bool CanExecute_cmdClose_Command(object? obj)
        {
            return true;
        }

        private void Execute_cmdClose_Command(object? obj)
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



        private void Execute_cmdCancel_Command(object? obj)
        {
            MijnSelectedItem = MijnRollenService.GetFirst();
            if (MijnSelectedItem != null)
            {
                MijnSelectedItem.MijnSelectedIndex = 0;
                MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
                SetCheckedChildren(MijnSelectedItem.Rechten);
            }
            NewStatus = false;
            IsFocusedAfterNew = false;
            IsFocused = true;
        }

        private bool CanExecute_cmdCancel_Command(object? obj)
        {
            return NewStatus;
        }

        private void LoadRoles()
        {
            MijnRechtenCollectie = MijnRechtenService.GetAll();
            MijnRechtenCatogorieCollectie = MijnRechtenCatogorieService.GetAll();
        }

        private void Execute_cmdNew_Command(object? obj)
        {
            clsRollenModel _itemToInsert = new clsRollenModel()
            {
                RolID = 0,
                RolName = string.Empty,
                Rechten = string.Empty
            };
            MijnSelectedItem = _itemToInsert;
            SetCheckedChildren(MijnSelectedItem.Rechten.ToString());
            MijnSelectedItem.MyVisibility = (int)Visibility.Hidden;
            NewStatus = true;
            IsFocusedAfterNew = true;
        }

        private bool CanExecute_cmdNew_Command(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_cmdDelete_Command(object? obj)
        {
            if (MessageBox.Show(
                 "Wil je " + MijnSelectedItem + " verwijderen?",
                 "Verwijderen?",
                 MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (MijnSelectedItem != null)
                {
                    if (MijnRollenService.Delete(MijnSelectedItem))
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

        private bool CanExecute_cmdDelete_Command(object? obj)
        {
            if (MijnSelectedItem != null)
            {
                if (MijnSelectedItem.RolName == "Admin")
                {
                    return false;
                }

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

        private void Execute_cmdSave_Command(object? obj)
        {
            OpslaanCommando();
        }

        private bool CanExecute_cmdSave_Command(object? obj)
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

        private bool CanExecute_cmdIsChecked_Command(object? obj)
        {
            return true;
        }
        private void Execute_cmdIsChecked_Command(object? obj)
        {
            if (obj is clsRechtenModel selectedRecht)
            {
                var parent = MijnRechtenCatogorieCollectie
                    .FirstOrDefault(c => c.Rechten.Contains(selectedRecht));

                if (parent != null)
                {
                    // Check of alle child-items zijn aangevinkt of uitgevinkt
                    if (parent.Rechten.All(r => r.IsChecked))
                    {
                        parent.IsChecked = true;
                    }
                    else if (parent.Rechten.All(r => !r.IsChecked))
                    {
                        parent.IsChecked = false;
                    }
                    else
                    {
                        parent.IsChecked = null; // Set to indeterminate
                    }
                }
            }
            MijnSelectedItem.Rechten = GetAangevinkteChildrenIds();

        }


        public string GetAangevinkteChildrenIds()
        {
            // Selecteer alle aangevinkte rechten binnen elke categorie en voeg hun Id's samen
            var aangevinkteIds = MijnRechtenCatogorieCollectie
                                 .SelectMany(c => c.Rechten)
                                 .Where(r => r.IsChecked)
                                 .Select(r => r.RechtenID);

            // Combineer de Id's in één string met een '|' tussen elk Id
            return string.Join("|", aangevinkteIds);
        }

        public void SetCheckedChildren(string idsString)
        {
            foreach (var categorie in MijnRechtenCatogorieCollectie)
            {
                categorie.IsChecked = false;
                foreach (var recht in categorie.Rechten)
                {
                    recht.IsChecked = false;
                }
            }

            // Converteer de string naar een lijst van integers, waarbij lege strings worden genegeerd
            var idsToCheck = idsString.Split('|')
                               .Where(id => !string.IsNullOrWhiteSpace(id)) // Filter lege waarden
                               .Select(id => int.Parse(id))
                               .ToList();

            // Doorloop elke categorie en vink de overeenkomende rechten aan
            foreach (var categorie in MijnRechtenCatogorieCollectie)
            {
                foreach (var recht in categorie.Rechten)
                {
                    // Stel IsChecked in op true als het Id overeenkomt
                    recht.IsChecked = idsToCheck.Contains(recht.RechtenID);
                }

                // Controleer en update de IsChecked-status van de parent op basis van de child-items
                if (categorie.Rechten.All(r => r.IsChecked))
                {
                    categorie.IsChecked = true; // Alle child-items zijn aangevinkt
                }
                else if (categorie.Rechten.All(r => !r.IsChecked))
                {
                    categorie.IsChecked = false; // Geen enkel child-item is aangevinkt
                }
                else
                {
                    categorie.IsChecked = null; // Gedeeltelijk aangevinkt (indeterminate)
                }
            }
        }




    }
}
