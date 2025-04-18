using HomeManager.Model.Homepage;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Homepage
{
    public class clsSnelkoppelingRepository : ISnelkoppelingRepository
    {
        private ObservableCollection<clsSnelkoppelingModel> MijnCollectie;

        private void GenerateCollection(int accountId)
        {
            MijnCollectie = new ObservableCollection<clsSnelkoppelingModel>();
            using (var reader = clsDAL.GetData(Properties.Resources.S_Snelkoppelingen,
                clsDAL.Parameter("@AccountID", accountId)))
            {
                while (reader.Read())
                {
                    MijnCollectie.Add(new clsSnelkoppelingModel
                    {
                        SnelkoppelingID = (int)reader["SnelkoppelingID"],
                        AccountID = (int)reader["AccountID"],
                        Naam = reader["Naam"].ToString(),
                        Pad = reader["Pad"].ToString(),
                        Type = reader["Type"].ToString(),
                        CreatedOn = (DateTime)reader["CreatedOn"],
                        ChangedOn = reader["ChangedOn"] as DateTime?
                    });
                }
            }
        }
        public bool Delete(clsSnelkoppelingModel entity)
        {
            

            (DataTable DT, bool OK, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_Snelkoppeling,
                clsDAL.Parameter("@AccountID", entity.AccountID),
                clsDAL.Parameter("@SnelkoppelingID", entity.SnelkoppelingID));

            if (!OK)
            {
                entity.ErrorBoodschap = boodschap;
            }

            return OK;
        }

        public clsSnelkoppelingModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsSnelkoppelingModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsSnelkoppelingModel> GetByAccountId(int accountId)
        {
            ObservableCollection<clsSnelkoppelingModel> lijst = new();

            try
            {
                (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                    Properties.Resources.S_Snelkoppelingen,
                    clsDAL.Parameter("@AccountID", accountId));

                foreach (DataRow row in dt.Rows)
                {
                    lijst.Add(new clsSnelkoppelingModel
                    {
                        SnelkoppelingID = Convert.ToInt32(row["SnelkoppelingID"]),
                        AccountID = Convert.ToInt32(row["AccountID"]),
                        Naam = row["Naam"].ToString(),
                        Pad = row["Pad"].ToString(),
                        Type = row["Type"].ToString(),
                        CreatedOn = Convert.ToDateTime(row["CreatedOn"]),
                        ChangedOn = row["ChangedOn"] == DBNull.Value ? null : (DateTime?)row["ChangedOn"]
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Fout bij ophalen: {ex.Message}");
            }

            return lijst;
        }

        public clsSnelkoppelingModel GetById(int id)
        {
            return MijnCollectie.FirstOrDefault(s => s.SnelkoppelingID == id);
        }

        public clsSnelkoppelingModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsSnelkoppelingModel entity)
        {
            try
            {
                (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                    Properties.Resources.I_Snelkoppeling,
                    clsDAL.Parameter("@AccountID", entity.AccountID),
                    clsDAL.Parameter("@Naam", entity.Naam),
                    clsDAL.Parameter("@Pad", entity.Pad),
                    clsDAL.Parameter("@Type", entity.Type)
                );

                if (ok && dt.Rows.Count > 0)
                {
                    entity.SnelkoppelingID = Convert.ToInt32(dt.Rows[0]["SnelkoppelingID"]);
                }
                else
                {
                    entity.ErrorBoodschap = boodschap;
                }

                return ok;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fout bij toevoegen snelkoppeling: {ex.Message}");
                return false;
            }
        }


        public bool Update(clsSnelkoppelingModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
