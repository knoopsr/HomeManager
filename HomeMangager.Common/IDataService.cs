using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMangager.Common
{
    public interface IDataService<T>
    {
        bool Insert(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        ObservableCollection<T> GetAll();
        T GetByID(int id);
        T GetFirst();
        T Find();
    }
}
