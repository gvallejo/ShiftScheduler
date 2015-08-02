using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Core.Entities;
using Scheduler.Core.Persistance;
using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.Entities.Business;

namespace Scheduler.Core.BusinessRules.Rules
{
    public class MinEmployeesPerShiftRule : ShiftRule //, IValidator<Scheduler.Core.Entities.Business.Shift>
    {
        //public int Id { get; set; }

        //public int EmployeeId 
        //{
        //    get { return this.ShiftRuleValue.employee_id; }  
        //}

        //public string Name { get; set; }

        //public string Description { get; set; }

        //public object Value { get; set; }

        //public RuleType RuleType { get; private set; }

        //public ConstraintType ConstraintType { get; private set; }

        protected ShiftRuleValue ShiftRuleValue { get; set; }

        //public IAssessor Assessor { get; private set; }

        public MinEmployeesPerShiftRule(ShiftRuleDefinition ruleDefinition,  ShiftRuleValue shiftRuleValue, IAssessor assessor,  RuleType type)
            : base(shiftRuleValue.rule_id, shiftRuleValue.employee_id, ruleDefinition.value, ruleDefinition.description, 
            assessor, type, ConstraintType.ShiftRuleValue, shiftRuleValue.value)
        {            
            this.ShiftRuleValue = shiftRuleValue;           
        }


        //public bool Assess(AssessorArgs args, out List<string> errors)
        //{
        //    return this.Assessor.Assess(args, out errors);
        //}


       
    }
}
