using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules.Assessors
{
    public class AssessorArgs
    {
        public int EmployeeId { get; set; }
        public object ItemToAssess { get; set; }
    }
}
