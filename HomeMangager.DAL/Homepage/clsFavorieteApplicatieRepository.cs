using HomeManager.Model.Homepage;
using Microsoft.Data.SqlClient;
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
    public class clsFavorieteApplicatieRepository : IFavorieteApplicatieRepository
    {
        public bool Delete(clsFavorieteApplicatieModel entity)
        {
            try
            {
                (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                    Properties.Resources.D_FavorieteApplicatie, // Stored Procedure
                    clsDAL.Parameter("@AccountID", entity.AccountID),
                    clsDAL.Parameter("@ApplicationID", entity.ApplicationID)
                );

                if (!ok)
                {
                    entity.ErrorBoodschap = boodschap;
                }

                return ok;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Fout bij verwijderen: {ex.Message}");
                return false;
            }
        }


        public clsFavorieteApplicatieModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteApplicatieModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteApplicatieModel> GetByAccountId(int accountId)
        {
            var collectie = new ObservableCollection<clsFavorieteApplicatieModel>();

            using (SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_FavorieteApplicatie,
                clsDAL.Parameter("@AccountID", accountId)))
            {
                while (reader.Read())
                {
                    collectie.Add(new clsFavorieteApplicatieModel()
                    {
                        ApplicationID = (int)reader["ApplicationID"],
                        AccountID = (int)reader["AccountID"],
                        ApplicationName = reader["ApplicationName"].ToString(),
                        ApplicationPath = reader["ApplicationPath"].ToString(),
                        IconPath = reader["IconPath"]?.ToString()
                    });
                }
            }
            return collectie;
        }

        public clsFavorieteApplicatieModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsFavorieteApplicatieModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsFavorieteApplicatieModel entity)
        {
            try
            {
                (DataTable dt, bool ok, string boodschap) = clsDAL.ExecuteDataTable(
                    Properties.Resources.I_FavorieteApplicatie,
                    clsDAL.Parameter("@AccountID", entity.AccountID),
                    clsDAL.Parameter("@ApplicationName", entity.ApplicationName),
                    clsDAL.Parameter("@ApplicationPath", entity.ApplicationPath),
                    clsDAL.Parameter("@IconPath", entity.IconPath)
                );

                if (ok && dt.Rows.Count > 0)
                {
                    entity.ApplicationID = Convert.ToInt32(dt.Rows[0]["ApplicationID"]);
                }
                else
                {
                    entity.ErrorBoodschap = boodschap;
                }

                return ok;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Fout bij toevoegen: {ex.Message}");
                return false;
            }
        }

        public bool Update(clsFavorieteApplicatieModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
