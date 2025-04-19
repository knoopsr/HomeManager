using System.Collections.ObjectModel;
using System.Data;
using HomeManager.Model.Todo;
using Microsoft.Data.SqlClient;

namespace HomeManager.DAL.ToDo.Collecties;

public class clsCollectiesRepository : ICollectiesRepository
{
    private ObservableCollection<clsCollectiesM> MijnCollectie;
    public clsCollectiesRepository() { }

    public bool Delete(clsCollectiesM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_ToDoCollecties,
            clsDAL.Parameter("TodoCollectieID", entity.ToDoCollectieID),
            clsDAL.Parameter("ControlField", entity.ControlField),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }

    public clsCollectiesM Find()
    {
        throw new NotImplementedException();
    }
    public ObservableCollection<clsCollectiesM> GetAll()
    {
        GenerateCollection();
        return MijnCollectie;
    }

    private void GenerateCollection()
    {
        SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_ToDoCollecties);
        MijnCollectie = new ObservableCollection<clsCollectiesM>();
        while (MijnDataReader.Read())
        {
            clsCollectiesM m = new clsCollectiesM()
            {
                ToDoCollectieID = (int)MijnDataReader["TodoCollectieID"],
                ToDoCollectie = MijnDataReader["TodoCollectie"].ToString(),
                ControlField = MijnDataReader["ControlField"]
            };
            MijnCollectie.Add(m);
        }
        MijnDataReader.Close();
    }

    public clsCollectiesM GetById(int id)
    {
        if (MijnCollectie == null)
        {
            GenerateCollection();
        }
        return MijnCollectie.FirstOrDefault();
    }

    public clsCollectiesM GetFirst()
    {
        if (MijnCollectie == null)
        {
            GenerateCollection();
        }
        return MijnCollectie.FirstOrDefault();
    }

    public bool Insert(clsCollectiesM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ToDoCollecties,
            clsDAL.Parameter("TodoCollectie", entity.ToDoCollectie),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }

    public bool Update(clsCollectiesM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_ToDoCollecties,
            clsDAL.Parameter("TodoCollectieID", entity.ToDoCollectieID),
            clsDAL.Parameter("TodoCollectie", entity.ToDoCollectie),
            clsDAL.Parameter("ControlField", entity.ControlField),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }
}