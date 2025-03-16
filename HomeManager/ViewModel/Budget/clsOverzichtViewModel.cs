﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HomeManager.Common;
using HomeManager.Helpers;
using HomeManager.Model.Budget;
using HomeManager.DataService.Budget;
using System.Data.SqlTypes;
using HomeManager.Messages;
using HomeManager.Services;
using HomeManager.View;
using HomeManager.Model.Security;
using ClosedXML.Excel;
using System.IO;




namespace HomeManager.ViewModel
{
    public class clsOverzichtViewModel : clsCommonModelPropertiesBase
    {
        clsOverzichtDataService MijnService;

        public ICommand cmdClose { get; set; }
        public ICommand cmdEmptyInkomstenComboboxen { get; set; }
        public ICommand cmdEmptyUitgavenComboboxen { get; set; }
        public ICommand cmdExportToExcel { get; set; }

        private ObservableCollection<clsOverzichtModel> _MijnUitgaven;
        public ObservableCollection<clsOverzichtModel> MijnUitgaven
        {
            get
            {
                return _MijnUitgaven;
            }
            set
            {
                _MijnUitgaven = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<clsOverzichtModel> _MijnInkomsten;
        public ObservableCollection<clsOverzichtModel> MijnInkomsten
        {
            get
            {
                return _MijnInkomsten;
            }
            set
            {
                _MijnInkomsten = value;
                OnPropertyChanged();

            }
        }

        #region Collections_Filters

        // Gefilterde inkomsten
        private ObservableCollection<clsOverzichtModel> _GefilterdeInkomsten;
        public ObservableCollection<clsOverzichtModel> GefilterdeInkomsten
        {
            get => _GefilterdeInkomsten;
            set
            {
                _GefilterdeInkomsten = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<clsOverzichtModel> _GefilterdeUitgaven;
        public ObservableCollection<clsOverzichtModel> GefilterdeUitgaven
        {
            get => _GefilterdeUitgaven;
            set
            {
                _GefilterdeUitgaven = value;
                OnPropertyChanged();

            }
        }

        // Geselecteerde filterwaarden
        private int? _geselecteerdInkomstenJaar;
        public int? GeselecteerdInkomstenJaar
        {
            get => _geselecteerdInkomstenJaar;
            set
            {
                _geselecteerdInkomstenJaar = value;
                OnPropertyChanged();
                PasInkomstenFilterToe();
                BerekenTotaalInkomsten();
            }
        }

        private ObservableCollection<int> _beschikbareInkomstenJaren;
        public ObservableCollection<int> BeschikbareInkomstenJaren
        {
            get => _beschikbareInkomstenJaren;
            set
            {
                _beschikbareInkomstenJaren = value;
                OnPropertyChanged();
            }
        }

        private int? _geselecteerdUitgavenJaar;
        public int? GeselecteerdUitgavenJaar
        {
            get => _geselecteerdUitgavenJaar;
            set
            {
                _geselecteerdUitgavenJaar = value;
                OnPropertyChanged();
                PasUitgavenFilterToe();
                BerekenTotaalUitgaven();
            }
        }

        private ObservableCollection<int> _beschikbareUitgavenJaren;
        public ObservableCollection<int> BeschikbareUitgavenJaren
        {
            get => _beschikbareUitgavenJaren;
            set
            {
                _beschikbareUitgavenJaren = value;
                OnPropertyChanged();
            }
        }

        private string _geselecteerdeInkomstenMaand;
        public string GeselecteerdeInkomstenMaand
        {
            get => _geselecteerdeInkomstenMaand;
            set
            {
                _geselecteerdeInkomstenMaand = value;
                OnPropertyChanged();
                PasInkomstenFilterToe();
                BerekenTotaalInkomsten();

            }
        }

        private ObservableCollection<string> _beschikbareInkomstenMaanden;
        public ObservableCollection<string> BeschikbareInkomstenMaanden
        {
            get => _beschikbareInkomstenMaanden;
            set
            {
                _beschikbareInkomstenMaanden = value;
                OnPropertyChanged();
            }
        }

        private string _geselecteerdeUitgavenMaand;
        public string GeselecteerdeUitgavenMaand
        {
            get => _geselecteerdeUitgavenMaand;
            set
            {
                _geselecteerdeUitgavenMaand = value;
                OnPropertyChanged();
                PasUitgavenFilterToe();
                BerekenTotaalUitgaven();

            }
        }

        private ObservableCollection<string> _beschikbareUitgavenMaanden;
        public ObservableCollection<string> BeschikbareUitgavenMaanden
        {
            get => _beschikbareUitgavenMaanden;
            set
            {
                _beschikbareUitgavenMaanden = value;
                OnPropertyChanged();
            }
        }

        private string _geselecteerdeInkomstenBegunstigde;
        public string GeselecteerdeInkomstenBegunstigde
        {
            get => _geselecteerdeInkomstenBegunstigde;
            set
            {
                _geselecteerdeInkomstenBegunstigde = value;
                OnPropertyChanged();
                PasInkomstenFilterToe();
                BerekenTotaalInkomsten();
            }
        }

        private ObservableCollection<string> _beschikbareInkomstenBegunstigden;
        public ObservableCollection<string> BeschikbareInkomstenBegunstigden
        {
            get => _beschikbareInkomstenBegunstigden;
            set
            {
                _beschikbareInkomstenBegunstigden = value;
                OnPropertyChanged();
            }
        }
        
        private string _geselecteerdeUitgavenBegunstigde;
        public string GeselecteerdeUitgavenBegunstigde
        {
            get => _geselecteerdeUitgavenBegunstigde;
            set
            {
                _geselecteerdeUitgavenBegunstigde = value;
                OnPropertyChanged();
                PasUitgavenFilterToe();
                BerekenTotaalUitgaven();
            }
        }

        private ObservableCollection<string> _beschikbareUitgavenBegunstigden;
        public ObservableCollection<string> BeschikbareUitgavenBegunstigden
        {
            get => _beschikbareUitgavenBegunstigden;
            set
            {
                _beschikbareUitgavenBegunstigden = value;
                OnPropertyChanged();
            }
        }

        private string _geselecteerdeInkomstenCategorie;
        public string GeselecteerdeInkomstenCategorie
        {
            get => _geselecteerdeInkomstenCategorie;
            set
            {
                _geselecteerdeInkomstenCategorie = value;
                OnPropertyChanged();
                PasInkomstenFilterToe();
                BerekenTotaalInkomsten();
            }
        }

        private ObservableCollection<string> _beschikbareInkomstenCategorie;
        public ObservableCollection<string> BeschikbareInkomstenCategorie
        {
            get => _beschikbareInkomstenCategorie;
            set
            {
                _beschikbareInkomstenCategorie = value;
                OnPropertyChanged();

            }
        }
        
        private string _geselecteerdeUitgavenCategorie;
        public string GeselecteerdeUitgavenCategorie
        {
            get => _geselecteerdeUitgavenCategorie;
            set
            {
                _geselecteerdeUitgavenCategorie = value;
                OnPropertyChanged();
                PasUitgavenFilterToe();
                BerekenTotaalUitgaven();
            }
        }

        private ObservableCollection<string> _beschikbareUitgavenCategorie;
        public ObservableCollection<string> BeschikbareUitgavenCategorie
        {
            get => _beschikbareUitgavenCategorie;
            set
            {
                _beschikbareUitgavenCategorie = value;
                OnPropertyChanged();

            }
        }

        private decimal _TotaalInkomsten;
        public decimal TotaalInkomsten
        {
            get => _TotaalInkomsten;
            set
            {
                _TotaalInkomsten = value;
                OnPropertyChanged();
                BerekenResultaat();
            }
        }
        
        private decimal _TotaalUitgaven;
        public decimal TotaalUitgaven
        {
            get => _TotaalUitgaven;
            set
            {
                _TotaalUitgaven = value;
                OnPropertyChanged();
                BerekenResultaat();
            }
        }

        private decimal _resultaat;

        public decimal Resultaat
        {
            get
            {
                return _resultaat;
            }
            set
            {
                _resultaat = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private void LoadData()
        {
            MijnInkomsten = MijnService.GetInkomsten();
            MijnUitgaven = MijnService.GetUitgaven();

            TotaalInkomsten = MijnInkomsten.Sum(i => i.Bedrag);
            TotaalUitgaven = MijnUitgaven.Sum(u => u.Bedrag);

            // Haal unieke waarden op
            BeschikbareInkomstenJaren = new ObservableCollection<int>(
                MijnInkomsten.Select(inkomen => inkomen.Jaar).Distinct().OrderByDescending(jaar => jaar)
            );

            BeschikbareInkomstenMaanden = new ObservableCollection<string>(
            MijnInkomsten.Select(inkomen => inkomen.Maand).Distinct().OrderBy(maand => maand)
            );

            BeschikbareInkomstenBegunstigden = new ObservableCollection<string>(
                MijnInkomsten.Select(inkomen => inkomen.Begunstigde).Distinct().OrderBy(begunstigde => begunstigde)
            );

            BeschikbareInkomstenCategorie = new ObservableCollection<string>(
                MijnInkomsten.Select(inkomen => inkomen.BudgetCategorie).Distinct().OrderBy(categorie => categorie)
            );

            // Zet gefilterde inkomsten gelijk aan alle inkomsten bij het starten
            GefilterdeInkomsten = new ObservableCollection<clsOverzichtModel>(MijnInkomsten);

            // Stel standaard het grootste jaartal in
            GeselecteerdInkomstenJaar = BeschikbareInkomstenJaren.Max();
            
            // Haal unieke waarden op
            BeschikbareUitgavenJaren = new ObservableCollection<int>(
                MijnUitgaven.Select(inkomen => inkomen.Jaar).Distinct().OrderByDescending(jaar => jaar)
            );

            BeschikbareUitgavenMaanden = new ObservableCollection<string>(
            MijnUitgaven.Select(inkomen => inkomen.Maand).Distinct().OrderBy(maand => maand)
            );

            BeschikbareUitgavenBegunstigden = new ObservableCollection<string>(
                MijnUitgaven.Select(inkomen => inkomen.Begunstigde).Distinct().OrderBy(begunstigde => begunstigde)
            );

            BeschikbareUitgavenCategorie = new ObservableCollection<string>(
                MijnUitgaven.Select(inkomen => inkomen.BudgetCategorie).Distinct().OrderBy(categorie => categorie)
            );

            // Zet gefilterde Uitgaven gelijk aan alle Uitgaven bij het starten
            GefilterdeUitgaven = new ObservableCollection<clsOverzichtModel>(MijnUitgaven);

            // Stel standaard het grootste jaartal in
            int huidigJaar = DateTime.Now.Year;

            GeselecteerdUitgavenJaar = huidigJaar;

        }



        public clsOverzichtViewModel()
        {
            MijnService = new clsOverzichtDataService();

            cmdClose = new clsCustomCommand(Execute_CloseCommand, CanExecute_CloseCommand);
            cmdEmptyInkomstenComboboxen = new clsCustomCommand(Execute_EmptyInkomstenComboboxenCommand, CanExecute_EmptyInkomstenComboboxenCommand);
            cmdEmptyUitgavenComboboxen = new clsCustomCommand(Execute_EmptyUitgavenComboboxenCommand, CanExecute_EmptyUitgavenComboboxenCommand);
            cmdExportToExcel = new clsCustomCommand(Execute_ExportToExcelCommand, CanExecute_ExportToExcelCommand);

            LoadData();

            
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

                clsHomeVM vm = (clsHomeVM)HomeWindow.DataContext;
                vm.CurrentViewModel = null;
            }


        }

        private bool CanExecute_EmptyInkomstenComboboxenCommand(object obj)
        {
            return true;
        }

        private void Execute_EmptyInkomstenComboboxenCommand(object obj)
        {
            // Reset selectie
            GeselecteerdInkomstenJaar = BeschikbareInkomstenJaren.Max();
            GeselecteerdeInkomstenMaand = null;
            GeselecteerdeInkomstenCategorie = null;
            GeselecteerdeInkomstenBegunstigde = null;
        }
        
        private bool CanExecute_EmptyUitgavenComboboxenCommand(object obj)
        {
            return true;
        }

        private void Execute_EmptyUitgavenComboboxenCommand(object obj)
        {
            // Reset selectie
            GeselecteerdUitgavenJaar = BeschikbareUitgavenJaren.Max();
            GeselecteerdeUitgavenMaand = null;
            GeselecteerdeUitgavenCategorie = null;
            GeselecteerdeUitgavenBegunstigde = null;
        }

        private void PasInkomstenFilterToe()
        {
            var gefilterd = MijnInkomsten.AsEnumerable();

            // Filter op Jaar
            if (GeselecteerdInkomstenJaar.HasValue)
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.Jaar == GeselecteerdInkomstenJaar.Value);
            }

            // Filter op Maand
            if (!string.IsNullOrEmpty(GeselecteerdeInkomstenMaand))
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.Maand == GeselecteerdeInkomstenMaand);
            }

            // Filter op Begunstigde
            if (!string.IsNullOrEmpty(GeselecteerdeInkomstenBegunstigde))
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.Begunstigde == GeselecteerdeInkomstenBegunstigde);
            }

