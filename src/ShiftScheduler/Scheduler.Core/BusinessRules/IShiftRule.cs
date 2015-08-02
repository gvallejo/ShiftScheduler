using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public interface IShiftRule
    {
        int Id { get; set; }
        int EmployeeId { get; }
        string Name { get; set; }
        string Description { get; set; }
        object Value { get; set; }
        RuleType RuleType { get; }
        ConstraintType ConstraintType { get; }
        IAssessor Assessor { get; }
        bool Enabled { get; set; }

        bool Assess(AssessorArgs args, out List<string> errors);

    }
}
