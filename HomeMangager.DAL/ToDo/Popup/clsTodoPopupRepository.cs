using HomeManager.DAL.ToDo.Popup;
using HomeManager.Model.Todo;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Todo.Popup
{
    public class clsTodoPopupRepository : ITodoPopupRepository
    {
        private ObservableCollection<clsTodoPopupM> MijnCollectie;
        public clsTodoPopupRepository() { }

        public bool Delete(clsTodoPopupM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_ToDoPopup,
                clsDAL.Parameter("TodoID", entity.TodoID),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("Detail", entity.Detail),
                clsDAL.Parameter("GebruikerID", entity.GebruikerID),
                clsDAL.Parameter("Belangrijk", entity.Belangrijk),
                clsDAL.Parameter("TodoCollectieID", entity.TodoCollectieID),
                clsDAL.Parameter("TodoCategorieID", entity.TodoCategorieID),
                clsDAL.Parameter("TodoColorID", entity.TodoColorID),
                clsDAL.Parameter("IsKlaar", entity.IsKlaar),
                clsDAL.Parameter("Volgorde", entity.Volgorde),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("ReturnValue", 0));
            if (OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsTodoPopupM Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsTodoPopupM> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_ToDoPopup);
            MijnCollectie = new ObservableCollection<clsTodoPopupM>();
            while (MijnDataReader.Read())
            {
                clsTodoPopupM m = new clsTodoPopupM()
                {
                    TodoID = (int)MijnDataReader["TodoID"],
                    Onderwerp = MijnDataReader["Onderwerp"].ToString(),
                    Detail = MijnDataReader["Detail"].ToString(),
                    GebruikerID = (int)MijnDataReader["GebruikerID"],
                    Belangrijk = (bool)MijnDataReader["Belangrijk"],
                    TodoCollectieID = (int)MijnDataReader["TodoCollectieID"],
                    TodoCategorieID = (int)MijnDataReader["TodoCategorieID"],
                    TodoColorID = (int)MijnDataReader["TodoColorID"],
                    IsKlaar = (bool)MijnDataReader["IsKlaar"],
                    Volgorde = (int)MijnDataReader["Volgorde"],
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public clsTodoPopupM GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public clsTodoPopupM GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsTodoPopupM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ToDoPopup,
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("Detail", entity.Detail),
                clsDAL.Parameter("GebruikerID", entity.GebruikerID),
                clsDAL.Parameter("Belangrijk", entity.Belangrijk),
                clsDAL.Parameter("TodoCollectieID", entity.TodoCollectieID),
                clsDAL.Parameter("TodoCategorieID", entity.TodoCategorieID),
                clsDAL.Parameter("TodoColorID", entity.TodoColorID),
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

        public bool Update(clsTodoPopupM entity)
        {
            (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_ToDoPopup,
                clsDAL.Parameter("TodoID", entity.TodoID),
                clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                clsDAL.Parameter("Detail", entity.Detail),
                clsDAL.Parameter("GebruikerID", entity.GebruikerID),
                clsDAL.Parameter("Belangrijk", entity.Belangrijk),
                clsDAL.Parameter("TodoCollectieID", entity.TodoCollectieID),
                clsDAL.Parameter("TodoCategorieID", entity.TodoCategorieID),
                clsDAL.Parameter("TodoColorID", entity.TodoColorID),
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
