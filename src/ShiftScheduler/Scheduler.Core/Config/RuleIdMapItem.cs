using Scheduler.Core.BusinessRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Config
{
    public class RuleIdMapItem 
    {
        public RuleType MappedTo { get; set; }
        public int? RuleId { get; set; }
        public ConstraintType? ConstraintType { get; set; }
        public int? EmployeeId { get; set; }
        public int? Week { get; set; }

    }
}
