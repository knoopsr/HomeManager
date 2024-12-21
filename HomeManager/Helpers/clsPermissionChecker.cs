using HomeManager.Model.Security;
using HomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            {"clsPersoonViewModel", "100" },
            {"clsProvincieViewModel", "100" },
            {"clsLandViewModel" , "100" },
            {"clsGemeenteViewModel" , "100" },
            {"clsFunctieViewModel","100" },
            {"clsEmailTypeViewModel","100" },
            {"clsEmailAdressenViewModel","100" },
            {"clsPersonenViewModel","100" },
            {"clsAdressenViewModel","100" },
            {"clsTelefoonNummersViewModel","100" },
            {"clsTelefoonTypeViewModel","100" },
            {"clsNotitiesViewModel","100" },
   



            { "clsRechtenViewModel", "200" },
            { "clsCredentialManagementViewModel", "200" },
            { "clsCredentialGroupViewModel", "200" },
            { "clsAccountViewModel", "200" }
        };



    }
}
