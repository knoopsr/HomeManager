using HomeManager.Common;
using HomeManager.Model.Personen;
using HomeManager.Model.StickyNotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.StickyNotes
{
    public interface IStickyNotesRepository : IRepository<clsStickyNotesModel>
    {
        public ObservableCollection<clsStickyNotesModel> GetAllByUserID(int userID);
        public clsStickyNotesModel GetFirstByUserID(int userID);
    }
}