            // Filter op Categorie
            if (!string.IsNullOrEmpty(GeselecteerdeInkomstenCategorie))
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.BudgetCategorie == GeselecteerdeInkomstenCategorie);
            }

            // Update de gefilterde lijst
            GefilterdeInkomsten = new ObservableCollection<clsOverzichtModel>(gefilterd);
        }
        
        private void PasUitgavenFilterToe()
        {
            var gefilterd = MijnUitgaven.AsEnumerable();

            // Filter op Jaar
            if (GeselecteerdUitgavenJaar.HasValue)
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.Jaar == GeselecteerdUitgavenJaar.Value);
            }

            // Filter op Maand
            if (!string.IsNullOrEmpty(GeselecteerdeUitgavenMaand))
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.Maand == GeselecteerdeUitgavenMaand);
            }

            // Filter op Begunstigde
            if (!string.IsNullOrEmpty(GeselecteerdeUitgavenBegunstigde))
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.Begunstigde == GeselecteerdeUitgavenBegunstigde);
            }

            // Filter op Categorie
            if (!string.IsNullOrEmpty(GeselecteerdeUitgavenCategorie))
            {
                gefilterd = gefilterd.Where(inkomen => inkomen.BudgetCategorie == GeselecteerdeUitgavenCategorie);
            }

            // Update de gefilterde lijst
            GefilterdeUitgaven = new ObservableCollection<clsOverzichtModel>(gefilterd);
        }

        private void BerekenTotaalInkomsten()
        {
            if (MijnInkomsten != null)
            {
                // Filter de inkomstenlijst op basis van de geselecteerde filters
                var gefilterdeInkomsten = MijnInkomsten.Where(inkomst =>
                    (!GeselecteerdInkomstenJaar.HasValue || inkomst.Jaar == GeselecteerdInkomstenJaar) &&
                    (string.IsNullOrEmpty(GeselecteerdeInkomstenMaand) || inkomst.Maand == GeselecteerdeInkomstenMaand) &&
                    (string.IsNullOrEmpty(GeselecteerdeInkomstenCategorie) || inkomst.BudgetCategorie == GeselecteerdeInkomstenCategorie) &&
                    (string.IsNullOrEmpty(GeselecteerdeInkomstenBegunstigde) || inkomst.Begunstigde == GeselecteerdeInkomstenBegunstigde)
                );

                // Bereken het totaal van de gefilterde lijst
                TotaalInkomsten = gefilterdeInkomsten.Sum(inkomst => inkomst.Bedrag);
            }
            else
            {
                TotaalInkomsten = 0; // Geen data? Dan is het totaal nul.
            }
        }
        
        private void BerekenTotaalUitgaven()
        {
            if (MijnUitgaven != null)
            {
                // Filter de Uitgavenlijst op basis van de geselecteerde filters
                var gefilterdeUitgaven = MijnUitgaven.Where(inkomst =>
                    (!GeselecteerdUitgavenJaar.HasValue || inkomst.Jaar == GeselecteerdUitgavenJaar) &&
                    (string.IsNullOrEmpty(GeselecteerdeUitgavenMaand) || inkomst.Maand == GeselecteerdeUitgavenMaand) &&
                    (string.IsNullOrEmpty(GeselecteerdeUitgavenCategorie) || inkomst.BudgetCategorie == GeselecteerdeUitgavenCategorie) &&
                    (string.IsNullOrEmpty(GeselecteerdeUitgavenBegunstigde) || inkomst.Begunstigde == GeselecteerdeUitgavenBegunstigde)
                );

                // Bereken het totaal van de gefilterde lijst
                TotaalUitgaven = gefilterdeUitgaven.Sum(inkomst => inkomst.Bedrag);
            }
            else
            {
                TotaalUitgaven = 0; // Geen data? Dan is het totaal nul.
            }
        }

        private void BerekenResultaat()
        {
            Resultaat = TotaalInkomsten - TotaalUitgaven;
        }

        private bool CanExecute_ExportToExcelCommand(object obj)
        {
            return true;
        }

        private void Execute_ExportToExcelCommand(object obj)
        {
            try
            {
                // Maak een nieuw Excel-bestand aan
                using (var workbook = new XLWorkbook())
                {
                    // Voeg een werkblad toe voor inkomsten
                    var inkomstenSheet = workbook.Worksheets.Add("Inkomsten");
                    inkomstenSheet.Cell(1, 1).Value = "Jaar";
                    inkomstenSheet.Cell(1, 2).Value = "Maand";
                    inkomstenSheet.Cell(1, 3).Value = "Categorie";
                    inkomstenSheet.Cell(1, 4).Value = "Begunstigde";
                    inkomstenSheet.Cell(1, 5).Value = "Bedrag";
                    inkomstenSheet.Cell(1, 6).Value = "Onderwerp";

                    // Vul het werkblad met data
                    int row = 2;
                    foreach (var item in GefilterdeInkomsten)
                    {
                        inkomstenSheet.Cell(row, 1).Value = item.Jaar;
                        inkomstenSheet.Cell(row, 2).Value = item.Maand;
                        inkomstenSheet.Cell(row, 3).Value = item.BudgetCategorie;
                        inkomstenSheet.Cell(row, 4).Value = item.Begunstigde;
                        inkomstenSheet.Cell(row, 5).Value = item.Bedrag;
                        inkomstenSheet.Cell(row, 6).Value = item.Onderwerp;
                        row++;
                    }

                    // Voeg een werkblad toe voor uitgaven
                    var uitgavenSheet = workbook.Worksheets.Add("Uitgaven");
                    uitgavenSheet.Cell(1, 1).Value = "Jaar";
                    uitgavenSheet.Cell(1, 2).Value = "Maand";
                    uitgavenSheet.Cell(1, 3).Value = "Categorie";
                    uitgavenSheet.Cell(1, 4).Value = "Begunstigde";
                    uitgavenSheet.Cell(1, 5).Value = "Bedrag";
                    uitgavenSheet.Cell(1, 6).Value = "Onderwerp";

                    // Vul het werkblad met data
                    row = 2;
                    foreach (var item in GefilterdeUitgaven)
                    {
                        uitgavenSheet.Cell(row, 1).Value = item.Jaar;
                        uitgavenSheet.Cell(row, 2).Value = item.Maand;
                        uitgavenSheet.Cell(row, 3).Value = item.BudgetCategorie;
                        uitgavenSheet.Cell(row, 4).Value = item.Begunstigde;
                        uitgavenSheet.Cell(row, 5).Value = item.Bedrag;
                        uitgavenSheet.Cell(row, 6).Value = item.Onderwerp;
                        row++;
                    }

                    // Opslaan in een tijdelijk bestand
                    string filePath = Path.Combine(Path.GetTempPath(), "ExportedData.xlsx");
                    workbook.SaveAs(filePath);

                    // Open het bestand in Excel
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout tijdens exporteren: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}



