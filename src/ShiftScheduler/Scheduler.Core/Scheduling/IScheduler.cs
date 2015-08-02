using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public interface IScheduler
    {
        ISchedulingAlgorithmFactory AlgorithmFactory { get; }
        ISchedulingAlgorithm CurrentAlgorithm {get;}

        ShiftSchedule GenerateSchedule(List<int> weeks);
        void SetSchedulingAlgorithm(string name);
    }
}
