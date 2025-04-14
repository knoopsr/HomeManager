using HomeManager.Model.StickyNotes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.StickyNotes
{
    class clsStickyNotesRepository : IStickyNotesRepository
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
            //SqlDataReader MijnDataReader = DAL.GetData(Properties.Resources.S_Taal);
            //MijnCollectie = new ObservableCollection<clsLanguageM>();
            //while (MijnDataReader.Read())
            //{
            //    clsLanguageM m = new clsLanguageM()
            //    {
            //        TaalID = (int)MijnDataReader["TaalID"],
            //        Taal = MijnDataReader["Taal"].ToString(),
            //        ControlField = MijnDataReader["ControlField"]
            //    };
            //    MijnCollectie.Add(m);
            //}
            //MijnDataReader.Close();
        }

        public clsStickyNotesModel GetByID(int id)
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }

            return StickyNotesCollection.Where(note => note.StickyNoteID == id).FirstOrDefault();
        }

        public clsStickyNotesModel GetFirst()
        {
            if (StickyNotesCollection == null)
            {
                GenerateCollection();
            }
            return StickyNotesCollection.FirstOrDefault();
        }

        public bool Delete(clsStickyNotesModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(clsStickyNotesModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(clsStickyNotesModel entity)
        {
            throw new NotImplementedException();
        }

        public clsStickyNotesModel GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}