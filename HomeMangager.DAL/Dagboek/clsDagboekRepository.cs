using HomeManager.Model.Dagboek;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Dagboek
{
    public class clsDagboekRepository : IDagboekRepository
    {
        private ObservableCollection<clsDagboekModel> mijnCollectie;


        private void GenerateCollection(int persoonID)
        {
            mijnCollectie = new ObservableCollection<clsDagboekModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_Dagboek,
                                                                                clsDAL.Parameter("@PersoonID", persoonID)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsDagboekModel obj = new clsDagboekModel()
                    {
                        DagboekId = (int)row[0],
                        PersoonID = (int)row[1],
                        DateCreated = (DateTime)row[2],
                        MyRTFString = (string)row[3],
                        ControlField = row[4] as object
                    };
                    
                    obj.DagboekContentString = obj.MyRTFString;
                    mijnCollectie.Add(obj);
                }
            }
        }

        #region implementation
        public bool Delete(clsDagboekModel entity)
        {
            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.D_Dagboek,
                                                                                 clsDAL.Parameter("@DagboekID", entity.DagboekId),
                                                                                 clsDAL.Parameter("@ControlField", entity.ControlField),
                                                                                 clsDAL.Parameter("@User", Environment.UserName),
                                                                                 clsDAL.Parameter("ReturnValue", 0)
                                                                                 );
            entity.ErrorBoodschap = boodschap;
            return ok;
        }





        //omwile van performance en privacy halen we alleen de content van de user binnen 
        public ObservableCollection<clsDagboekModel> GetAllByPersoonID(int PersoonID)
        {
            GenerateCollection(PersoonID);
            return mijnCollectie;
        }





        public bool Insert(clsDagboekModel entity)
        {
            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_Dagboek,
                                                                                 clsDAL.Parameter("@PersoonID", entity.PersoonID),
                                                                                 clsDAL.Parameter("@DagboekNotitie", entity.MyRTFString),
                                                                                 clsDAL.Parameter("@User", Environment.UserName),
                                                                                 clsDAL.Parameter("ReturnValue", 0)
                                                                                 );
            entity.ErrorBoodschap = boodschap;
            return ok;
        }

        public bool Update(clsDagboekModel entity)
        {
            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.U_Dagboek,
                                                                                 clsDAL.Parameter("@DagboekID", entity.DagboekId),
                                                                                 clsDAL.Parameter("@DagboekNotitie", entity.MyRTFString),
                                                                                 clsDAL.Parameter("@User", Environment.UserName),
                                                                                 clsDAL.Parameter("@ControlField", entity.ControlField),
                                                                                 clsDAL.Parameter("ReturnValue", 0)
                                                                                 );
            entity.ErrorBoodschap = boodschap;
            return ok;
        }
        #endregion

        #region implementationsNotUsed
        public clsDagboekModel GetFirst()
        {
            throw new NotImplementedException();
        }

        public clsDagboekModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public clsDagboekModel Find()
        {
            throw new NotImplementedException();
        }

        //we gebruiken GetAllByPersoonID ipv deze
        public ObservableCollection<clsDagboekModel> GetAll()
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}

