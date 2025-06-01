using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Dagboek
{
	public class clsDagboekModel : clsCommonModelPropertiesBase
	{
		public int DagboekId { get; set; }

		public string PersoonID { get; set; }

		public string? MyRTFString { get; set; } //wordt niet meer gebruikt

		public byte[]? MyFlowDocument { get; set; }

		private string _DagboekContentString; //wordt niet meer gebruikt

		public string DagboekContentString
		{
			get { return _DagboekContentString; }
			set 
			{
				if (_DagboekContentString != value)
				{
					if (_DagboekContentString != null)
					{
						IsDirty = true;
					}
				}
				_DagboekContentString = value;
				OnPropertyChanged();
			}
		} //wordt niet meer gebruikt


		public DateTime DateCreated { get; set; }

		public override string ToString()
		{
			return DateCreated.ToLongDateString();
		}

	}
}
