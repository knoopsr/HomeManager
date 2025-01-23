using HomeManager.Common;
using HomeManager.Model.Agenda;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Agenda
{
    public interface IAgendaItemRepository : IRepository<clsAgendaItemModel>
    {
        ObservableCollection<clsAgendaItemModel> GetWeek(DateOnly _date);
    }
}
