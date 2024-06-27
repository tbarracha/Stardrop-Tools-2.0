using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardropTools
{
    public interface IStateExit
    {
        void ExitState();
    }

    public interface IStateExit<T>
    {
        void ExitState(T item);
    }
}
