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
    public class clsFavorieteVensterRepository : IFavorieteVensterRepository
    {
        public bool Delete(clsFavorieteVensterModel entity)
        {
            try
            {
                (DataTable DT, bool OK, string boodschap) = clsDAL.ExecuteDataTable(
                    Properties.Resources.D_FavorietVenster,
                    clsDAL.Parameter("@AccountID", entity.AccountID),
                    clsDAL.Parameter("@FavorietID", entity.FavorietID)
                );

                if (!OK)
                    entity.ErrorBoodschap = boodschap;

                return OK;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Fout bij delete: {ex.Message}");
                return false;
            }
        }


        public clsFavorieteVensterModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteVensterModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFavorieteVensterModel> GetByAccountId(int accountId)
        {
            ObservableCollection<clsFavorieteVensterModel> lijst = new();

            try
            {
                using (var reader = clsDAL.GetData(Properties.Resources.S_FavorietVenster,
                    clsDAL.Parameter("@AccountID", accountId)))
                {
                    while (reader.Read())
                    {
                        lijst.Add(new clsFavorieteVensterModel
                        {
                            FavorietID = (int)reader["FavorietID"],
                            AccountID = (int)reader["AccountID"],
                            VensterNaam = reader["VensterNaam"].ToString(),
                            CreatedOn = (DateTime)reader["CreatedOn"],
                            ChangedOn = reader["ChangedOn"] as DateTime?
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Fout bij ophalen: {ex.Message}");
            }

            return lijst;
        }


        public clsFavorieteVensterModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsFavorieteVensterModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsFavorieteVensterModel entity)
        {
            try
            {
                (DataTable DT, bool OK, string boodschap) = clsDAL.ExecuteDataTable(
                    Properties.Resources.I_FavorietVenster,
                    clsDAL.Parameter("@AccountID", entity.AccountID),
                    clsDAL.Parameter("@VensterNaam", entity.VensterNaam)
                );

                if (OK && DT.Rows.Count > 0)
                {
                    entity.FavorietID = Convert.ToInt32(DT.Rows[0]["FavorietVensterID"]);
                }
                else
                {
                    entity.ErrorBoodschap = boodschap;
                }

                return OK;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Fout bij insert: {ex.Message}");
                return false;
            }
        }

        public bool Update(clsFavorieteVensterModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
