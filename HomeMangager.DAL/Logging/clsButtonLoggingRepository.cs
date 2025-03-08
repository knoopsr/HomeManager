using HomeManager.Model.Dagboek;
using HomeManager.Model.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Logging
{
    /* dit is een heel simpele repository.
     * We kunnen aleen inserts doen en selecteren op filters
     * de errorhandling is hier niet optimaal, maar ik voorzie geen fouten met enkel selects en inserts
     * 
     * default collectie die we terugkrijgen is de eerste 1000 entries (in desc order)
     * andere selects volgen het patroon GetAllByModelPropNaam(modelPropValue) en de filter is de modelPropValue
     */

    public class clsButtonLoggingRepository : IButtonLoggingRepsitory
    {
        private ObservableCollection<clsButtonLoggingModel> MijnCollectie;

        public bool Insert(clsButtonLoggingModel entity)
        {
            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ButtonLog,
                                                                                clsDAL.Parameter("@accountId", entity.AccountId),
                                                                                clsDAL.Parameter("@actionName", entity.ActionName),
                                                                                clsDAL.Parameter("@actionTarget", entity.ActionTarget),
                                                                                clsDAL.Parameter("@returnValue", 0)
                                                                                );
            return ok;
        }


        //we geven verschilende collecties terug aan de hand van welke filter we gebruiken in de view
        public ObservableCollection<clsButtonLoggingModel> GetAll()
        {
            MijnCollectie = new ObservableCollection<clsButtonLoggingModel>();

            DataTable DT = clsDAL.ExecuteDataTable(Properties.Resources.S_ButtonLogsFirst1000);

            foreach (DataRow row in DT.Rows)
            {
                clsButtonLoggingModel obj = new clsButtonLoggingModel()
                {
                    ButtonLogId = (int)row[0],
                    AccountId = (int)row[1],
                    ActionName = (string)row[2],
                    ActionTarget = (string)row[3],
                    LogTime = (DateTime)row[4],
                    AccountName = (string)row[5]
                };

                MijnCollectie.Add(obj);
            }

            return MijnCollectie;
        }

        public ObservableCollection<clsButtonLoggingModel> GetAllByAccountId(int accountId)
        {
            MijnCollectie = new ObservableCollection<clsButtonLoggingModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ButtonLogsByAccountId,
                                                                                clsDAL.Parameter("@accountID", accountId)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsButtonLoggingModel obj = new clsButtonLoggingModel()
                    {
                        ButtonLogId = (int)row[0],
                        AccountId = (int)row[1],
                        ActionName = (string)row[2],
                        ActionTarget = (string)row[3],
                        LogTime = (DateTime)row[4],
                        AccountName = (string)row[5]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;
            
        }

        public ObservableCollection<clsButtonLoggingModel> GetAllByActionName(string actionName)
        {
            MijnCollectie = new ObservableCollection<clsButtonLoggingModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ButtonLogsByActionName,
                                                                                clsDAL.Parameter("@actionName", actionName)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsButtonLoggingModel obj = new clsButtonLoggingModel()
                    {
                        ButtonLogId = (int)row[0],
                        AccountId = (int)row[1],
                        ActionName = (string)row[2],
                        ActionTarget = (string)row[3],
                        LogTime = (DateTime)row[4],
                        AccountName = (string)row[5]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;

        }

        public ObservableCollection<clsButtonLoggingModel> GetAllByActionTarget(string actionTarget)
        {
            MijnCollectie = new ObservableCollection<clsButtonLoggingModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ButtonLogsByActionTarget,
                                                                                clsDAL.Parameter("@actionTarget", actionTarget)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsButtonLoggingModel obj = new clsButtonLoggingModel()
                    {
                        ButtonLogId = (int)row[0],
                        AccountId = (int)row[1],
                        ActionName = (string)row[2],
                        ActionTarget = (string)row[3],
                        LogTime = (DateTime)row[4],
                        AccountName = (string)row[5]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;
        }

        //we nemen al de records tussen start en end date
        public ObservableCollection<clsButtonLoggingModel> GetAllBydate(DateTime startDate, DateTime endDate)
        {
            MijnCollectie = new ObservableCollection<clsButtonLoggingModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ButtonLogsByDate,
                                                                                clsDAL.Parameter("@startDate", startDate),
                                                                                clsDAL.Parameter("@endDate", endDate)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsButtonLoggingModel obj = new clsButtonLoggingModel()
                    {
                        ButtonLogId = (int)row[0],
                        AccountId = (int)row[1],
                        ActionName = (string)row[2],
                        ActionTarget = (string)row[3],
                        LogTime = (DateTime)row[4],
                        AccountName = (string)row[5]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;

        }



       
       


        #region not implemented
        public bool Update(clsButtonLoggingModel entity)
        {
            throw new NotImplementedException();
        }

        public clsButtonLoggingModel GetFirst()
        {
            throw new NotImplementedException();
        }

        //we gaan niet echt 1 log opvragen
        public clsButtonLoggingModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(clsButtonLoggingModel entity)
        {
            throw new NotImplementedException();
        }

        public clsButtonLoggingModel Find()
        {
            throw new NotImplementedException();
        }



        #endregion
    }
}
