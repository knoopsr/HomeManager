using HomeManager.Model.StickyNotes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.StickyNotes
{
    public class clsStickyNotesRepository : IStickyNotesRepository
    {
        private ObservableCollection<clsStickyNotesModel> StickyNotesCollection;

        public clsStickyNotesRepository() { }

        public clsStickyNotesModel Find()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<clsStickyNotesModel> GetAll()
        {
            GenerateCollection();
            return StickyNotesCollection;
        }

        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_StickyNotes);
            StickyNotesCollection = new ObservableCollection<clsStickyNotesModel>();
            
            while (MijnDataReader.Read())
            {
                clsStickyNotesModel item = new clsStickyNotesModel()
                {
                    StickyNoteID = (int)MijnDataReader[0],
                    UserID = (int)MijnDataReader[1],
                    Title = MijnDataReader["Title"].ToString(),
                    Content = MijnDataReader["Note"].ToString(),
                    Thumbnail = MijnDataReader["Thumbnail"] != DBNull.Value ? (byte[])MijnDataReader["Thumbnail"] : null,
                    ThumbnailName = MijnDataReader["ThumbnailName"].ToString(),
                    Date = (DateTime)MijnDataReader["SelectedDate"],
                    SelectedBrush = MijnDataReader["SelectedBrush"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                StickyNotesCollection.Add(item);
            }
            MijnDataReader.Close();
        }

        public clsStickyNotesModel GetByID(int id)
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }

            return StickyNotesCollection.Where(note => note.StickyNoteID == id).FirstOrDefault();
        }

        public clsStickyNotesModel GetByUserID(int id)
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }
            return StickyNotesCollection.Where(note => note.UserID == id).FirstOrDefault();
        }

        public clsStickyNotesModel GetFirst()
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }
            return StickyNotesCollection.FirstOrDefault();
        }

        public bool Insert(clsStickyNotesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.I_StickyNotes,
                clsDAL.Parameter("PersoonID", entity.UserID),
                clsDAL.Parameter("Title", entity.Title),
                clsDAL.Parameter("Note", entity.Content),
                clsDAL.Parameter("Thumbnail", entity.Thumbnail != null ? (object)entity.Thumbnail : DBNull.Value, SqlDbType.VarBinary),
                clsDAL.Parameter("ThumbnailName", entity.ThumbnailName),
                clsDAL.Parameter("SelectedDate", entity.Date),
                clsDAL.Parameter("SelectedBrush", entity.SelectedBrush),
                clsDAL.Parameter("Position", entity.Position),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Update(clsStickyNotesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.U_StickyNotes,
                clsDAL.Parameter("StickyNoteID", entity.StickyNoteID),
                clsDAL.Parameter("PersoonID", entity.UserID),
                clsDAL.Parameter("Title", entity.Title),
                clsDAL.Parameter("Note", entity.Content),
                clsDAL.Parameter("Thumbnail", entity.Thumbnail != null ? (object)entity.Thumbnail : DBNull.Value, SqlDbType.VarBinary),
                clsDAL.Parameter("ThumbnailName", entity.ThumbnailName),
                clsDAL.Parameter("SelectedDate", entity.Date),
                clsDAL.Parameter("SelectedBrush", entity.SelectedBrush),
                clsDAL.Parameter("Position", entity.Position),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0)
                );
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public bool Delete(clsStickyNotesModel entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                clsDAL.ExecuteDataTable(Properties.Resources.D_StickyNotes,
                clsDAL.Parameter("StickyNoteID", entity.StickyNoteID),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        public clsStickyNotesModel GetById(int id)
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }
            return StickyNotesCollection.Where(stickyNote => stickyNote.StickyNoteID == id).FirstOrDefault();
        }
    }
}