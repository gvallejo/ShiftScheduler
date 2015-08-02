using Scheduler.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public interface IShiftRuleFactory
    {
        IDataAccess DataAccess { get; set; }

         ShiftRuleCollection GetHardRules(int employeeId);
        
         ShiftRuleCollection GetSoftRules(int employeeId);
       
    }
}
