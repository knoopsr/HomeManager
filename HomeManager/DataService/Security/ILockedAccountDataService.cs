﻿using HomeManager.Common;
using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.DataService.Security
{
   public interface ILockedAccountDataService : IDataService<clsLockedAccountModel>
    {
        bool UnLockUsers(clsLockedAccountModel AccountsIds);
    }
}
