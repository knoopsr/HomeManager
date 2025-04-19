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
    public class clsFotoCarouselRepository : IFotoCarouselRepository
    {
        public bool Delete(clsFotoCarouselModel entity)
        {
            throw new NotImplementedException();
        }

        public clsFotoCarouselModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFotoCarouselModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsFotoCarouselModel> GetByAccountId(int accountId)
        {
            var collectie = new ObservableCollection<clsFotoCarouselModel>();
            using (SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_FotoCarousel,
                clsDAL.Parameter("@AccountID", accountId)))
            {
                while (reader.Read())
                {
                    collectie.Add(new clsFotoCarouselModel
                    {
                      
                        AccountID = (int)reader["AccountID"],
                        FolderPath = reader["FolderPath"].ToString()
                    });
                }
            }
            return collectie;
        }

        public clsFotoCarouselModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsFotoCarouselModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsFotoCarouselModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_FotoCarousel,
                clsDAL.Parameter("AccountID", entity.AccountID),
                clsDAL.Parameter("FolderPath", entity.FolderPath));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsFotoCarouselModel entity)
        {
            return Insert(entity);
        }
    }
}
