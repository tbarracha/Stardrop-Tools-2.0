using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardropTools
{
    public interface IStateUpdate
    {
        void UpdateState();
    }

    public interface IStateUpdate<T>
    {
        void UpdateState(T item);
    }
}
