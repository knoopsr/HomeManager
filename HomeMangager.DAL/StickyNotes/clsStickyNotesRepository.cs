using HomeManager.Model.Personen;
using HomeManager.Model.Security;
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
    /// <summary>
    /// Repository class for accessing and manipulating sticky note data in the database.
    /// Handles CRUD operations and retrieval of sticky notes by user or ID.
    /// </summary>
    public class clsStickyNotesRepository : IStickyNotesRepository
    {
        private ObservableCollection<clsStickyNotesModel> StickyNotesCollection;

        /// <summary>
        /// Not implemented: used for fetching a single sticky note based on a condition.
        /// </summary>
        public clsStickyNotesModel Find()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all sticky notes from the database.
        /// </summary>
        public ObservableCollection<clsStickyNotesModel> GetAll()
        {
            GenerateCollection();
            return StickyNotesCollection;
        }

        /// <summary>
        /// Loads sticky notes from the database into the collection.
        /// </summary>
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

        /// <summary>
        /// Retrieves the first sticky note from the collection.
        /// </summary>
        /// <returns>The first <see cref="clsStickyNotesModel"/> or null if the collection is empty.</returns>
        public clsStickyNotesModel GetFirst()
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }
            return StickyNotesCollection.FirstOrDefault();
        }

        /// <summary>
        /// Inserts a new sticky note into the database.
        /// </summary>
        /// <param name="entity">The sticky note model to insert.</param>
        /// <returns>True if successful, false otherwise.</returns>
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

        /// <summary>
        /// Updates an existing sticky note in the database.
        /// </summary>
        /// <param name="entity">The sticky note model to update.</param>
        /// <returns>True if successful, false otherwise.</returns>
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

        /// <summary>
        /// Deletes a sticky note from the database.
        /// </summary>
        /// <param name="entity">The sticky note model to delete.</param>
        /// <returns>True if successful, false otherwise.</returns>
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

        /// <summary>
        /// Gets a sticky note by its ID.
        /// </summary>
        /// <param name="id">The ID of the sticky note.</param>
        /// <returns>The corresponding <see cref="clsStickyNotesModel"/> or null.</returns>
        public clsStickyNotesModel GetById(int id)
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }
            return StickyNotesCollection.Where(stickyNote => stickyNote.StickyNoteID == id).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves all sticky notes for a specific user.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>An <see cref="ObservableCollection{clsStickyNotesModel}"/> for the user.</returns>
        public ObservableCollection<clsStickyNotesModel> GetAllByUserID(int userID)
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_StickyNotesByID,
                clsDAL.Parameter("PersoonID", clsLoginModel.Instance.PersoonID));

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
            return StickyNotesCollection;
        }

        /// <summary>
        /// Retrieves the first sticky note for a specific user.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>The first <see cref="clsStickyNotesModel"/> or null.</returns>
        public clsStickyNotesModel GetFirstByUserID(int userID)
        {
            if (StickyNotesCollection == null)
            {
                GetAllByUserID(userID);
            }
            return StickyNotesCollection.FirstOrDefault();
        }
    }
}