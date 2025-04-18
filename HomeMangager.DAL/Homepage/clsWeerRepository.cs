using HomeManager.Model.Homepage;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Homepage
{
    public class clsWeerRepository : IWeerRepository
    {
        public ObservableCollection<clsWeerModel> GetWeerData(string gemeente)
        {
            return Task.Run(async () => await FetchWeerData(gemeente)).Result;
        }
        private async Task<ObservableCollection<clsWeerModel>> FetchWeerData(string gemeente)
        {
            var weerLijst = new ObservableCollection<clsWeerModel>();
            using (HttpClient client = new HttpClient())
            {
                string apiKey = "00652d96a36a32d89cfda709699729b3";
                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={gemeente}&appid={apiKey}&units=metric&lang=nl";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);

                    foreach (var item in data["list"])
                    {
                        weerLijst.Add(new clsWeerModel
                        {
                            Gemeente = gemeente,
                            Datum = DateTime.Parse(item["dt_txt"].ToString()),
                            Temperatuur = (double)item["main"]["temp"],
                            Omschrijving = item["weather"][0]["description"].ToString(),
                            Icoon = $"https://openweathermap.org/img/wn/{item["weather"][0]["icon"]}.png"
                        });
                    }
                }
            }
            return weerLijst;
        }
        public bool Delete(clsWeerModel entity)
        {
            throw new NotImplementedException();
        }

        public clsWeerModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsWeerModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public clsWeerModel GetById(int id)
        {
            (DataTable DT, bool OK, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_WeerVoorkeur,
                clsDAL.Parameter("@AccountID", id));

            if (!OK)
            {
                Debug.WriteLine($"❌ SQL-fout: {boodschap}");
                return null;
            }

            if (DT.Rows.Count == 0)
            {
                Debug.WriteLine("ℹ️ Geen voorkeur gevonden voor deze gebruiker.");
                return null;
            }

            DataRow row = DT.Rows[0];
            return new clsWeerModel
            {
                AccountID = id,
                Gemeente = row["Gemeente"].ToString()
            };
        }

        public clsWeerModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public string GetGemeenteByAccountID(int accountId)
        {
            using (SqlDataReader reader = clsDAL.GetData("S_WeerVoorkeur", clsDAL.Parameter("@AccountID", accountId)))
            {
                if (reader.Read())
                {
                    return reader["Gemeente"].ToString();
                }
            }
            return null;
        }



        public bool Insert(clsWeerModel entity)
        {
            Debug.WriteLine($"📥 Insert gemeente: {entity.Gemeente} voor account {entity.AccountID}");
            (DataTable DT, bool OK, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_WeerVoorkeur,
                clsDAL.Parameter("@AccountID", entity.AccountID),
                clsDAL.Parameter("@Gemeente", entity.Gemeente));

            if (!OK)
            {
                entity.ErrorBoodschap = boodschap;
                Debug.WriteLine($"❌ Insert fout: {boodschap}");
            }
            return OK;
        }

        public bool Update(clsWeerModel entity)
        {
            Debug.WriteLine($"📥 Insert gemeente: {entity.Gemeente} voor account {entity.AccountID}");
            (DataTable DT, bool OK, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_WeerVoorkeur,
                clsDAL.Parameter("@AccountID", entity.AccountID),
                clsDAL.Parameter("@Gemeente", entity.Gemeente));

            if (!OK)
            {
                entity.ErrorBoodschap = boodschap;
                Debug.WriteLine($"❌ Insert fout: {boodschap}");
            }
            return OK;
        }
    }
}
