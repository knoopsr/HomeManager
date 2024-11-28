using System.ComponentModel;
using HomeManager.Common;

namespace HomeManager.Model.Todo;

public class clsCollectiesM : clsCommonModelPropertiesBase, IDataErrorInfo
{
    public string this[string columnName] => throw new NotImplementedException();
}