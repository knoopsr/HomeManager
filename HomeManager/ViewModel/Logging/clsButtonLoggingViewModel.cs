using ClosedXML.Excel;
using HomeManager.Common;
using HomeManager.DataService.Logging;
using HomeManager.Helpers;
using HomeManager.Model.Logging;
using HomeManager.Model.Security;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.ViewModel.Logging
{
    /// <summary>
    /// ViewModel voor het beheren, filteren en exporteren van knoplogging.
    /// </summary>
    public class clsButtonLoggingViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsButtonLoggingDataService MijnService;

        #region Commands

        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdExport { get; set; }

        #endregion

        #region Properties - Collecties

        public ObservableCollection<clsButtonLoggingModel> MijnCollectie { get; set; }
        public ObservableCollection<clsButtonLoggingModel> MijnGefilterdeCollectie { get; set; }
        public ObservableCollection<clsAccountModel> MijnAccounten { get; set; }
        public ObservableCollection<string> MijnActies { get; set; }
        public ObservableCollection<string> MijnActieTargets { get; set; }

        #endregion

        #region Properties - Filters

        private clsAccountModel _selectedAccount;
        public clsAccountModel SelectedAccount
        {
            get => _selectedAccount;
            set { _selectedAccount = value; OnPropertyChanged(); FilterData(); }
        }

        private string _selectedActies;
        public string SelectedActies
        {
            get => _selectedActies;
            set { _selectedActies = value; OnPropertyChanged(); FilterData(); }
        }

        private string _selectedActieTargets;
        public string SelectedActieTargets
        {
            get => _selectedActieTargets;
            set { _selectedActieTargets = value; OnPropertyChanged(); FilterData(); }
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set { _startDate = value; OnPropertyChanged(); FilterData(); }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set { _endDate = value; OnPropertyChanged(); FilterData(); }
        }

        #endregion

        #region Constructor

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

        #endregion

        #region Data Load & Filter

        private void LoadData()
        {
            MijnCollectie = MijnService.GetAll();

            var uniekeAccounts = MijnCollectie
                .Select(x => new clsAccountModel { AccountID = x.AccountId, AccountName = x.AccountName })
                .DistinctBy(x => x.AccountID).ToList();

            var uniekeActies = MijnCollectie.Select(x => x.ActionName).Distinct().OrderBy(x => x).ToList();
            var uniekeTargets = MijnCollectie.Select(x => x.ActionTarget).Distinct().OrderBy(x => x).ToList();

            uniekeActies.Insert(0, "-- Alle Acties --");
            uniekeTargets.Insert(0, "-- Alle ActieTargets --");
            uniekeAccounts.Insert(0, new clsAccountModel { AccountID = 0, AccountName = "-- Alle Accounts --" });

            MijnAccounten = new ObservableCollection<clsAccountModel>(uniekeAccounts);
            MijnActies = new ObservableCollection<string>(uniekeActies);
            MijnActieTargets = new ObservableCollection<string>(uniekeTargets);

            SelectedAccount = MijnAccounten[0];
            SelectedActies = MijnActies[0];
            SelectedActieTargets = MijnActieTargets[0];
        }

        private void FilterData()
        {
            if (MijnCollectie == null || !MijnCollectie.Any()) return;

            var gefilterd = MijnCollectie.ToList();

            if (SelectedAccount?.AccountID > 0)
                gefilterd = gefilterd.Where(x => x.AccountId == SelectedAccount.AccountID).ToList();

            if (!string.IsNullOrEmpty(SelectedActies) && SelectedActies != "-- Alle Acties --")
                gefilterd = gefilterd.Where(x => x.ActionName == SelectedActies).ToList();

            if (!string.IsNullOrEmpty(SelectedActieTargets) && SelectedActieTargets != "-- Alle ActieTargets --")
                gefilterd = gefilterd.Where(x => x.ActionTarget == SelectedActieTargets).ToList();

            if (StartDate.HasValue)
                gefilterd = gefilterd.Where(x => x.LogTime >= StartDate.Value).ToList();

            if (EndDate.HasValue)
                gefilterd = gefilterd.Where(x => x.LogTime <= EndDate.Value).ToList();

            MijnGefilterdeCollectie = new ObservableCollection<clsButtonLoggingModel>(gefilterd);
        }

        #endregion

        #region Export

        private bool CanExecute_Export_Command(object? obj) => true;

        private void Execute_Export_Command(object? obj)
        {
            if (MijnGefilterdeCollectie == null || !MijnGefilterdeCollectie.Any())
            {
                MessageBox.Show("Geen gegevens om te exporteren!", "Exporteren", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Excel bestanden (*.xlsx)|*.xlsx",
                Title = "Exporteer naar Excel",
                FileName = "Export.xlsx"
            };

            if (dlg.ShowDialog() != true) return;

            try
            {
                using var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Logging Data");

                ws.Cell(1, 1).Value = "Account";
                ws.Cell(1, 2).Value = "Action";
                ws.Cell(1, 3).Value = "Target";
                ws.Cell(1, 4).Value = "Datum Tijd";

                int row = 2;
                foreach (var item in MijnGefilterdeCollectie)
                {
                    ws.Cell(row, 1).Value = item.AccountName;
                    ws.Cell(row, 2).Value = item.ActionName;
                    ws.Cell(row, 3).Value = item.ActionTarget;
                    ws.Cell(row, 4).Value = item.LogTime.ToString("yyyy-MM-dd HH:mm:ss");
                    row++;
                }

                ws.Columns().AdjustToContents();
                wb.SaveAs(dlg.FileName);

                MessageBox.Show("Export succesvol!", "Exporteren", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Command Handlers

        private bool CanExecute_Close_Command(object? obj) => true;
        private void Execute_Close_Command(object? obj)
        {
            if (obj is Window window)
                window.Close();
        }

        private bool CanExecute_Cancel_Command(object? obj) => true;
        private void Execute_Cancel_Command(object? obj)
        {
            StartDate = null;
            EndDate = null;
            SelectedActies = "-- Alle Acties --";
            SelectedAccount = MijnAccounten.FirstOrDefault(a => a.AccountID == 0);
            SelectedActieTargets = "-- Alle ActieTargets --";
        }

        private bool CanExecute_New_Command(object? obj) => false;
        private void Execute_New_Command(object? obj) => throw new NotImplementedException();

        private bool CanExecute_Delete_Command(object? obj) => false;
        private void Execute_Delete_Command(object? obj) => throw new NotImplementedException();

        private bool CanExecute_Save_Command(object? obj) => false;
        private void Execute_Save_Command(object? obj) => throw new NotImplementedException();

        #endregion
    }
}
