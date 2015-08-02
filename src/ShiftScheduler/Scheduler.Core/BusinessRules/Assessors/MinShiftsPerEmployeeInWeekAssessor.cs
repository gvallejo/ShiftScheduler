using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules.Assessors
{
    public class MinShiftsPerEmployeeInWeekAssessor: ShiftsPerEmployeeInWeekAssessor
    {
        public MinShiftsPerEmployeeInWeekAssessor(object refValue)
            : base(refValue)
        {

        }

        protected override bool IsShiftsAssignedToEmployeeCompliantWithRule(int shiftsAssignedToEmployee)
        {
            return (shiftsAssignedToEmployee >= (this.ReferenceValue as ShiftRuleValue).value);
        }

        protected override string GetNotCompliantFormatMessage(int shiftsAssignedToEmployee, int employeeId, int weekId, int ruleValue)
        {
           return  string.Format("Number of Shifts ({0}) assigned to employee {1} in week {2} is less than the MINIMUM allowed which is {3}",
                                shiftsAssignedToEmployee, employeeId, weekId, ruleValue);

            
        }
    }
}
