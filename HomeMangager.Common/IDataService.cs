using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Common
{
    public interface IDataService<T>
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        ObservableCollection<T> GetAll();
        T GetById(int id);
        T GetFirst();
        T Find();

    }
}
