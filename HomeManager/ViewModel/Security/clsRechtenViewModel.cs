using HomeManager.Common;
using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
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


        private void LoadData()
        {

        }


        public ObservableCollection<clsRechtenCatogorieModel> RechtenCategorieën { get; set; }

        public clsRechtenViewModel()
        {
            MijnRechtenService = new clsRechtenDataService();
            MijnRechtenCatogorieService = new clsRechtenCatogorieDataService();

            cmdIsChecked = new clsCustomCommand(Execute_cmdIsChecked_Command, CanExecute_cmdIsChecked_Command);
            cmdSave = new clsCustomCommand(Execute_cmdSave_Command, CanExecute_cmdSave_Command);
            cmdNew = new clsCustomCommand(Execute_cmdNew_Command, CanExecute_cmdNew_Command);
            cmdDelete = new clsCustomCommand(Execute_cmdDelete_Command, CanExecute_cmdDelete_Command);

       
            LoadRoles();
            LoadData();
        }

        private void LoadRoles()
        {
            MijnRechtenCollectie = MijnRechtenService.GetAll();
            MijnRechtenCatogorieCollectie = MijnRechtenCatogorieService.GetAll();
        }


        private void Execute_cmdNew_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_cmdNew_Command(object? obj)
        {
            return !NewStatus;
        }

        private void Execute_cmdDelete_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_cmdDelete_Command(object? obj)
        {
            return true;
        }

        private void Execute_cmdSave_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_cmdSave_Command(object? obj)
        {
            return IsDirty;
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
        }
    }
}
