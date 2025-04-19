using HomeManager.Model.Todo;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.ToDo.Categorieën;

public class clsCategorieënRepository : ICategorieënRepository
{
    private ObservableCollection<clsCategorieënM> MijnCollectie;
    public clsCategorieënRepository() { }

    public bool Delete(clsCategorieënM entity)
    {
        (DataTable DT, bool OK, string Boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_ToDoCategorieën,
            clsDAL.Parameter("TodoCategorieID", entity.ToDoCategorieID),
            clsDAL.Parameter("ControlField", entity.ControlField),
            clsDAL.Parameter("ReturnValue", 0));
        if (OK)
        {
            entity.ErrorBoodschap = Boodschap;  
        }
        return OK;
    }

    public clsCategorieënM Find()
    {
        throw new NotImplementedException();
    }

    public ObservableCollection<clsCategorieënM> GetAll()
    {
        GenerateCollection();
        return MijnCollectie;
    }

    private void GenerateCollection()
    {
        SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_ToDoCategorieën);
        MijnCollectie = new ObservableCollection<clsCategorieënM>();

        while (MijnDataReader.Read())
        {
            clsCategorieënM m = new clsCategorieënM()
            {
                ToDoCategorieID = (int)MijnDataReader["TodoCategorieID"],
                ToDoCategorie = MijnDataReader["TodoCategorie"].ToString(),
                ControlField = MijnDataReader["ControlField"]

            };
            MijnCollectie.Add(m);
        }
        MijnDataReader.Close();
    }

    public clsCategorieënM GetById(int id)
    {
        if (MijnCollectie == null)
        {
            GenerateCollection();
        }
        return MijnCollectie.FirstOrDefault();
    }

    public clsCategorieënM GetFirst()
    {
        if (MijnCollectie == null)
        {
            GenerateCollection();
        }
        return MijnCollectie.FirstOrDefault();
    }

    public bool Insert(clsCategorieënM entity)
    {
        (DataTable DT, bool OK, string Boodschap) =
            clsDAL.ExecuteDataTable(Properties.Resources.I_ToDoCategorieën,
            clsDAL.Parameter("TodoCategorie", entity.ToDoCategorie),
            clsDAL.Parameter("@ReturnValue", 0));

        if (!OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }

    public bool Update(clsCategorieënM entity)
    {
        (DataTable DT, bool OK, string Boodschap) =
            clsDAL.ExecuteDataTable(Properties.Resources.U_ToDoCategorieën,
            clsDAL.Parameter("TodoCategorieID", entity.ToDoCategorieID),
            clsDAL.Parameter("TodoCategorie", entity.ToDoCategorie),
            clsDAL.Parameter("ControlField", entity.ControlField),
            clsDAL.Parameter("@ReturnValue", 0));

        if (!OK)
        {
            entity.ErrorBoodschap = Boodschap;
        }
        return OK;
    }






}