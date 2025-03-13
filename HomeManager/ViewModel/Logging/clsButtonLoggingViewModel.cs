using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using HomeManager.Common;
using HomeManager.DataService.Logging;
using HomeManager.Helpers;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Logging
{
    public class clsButtonLoggingViewModel : clsCommonModelPropertiesBase
    {
        clsButtonLoggingDataService MijnService;

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdExport { get; set; }


        private ObservableCollection<clsButtonLoggingModel> _mijnCollectie;
        public ObservableCollection<clsButtonLoggingModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsButtonLoggingModel> _mijnGefilterdeCollectie;
        public ObservableCollection<clsButtonLoggingModel> MijnGefilterdeCollectie
        {
            get { return _mijnGefilterdeCollectie; }
            set
            {
                _mijnGefilterdeCollectie = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsAccountModel> _mijnAccounten;
        public ObservableCollection<clsAccountModel> MijnAccounten
        {
            get { return _mijnAccounten; }
            set
            {
                _mijnAccounten = value;
                OnPropertyChanged();
            }
        }


        private clsAccountModel _selectedAccount;
        public clsAccountModel SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged();
                FilterData();
            }
        }


        private ObservableCollection<string> _mijnActies;
        public ObservableCollection<string> MijnActies
        {
            get { return _mijnActies; }
            set
            {
                _mijnActies = value;
                OnPropertyChanged();
            }
        }

        private string _selectedActies;
        public string SelectedActies
        {
            get => _selectedActies;
            set
            {
                _selectedActies = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        private ObservableCollection<string> _mijnActieTargets;
        public ObservableCollection<string> MijnActieTargets
        {
            get { return _mijnActieTargets; }
            set
            {
                _mijnActieTargets = value;
                OnPropertyChanged();
            }
        }

        private string _selectedActieTargets;
        public string SelectedActieTargets
        {
            get => _selectedActieTargets;
            set
            {
                _selectedActieTargets = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
                FilterData(); 
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
                FilterData();
            }
        }










        public clsButtonLoggingViewModel()
        {
            MijnService = new clsButtonLoggingDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdExport = new clsCustomCommand(Execute_Export_Command, CanExecute_Export_Command);

            LoadData();
        }



private void Execute_Export_Command(object? obj)
    {
        if (MijnGefilterdeCollectie == null || !MijnGefilterdeCollectie.Any())
        {
            MessageBox.Show("Geen gegevens om te exporteren!", "Exporteren", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        // Laat de gebruiker een bestandslocatie kiezen
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel bestanden (*.xlsx)|*.xlsx",
            Title = "Exporteer naar Excel",
            FileName = "Export.xlsx"
        };

        if (saveFileDialog.ShowDialog() != true)
            return;

        try
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Logging Data");

                // Kolomheaders toevoegen
                worksheet.Cell(1, 1).Value = "Account";
                worksheet.Cell(1, 2).Value = "Action";
                worksheet.Cell(1, 3).Value = "Target";
                worksheet.Cell(1, 4).Value = "Datum Tijd";

                // Data invullen
                int row = 2;
                foreach (var item in MijnGefilterdeCollectie)
                {
                    worksheet.Cell(row, 1).Value = item.AccountName;
                    worksheet.Cell(row, 2).Value = item.ActionName;
                    worksheet.Cell(row, 3).Value = item.ActionTarget;
                    worksheet.Cell(row, 4).Value = item.LogTime.ToString("yyyy-MM-dd HH:mm:ss");
                    row++;
                }

                // Autofit kolommen
                worksheet.Columns().AdjustToContents();

                // Bestand opslaan
                workbook.SaveAs(saveFileDialog.FileName);

                MessageBox.Show("Export succesvol!", "Exporteren", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    private bool CanExecute_Export_Command(object? obj)
        {return true; 
        }

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();

           
            var uniekeAccounts = MijnCollectie
                .Select(x => new clsAccountModel { AccountID = x.AccountId, AccountName = x.AccountName })
                .DistinctBy(x => x.AccountID) // Alleen unieke AccountID's
                .ToList();

            var uniekeActies = MijnCollectie
                .Select(x => x.ActionName)
                .Distinct()
                .OrderBy(x => x) // Sorteer alfabetisch
                .ToList();

            var uniekeActieTargets = MijnCollectie
                .Select(x => x.ActionTarget)
                .Distinct()
                .OrderBy(x => x) // Sorteer alfabetisch
                .ToList();

            var uniekeActiesOptie = new string ("-- Alle Acties --");
            uniekeActies.Insert(0, uniekeActiesOptie);
            var uniekeActieTargetsOptie = new string("-- Alle ActieTargets --");       
            uniekeActieTargets.Insert(0, uniekeActieTargetsOptie);

            var alleAccountsOptie = new clsAccountModel { AccountID = 0, AccountName = "-- Alle Accounts --" };
            uniekeAccounts.Insert(0, alleAccountsOptie);

            // Koppel de lijst aan de ObservableCollection
            MijnAccounten = new ObservableCollection<clsAccountModel>(uniekeAccounts);
            MijnActies = new ObservableCollection<string>(uniekeActies);
            MijnActieTargets = new ObservableCollection<string>(uniekeActieTargets);


            // ❗ Standaard "-- Alle Accounts --" selecteren
            SelectedAccount = alleAccountsOptie;
            SelectedActies = uniekeActiesOptie;
            SelectedActieTargets = uniekeActieTargetsOptie;
        }



        private void FilterData()
        {
            if (MijnCollectie == null || !MijnCollectie.Any())
                return;

            var gefilterdeCollectie = MijnCollectie.ToList();

            // Filter op AccountID (indien niet "-- Alle Accounts --")
            if (SelectedAccount != null && SelectedAccount.AccountID != 0)
            {
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.AccountId == SelectedAccount.AccountID)
                    .ToList();
            }

            // Filter op Actie (indien niet "-- Alle Acties --")
            if (!string.IsNullOrEmpty(SelectedActies) && SelectedActies != "-- Alle Acties --")
            {
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.ActionName == SelectedActies)
                    .ToList();
            }

            // Filter op ActieTarget (indien niet "-- Alle ActieTargets --")
            if (!string.IsNullOrEmpty(SelectedActieTargets) && SelectedActieTargets != "-- Alle ActieTargets --")
            {
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.ActionTarget == SelectedActieTargets)
                    .ToList();
            }

            // Filter op StartDate en EndDate
            if (StartDate.HasValue && EndDate.HasValue)
            {
                // Beide datums ingevuld -> filter tussen deze datums
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.LogTime >= StartDate.Value && x.LogTime <= EndDate.Value)
                    .ToList();
            }
            else if (StartDate.HasValue)
            {
                // Alleen StartDate ingevuld -> toon vanaf deze datum
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.LogTime >= StartDate.Value)
                    .ToList();
            }
            else if (EndDate.HasValue)
            {
                // Alleen EndDate ingevuld -> toon tot deze datum
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.LogTime <= EndDate.Value)
                    .ToList();
            }

            // Update de gefilterde collectie
            MijnGefilterdeCollectie = new ObservableCollection<clsButtonLoggingModel>(gefilterdeCollectie);
        }





        #region Command Methods

        private bool CanExecute_Close_Command(object? obj)
        {
            return true;
        }

        private void Execute_Close_Command(object? obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }


        private bool CanExecute_Cancel_Command(object? obj)
        {
            return true;
        }

        private void Execute_Cancel_Command(object? obj)
        {
            StartDate = null;  // Datumvelden resetten
            EndDate = null;
            SelectedActies = "-- Alle Acties --";
            SelectedAccount = MijnAccounten.FirstOrDefault(a => a.AccountID == 0);
            SelectedActieTargets = "-- Alle ActieTargets --";
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return false;
        }

        private void Execute_New_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return false;
        }

        private void Execute_Delete_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            return false;
        }

        private void Execute_Save_Command(object? obj)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
