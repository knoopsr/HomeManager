using HomeManager.Model.Todo;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.ToDo.Kleuren;

public class clsKleurenRepository : IKleurenRepository
{
    private ObservableCollection<clsKleurenM> MijnCollectie;
    public clsKleurenRepository() { }

    public bool Delete(clsKleurenM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_ToDoKLeuren,
            clsDAL.Parameter("TodoColorID", entity.ToDoColorID),
            clsDAL.Parameter("ControlField", entity.ControlField),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }

    public clsKleurenM Find()
    {
        throw new NotImplementedException();
    }
    public ObservableCollection<clsKleurenM> GetAll()
    {
        GenerateCollection();
        return MijnCollectie;
    }

    private void GenerateCollection()
    {
        SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_ToDoKLeuren);
        MijnCollectie = new ObservableCollection<clsKleurenM>();
        while (MijnDataReader.Read())
        {
            clsKleurenM m = new clsKleurenM()
            {
                ToDoColorID = (int)MijnDataReader["TodoColorID"],
                ToDoColor = MijnDataReader["TodoColor"].ToString(),
                ControlField = MijnDataReader["ControlField"]
            };
            MijnCollectie.Add(m);
        }
        MijnDataReader.Close();
    }

    public clsKleurenM GetById(int id)
    {
        if (MijnCollectie == null)
        {
            GenerateCollection();
        }
        return MijnCollectie.FirstOrDefault();
    }

    public clsKleurenM GetFirst()
    {
        if (MijnCollectie == null)
        {
            GenerateCollection();
        }
        return MijnCollectie.FirstOrDefault();
    }

    public bool Insert(clsKleurenM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ToDoKLeuren,
            clsDAL.Parameter("TodoColor", entity.ToDoColor),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }

    public bool Update(clsKleurenM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_ToDoKLeuren,
            clsDAL.Parameter("TodoColorID", entity.ToDoColorID),
            clsDAL.Parameter("TodoColor", entity.ToDoColor),
            clsDAL.Parameter("ControlField", entity.ControlField),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }
}