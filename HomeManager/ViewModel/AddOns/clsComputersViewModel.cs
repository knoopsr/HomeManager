using HomeManager.Common;
using HomeManager.Model.AddOns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.ViewModel.AddOns
{
    public class clsComputersViewModel : clsCommonModelPropertiesBase
    {

        private ObservableCollection<clsComputerModel> _mijnComputers;
        public ObservableCollection<clsComputerModel> MijnComputers
        {
            get
            {
                return _mijnComputers;
            }
            set
            {
                _mijnComputers = value;
                OnPropertyChanged();
            }
        }

        private clsComputerModel _selectedComputer;
        public clsComputerModel SelectedComputer
        {
            get
            {
                return _selectedComputer;
            }
            set
            {
                _selectedComputer = value;
                OnPropertyChanged();
            }
        }

        public clsComputersViewModel()
        {
            MijnComputers = new ObservableCollection<clsComputerModel>();
            LoadComputers();
        }

        private void LoadComputers()
        {
            MijnComputers.Add(new clsComputerModel
            {
                ComputerNaam = "PC1",
                IpAdres = "192.168.1.15",
                MacAdres = "00-1A-2B-3C-4D-5E"
            });
        }
    }
}
