using HomeManager.Model.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeManager.Messages
{
    class clsCollectieAangemaaktMessage
    {
        public clsCollectiesM NieuweCollectie { get; }

        public clsCollectieAangemaaktMessage(clsCollectiesM nieuweCollectie)
        {
            NieuweCollectie = nieuweCollectie;
        }
    }
}
