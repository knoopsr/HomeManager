using HomeManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Model.Agenda
{
    public class clsAgendaCategoryModel : clsCommonModelPropertiesBase
    {
        public override string ToString()
        {
            return CategoryName.ToString();
        }


        private int _categoryID;
        public int CategoryID
        {
            get { return _categoryID; }
            set { _categoryID = value; }
        }

        private string _categoryName;
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        private string _categoryDescription;
        public string CategoryDescription
        {
            get { return _categoryDescription; }
            set { _categoryDescription = value; }
        }

        private string _backgroundColor;
        public string BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }

        private string _borderColor;
        public string BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; }
        }

    }
}
