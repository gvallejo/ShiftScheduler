using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using Scheduler.Core.Persistance;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.Log;
using Scheduler.Core.BusinessRules.Assessors;

namespace Scheduler.Core.BusinessRules.Rules
{
    public class TimeOffRequestRule : ShiftRule 
    {
        protected DTimeOffRequest TimeOffRequest { get; set; }
        

        public TimeOffRequestRule(string ruleName, DTimeOffRequest timeOffRequest, IAssessor assessor, RuleType ruleType):
            base(-2, timeOffRequest.employee_id, ruleName, null, assessor, ruleType, ConstraintType.TimeOffRequest, timeOffRequest )
        {
            this.Id = int.Parse(string.Format("{0}{1}{2}", timeOffRequest.employee_id, timeOffRequest.week, timeOffRequest.days[0]));
            this.Description = string.Format("Time off request by employee {0} for week {1} and days: {2}", timeOffRequest.employee_id, timeOffRequest.week, String.Join(",", timeOffRequest.days.ToArray()));
            this.TimeOffRequest = timeOffRequest;
                       
        }
    
       
    }
}
