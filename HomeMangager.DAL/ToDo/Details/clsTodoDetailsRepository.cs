using HomeManager.Model.Todo;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Todo.Details
{
    public class clsTodoDetailsRepository : ITodoDetailsRepository
    {
        private ObservableCollection<clsTodoDetailsM> MijnDetails;
        public clsTodoDetailsRepository() { }

        public bool Delete(clsTodoDetailsM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_ToDoDetails,
                clsDAL.Parameter("TodoDetailID", entity.ToDoDetailID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;

        }

        public clsTodoDetailsM Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsTodoDetailsM> GetAll()
        {
            GenerateCollection();
            return MijnDetails;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_ToDoDetails);
            MijnDetails = new ObservableCollection<clsTodoDetailsM>();
            while (MijnDataReader.Read())
            {
                clsTodoDetailsM m = new clsTodoDetailsM()
                {
                    ToDoDetailID = (int)MijnDataReader["TodoDetailID"],
                    ToDoID = (int)MijnDataReader["TodoID"],
                    ToDoDetail = MijnDataReader["TodoDetail"].ToString(),
                    IsKlaar = (bool)MijnDataReader["IsKlaar"],
                    Volgorde = (int)MijnDataReader["Volgorde"],
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnDetails.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsTodoDetailsM GetById(int id)
        {
            if (MijnDetails == null)
            {
                GenerateCollection();
            }
            return MijnDetails.FirstOrDefault();
        }

        public clsTodoDetailsM GetFirst()
        {
            if (MijnDetails == null)
            {
                GenerateCollection();
            }
            return MijnDetails.FirstOrDefault();
        }

        public bool Insert(clsTodoDetailsM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ToDoDetails,
                clsDAL.Parameter("TodoID", entity.ToDoID),
                clsDAL.Parameter("TodoDetail", entity.ToDoDetail),
                clsDAL.Parameter("IsKlaar", entity.IsKlaar),
                clsDAL.Parameter("Volgorde", entity.Volgorde),
                //clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsTodoDetailsM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_ToDoDetails,
                clsDAL.Parameter("TodoDetailID", entity.ToDoDetailID),
                clsDAL.Parameter("TodoID", entity.ToDoID),
                clsDAL.Parameter("TodoDetail", entity.ToDoDetail),
                clsDAL.Parameter("IsKlaar", entity.IsKlaar),
                clsDAL.Parameter("Volgorde", entity.Volgorde),
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
