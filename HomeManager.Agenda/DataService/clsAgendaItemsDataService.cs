using HomeManager.DAL.Agenda;
using HomeManager.Model.Agenda;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Agenda.DataService
{
    public class clsAgendaItemsDataService : IAgendaItemsDataService
    {
        IAgendaItemRepository _repo = new clsAgendaItemRepository();
        public bool Delete(clsAgendaItemModel entity)
        {
            return _repo.Delete(entity);
        }

        public clsAgendaItemModel Find()
        {
            return _repo.Find();
        }

        public ObservableCollection<clsAgendaItemModel> GetAll()
        {
            return _repo.GetAll();
        }

        public clsAgendaItemModel GetById(int id)
        {
            return _repo.GetById(id);
        }

        public clsAgendaItemModel GetFirst()
        {
            return _repo.GetFirst();
        }

        public ObservableCollection<clsAgendaItemModel> GetWeek(DateOnly _date)
        {
            return _repo.GetWeek(_date);
        }

        public bool Insert(clsAgendaItemModel entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(clsAgendaItemModel entity)
        {
            return _repo.Update(entity);
        }
    }
}
