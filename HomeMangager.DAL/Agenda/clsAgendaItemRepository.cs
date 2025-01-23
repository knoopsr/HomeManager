using HomeManager.Model.Agenda;
using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Agenda
{
    public class clsAgendaItemRepository : IAgendaItemRepository
    {
        private ObservableCollection<clsAgendaItemModel> _mijnCollectie;
        int nr =0;
        public clsAgendaItemRepository()
        {
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_AgendaItems);
            _mijnCollectie = new ObservableCollection<clsAgendaItemModel>();

            while (MijnDataReader.Read())
            {
                clsAgendaItemModel m = new clsAgendaItemModel()
                {
                    AgendaID = (int)MijnDataReader["AgendaID"],
                    AgendaTitle = (string)MijnDataReader["AgendaTitle"],
                    AgendaDescription = (string)MijnDataReader["AgendaDescription"],
                    AgendaCategoryID = (int)MijnDataReader["AgendaCategoryID"],
                    AgendaDate = (DateTime)MijnDataReader["AgendaDate"],
                    AgendaBeginTime = (TimeSpan)MijnDataReader["AgendaBeginTime"],
                    AgendaEndTime = (TimeSpan)MijnDataReader["AgendaEndTime"],
                    AccountID = (int)MijnDataReader["AccountID"],
                    AgendaColor = (string)MijnDataReader["BackgroundColor"],
                    AgendaBorderColor = (string)MijnDataReader["BorderColor"],
                    ControlField = MijnDataReader["ControlField"]
                };



                _mijnCollectie.Add(m);
            }
            MijnDataReader.Close();
        }

        public bool Delete(clsAgendaItemModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_AgendaItems,
                clsDAL.Parameter("AgendaID", entity.AgendaID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public clsAgendaItemModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsAgendaItemModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        public clsAgendaItemModel GetById(int id)
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.Where(x => x.AgendaID == id).FirstOrDefault();
        }

        public clsAgendaItemModel GetFirst()
        {
            if (_mijnCollectie == null)
            {
                GenerateCollection();
            }
            return _mijnCollectie.FirstOrDefault();
        }

        public bool Insert(clsAgendaItemModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_AgendaItems,
                clsDAL.Parameter("AgendaTitle", entity.AgendaTitle),
                clsDAL.Parameter("AgendaDescription", entity.AgendaDescription),
                clsDAL.Parameter("AgendaCategoryID", entity.AgendaCategoryID),
                clsDAL.Parameter("AgendaDate", entity.AgendaDate),
                clsDAL.Parameter("AgendaBeginTime", entity.AgendaBeginTime),
                clsDAL.Parameter("AgendaEndTime", entity.AgendaEndTime),
                clsDAL.Parameter("AccountID", entity.AccountID),
                clsDAL.Parameter("@Returnvalue", 0));
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public bool Update(clsAgendaItemModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_AgendaItems,
                clsDAL.Parameter("AgendaID", entity.AgendaID),
                clsDAL.Parameter("AgendaTitle", entity.AgendaTitle),
                clsDAL.Parameter("AgendaDescription", entity.AgendaDescription),
                clsDAL.Parameter("AgendaCategoryID", entity.AgendaCategoryID),
                clsDAL.Parameter("AgendaDate", entity.AgendaDate),
                clsDAL.Parameter("AgendaBeginTime", entity.AgendaBeginTime),
                clsDAL.Parameter("AgendaEndTime", entity.AgendaEndTime),
                clsDAL.Parameter("AccountID", entity.AccountID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));
            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return Ok;
        }

        public ObservableCollection<clsAgendaItemModel> GetWeek(DateOnly date)
        {
                   GenerateCollection();
          

            // Bepaal de startdatum van de week (maandag) als DateOnly
            int daysUntilMonday = ((int)date.DayOfWeek + 6) % 7; // Hiermee wordt maandag de eerste dag
            DateOnly weekStartDateOnly = date.AddDays(-daysUntilMonday);

            // Bepaal de einddatum van de week (zondag) als DateOnly
            DateOnly weekEndDateOnly = weekStartDateOnly.AddDays(6);

            // Converteer de DateOnly start- en einddatums naar DateTime
            DateTime weekStart = weekStartDateOnly.ToDateTime(TimeOnly.MinValue);
            DateTime weekEnd = weekEndDateOnly.ToDateTime(TimeOnly.MaxValue);

            // Filter de collectie om alleen de items binnen het weekbereik te selecteren
            var weekItems = new ObservableCollection<clsAgendaItemModel>(
                _mijnCollectie.Where(item => item.AgendaDate >= weekStart && item.AgendaDate <= weekEnd)
            );

            return weekItems;
        }


    }
}
