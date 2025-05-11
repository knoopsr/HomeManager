using HomeManager.Model.Security;

namespace HomeManager.Helpers
{
    public class clsPermissionChecker
    {
        public bool HasPermission(string code)
        {
            string rechtenCodes = clsLoginModel.Instance.RechtenCodes;
            if (string.IsNullOrEmpty(rechtenCodes))
            {
                return false;
            }
            var rechtenLijst = rechtenCodes.Split('|');
            return rechtenLijst.Contains(code);
        }

        public bool PermissionViewmodel(string viewModel)
        {
            if (_destinationToPermissionMap.TryGetValue(viewModel, out string requiredPermission))
            {
                string rechtenCodes = clsLoginModel.Instance.RechtenCodes;
                if (string.IsNullOrEmpty(rechtenCodes))
                {
                    return false;
                }
                var rechtenLijst = rechtenCodes.Split('|');
                return rechtenLijst.Contains(requiredPermission);
            }
            return false;
        }


        private readonly Dictionary<string, string> _destinationToPermissionMap = new Dictionary<string, string>
        {
            {"clsPersonenViewModel","100" },
            {"clsPersoonViewModel", "120" },
            {"clsProvincieViewModel", "160" },
            {"clsLandViewModel" , "150" },
            {"clsGemeenteViewModel" , "170" },
            {"clsFunctieViewModel","180" },
            {"clsEmailTypeViewModel","130" },
            {"clsTelefoonTypeViewModel","140" },




            { "clsRechtenViewModel", "200" },
            { "clsCredentialManagementViewModel", "200" },
            { "clsCredentialGroupViewModel", "200" },
            { "clsAccountViewModel", "200" },

            { "clsCategorieënVM", "500" },
            { "clsCollectiesVM", "500" },
            { "clsKleurenVM", "500" },
            { "clsTodoVM", "500" },

            {"clsDagboekViewModel", "300" },

            { "clsOverzichtViewModel", "400" },
            { "clsTransactieViewModel", "410" },
            { "clsDomicilieringViewModel", "420" },
            { "clsBegunstigdenViewModel", "430" },
            { "clsFrequentieViewModel", "440" },
            { "clsCategorieViewModel", "450" },










            {"clsUnLockViewModel","711"},
            {"clsButtonLoggingViewModel","712"}

        };



    }
}
