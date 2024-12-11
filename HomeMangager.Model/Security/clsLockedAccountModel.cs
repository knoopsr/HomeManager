using HomeManager.Model.Personen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Security
{
    public class clsLockedAccountModel
    {
        public clsPersoonModel Persoon { get; set; }
        public clsAccountModel Account { get; set; }
    }
}
