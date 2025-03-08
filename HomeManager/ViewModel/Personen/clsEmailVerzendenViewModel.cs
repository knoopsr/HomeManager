using HomeManager.DataService.Personen;
using HomeManager.Model.Personen;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel.Personen
{
    public class clsEmailVerzendenViewModel
    {
        clsEmailAdressenDataService VerzendenService;


        private ObservableCollection<clsEmailAdressenModel> _mijnVerzenderEmailAdres;
        public ObservableCollection<clsEmailAdressenModel> MijnVerzenderEmailAdres
        {
            get { return _mijnVerzenderEmailAdres; }
            set { _mijnVerzenderEmailAdres = value; }
        }



        public clsEmailVerzendenViewModel()
        {
            VerzendenService = new clsEmailAdressenDataService();

            MijnVerzenderEmailAdres = VerzendenService.GetByPersoonID(clsLoginModel.Instance.PersoonID);
        }
    }
}
