using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardropTools
{
    public interface IStateInput
    {
        void HandleStateInput();
    }

    public interface IStateInput<T>
    {
        void HandleStateInput(T item);
    }
}
