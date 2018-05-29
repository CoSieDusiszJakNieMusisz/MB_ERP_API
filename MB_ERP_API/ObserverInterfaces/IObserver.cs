using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.ObserverInterfaces
{
    public interface IObserver
    {
        void Update<T>(T Info);
    }
}
