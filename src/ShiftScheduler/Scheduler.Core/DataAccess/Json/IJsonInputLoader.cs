using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.DataAccess.Json
{
    public interface IJsonInputLoader
    {        
        string Load(string path, out int status);
    }
}
