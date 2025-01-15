using HomeManager.Common;
using HomeManager.DataService.Dagboek;
using HomeManager.Helpers;
using HomeManager.Model.Dagboek;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace HomeManager.ViewModel
{
    public class clsDagboekViewModel : clsCommonModelPropertiesBase
    {
        private int PersoonID
        {
            get { return clsLoginModel.Instance.AccountID; }
        }
            
        private bool isNew = false;
        private bool isEmptyCollection = false;

        private ObservableCollection<clsDagboekModel> _mijnCollectie;

        public ObservableCollection<clsDagboekModel> MijnCollectie
        {
            get { return _mijnCollectie; }
            set
            {
                _mijnCollectie = value;
                OnPropertyChanged();
            }
        }

        private clsDagboekModel _mySelectedItem;

        public clsDagboekModel MySelectedItem
        {
            get { return _mySelectedItem; }
            set
            {
                if (_mySelectedItem != value)
                {
                    if (_mySelectedItem != null)
                    {
                        IsDirty = true;
                    }
                }
                _mySelectedItem = value;
                OnPropertyChanged();
            }
        }

        public clsDagboekDataService MyService { get; set; }

        private void GenerateCollection()
        {
            MijnCollectie = new ObservableCollection<clsDagboekModel>();
            MijnCollectie = MyService.GetAllByPersoonID(PersoonID);
            if (MijnCollectie.Count == 0)
            {
                isEmptyCollection = true;
                //MessageBox.Show("collection is empty = " + isEmptyCollection.ToString());
            }


        }

        public clsDagboekViewModel()
        {
            cmdSave = new clsCustomCommand(Execute_Save_Command, CanExecute_Save_Command);
            cmdDelete = new clsCustomCommand(Execute_Delete_Command, CanExecute_Delete_Command);
            cmdNew = new clsCustomCommand(Execute_New_Command, CanExecute_New_Command);
            cmdCancel = new clsCustomCommand(Execute_Cancel_Command, CanExecute_Cancel_Command);
            cmdClose = new clsCustomCommand(Execute_Close_Command, CanExecute_Close_Command);
            cmdTest = new clsCustomCommand(Execute_Test, CanExecute_Test);
            UpdateRichTextBoxCommand = new clsCustomCommand(UpdateRichTextBox, CanExecute_UpdateRTB);


            MyService = new clsDagboekDataService();
            GenerateCollection();

            //validatie
            if (isEmptyCollection)
            {
                //dit maakt een item aan om geen lege collecties te hebben
                var _obj = new clsDagboekModel()
                {
                    PersoonID = this.PersoonID,
                    DateCreated = DateTime.Now,
                    DagboekContentString = "Er zijn nog geen files opgeslagen"
                };
                MijnCollectie.Add(_obj);
                MySelectedItem = _obj;

                isNew = true;
                isEmptyCollection = false;
            }
            else
            {
                MySelectedItem = MijnCollectie.FirstOrDefault();
               
                
            }


        }

       

        private bool CanExecute_Test(object? obj)
        {
            return true;
        }

        private void Execute_Test(object? obj)
        {
            MessageBox.Show(MySelectedItem.DagboekContentString + "\n" +
                            "IsDirty = " + MySelectedItem.IsDirty);
        }


        //testing
        public string ConvertRichTextBoxToRtf(RichTextBox richTextBox)
        {
            if (richTextBox == null) throw new ArgumentNullException(nameof(richTextBox));

            using (var memoryStream = new System.IO.MemoryStream())
            {
                var range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                range.Save(memoryStream, DataFormats.Rtf);
                return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }


        /*Commands zijn onderverdeeld in 3 regions
         * -Fields
         * -CanExecutes
         * -Actions
         */
        #region Commands
        #region CommandFields
        public ICommand cmdDelete { get; set; }
        public ICommand cmdNew { get; set; }
        public ICommand cmdSave { get; set; }
        public ICommand cmdCancel { get; set; }
        public ICommand cmdClose { get; set; }
        public ICommand cmdTest { get; set; }
        public ICommand UpdateRichTextBoxCommand { get; }
        #endregion

        #region Command CanExecutes
        private bool CanExecute_UpdateRTB(object? obj)
        {
            return true;
        }

        private bool CanExecute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_New_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Delete_Command(object? obj)
        {
            return true;
        }

        private bool CanExecute_Save_Command(object? obj)
        {
            //if (isNew || MySelectedItem.IsDirty)
            //{
            //    return true;
            //}
            //return false;

            return true;
        }
        #endregion

        #region Command Actions
        private void UpdateRichTextBox(object? obj)
        {
            var richTextBox = obj as RichTextBox;

            if (MySelectedItem != null && richTextBox != null)
            {
                string rtfString = MySelectedItem.MyRTFString;
                if (!string.IsNullOrEmpty(rtfString))
                {
                    using (var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(rtfString)))
                    {
                        richTextBox.Selection.Load(stream, DataFormats.Rtf);
                    }
                }
            }
        }

        private void Execute_Close_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_Cancel_Command(object? obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_New_Command(object? obj)
        {
            safetySave();

            var _obj = new clsDagboekModel()
            {
                PersoonID = this.PersoonID,
                DateCreated = DateTime.Now,
                DagboekContentString = string.Empty
            };
            MijnCollectie.Add(_obj);
            MySelectedItem = _obj;

            isNew = true;
        }

        private void Execute_Delete_Command(object? obj)
        {
            if (MessageBox.Show("wil je deze entry verwijderen?", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MyService.Delete(MySelectedItem);
                MijnCollectie.Remove(MySelectedItem);
                if (MijnCollectie.Count == 0)
                {
                    var _obj = new clsDagboekModel()
                    {
                        PersoonID = this.PersoonID,
                        DateCreated = DateTime.Now,
                        DagboekContentString = "Er zijn nog geen files opgeslagen"
                    };
                    MijnCollectie.Add(_obj);
                    MySelectedItem = _obj;

                    isNew = true;
                    isEmptyCollection = false;
                }
                MySelectedItem = MijnCollectie.FirstOrDefault();
            }
        }

        private void Execute_Save_Command(object? obj)
        {
            RichTextBox richTextBox = obj as RichTextBox;
            if (richTextBox != null)
            {
                MySelectedItem.MyRTFString = ConvertRichTextBoxToRtf(richTextBox);
                MessageBox.Show(MySelectedItem.MyRTFString.ToString());

                if (isNew)
                {
                    if (!MyService.Insert(MySelectedItem))
                    {
                        MessageBox.Show(MySelectedItem.ErrorBoodschap);
                    }
                    isNew = false;
                }
                else
                {
                    if(!MyService.Update(MySelectedItem))
                    {
                        MessageBox.Show(MySelectedItem.ErrorBoodschap);
                    }
                }

                MySelectedItem.IsDirty = false;

            }
            else
            {
                MessageBox.Show("casting error -> rtb isn't correct");
            }



            
        }
        #endregion
        #endregion

        private void safetySave()
        {
            if (MySelectedItem.IsDirty = true)
            {
                if (MessageBox.Show("wil je opslaan? laatste veranderingen worden anders niet opgeslagen", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes )
                {
                    if (isNew)
                    {
                        MyService.Insert(MySelectedItem);
                    }
                    else
                    {
                        MyService.Update(MySelectedItem);
                    }
                }
                else
                {
                    if (isEmptyCollection)
                    {
                        if (MessageBox.Show("wil verwijderen", "ok", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            MijnCollectie.Remove(MySelectedItem);
                        }
                        
                    }
                }
            }
        }
    }
}

