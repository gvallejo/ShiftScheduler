using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities.Business
{
    public class Week
    {
        public int Id { get; set; }
        public DateTime Start_Date { get; set; }
        public WorkingDays WorkingDays {get; set;}

        public Week()
        {
            this.WorkingDays = new WorkingDays();
        }

        
    }
}
