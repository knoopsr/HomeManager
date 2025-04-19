﻿using HomeManager.Common;
using HomeManager.DataService.Homepage;
using HomeManager.Helpers;
using HomeManager.Model.Homepage;
using HomeManager.Model.Security;
using HomeManager.View;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeManager.ViewModel.Homepage
{
    public class clsWeerViewModel : clsCommonModelPropertiesBase
    {
        private readonly clsWeerDataService _dataService;
        private const string API_KEY = "00652d96a36a32d89cfda709699729b3";
        private const string BASE_URL = "https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid={1}";

        public ICommand ZoekWeerCommand { get; }
        public ICommand OpslaanStadCommand { get; }
        public ICommand VolgendeDagCommand { get; }
        public ICommand VorigeDagCommand { get; }

        public clsWeerViewModel()
        {
            _dataService = new clsWeerDataService();

            ZoekWeerCommand = new clsCustomCommand(async (o) => await ZoekWeer(), KanZoekWeer);
            OpslaanStadCommand = new clsCustomCommand(OpslaanStad, KanOpslaanStad);
            VolgendeDagCommand = new clsCustomCommand(GaNaarVolgendeDag, KanNaarVolgendeDag);
            VorigeDagCommand = new clsCustomCommand(GaNaarVorigeDag, KanNaarVorigeDag);

            LaadOpgeslagenStad();

        }

        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
            }
        }

        private string _currentWeather;
        public string CurrentWeather
        {
            get => _currentWeather;
            set
            {
                _currentWeather = value;
                OnPropertyChanged();
            }
        }

        private int _huidigeDagIndex = 0;
        public int HuidigeDagIndex
        {
            get => _huidigeDagIndex;
            set
            {
                _huidigeDagIndex = value;
                OnPropertyChanged();
                UpdateHuidigeDagVoorspelling();
            }
        }

        private ObservableCollection<clsWeerModel> _weatherForecast = new();
        public ObservableCollection<clsWeerModel> WeatherForecast
        {
            get => _weatherForecast;
            set
            {
                _weatherForecast = value;
                OnPropertyChanged();
                UpdateHuidigeDagVoorspelling();
            }
        }

        private ObservableCollection<clsWeerModel> _huidigeDagVoorspelling = new();
        public ObservableCollection<clsWeerModel> HuidigeDagVoorspelling
        {
            get => _huidigeDagVoorspelling;
            set
            {
                _huidigeDagVoorspelling = value;
                OnPropertyChanged();
            }
        }

        private Dictionary<DateTime, List<clsWeerModel>> _dagVoorspellingen = new();


        private async Task ZoekWeer()
        {
            if (string.IsNullOrEmpty(SelectedCity))
            {
                Debug.WriteLine(" Geen stad opgegeven.");
                return;
            }

            string requestUrl = string.Format(BASE_URL, SelectedCity, API_KEY);
            Debug.WriteLine($" API-aanvraag: {requestUrl}");

            try
            {
                using HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonSerializer.Deserialize<clsOpenWeatherResponse>(jsonResponse);

                    WeatherForecast.Clear();
                    _dagVoorspellingen.Clear();

                    foreach (var forecast in weatherData.list)
                    {
                        DateTime datumTijd = DateTimeOffset.FromUnixTimeSeconds(forecast.dt).DateTime;

                        var weer = new clsWeerModel
                        {
                            AccountID = clsLoginModel.Instance.AccountID,
                            Gemeente = SelectedCity,
                            Temperatuur = forecast.main.temp,
                            Omschrijving = forecast.weather[0].description,
                            Icoon = $"https://openweathermap.org/img/wn/{forecast.weather[0].icon}.png",
                            Datum = datumTijd
                        };

                        if (!_dagVoorspellingen.ContainsKey(datumTijd.Date))
                            _dagVoorspellingen[datumTijd.Date] = new List<clsWeerModel>();

                        _dagVoorspellingen[datumTijd.Date].Add(weer);
                    }

                    HuidigeDagIndex = 0;
                    UpdateHuidigeDagVoorspelling();
                }
                else
                {
                    System.Windows.MessageBox.Show($"API Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($" Fout bij ophalen weerdata: {ex.Message}");
            }
        }

            private void UpdateHuidigeDagVoorspelling()
        {
            if (_dagVoorspellingen.Count == 0)
                return;

            var datums = _dagVoorspellingen.Keys.OrderBy(d => d).ToList();
            if (HuidigeDagIndex >= 0 && HuidigeDagIndex < datums.Count)
            {
                var huidigeDag = datums[HuidigeDagIndex];
                HuidigeDagVoorspelling = new ObservableCollection<clsWeerModel>(_dagVoorspellingen[huidigeDag]);
            }
        }

        private void GaNaarVolgendeDag(object parameter) => HuidigeDagIndex++;
        private void GaNaarVorigeDag(object parameter) => HuidigeDagIndex--;

        private bool KanNaarVolgendeDag(object parameter) => HuidigeDagIndex < _dagVoorspellingen.Count - 1;
        private bool KanNaarVorigeDag(object parameter) => HuidigeDagIndex > 0;

        private void OpslaanStad(object parameter)
        {
            if (string.IsNullOrWhiteSpace(SelectedCity))
            {
                Debug.WriteLine(" Geen stad om op te slaan.");
                return;
            }

            var opgeslagenData = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);
            if (opgeslagenData != null)
            {
                opgeslagenData.Gemeente = SelectedCity;
                _dataService.Update(opgeslagenData);
            }
            else
            {
                _dataService.Insert(new clsWeerModel
                {
                    AccountID = clsLoginModel.Instance.AccountID,
                    Gemeente = SelectedCity
                });
            }
        }
        private async void LaadOpgeslagenStad()
        {
            var opgeslagen = _dataService.GetByAccountId(clsLoginModel.Instance.AccountID);
            if (opgeslagen != null && !string.IsNullOrWhiteSpace(opgeslagen.Gemeente))
            {
                Debug.WriteLine($" Gemeente geladen: {opgeslagen.Gemeente}");
                SelectedCity = opgeslagen.Gemeente;
                await ZoekWeer(); 
            }
            else
            {
                Debug.WriteLine(" Geen opgeslagen gemeente gevonden.");
            }
        }

        private bool KanZoekWeer(object parameter) => true;
        private bool KanOpslaanStad(object parameter) => !string.IsNullOrWhiteSpace(SelectedCity);
    }
}

