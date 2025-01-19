using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Messages;
using HomeManager.Model.Personen;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsEmailAdressenViewModel : clsCommonModelPropertiesBase
    {
        clsEmailAdressenDataService MijnService;
        clsPersoonDataService MijnPersoonService;
        private bool NewStatus = false;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdSave { get; set; }

        private ObservableCollection<clsEmailAdressenModel> mijnCollectie;

        public ObservableCollection<clsEmailAdressenModel> MijnCollectie
        {
            get
            {
                return mijnCollectie;
            }
            set
            {
                mijnCollectie = value;
                OnPropertyChanged();
            }
        }


        private clsEmailAdressenModel mijnSelectedItem;
        public clsEmailAdressenModel MijnSelectedItem
        {
            get
            {
                return mijnSelectedItem;
            }
            set
            {
                if (value != null)
                {
                    if (mijnSelectedItem != null && mijnSelectedItem.IsDirty)
                    {
                        if (MessageBox.Show("Wil je " + mijnSelectedItem + "Opslaan?", "Opslaan",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            mijnSelectedItem.IsDirty = false;
                            mijnSelectedItem.MijnSelectedIndex = 0;
                            OpslaanCommando();
                            LoadData();
                        }
                    }
                }
                mijnSelectedItem = value;
                OnPropertyChanged();
            }
        }


        private clsPersoonModel _mijnSelectedPersoonItem;
        public clsPersoonModel MijnSelectedPersoonItem
        {
            get
            {
                return _mijnSelectedPersoonItem;
            }
            set
            {
                _mijnSelectedPersoonItem = value;
                OnPropertyChanged();
            }
        }

        private void OpslaanCommando()
        {
            if (MijnSelectedItem != null)
            {
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


        public clsEmailAdressenViewModel()
        {
            MijnService = new clsEmailAdressenDataService();
            MijnPersoonService = new clsPersoonDataService();

            cmdNew = new clsCustomCommand(Execute_NewCommand, CanExecute_NewCommand);
            cmdDelete = new clsCustomCommand(Execute_DeleteCommand, CanExecute_DeleteCommand);
            cmdSave = new clsCustomCommand(Execute_SaveCommand, CanExecute_SaveCommand);
            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdCancel = new clsCustomCommand(Execute_CancelCommand, CanExecute_CancelCommand);
            clsMessenger.Default.Register<clsEmailAdressenModel>(this, OnEmailAdressenReceived);

            LoadData();
            MijnSelectedItem = MijnService.GetFirst();
            //MijnSelectedItem.MijnSelectedIndex = 0;
        }

        private void OnEmailAdressenReceived(clsEmailAdressenModel obj)
        {
            if (obj != null)
            {
                MijnSelectedItem = obj;
                MijnSelectedPersoonItem = MijnPersoonService.GetById(MijnSelectedItem.PersoonID);

                if (obj.EmailAdresID == 0)
                {
                    NewStatus = true;
                }
            }
        }

        private bool CanExecute_NewCommand(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_NewCommand(object? obj)
        {
            clsEmailAdressenModel ItemToInsert = new clsEmailAdressenModel()
            {
                EmailAdresID = 0,
                Emailadres = string.Empty,
                PersoonID = MijnSelectedPersoonItem.PersoonID,
                EmailTypeID = 0,
            };
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
                    if (MessageBox.Show(MijnSelectedItem.ToString().ToUpper() + "is nog niet opgeslagen, wil je opslaan ?", "Opslaan of sluiten?",
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

            clsMessenger.Default.Send<clsUpdateListMessages>(new clsUpdateListMessages());
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

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();
        }

        private bool CanExecute_SaveCommand(object obj)
        {
            if (MijnSelectedItem != null && MijnSelectedItem.Error == null && MijnSelectedItem.IsDirty == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Execute_SaveCommand(object obj)
        {
            OpslaanCommando();

            //if (MijnSelectedItem != null)
            //{
            //    if (NewStatus)
            //    {
            //        if (MijnService.Insert(MijnSelectedItem))
            //        {
            //            MijnSelectedItem.IsDirty = false;
            //            MijnSelectedItem.MijnSelectedIndex = 0;
            //            MijnSelectedItem.MyVisibility = (int)Visibility.Visible;
            //            NewStatus = false;
            //            LoadData();
            //        }
            //        else
            //        {
            //            MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
            //        }
            //    }
            //    else
            //    {
            //        if (MijnService.Update(MijnSelectedItem))
            //        {
            //            MijnSelectedItem.IsDirty = false;
            //            MijnSelectedItem.MijnSelectedIndex = 0;
            //            NewStatus = false;
            //            LoadData();
            //        }
            //        else
            //        {
            //            MessageBox.Show(MijnSelectedItem.ErrorBoodschap, "Error?");
            //        }
            //    }
            //}
        }
    }
}


