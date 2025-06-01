using HomeManager.DAL.Security;
using HomeManager.Model.Security;
using System.Collections.ObjectModel;


namespace HomeManager.DataService.Security
{
    /// <summary>
    /// Dataservice voor loginfunctionaliteit binnen HomeManager.
    /// Verzorgt authenticatie en wachtwoordbeheer via de repositorylaag.
    /// </summary>
    public class clsloginDataService : IloginDataService
    {
        private readonly ILoginRepository _repo = new clsLoginRepository();

        /// <summary>
        /// Standaard constructor.
        /// </summary>
        public clsloginDataService() { }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public bool Delete(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public clsLoginModel Find()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public ObservableCollection<clsLoginModel> GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public clsLoginModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Authenticeert een gebruiker op basis van login en wachtwoord.
        /// </summary>
        /// <param name="login">De gebruikersnaam of login.</param>
        /// <param name="wachtwoord">Het bijhorende wachtwoord.</param>
        /// <returns>Een <see cref="clsLoginModel"/> bij succesvolle authenticatie; anders <c>null</c>.</returns>
        public clsLoginModel GetByLogin(string login, string wachtwoord)
        {
            return _repo.GetByLogin(login, wachtwoord);
        }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public clsLoginModel GetFirst()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public bool Insert(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Niet geïmplementeerd. Gooi <see cref="NotImplementedException"/>.
        /// </summary>
        public bool Update(clsLoginModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Wijzigt het wachtwoord voor een specifieke gebruiker.
        /// </summary>
        /// <param name="entity">Het loginmodel van de gebruiker.</param>
        /// <param name="Pass">Het nieuwe wachtwoord (reeds geëncrypteerd of plain afhankelijk van repo).</param>
        /// <returns><c>true</c> indien het wachtwoord succesvol werd aangepast; anders <c>false</c>.</returns>
        public bool UpdatePassWord(clsLoginModel entity, string Pass)
        {
            return _repo.UpdatePassWord(entity, Pass);
        }
    }
}
