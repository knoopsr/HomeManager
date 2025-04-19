using HomeManager.Model.StickyNotes;
using HomeManager.DAL.StickyNotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeManager.Model.Dagboek;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace HomeManager.DataService.StickyNotes
{
    public class clsStickyNotesDataService : IStickyNotesDateService
    {
        private IStickyNotesRepository myRepo = new clsStickyNotesRepository();

        public clsStickyNotesModel Find()
        {
            return myRepo.Find();
        }

        public ObservableCollection<clsStickyNotesModel> GetAll()
        {
            return myRepo.GetAll();
        }

        public clsStickyNotesModel GetById(int id)
        {
            return myRepo.GetById(id);
        }

        public clsStickyNotesModel GetFirst()
        {
            return myRepo.GetFirst();
        }

        public bool Insert(clsStickyNotesModel entity)
        {
            return myRepo.Insert(entity);
        }

        public bool Update(clsStickyNotesModel entity)
        {
            return myRepo.Update(entity);
        }

        public bool Delete(clsStickyNotesModel entity)
        {
            return myRepo.Delete(entity);
        }
    }
}
