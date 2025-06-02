using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using HomeManager.Common;
using HomeManager.DataService.Exceptions;
using HomeManager.Helpers;
using HomeManager.Model.Exceptions;
using HomeManager.Model.Security;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Exceptions
{
    /// <summary>
    /// ViewModel class for managing exceptions
    /// Provides functionalities to filter, save, delete, export, and manage exceptions data.
    /// </summary>
    public class clsExceptionsViewModel : clsCommonModelPropertiesBase
    {
        #region FIELDS
        private ObservableCollection<clsExceptionsModel> _mijnCollectie;
        private ObservableCollection<clsExceptionsModel> _mijnGefilterdeCollectie;
        private ObservableCollection<clsAccountModel> _mijnAccounten;
        private clsAccountModel _selectedAccount;
        private ObservableCollection<string> _mijnExceptions;
        private string _selectedExceptions;
        private ObservableCollection<string> _mijnTargetSites;
        private string _selectedTargetSites;
        private DateTime? _startDate;
        private DateTime? _endDate;

        clsExceptionsDataService MijnService;
        #endregion

        #region PROPERTIES
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdExport { get; set; }

        public ObservableCollection<clsExceptionsModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<clsExceptionsModel> MijnGefilterdeCollectie
        {
            get { return _mijnGefilterdeCollectie; }
            set
            {
                _mijnGefilterdeCollectie = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<clsAccountModel> MijnAccounten
        {
            get { return _mijnAccounten; }
            set
            {
                _mijnAccounten = value;
                OnPropertyChanged();
            }
        }

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

        public ObservableCollection<string> MijnExceptions
        {
            get { return _mijnExceptions; }
            set
            {
                _mijnExceptions = value;
                OnPropertyChanged();
            }
        }

        public string SelectedExceptions
        {
            get => _selectedExceptions;
            set
            {
                _selectedExceptions = value;
                OnPropertyChanged();
                FilterData();
            }
        }

        public ObservableCollection<string> MijnTargetSites
        {
            get { return _mijnTargetSites; }
            set
            {
                _mijnTargetSites = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTargetSites
        {
            get => _selectedTargetSites;
            set
            {
                _selectedTargetSites = value;
                OnPropertyChanged();
                FilterData();
            }
        }

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
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes the ViewModel and sets up commands and data loading.
        /// </summary>
        public clsExceptionsViewModel()
        {
            MijnService = new clsExceptionsDataService();

            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdExport = new clsCustomCommand(Execute_Export_Command, CanExecute_Export_Command);

            LoadData();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads data into the collections, filtering unique values for accounts, exceptions, and target sites.
        /// </summary>
        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();


            var uniekeAccounts = MijnCollectie
                .Select(x => new clsAccountModel { AccountID = x.AccountID, AccountName = x.AccountName })
                .DistinctBy(x => x.AccountID) // Alleen unieke AccountID's
                .ToList();

            var uniekeExceptions = MijnCollectie
                .Select(x => x.ExceptionName)
                .Distinct()
                .OrderBy(x => x) // Sorteer alfabetisch
                .ToList();

            var uniekeTargetSites = MijnCollectie
                .Select(x => x.TargetSite)
                .Distinct()
                .OrderBy(x => x) // Sorteer alfabetisch
                .ToList();

            var uniekeExceptionsOptie = new string("-- Alle Exceptions --");
            uniekeExceptions.Insert(0, uniekeExceptionsOptie);

            var uniekeTargetSitesOptie = new string("-- Alle TargetSites --");
            uniekeTargetSites.Insert(0, uniekeTargetSitesOptie);

            var alleAccountsOptie = new clsAccountModel { AccountID = 0, AccountName = "-- Alle Accounts --" };
            uniekeAccounts.Insert(0, alleAccountsOptie);

            // Koppel de lijst aan de ObservableCollection
            MijnAccounten = new ObservableCollection<clsAccountModel>(uniekeAccounts);
            MijnExceptions = new ObservableCollection<string>(uniekeExceptions);
            MijnTargetSites = new ObservableCollection<string>(uniekeTargetSites);

            // ❗ Standaard "-- Alle Accounts --" selecteren
            SelectedAccount = alleAccountsOptie;
            SelectedExceptions = uniekeExceptionsOptie;
            SelectedTargetSites = uniekeTargetSitesOptie;
        }

        /// <summary>
        /// Filters the exceptions collection based on selected criteria: account, exception, target site, and date range.
        /// </summary>
        private void FilterData()
        {
            if (MijnCollectie == null || !MijnCollectie.Any()) return;

            var gefilterdeCollectie = MijnCollectie.ToList();

            // Filter op AccountID (indien niet "-- Alle Accounts --")
            if (SelectedAccount != null && SelectedAccount.AccountID != 0)
            {
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.AccountID == SelectedAccount.AccountID)
                    .ToList();
            }

            // Filter op Actie (indien niet "-- Alle Acties --")
            if (!string.IsNullOrEmpty(SelectedExceptions) && SelectedExceptions != "-- Alle Exceptions --")
            {
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.ExceptionName == SelectedExceptions)
                    .ToList();
            }

            // Filter op ActieTarget (indien niet "-- Alle ActieTargets --")
            if (!string.IsNullOrEmpty(SelectedTargetSites) && SelectedTargetSites != "-- Alle TargetSites --")
            {
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.TargetSite == SelectedTargetSites)
                    .ToList();
            }

            // Filter op StartDate en EndDate
            if (StartDate.HasValue && EndDate.HasValue)
            {
                // Beide datums ingevuld -> filter tussen deze datums
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.CreatedOn >= StartDate.Value && x.CreatedOn <= EndDate.Value)
                    .ToList();
            }
            else if (StartDate.HasValue)
            {
                // Alleen StartDate ingevuld -> toon vanaf deze datum
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.CreatedOn >= StartDate.Value)
                    .ToList();
            }
            else if (EndDate.HasValue)
            {
                // Alleen EndDate ingevuld -> toon tot deze datum
                gefilterdeCollectie = gefilterdeCollectie
                    .Where(x => x.CreatedOn <= EndDate.Value)
                    .ToList();
            }

            // Update de gefilterde collectie
            MijnGefilterdeCollectie = new ObservableCollection<clsExceptionsModel>(gefilterdeCollectie);
        }
        #endregion

        #region COMMANDS
        private bool CanExecute_Export_Command(object? obj) => true;
        private bool CanExecute_Close_Command(object? obj) => true;
        private bool CanExecute_Cancel_Command(object? obj) => true;
        private bool CanExecute_New_Command(object? obj) => true;
        private bool CanExecute_Save_Command(object? obj) => false;
        private bool CanExecute_Delete_Command(object? obj) => false;

        /// <summary>
        /// Executes the export command, saving filtered data to an Excel file.
        /// </summary>
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

            if (saveFileDialog.ShowDialog() != true) return;

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Exceptions Data");

                    // Kolomheaders toevoegen
                    worksheet.Cell(1, 1).Value = "Account";
                    worksheet.Cell(1, 2).Value = "Exception";
                    worksheet.Cell(1, 3).Value = "Module";
                    worksheet.Cell(1, 4).Value = "Source";
                    worksheet.Cell(1, 5).Value = "TargetSite";
                    worksheet.Cell(1, 6).Value = "ExceptionMessage";
                    worksheet.Cell(1, 7).Value = "InnerExceptionMessage";
                    worksheet.Cell(1, 8).Value = "StackTrace";
                    worksheet.Cell(1, 9).Value = "DotNetAssembly";
                    worksheet.Cell(1, 10).Value = "Datum Tijd";

                    // Data invullen
                    int row = 2;
                    foreach (var item in MijnGefilterdeCollectie)
                    {
                        worksheet.Cell(row, 1).Value = item.AccountName;
                        worksheet.Cell(row, 2).Value = item.ExceptionName;
                        worksheet.Cell(row, 3).Value = item.Module;
                        worksheet.Cell(row, 4).Value = item.Source;
                        worksheet.Cell(row, 5).Value = item.TargetSite;
                        worksheet.Cell(row, 6).Value = item.ExceptionMessage;
                        worksheet.Cell(row, 7).Value = item.InnerExceptionMessage;
                        worksheet.Cell(row, 8).Value = item.StackTrace;
                        worksheet.Cell(row, 9).Value = item.DotNetAssembly;
                        worksheet.Cell(row, 10).Value = item.CreatedOn.ToString("yyyy-MM-dd HH:mm:ss");
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

        private void Execute_Close_Command(object? obj)
        {
            if (obj is Window window)
            {
                window.Close();
            }
        }

        private void Execute_Cancel_Command(object? obj)
        {
            StartDate = null;  // Datumvelden resetten
            EndDate = null;
            SelectedExceptions = "-- Alle Exceptions --";
            SelectedAccount = MijnAccounten.FirstOrDefault(a => a.AccountID == 0);
            SelectedTargetSites = "-- Alle TargetSites --";
        }

        private void Execute_New_Command(object? obj) { throw new Exception("This is an example exception thrown with: Execute_New_Command@HomeManager.ViewModel.Exceptions.clsExceptionsViewModel"); }

        private void Execute_Delete_Command(object? obj) { throw new NotImplementedException(); }

        private void Execute_Save_Command(object? obj) { throw new NotImplementedException(); }
        #endregion
    }
}
