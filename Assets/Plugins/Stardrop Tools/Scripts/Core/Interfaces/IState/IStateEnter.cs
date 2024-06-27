using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardropTools
{
    public interface IStateEnter
    {
        void EnterState();
    }

    public interface IStateEnter<T>
    {
        void EnterState(T item);
    }
}
