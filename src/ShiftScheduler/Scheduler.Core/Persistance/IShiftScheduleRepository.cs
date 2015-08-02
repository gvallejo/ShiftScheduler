using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Persistance
{
    public interface IShiftScheduleRepository
    {
        ShiftSchedule GetShiftSchedule();
    }
}
