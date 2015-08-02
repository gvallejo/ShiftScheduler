using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules.Rules
{
    public class MaxEmployeeShiftsPerWeekRule : ShiftRule
    {
        protected ShiftRuleValue ShiftRuleValue { get; set; }
        

        public MaxEmployeeShiftsPerWeekRule(ShiftRuleDefinition ruleDefinition, ShiftRuleValue shiftRuleValue, IAssessor assessor, RuleType type):
            base(shiftRuleValue.rule_id, shiftRuleValue.employee_id, ruleDefinition.value, ruleDefinition.description, assessor, 
            type, ConstraintType.ShiftRuleValue, shiftRuleValue.value)
        {           
            this.ShiftRuleValue = shiftRuleValue;            
        }

    }
}
