using HomeManager.Model.Todo;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Todo.Bijlage
{
    public class clsTodoBijlageRepository : ITodoBijlageRepository
    {
        private ObservableCollection<clsTodoBijlageM> MijnBijlageCollectie;
        public clsTodoBijlageRepository() { }

        public bool Delete(clsTodoBijlageM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_ToDoBijlage,
                clsDAL.Parameter("TodoBijlageID", entity.TodoBijlageID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;

        }

        public clsTodoBijlageM Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsTodoBijlageM> GetAll()
        {
            GenerateCollection();
            return MijnBijlageCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_ToDoBijlage);
            MijnBijlageCollectie = new ObservableCollection<clsTodoBijlageM>();
            while (MijnDataReader.Read())
            {
                clsTodoBijlageM m = new clsTodoBijlageM()
                {
                    TodoBijlageID = (int)MijnDataReader["TodoBijlageID"],
                    TodoID = (int)MijnDataReader["TodoID"],
                    Bijlage = (byte[])MijnDataReader["Bijlage"],
                    BijlageNaam = MijnDataReader["BijlageNaam"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnBijlageCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsTodoBijlageM GetById(int id)
        {
            if (MijnBijlageCollectie == null)
            {
                GenerateCollection();
            }
            return MijnBijlageCollectie.FirstOrDefault();
        }

        public clsTodoBijlageM GetFirst()
        {
            if (MijnBijlageCollectie == null)
            {
                GenerateCollection();
            }
            return MijnBijlageCollectie.FirstOrDefault();
        }

        public bool Insert(clsTodoBijlageM entity)
        {
            //TODO: Implement Insert
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ToDoBijlage,
                clsDAL.Parameter("TodoID", entity.TodoID),
                clsDAL.Parameter("Bijlage", entity.Bijlage),
                clsDAL.Parameter("BijlageNaam", entity.BijlageNaam),
                clsDAL.Parameter("ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsTodoBijlageM entity)
        {
            //TODO: Implement Update
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_ToDoBijlage,
                clsDAL.Parameter("TodoBijlageID", entity.TodoBijlageID),
                clsDAL.Parameter("TodoID", entity.TodoID),
                clsDAL.Parameter("Bijlage", entity.Bijlage),
                clsDAL.Parameter("BijlageNaam", entity.BijlageNaam),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }
    }
}
