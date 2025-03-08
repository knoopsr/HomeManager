using HomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;
using HomeManager.Model.Budget;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace HomeManager.DAL.Budget
{
    public class clsOverzichtRepository  : IOverzichtRepository 
    {
        private ObservableCollection<clsOverzichtModel> MijnInkomsten;
        private ObservableCollection<clsOverzichtModel> MijnUitgaven;

        public clsOverzichtRepository()
        {

        }

        public ObservableCollection<clsOverzichtModel> GetInkomsten()
        {
            if (MijnInkomsten == null)
            {
                GenerateInkomsten();
            }
            return MijnInkomsten;
        }

        private void GenerateInkomsten()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetOverzichtInkomsten);
            MijnInkomsten = new ObservableCollection<clsOverzichtModel>();

            while (MijnDataReader.Read())
            {
                clsOverzichtModel m = new clsOverzichtModel()
                {
                    Jaar = (int)MijnDataReader["Jaar"],
                    Maand = MijnDataReader["Maand"].ToString(),
                    Begunstigde = MijnDataReader["Begunstigde"].ToString(),
                    BudgetCategorie = MijnDataReader["BudgetCategorie"].ToString(),
                    Bedrag = MijnDataReader["Bedrag"] as decimal? ?? 0,
                    Onderwerp = MijnDataReader["Onderwerp"].ToString()
                    
                };
                MijnInkomsten.Add(m);
            }
            MijnDataReader.Close();
        }

        public ObservableCollection<clsOverzichtModel> GetUitgaven()
        {
            if (MijnUitgaven == null)
            {
                GenerateUitgaven();
            }
            return MijnUitgaven;
        }

        private void GenerateUitgaven()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_BudgetOverzichtUitgaven);
            MijnUitgaven = new ObservableCollection<clsOverzichtModel>();

            while (MijnDataReader.Read())
            {
                clsOverzichtModel m = new clsOverzichtModel()
                {
                    Jaar = (int)MijnDataReader["Jaar"],
                    Maand = MijnDataReader["Maand"].ToString(),
                    Begunstigde = MijnDataReader["Begunstigde"].ToString(),
                    BudgetCategorie = MijnDataReader["BudgetCategorie"].ToString(),
                    Bedrag = MijnDataReader["Bedrag"] as decimal? ?? 0,
                    Onderwerp = MijnDataReader["Onderwerp"].ToString()

                };
                MijnUitgaven.Add(m);
            }
            MijnDataReader.Close();
        }

        public bool Insert(clsOverzichtModel entity)
        {
            throw new NotImplementedException();
        }
        public bool Update(clsOverzichtModel entity)
        {
            throw new NotImplementedException();
        }
        public bool Delete(clsOverzichtModel entity)
        {
            throw new NotImplementedException();
        }

        public clsOverzichtModel GetById(int id)
        {
            throw new NotImplementedException();

        }
        public clsOverzichtModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public clsOverzichtModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsOverzichtModel> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
