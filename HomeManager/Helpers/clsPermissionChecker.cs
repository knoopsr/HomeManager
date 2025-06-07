using HomeManager.Model.Security;

namespace HomeManager.Helpers
{
    /// <summary>
    /// Helperklasse om gebruikersrechten te controleren op basis van RechtenCodes van de ingelogde gebruiker.
    /// </summary>
    public class clsPermissionChecker
    {
        /// <summary>
        /// Controleert of de huidige gebruiker een specifieke permissiecode bezit.
        /// </summary>
        /// <param name="code">De te controleren rechten- of permissiecode.</param>
        /// <returns><c>true</c> als de gebruiker deze rechten bezit; anders <c>false</c>.</returns>
        public bool HasPermission(string code)
        {
            string rechtenCodes = clsLoginModel.Instance.RechtenCodes;

            if (string.IsNullOrEmpty(rechtenCodes))
                return false;

            var rechtenLijst = rechtenCodes.Split('|');
            return rechtenLijst.Contains(code);
        }

        /// <summary>
        /// Controleert of de gebruiker rechten heeft om een bepaalde ViewModel te openen op basis van mapping naar een rechten-code.
        /// </summary>
        /// <param name="viewModel">De naam van het ViewModel waarvoor toegang geverifieerd moet worden.</param>
        /// <returns><c>true</c> als de gebruiker toegang heeft; anders <c>false</c>.</returns>
        public bool PermissionViewmodel(string viewModel)
        {
            if (_destinationToPermissionMap.TryGetValue(viewModel, out string requiredPermission))
            {
                string rechtenCodes = clsLoginModel.Instance.RechtenCodes;

                if (string.IsNullOrEmpty(rechtenCodes))
                    return false;

                var rechtenLijst = rechtenCodes.Split('|');
                return rechtenLijst.Contains(requiredPermission);
            }

            return false;
        }

        /// <summary>
        /// Interne mapping van ViewModel-namen naar vereiste rechten-codes.
        /// </summary>
        private readonly Dictionary<string, string> _destinationToPermissionMap = new Dictionary<string, string>
        {
            // Personenbeheer
            { "clsPersonenViewModel", "100" },
            { "clsPersoonViewModel", "120" },
            { "clsProvincieViewModel", "160" },
            { "clsLandViewModel", "150" },
            { "clsGemeenteViewModel", "170" },
            { "clsFunctieViewModel", "180" },
            { "clsEmailTypeViewModel", "130" },
            { "clsTelefoonTypeViewModel", "140" },

            // Securitybeheer
            { "clsRechtenViewModel", "200" },
            { "clsCredentialManagementViewModel", "220" },
            { "clsCredentialGroupViewModel", "210" },
            { "clsAccountViewModel", "230" },

            // Todo/Collectie beheer
            { "clsCategorieënVM", "520" },
            { "clsCollectiesVM", "510" },
            { "clsKleurenVM", "530" },
            { "clsTodoVM", "500" },

            // Dagboek
            { "clsDagboekViewModel", "300" },

            // Budgetbeheer
            { "clsOverzichtViewModel", "400" },
            { "clsTransactieViewModel", "410" },
            { "clsDomicilieringViewModel", "420" },
            { "clsBegunstigdenViewModel", "430" },
            { "clsFrequentieViewModel", "440" },
            { "clsCategorieViewModel", "450" },

            // Admin-tools
            { "clsUnLockViewModel", "711" },
            { "clsButtonLoggingViewModel", "712" }
        };
    }
}
