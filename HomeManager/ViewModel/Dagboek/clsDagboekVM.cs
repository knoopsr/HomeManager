using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Common;
using HomeManager.Model;
using HomeManager.Model.Dagboek;
using HomeManager.Services;

namespace HomeManager.ViewModel.Dagboek
{
    public class clsDagboekVM : clsCommonModelPropertiesBase
    {
		private int PersoonID;

		private ObservableCollection<clsDagboekModel> _MijnCollectie;

		public ObservableCollection<clsDagboekModel> MijnCollectie
		{
			get { return _MijnCollectie; }
			set 
			{ 
				_MijnCollectie = value;
			}
		}

		private clsDagboekModel _MySelectedItem;

		public clsDagboekModel MySelectedItem
		{
			get { return _MySelectedItem; }
			set 
			{
				if (_MySelectedItem != value)
				{
					if (_MySelectedItem != null)
					{
						IsDirty = true;
					}
				}
				_MySelectedItem = value;
				OnPropertyChanged();
			}
		}

		public clsDagboekDataService MyService { get; set; }

        private void GenerateCollection()
		{
			MijnCollectie = new ObservableCollection<clsDagboekModel>();
			MijnCollectie = MyService.GetAllByPersoonID(PersoonID);
			
		}

        public clsDagboekVM()
        {
			MyService = new clsDagboekDataService();
			GenerateCollection();
        }

    }
}
