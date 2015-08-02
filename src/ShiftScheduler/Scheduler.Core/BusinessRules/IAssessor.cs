using Scheduler.Core.BusinessRules.Assessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public interface IAssessor
    {
        bool Assess(AssessorArgs args, out List<string> errors);
    }
}
