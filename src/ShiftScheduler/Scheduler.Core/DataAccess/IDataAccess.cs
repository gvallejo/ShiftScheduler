using Scheduler.Core.BusinessRules;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.DataAccess
{
    public interface IDataAccess
    {
     

        List<Employee> GetEmployees();
        List<ShiftRuleValue> GetShiftRulesValues();
        List<DTimeOffRequest> GetTimeOffRequests();
        List<ShiftRuleDefinition> GetShiftRulesDefinitions();
        ShiftRuleDefinition GetRuleDefinition(int ruleId);
        Weeks GetWeeks();
     
    }
}
