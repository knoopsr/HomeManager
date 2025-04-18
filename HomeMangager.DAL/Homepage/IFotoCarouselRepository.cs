using HomeManager.Common;
using HomeManager.Model.Homepage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DAL.Homepage
{
    public interface IFotoCarouselRepository : IRepository<clsFotoCarouselModel>
    {
        ObservableCollection<clsFotoCarouselModel> GetByAccountId(int accountId);
    }
}
