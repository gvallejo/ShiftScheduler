using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoC
{
    public interface IResolver
    {

        T Resolve<T>();

    }
}
