using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Validation
{
    public interface IShiftScheduleValidator
    {
        bool IsValid(ShiftSchedule item, out List<string> hardErrors, out List<string> softErrors);
      
    }
}
