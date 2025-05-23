﻿using HomeManager.DataService.Security;
using HomeManager.Helpers;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Model.Personen;
using HomeManager.DataService.Personen;
using HomeManager.Model.Mail;
using HomeManager.Mail;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;
using HomeManager.Model.Exceptions;
using HomeManager.MailService;
using System.Net.Mail;
using ClosedXML.Excel;
using Microsoft.Win32;

namespace HomeManager.ViewModel.Exceptions
{
    /// <summary>
    /// ViewModel for handling exception email operations, such as sending exception data to email addresses,
    /// exporting the exception data to Excel, and managing email-related actions for a specific user and development team.
    /// </summary>
    public class clsExceptionsMailViewModel : clsCommonModelPropertiesBase
    {
        #region FIELDS
        private static clsEmailAdressenDataService EmailDataService;
        private static clsMailService MailService;
        private static ObservableCollection<clsEmailAdressenModel> _currentUserMailCollection;
        private static ObservableCollection<clsEmailAdressenModel> _mailCollectionDevTeam;
        #endregion

        #region PROPERTIES
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdDelete { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdExport { get; set; }

        /// <summary>
        /// ObservableCollection for storing the current user's email addresses.
        /// </summary>
        public static ObservableCollection<clsEmailAdressenModel> CurrentUserMailCollection { get; set; }

        /// <summary>
        /// ObservableCollection for storing the development team's email addresses.
        /// </summary>
        public static ObservableCollection<clsEmailAdressenModel> MailCollectionDevTeam { get; set; }

        /// <summary>
        /// Bindable property for the development team's email addresses collection.
        /// </summary>
        public ObservableCollection<clsEmailAdressenModel> BindableMailCollectionDevTeam { get => MailCollectionDevTeam; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes the ViewModel, loads data, and sets up commands.
        /// </summary>
        public clsExceptionsMailViewModel()
        {
            EmailDataService = new clsEmailAdressenDataService();
            MailService = new clsMailService();
            LoadData();

            cmdSave = new clsCustomCommand(Execute_cmdSave_Command, CanExecute_cmdSave_Command);
            cmdNew = new clsCustomCommand(Execute_cmdNew_Command, CanExecute_cmdNew_Command);
            cmdDelete = new clsCustomCommand(Execute_cmdDelete_Command, CanExecute_cmdDelete_Command);
            cmdCancel = new clsCustomCommand(Execute_cmdCancel_Command, CanExecute_cmdCancel_Command);
            cmdClose = new clsCustomCommand(Execute_cmdClose_Command, CanExecute_cmdClose_Command);
            cmdExport = new clsCustomCommand(Execute_Export_Command, CanExecute_Export_Command);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads email data for the current user and development team into the respective collections.
        /// </summary>
        /// <remarks>
        /// Calls the EmailDataService to fetch the email addresses for the current user and development team.
        /// </remarks>
        private static void LoadData()
        {
            EmailDataService = new clsEmailAdressenDataService();
            if (clsLoginModel.Instance == null)
            {
                MessageBox.Show("clsLoginModel.Instance is null");
                return;
            }


            if (clsLoginModel.Instance != null)
            {
          
                CurrentUserMailCollection = EmailDataService.GetByPersoonID(clsLoginModel.Instance.PersoonID);
            }
            else MessageBox.Show("No CurrentUserMailCollection was found by the EmailDataService.");

            MailCollectionDevTeam = EmailDataService.GetAllByRechtenCode(713);
            if (MailCollectionDevTeam == null) MessageBox.Show("No MailCollectionDevTeam was found by the EmailDataService.");
        }

        /// <summary>
        /// Sends exception details to the email addresses of the current user and the development team.
        /// </summary>
        /// <param name="ex">The exception model containing exception details to be sent.</param>
        /// <remarks>
        /// Sends the exception data to both the current user’s email addresses and the development team's email addresses.
        /// </remarks>
        public static void SendExceptionToMailAddresses(clsExceptionsModel ex)
        {
            MailService = new clsMailService();
            LoadData();

            if (!CurrentUserMailCollection.IsNullOrEmpty()
                && !MailCollectionDevTeam.IsNullOrEmpty())
            {
                foreach (clsEmailAdressenModel mailAddress in CurrentUserMailCollection)
                {
                    MailService.SendExceptionToMailAddress(mailAddress, ex, false);
                }
                foreach (clsEmailAdressenModel mailAddress in MailCollectionDevTeam)
                {
                    MailService.SendExceptionToMailAddress(mailAddress, ex, true);
                }
            }
            else
            {
                MessageBox.Show("SendExceptionToMailAddresses failed because one of the collections is null or empty.");
            }
        }
        #endregion

        #region COMMANDS
        private bool CanExecute_cmdNew_Command(object? obj) => false;
        private bool CanExecute_cmdSave_Command(object? obj) => false;
        private bool CanExecute_cmdDelete_Command(object? obj) => false;
        private bool CanExecute_cmdCancel_Command(object? obj) => false;
        private bool CanExecute_cmdClose_Command(object? obj) => true;
        private bool CanExecute_Export_Command(object? obj) => true;

        private void Execute_cmdClose_Command(object? obj)
        {
            MainWindow HomeWindow = obj as MainWindow;
            if (HomeWindow != null)
            {
                clsHomeVM vm2 = (clsHomeVM)HomeWindow.DataContext;
                vm2.CurrentViewModel = null;
                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }
        }

        /// <summary>
        /// Executes the "Export" command, allowing the user to export email data to an Excel file.
        /// </summary>
        private void Execute_Export_Command(object? obj)
        {
            if (MailCollectionDevTeam == null || !MailCollectionDevTeam.Any())
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
                    var worksheet = workbook.Worksheets.Add("ExceptionsMail Data");

                    // Kolomheaders toevoegen
                    worksheet.Cell(1, 1).Value = "EmailAdresID";
                    worksheet.Cell(1, 2).Value = "Emailadres";
                    worksheet.Cell(1, 3).Value = "PersoonID";
                    worksheet.Cell(1, 4).Value = "EmailTypeID";
                    // Data invullen
                    int row = 2;
                    foreach (var item in MailCollectionDevTeam)
                    {
                        worksheet.Cell(row, 1).Value = item.EmailAdresID;
                        worksheet.Cell(row, 2).Value = item.Emailadres;
                        worksheet.Cell(row, 3).Value = item.PersoonID;
                        worksheet.Cell(row, 4).Value = item.EmailTypeID;
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

        #region NOT IMPLEMENTED
        private void Execute_cmdCancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }
        private void Execute_cmdNew_Command(object? obj)
        {
            throw new NotImplementedException();
        }
        private void Execute_cmdDelete_Command(object? obj)
        {
            throw new NotImplementedException();
        }
        private void Execute_cmdSave_Command(object? obj)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
