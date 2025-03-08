//using System;
//using HomeManager.Model.Personen;
//using Microsoft.Data.SqlClient;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Collections.ObjectModel;
//using System.Data;
//using System.Threading.Tasks;

//namespace HomeManager.DAL.Personen
//{
//    public class clsNotitiesRepository : INotitiesRepository
//    {
//        private ObservableCollection<clsNotitiesModel> MijnCollectie;
//        int nr = 0;
//        public clsNotitiesRepository()
//        { }
//        public bool Delete(clsNotitiesModel entity)
//        {
//            (DataTable DT, bool OK, string Boodschap) =
//                clsDAL.ExecuteDataTable(Properties.Resources.D_Notities,
//                    clsDAL.Parameter("NotitieID", entity.NotitieID),
//                    clsDAL.Parameter("ControlField", entity.ControlField),
//                    clsDAL.Parameter("@ReturnValue", 0));
//            if (!OK)
//            {
//                entity.ErrorBoodschap = Boodschap;
//            }
//            return OK;
//        }

//        public clsNotitiesModel Find()
//        {
//            throw new NotImplementedException();
//        }

//        public ObservableCollection<clsNotitiesModel> GetAll()
//        {
//            GenerateCollection();
//            return MijnCollectie;
//        }
//        private void GenerateCollection()
//        {
//            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Notities);
//            MijnCollectie = new ObservableCollection<clsNotitiesModel>();

//            while (MijnDataReader.Read())
//            {
//                clsNotitiesModel m = new clsNotitiesModel()
//                {
//                    NotitieID = (int)MijnDataReader["NotitieID"],
//                    PersoonID = (int)MijnDataReader["PersoonID"],
//                    Onderwerp = MijnDataReader["Onderwerp"].ToString(),
//                    Notitie = MijnDataReader["Notitie"].ToString(),
//                    ControlField = MijnDataReader["ControlField"]
//                };
//                MijnCollectie.Add(m);
//            }
//            MijnDataReader.Close();
//        }

//        public clsNotitiesModel GetById(int id)
//        {
//            if (MijnCollectie == null)
//            {
//                GenerateCollection();
//            }
//            return MijnCollectie.Where(notities => notities.NotitieID == id).FirstOrDefault();
//        }

//        public ObservableCollection<clsNotitiesModel> GetByPersoonID(int id)
//        {
//            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_NotitiesByID,
//                clsDAL.Parameter("PersoonID", id));
//            MijnCollectie = new ObservableCollection<clsNotitiesModel>();
//            while (MijnDataReader.Read())
//            {
//                clsNotitiesModel n = new clsNotitiesModel()
//                {
//                    NotitieID = (int)MijnDataReader["NotitieID"],
//                    PersoonID = (int)MijnDataReader["PersoonID"],
//                    Onderwerp = MijnDataReader["Onderwerp"].ToString(),
//                    Notitie = MijnDataReader["Notitie"].ToString(),
//                    CreatedOn = (DateTime)MijnDataReader["CreatedOn"],
//                    ControlField = MijnDataReader["ControlField"]
//                };
//                MijnCollectie.Add(n);
//            }
//            MijnDataReader.Close();
//            return MijnCollectie;
//        }

//        public clsNotitiesModel GetFirst()
//        {
//            if (MijnCollectie == null)
//            {
//                GenerateCollection();
//            }
//            return MijnCollectie.FirstOrDefault();
//        }

//        public bool Insert(clsNotitiesModel entity)
//        {
//            (DataTable DT, bool OK, string Boodschap) =
//                clsDAL.ExecuteDataTable(Properties.Resources.I_Notities,
//                    clsDAL.Parameter("PersoonID", entity.PersoonID),
//                    clsDAL.Parameter("Onderwerp", entity.Onderwerp),
//                    clsDAL.Parameter("Notitie", entity.Notitie),
//                    clsDAL.Parameter("@ReturnValue", 0)
//            );
//            Console.WriteLine($"ReturnValue: {Boodschap}");
//            if (!OK)
//            {
//                entity.ErrorBoodschap = Boodschap;
//                Console.WriteLine($"Insert failed: {Boodschap}"); // Log de fout
//            }
//            else
//            {
//                Console.WriteLine("Insert successful");
//            }
//            return OK;
//        }

