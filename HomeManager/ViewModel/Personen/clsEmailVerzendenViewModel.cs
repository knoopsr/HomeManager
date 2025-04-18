using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using HomeManager.Common;
using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Mail;
using HomeManager.MailService;
using HomeManager.Messages;
using HomeManager.Model.Budget;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static HomeManager.ViewModel.clsPersonenViewModel;


namespace HomeManager.ViewModel.Personen
{
    public class clsEmailVerzendenViewModel : clsCommonModelPropertiesBase
    {
        clsEmailAdressenDataService VerzendenService;
        public ICommand cmdUploadBijlage { get; set; }
        public ICommand cmdShowBijlage { get; set; }
        public ICommand cmdDeleteBijlage { get; set; }
        public ICommand cmdDropBijlage { get; set; }
        public ICommand SubmitCommand { get; private set; }


        private bool isSendMail = false;


        private ObservableCollection<clsEmailAdressenModel> _mijnVerzenderEmailAdres;
        public ObservableCollection<clsEmailAdressenModel> MijnVerzenderEmailAdres
        {
            get { return _mijnVerzenderEmailAdres; }
            set { _mijnVerzenderEmailAdres = value; }
        }

        private clsEmailAdressenModel _mijnSelectedItem;
        public clsEmailAdressenModel MijnSelectedItem
        {
            get { return _mijnSelectedItem; }
            set
            {
                _mijnSelectedItem = value;
                // Hier kun je eventueel extra logica toevoegen als de geselecteerde item verandert
            }
        }


        private ObservableCollection<clsBijlageModel> _mijnCollectieBijlage;

        public ObservableCollection<clsBijlageModel> MijnCollectieBijlage
        {
            get
            {
                return _mijnCollectieBijlage;
            }
            set
            {
                _mijnCollectieBijlage = value;
                OnPropertyChanged();
            }
        }

        private clsBijlageModel _MijnSelectedBijlage;
        public clsBijlageModel MijnSelectedBijlage
        {
            get
            {
                return _MijnSelectedBijlage;
            }
            set
            {
                _MijnSelectedBijlage = value;
                OnPropertyChanged();
            }
        }
        public clsEmailVerzendenViewModel()
        {
            //VerzendenService = new clsEmailAdressenDataService();

            //MijnVerzenderEmailAdres = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);

            //// Initialiseer het commando
            //SubmitCommand = new RelayCommand(ExecuteSubmit, CanExecuteSubmit);
            ////Messenger
            //clsMessenger.Default.Register<clsEmailVerzendenModel>(this, OnUpdateListMessageReceived);
            VerzendenService = new clsEmailAdressenDataService();
            MijnVerzenderEmailAdres = new ObservableCollection<clsEmailAdressenModel>();

            // Initialiseer MijnSelectedItem met het eerste item in de lijst, als deze niet leeg is
            if (MijnVerzenderEmailAdres.Count > 0)
            {
                MijnSelectedItem = MijnVerzenderEmailAdres[0];
            }

            // Initialiseer het commando
            SubmitCommand = new RelayCommand(ExecuteSubmit, CanExecuteSubmit);
            //Messenger
            clsMessenger.Default.Register<clsEmailVerzendenModel>(this, OnUpdateListMessageReceived);

        }

        private void OnUpdateListMessageReceived(clsEmailVerzendenModel obj)
        {
            Ontvanger = obj.Ontvanger;
            MijnVerzenderEmailAdres = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);
            cmdUploadBijlage = new clsCustomCommand(Execute_UploadBijlage, CanExecute_UploadBijlage);
            cmdShowBijlage = new clsCustomCommand(Execute_ShowBijlage, CanExecute_ShowBijlage);
            cmdDeleteBijlage = new clsCustomCommand(Execute_DeleteBijlage, CanExecute_DeleteBijlage);
            cmdDropBijlage = new clsRelayCommand<object>(Execute_Drop);
            MijnCollectieBijlage = new ObservableCollection<clsBijlageModel>();
        }

        private bool CanExecute_DeleteBijlage(object? obj)
        {
            return MijnSelectedBijlage != null;
        }

        private bool CanExecute_ShowBijlage(object? obj)
        {
            return MijnSelectedBijlage != null;
        }

        private bool CanExecute_UploadBijlage(object? obj)
        {
            return true;
        }

