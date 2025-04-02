using HomeManager.DataService.Personen;
using HomeManager.Helpers;
using HomeManager.Mail;
using HomeManager.MailService;
using HomeManager.Messages;
using HomeManager.Model.Mail;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static HomeManager.ViewModel.clsPersonenViewModel;

namespace HomeManager.ViewModel.Personen
{
    public class clsEmailVerzendenViewModel
    {
        clsEmailAdressenDataService VerzendenService;

        public ICommand SubmitCommand { get; private set; }

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



        public clsEmailVerzendenViewModel()
        {
            //VerzendenService = new clsEmailAdressenDataService();

            //MijnVerzenderEmailAdres = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);

            //// Initialiseer het commando
            //SubmitCommand = new RelayCommand(ExecuteSubmit, CanExecuteSubmit);
            ////Messenger
            //clsMessenger.Default.Register<clsEmailVerzendenModel>(this, OnUpdateListMessageReceived);
            VerzendenService = new clsEmailAdressenDataService();
            MijnVerzenderEmailAdres = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);

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
        }

        private bool CanExecuteSubmit()
        {
            return true; 
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

            clsMailModel mailModel = new clsMailModel
            {
                MailFromEmail = MijnSelectedItem.PersoonID.ToString(), // Zorg ervoor dat je hier het juiste e-mailadres gebruikt
                MailToEmail = Ontvanger,
                Subject = Onderwerp,
                Body = Bericht
            };

            bool emailVerzonden = await clsMail.SendEmail(mailModel);

            if (!emailVerzonden)
            {
                MessageBox.Show("Er is een fout opgetreden bij het versturen van de e-mail.");
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
