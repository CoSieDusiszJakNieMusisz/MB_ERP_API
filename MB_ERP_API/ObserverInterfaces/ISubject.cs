using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB_ERP_API.ObserverInterfaces
{
    public interface ISubject
    {         
        void Register(IObserver observer);
        void UnRegister(IObserver observer);
        void NotifyObservers<T>(T Info);
    }
}
