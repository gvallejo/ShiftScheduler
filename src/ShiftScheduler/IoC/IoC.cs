using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class IoC
    {

        private static IResolver _resolver;



        public static T Resolve<T>()
        {

            return _resolver.Resolve<T>();

        }



        public static void Initialize(IResolver resolver)
        {

            _resolver = resolver;

        }

    }
}
