using HomeManager.Model.Security;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace HomeManager.DAL.Security
{
    /// <summary>
    /// Repository voor loginfunctionaliteit en wachtwoordbeheer.
    /// </summary>
    public class clsLoginRepository : ILoginRepository
    {
        #region Velden

        private ObservableCollection<clsLoginModel> _mijnCollectie;
        int nr = 0;

        #endregion

        #region CRUD-methodes (niet geïmplementeerd)

        /// <inheritdoc/>
        public bool Delete(clsLoginModel entity) => throw new NotImplementedException();

        /// <inheritdoc/>
        public clsLoginModel Find() => throw new NotImplementedException();

        /// <inheritdoc/>
        public ObservableCollection<clsLoginModel> GetAll() => throw new NotImplementedException();

        /// <inheritdoc/>
        public clsLoginModel GetById(int id) => throw new NotImplementedException();

        /// <inheritdoc/>
        public clsLoginModel GetFirst() => throw new NotImplementedException();

        /// <inheritdoc/>
        public bool Insert(clsLoginModel entity) => throw new NotImplementedException();

        /// <inheritdoc/>
        public bool Update(clsLoginModel entity) => throw new NotImplementedException();

        #endregion

        #region Login-functionaliteit

        /// <summary>
        /// Haalt een loginrecord op uit de database op basis van login en wachtwoord.
        /// </summary>
        /// <param name="login">Gebruikersnaam.</param>
        /// <param name="wachtwoord">Wachtwoord.</param>
        /// <returns>Een instantie van <see cref="clsLoginModel"/> met ingevulde eigenschappen of foutinformatie.</returns>
        public clsLoginModel GetByLogin(string login, string wachtwoord)
        {
            var _login = clsLoginModel.Instance;

            try
            {
                using SqlDataReader reader = clsDAL.GetData(Properties.Resources.S_Login,
                    clsDAL.Parameter("@Login", login),
                    clsDAL.Parameter("@Wachtwoord", wachtwoord));

                if (reader.Read())
                {
                    _login.AccountID = (int)reader["AccountID"];
                    _login.PersoonID = (int)reader["PersoonID"];
                    _login.Naam = reader["Naam"].ToString();
                    _login.VoorNaam = reader["VoorNaam"].ToString();
                    _login.Foto = reader["Foto"] != DBNull.Value ? (byte[])reader["Foto"] : null;
                    _login.RolID = (int)reader["RolID"];
                    _login.RolName = reader["RolName"].ToString();
                    _login.CountFailLogins = (int)reader["CountFailLogins"];
                    _login.IsNew = (bool)reader["IsNew"];
                    _login.IsLock = (bool)reader["IsLock"];
                    _login.RechtenCodes = reader["RechtenCodes"].ToString();
                    _login.ControlField = reader["ControlField"];
                }
            }
            catch (SqlException ex)
            {
                foreach (SqlError error in ex.Errors)
                {
                    _login.ErrorBoodschap = error.Message;
                    _login.ErrorCode = Convert.ToInt16($"{error.Class}{error.State}");
                }

                _login.AccountID = 0; // Reset ID bij fout
            }
            catch (Exception ex)
            {
                _login.ErrorBoodschap = $"Er is een onbekende fout opgetreden: {ex.Message}";
            }

            return _login;
        }

        #endregion

        #region Wachtwoordbeheer

        /// <summary>
        /// Werkt het wachtwoord bij van een bestaand loginrecord.
        /// </summary>
        /// <param name="entity">Het loginmodel waarvan het wachtwoord aangepast moet worden.</param>
        /// <param name="Pass1">Het nieuwe wachtwoord.</param>
        /// <returns>True als de update succesvol is, anders false met foutboodschap in het model.</returns>
        public bool UpdatePassWord(clsLoginModel entity, string Pass1)
        {
            (DataTable _, bool Ok, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_Login,
                    clsDAL.Parameter("AccountID", entity.AccountID),
                    clsDAL.Parameter("PersoonID", entity.PersoonID),
                    clsDAL.Parameter("Wachtwoord", Pass1),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@Returnvalue", 0));

            if (!Ok)
            {
                entity.ErrorBoodschap = Boodschap;
            }

            return Ok;
        }

        #endregion
    }
}
