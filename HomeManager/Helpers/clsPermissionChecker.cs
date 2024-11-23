using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Helpers
{
    public class clsPermissionChecker
    {
        // Methode om te controleren of een specifieke code aanwezig is in RechtenCodes
        public bool HasPermission(string code)
        {
            // Haal de rechten codes op van de singleton instance van clsLoginModel
            string rechtenCodes = clsLoginModel.Instance.RechtenCodes;

            if (string.IsNullOrEmpty(rechtenCodes))
            {
                return false; // Als er geen rechten zijn ingesteld, dan meteen false retourneren
            }

            // Split de rechten codes op basis van de | teken
            var rechtenLijst = rechtenCodes.Split('|');

            // Controleer of de opgegeven code in de lijst voorkomt
            return rechtenLijst.Contains(code);
        }

        public bool PermissionViewmodel(string viewModel)
        {
            // Controleer of er een rechtencode is voor de bestemming
            if (_destinationToPermissionMap.TryGetValue(viewModel, out string requiredPermission))
            {
                // Haal de rechten codes op van de singleton instance van clsLoginModel
                string rechtenCodes = clsLoginModel.Instance.RechtenCodes;

                if (string.IsNullOrEmpty(rechtenCodes))
                {
                    return false; // Als er geen rechten zijn ingesteld, dan meteen false retourneren
                }

                // Split de rechten codes op basis van de | teken
                var rechtenLijst = rechtenCodes.Split('|');

                // Controleer of de vereiste code in de lijst voorkomt
                return rechtenLijst.Contains(requiredPermission);
            }

            // Als er geen rechtencode is gekoppeld aan de bestemming, geef false terug
            return false;
        }






        // Dictionary met hardcoded koppelingen tussen bestemming en rechtencode
        private readonly Dictionary<string, string> _destinationToPermissionMap = new Dictionary<string, string>
        {
            {"clsPersoonVM", "100" },



            { "clsRechtenViewModel", "200" },
            { "clsCredentialManagementViewModel", "200" },
            { "clsCredentialGroupViewModel", "200" },
            { "clsAccountViewModel", "200" }
        };



    }
}
