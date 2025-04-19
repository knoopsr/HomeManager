using HomeManager.Common;
using HomeManager.Model.StickyNotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.StickyNotes
{
    public interface IStickyNotesDateService : IDataService<clsStickyNotesModel>
    {
        public ObservableCollection<clsStickyNotesModel> GetAllByUserID(int userID);
        public clsStickyNotesModel GetFirstByUserID(int userID);
    }
}