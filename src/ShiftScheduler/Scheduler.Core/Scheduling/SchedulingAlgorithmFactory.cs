using Scheduler.Core.BusinessRules;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public class SchedulingAlgorithmFactory : ISchedulingAlgorithmFactory
    {

        public SchedulingAlgorithmFactory(IShiftScheduleValidator scheduleValidator, IShiftRuleFactory shiftRuleFactory)
        {
            this.ScheduleValidator = scheduleValidator;
            this.ShiftRuleFactory = shiftRuleFactory;

        }


        public IShiftScheduleValidator ScheduleValidator { get; private set; }
        public IShiftRuleFactory ShiftRuleFactory { get; set; }
        

        public virtual ISchedulingAlgorithm GetSchedulingAlgorithm(string name)
        {
            ISchedulingAlgorithm algorithm = null;

            switch (name.ToUpper())
            {
                case "DUMMY":
                    algorithm = new DummyAlgorithm();
                    break;
                case "BRUTE FORCE":
                    algorithm = new BruteForceAlgorithm(this.ShiftRuleFactory, this.ScheduleValidator);
                    break;
                default:
                    break;
            }

            return algorithm;
        }
    }
}