//        public bool Update(clsNotitiesModel entity)
//        {
//            (DataTable DT, bool OK, string Boodschap) =
//                clsDAL.ExecuteDataTable(Properties.Resources.U_Notities,
//                    clsDAL.Parameter("NotitieID", entity.NotitieID),
//                    clsDAL.Parameter("PersoonID", entity.PersoonID),
//                    clsDAL.Parameter("Onderwerp", entity.Onderwerp),
//                    clsDAL.Parameter("Notitie", entity.Notitie),
//                    clsDAL.Parameter("@ControlField", entity.ControlField),
//                    clsDAL.Parameter("@ReturnValue", 0)
//                );
//            if (!OK)
//            {
//                entity.ErrorBoodschap = Boodschap;
//                Console.WriteLine($"Update failed: {Boodschap}"); // Log de fout
//            }
//            else
//            {
//                Console.WriteLine("Update successful");
//            }
//            return OK;
//        }
//    }
//}


using System;
using HomeManager.Model.Personen;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace HomeManager.DAL.Personen
{
    public class clsNotitiesRepository : INotitiesRepository
    {
        private ObservableCollection<clsNotitiesModel> MijnCollectie;
        int nr = 0;
        public clsNotitiesRepository()
        { }

        public bool Delete(clsNotitiesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_Notities,
                    clsDAL.Parameter("NotitieID", entity.NotitieID),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0));

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
                Console.WriteLine($"Delete failed: {Boodschap}");
            }
            else
            {
                Console.WriteLine("Delete successful");
            }

            return OK;
        }

        public clsNotitiesModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsNotitiesModel> GetAll()
        {
            GenerateCollection();
            return MijnCollectie;
        }

        private void GenerateCollection()
        {
            try
            {
                SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Notities);
                MijnCollectie = new ObservableCollection<clsNotitiesModel>();

                while (MijnDataReader.Read())
                {
                    clsNotitiesModel m = new clsNotitiesModel()
                    {
                        NotitieID = (int)MijnDataReader["NotitieID"],
                        PersoonID = (int)MijnDataReader["PersoonID"],
                        Onderwerp = MijnDataReader["Onderwerp"].ToString(),
                        Notitie = MijnDataReader["Notitie"].ToString(),
                        ControlField = MijnDataReader["ControlField"]
                    };
                    MijnCollectie.Add(m);
                }
                MijnDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GenerateCollection: {ex.Message}");
            }
        }

        public clsNotitiesModel GetById(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie?.FirstOrDefault(notities => notities.NotitieID == id);
        }

        public ObservableCollection<clsNotitiesModel> GetByPersoonID(int id)
        {
            try
            {
                SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_NotitiesByID,
                    clsDAL.Parameter("PersoonID", id));
                MijnCollectie = new ObservableCollection<clsNotitiesModel>();
                while (MijnDataReader.Read())
                {
                    clsNotitiesModel n = new clsNotitiesModel()
                    {
                        NotitieID = (int)MijnDataReader["NotitieID"],
                        PersoonID = (int)MijnDataReader["PersoonID"],
                        Onderwerp = MijnDataReader["Onderwerp"].ToString(),
                        Notitie = MijnDataReader["Notitie"].ToString(),
                        CreatedOn = (DateTime)MijnDataReader["CreatedOn"],
                        ControlField = MijnDataReader["ControlField"]
                    };
                    MijnCollectie.Add(n);
                }
                MijnDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByPersoonID: {ex.Message}");
            }
            return MijnCollectie;
        }

        public clsNotitiesModel GetFirst()
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie?.FirstOrDefault();
        }

        public bool Insert(clsNotitiesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_Notities,
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                    clsDAL.Parameter("Notitie", entity.Notitie),
                    clsDAL.Parameter("@ReturnValue", 0)
            );

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
                Console.WriteLine($"Insert failed: {Boodschap}");
            }
            else
            {
                Console.WriteLine("Insert successful");
            }
            return OK;
        }

        public bool Update(clsNotitiesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Notities,
                    clsDAL.Parameter("NotitieID", entity.NotitieID),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("Onderwerp", entity.Onderwerp),
                    clsDAL.Parameter("Notitie", entity.Notitie),
                    clsDAL.Parameter("@ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0)
                );

            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
                Console.WriteLine($"Update failed: {Boodschap}");
            }
            else
            {
                Console.WriteLine("Update successful");
            }
            return OK;
        }
    }
}

