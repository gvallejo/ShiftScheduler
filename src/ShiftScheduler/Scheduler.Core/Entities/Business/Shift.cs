using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities.Business
{
    public class Shift 
    {
        public int Id { get; set; }
        public int Week { get; set; }
        public int WorkingDay { get; set;}
        public Employees AssignedEmployees { get; set; }

        public Shift()
        {
            this.AssignedEmployees = new Employees();
        }

        
    }
}
