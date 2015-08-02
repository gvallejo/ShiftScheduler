using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.DataAccess.Json
{

    public interface IJsonSerializer
    {

        T Deserialize<T>(string json);

        string Serialize(object objectToSerialize);
    }
}
