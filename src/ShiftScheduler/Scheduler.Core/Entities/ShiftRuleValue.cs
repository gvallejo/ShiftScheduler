using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities
{
    public class ShiftRuleValue
    {

        public int employee_id { get; set; }
        public int rule_id { get; set; }
        public int value { get; set; }

        public ShiftRuleValue()
        {
            this.employee_id = Properties.Settings.Default.NULL_EMPLOYEE_ID;
        }

       

    }
}
