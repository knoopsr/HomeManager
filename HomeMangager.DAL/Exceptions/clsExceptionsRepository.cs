using HomeManager.Model.Dagboek;
using HomeManager.Model.Exceptions;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Exceptions
{
    public class clsExceptionsRepository : IExceptionsRepsitory
    {
        private ObservableCollection<clsExceptionsModel> MijnCollectie;

        public bool Insert(clsExceptionsModel entity)
        {
            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.I_ExceptionLog,
                                                                                clsDAL.Parameter("@AccountID", entity.AccountID),
                                                                                clsDAL.Parameter("@ExceptionName", entity.ExceptionName),
                                                                                clsDAL.Parameter("@Module", entity.Module),
                                                                                clsDAL.Parameter("@Source", entity.Source),
                                                                                clsDAL.Parameter("@ExceptionMessage", entity.ExceptionMessage),
                                                                                clsDAL.Parameter("@InnerExceptionMessage", entity.InnerExceptionMessage),
                                                                                clsDAL.Parameter("@StackTrace", entity.StackTrace),
                                                                                clsDAL.Parameter("@DotNetAssembly", entity.DotNetAssembly),
                                                                                clsDAL.Parameter("@returnValue", 0)
                                                                                );
            return ok;
        }

        public ObservableCollection<clsExceptionsModel> GetAll()
        {
            MijnCollectie = new ObservableCollection<clsExceptionsModel>();
            DataTable DT = clsDAL.ExecuteDataTable(Properties.Resources.S_ExceptionsFirst1000);

            foreach (DataRow row in DT.Rows)
            {
                clsExceptionsModel obj = new clsExceptionsModel()
                {
                    ExceptionID = (int)row[0],
                    AccountID = (int)row[1],
                    ExceptionName = (string)row[2],
                    Module = (string)row[3],
                    Source = (string)row[4],
                    TargetSite = (string)row[5],
                    ExceptionMessage = (string)row[6],
                    InnerExceptionMessage = (string)row[7],
                    StackTrace = (string)row[8],
                    DotNetAssembly = (string)row[9],
                    CreatedOn = (DateTime)row[10],
                    AccountName = (string)row[11]
                };
                MijnCollectie.Add(obj);
            }
            return MijnCollectie;
        }

        public ObservableCollection<clsExceptionsModel> GetAllByAccountID(int accountID)
        {
            MijnCollectie = new ObservableCollection<clsExceptionsModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ExceptionsByAccountID,
                                                                                clsDAL.Parameter("@AccountID", accountID));

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsExceptionsModel obj = new clsExceptionsModel()
                    {
                        ExceptionID = (int)row[0],
                        AccountID = (int)row[1],
                        ExceptionName = (string)row[2],
                        Module = (string)row[3],
                        Source = (string)row[4],
                        TargetSite = (string)row[5],
                        ExceptionMessage = (string)row[6],
                        InnerExceptionMessage = (string)row[7],
                        StackTrace = (string)row[8],
                        DotNetAssembly = (string)row[9],
                        CreatedOn = (DateTime)row[10],
                        AccountName = (string)row[11]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;
        }

        public ObservableCollection<clsExceptionsModel> GetAllByExceptionName(string exceptionName)
        {
            MijnCollectie = new ObservableCollection<clsExceptionsModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ExceptionsByExceptionName,
                                                                                clsDAL.Parameter("@ExceptionName", exceptionName)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsExceptionsModel obj = new clsExceptionsModel()
                    {
                        ExceptionID = (int)row[0],
                        AccountID = (int)row[1],
                        ExceptionName = (string)row[2],
                        Module = (string)row[3],
                        Source = (string)row[4],
                        TargetSite = (string)row[5],
                        ExceptionMessage = (string)row[6],
                        InnerExceptionMessage = (string)row[7],
                        StackTrace = (string)row[8],
                        DotNetAssembly = (string)row[9],
                        CreatedOn = (DateTime)row[10],
                        AccountName = (string)row[11]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;
        }

        public ObservableCollection<clsExceptionsModel> GetAllByTargetSite(string targetSite)
        {
            MijnCollectie = new ObservableCollection<clsExceptionsModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ExceptionsByTargetSite,
                                                                                clsDAL.Parameter("@TargetSite", targetSite)
                                                                                );

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsExceptionsModel obj = new clsExceptionsModel()
                    {
                        ExceptionID = (int)row[0],
                        AccountID = (int)row[1],
                        ExceptionName = (string)row[2],
                        Module = (string)row[3],
                        Source = (string)row[4],
                        TargetSite = (string)row[5],
                        ExceptionMessage = (string)row[6],
                        InnerExceptionMessage = (string)row[7],
                        StackTrace = (string)row[8],
                        DotNetAssembly = (string)row[9],
                        CreatedOn = (DateTime)row[10],
                        AccountName = (string)row[11]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;
        }

        public ObservableCollection<clsExceptionsModel> GetAllBydate(DateTime startDate, DateTime endDate)
        {
            MijnCollectie = new ObservableCollection<clsExceptionsModel>();

            (DataTable? DT, bool ok, string boodschap) = clsDAL.ExecuteDataTable(Properties.Resources.S_ExceptionsByDate,
                                                                                clsDAL.Parameter("@startDate", startDate),
                                                                                clsDAL.Parameter("@endDate", endDate));

            if (ok)
            {
                foreach (DataRow row in DT.Rows)
                {
                    clsExceptionsModel obj = new clsExceptionsModel()
                    {
                        ExceptionID = (int)row[0],
                        AccountID = (int)row[1],
                        ExceptionName = (string)row[2],
                        Module = (string)row[3],
                        Source = (string)row[4],
                        TargetSite = (string)row[5],
                        ExceptionMessage = (string)row[6],
                        InnerExceptionMessage = (string)row[7],
                        StackTrace = (string)row[8],
                        DotNetAssembly = (string)row[9],
                        CreatedOn = (DateTime)row[10],
                        AccountName = (string)row[11]
                    };

                    MijnCollectie.Add(obj);
                }
            }
            return MijnCollectie;
        }

        #region NOT IMPLEMENTED
        public bool Update(clsExceptionsModel entity){ throw new NotImplementedException(); }
        public clsExceptionsModel GetFirst(){ throw new NotImplementedException(); }
        public clsExceptionsModel GetById(int id){ throw new NotImplementedException(); }
        public bool Delete(clsExceptionsModel entity){ throw new NotImplementedException(); }
        public clsExceptionsModel Find(){ throw new NotImplementedException(); }
        #endregion
    }
}
