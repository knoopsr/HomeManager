using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;


namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor het beheren van rollen en hun rechten.
    /// </summary>
    public class clsRollenRepository : IRollenRepository
    {
        #region Velden

        private ObservableCollection<clsRollenModel> _mijnCollectie;
        private int nr = 0;

        #endregion

        #region Data-opbouw

        /// <summary>
        /// Laadt de rollen uit de database en vult de collectie.
        /// </summary>
        private void GenerateCollection()
        {
            SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_Rollen);
            _mijnCollectie = new ObservableCollection<clsRollenModel>();

            while (reader.Read())
            {
                var model = new clsRollenModel()
                {
                    RolID = (int)reader["RolID"],
                    RolName = (string)reader["RolName"],
                    Rechten = (string)reader["Rechten"],
                    ControlField = reader["ControlField"]
                };

                _mijnCollectie.Add(model);
            }

            reader.Close();
        }

        #endregion

        #region CRUD-methodes

        /// <inheritdoc/>
        public ObservableCollection<clsRollenModel> GetAll()
        {
            GenerateCollection();
            return _mijnCollectie;
        }

        /// <inheritdoc/>
        public clsRollenModel GetById(int id)
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault(x => x.RolID == id);
        }

        /// <inheritdoc/>
        public clsRollenModel GetFirst()
        {
            if (_mijnCollectie == null)
                GenerateCollection();

            return _mijnCollectie.FirstOrDefault();
        }

        /// <inheritdoc/>
        public bool Insert(clsRollenModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.I_Rollen,
                clsDAL.Parameter("RolName", entity.RolName),
                clsDAL.Parameter("Rechten", entity.Rechten),
                clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public bool Update(clsRollenModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.U_Rollen,
                clsDAL.Parameter("RolID", entity.RolID),
                clsDAL.Parameter("RolName", entity.RolName),
                clsDAL.Parameter("Rechten", entity.Rechten),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        /// <inheritdoc/>
        public bool Delete(clsRollenModel entity)
        {
            (DataTable DT, bool Ok, string Boodschap) = clsDAL.ExecuteDataTable(
                Properties.Resources.D_Rollen,
                clsDAL.Parameter("RolID", entity.RolID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
                entity.ErrorBoodschap = Boodschap;

            return Ok;
        }

        #endregion

        #region Niet geïmplementeerde methodes

        /// <inheritdoc/>
        public clsRollenModel Find()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