        private void Execute_UploadBijlage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Alle bestanden (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {

                    string fileName = Path.GetFileName(filePath);

                    // Controleer of er al een bijlage met dezelfde naam bestaat
                    if (MijnCollectieBijlage.Any(b => b.BijlageNaam.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Toon een waarschuwing aan de gebruiker
                        MessageBox.Show($"Er bestaat al een bijlage met de naam '{fileName}'.", "Duplicaat bijlage", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MijnCollectieBijlage.Add(new clsBijlageModel
                        {
                            BijlageNaam = Path.GetFileName(filePath),
                            IsNew = true,
                            Bijlage = File.ReadAllBytes(filePath)
                        });
                 
                        // Sla de bijlage tijdelijk op de schijf op
                        string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                        File.WriteAllBytes(tempFilePath, File.ReadAllBytes(filePath));
                    }
                }
            }
        }
        private void Execute_ShowBijlage(object obj)
        {
            if (MijnSelectedBijlage is clsBijlageModel bijlage)
            {
                try
                {
                    // Zoek het tijdelijke bestandspad
                    string tempFilePath = Path.Combine(Path.GetTempPath(), bijlage.BijlageNaam);

                    // Controleer of het bestand bestaat
                    if (!File.Exists(tempFilePath))
                    {
                        // Als het bestand niet bestaat, maak het dan opnieuw aan
                        File.WriteAllBytes(tempFilePath, bijlage.Bijlage);
                    }

                    // Open het bestand met de standaardtoepassing
                    Process.Start(new ProcessStartInfo(tempFilePath) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Er is een fout opgetreden bij het openen van de bijlage: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Geen geldige bijlage geselecteerd.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Execute_DeleteBijlage(object obj)
        {
            if (MijnSelectedBijlage is clsBijlageModel bijlage)
            {
                // Check of de bijlage in de tijdelijke collectie zit
                if (MijnCollectieBijlage.Contains(bijlage))
                {


                    // Verwijder bijlage uit tijdelijke collectie
                    MijnCollectieBijlage.Remove(bijlage);
          

                    // Verwijder tijdelijk bestand indien aanwezig
                    string tempFilePath = Path.Combine(Path.GetTempPath(), bijlage.BijlageNaam);
                    if (File.Exists(tempFilePath))
                    {
                        File.Delete(tempFilePath);
                    }
                }
            }
            else
            {
                MessageBox.Show("Het geselecteerde item is geen geldige bijlage.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Execute_Drop(object obj)
        {
            if (obj is DataObject dataObject && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])dataObject.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    foreach (var file in files)
                    {
                        string bijlageNaam = Path.GetFileName(file);
                        MijnCollectieBijlage.Add(new clsBijlageModel
                        {
                            IsNew = true,
                            BijlageNaam = bijlageNaam,
                            Bijlage = File.ReadAllBytes(file),
                            BudgetBijlageID = 0,
                            BudgetTransactionID = 0
                        });
                    }

                }
            }
        }

        private bool CanExecuteSubmit()
        {
            return !isSendMail; 
        }

        private async void ExecuteSubmit()
        {
            //// Hier haal je de gegevens op en verzend je de e-mail
            //clsMailModel mailModel = new clsMailModel
            //{
            //    MailFromEmail = MijnSelectedItem.PersoonID.ToString(),
            //    MailToEmail = Ontvanger,
            //    Subject = Onderwerp,
            //    Body = Bericht
            //};

            //bool emailVerzonden = await clsMail.SendEmail(mailModel);

            //if (!emailVerzonden)
            //{
            //    MessageBox.Show("Er is een fout opgetreden bij het versturen van de e-mail.");
            //}
            if (MijnSelectedItem == null)
            {
                MessageBox.Show("Selecteer een verzender.");
                return;
            }


            isSendMail = true;



            clsMailModel mailModel = new clsMailModel
            {
                MailFromEmail = MijnSelectedItem.ToString(), // Zorg ervoor dat je hier het juiste e-mailadres gebruikt
                MailToEmail = Ontvanger,
                Subject = Onderwerp,
                Body = Bericht,
                Attachments = MijnCollectieBijlage.Select(b => new clsAttachmentModel
                {
                    FileName = b.BijlageNaam,
                    ContentType = clsMimeHelper.GetMimeType(b.BijlageNaam),
                    FileData = b.Bijlage
                }).ToList()


            };

            bool emailVerzonden = await clsMail.SendEmail(mailModel);

            isSendMail = false;

            if (emailVerzonden)
            {
                MessageBox.Show("De mail is verzonden.", "Verzonden", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Er is een fout opgetreden bij het versturen van de e-mail.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private string _onderwerp;
        public string Onderwerp
        {
            get { return _onderwerp; }
            set
            {
                _onderwerp = value;
            }
        }

        private string _bericht;
        public string Bericht
        {
            get { return _bericht; }
            set
            {
                _bericht = value;
            }
        }

        private string _ontvanger;
        public string Ontvanger
        {
            get { return _ontvanger; }
            set
            {
                _ontvanger = value;
            }
        }
    }
}
