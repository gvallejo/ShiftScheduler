using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public class Scheduler: IScheduler
    {
        public Scheduler(ISchedulingAlgorithmFactory algorithmFactory)
        {
            this.AlgorithmFactory = algorithmFactory;
        }

        public ISchedulingAlgorithmFactory AlgorithmFactory { get; private set; }
        public ISchedulingAlgorithm CurrentAlgorithm { get; private set; }
        

        public virtual ShiftSchedule GenerateSchedule(List<int> weeks)
        {
            return this.CurrentAlgorithm.CalculateShiftSchedule(weeks);
        }

        public virtual void SetSchedulingAlgorithm(string name)
        {
            ISchedulingAlgorithm alg = this.AlgorithmFactory.GetSchedulingAlgorithm(name);
            if (alg != null)
                this.CurrentAlgorithm = alg;            
        }
    }
}
