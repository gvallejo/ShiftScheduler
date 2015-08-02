using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities
{
    public class EmployeeSchedule
    {

        public int employee_id { get; set; }
        public List<int> schedule { get; set; }
        public EmployeeSchedule()
        {
            this.schedule = new List<int>();
        }

        public static Func<EmployeeSchedule, bool> ByEmployeeId(int employeeId)
        {
            return delegate(EmployeeSchedule employeeSchedule)
            {
                bool result = false;

                try
                {
                    //LogSession.EnterMethod("Range.ByLengthInRange");
                    /*--------- Your code goes here-------*/

                    if ((employeeSchedule.employee_id == employeeId))
                        result = true;

                    return result;
                    /*------------------------------------*/
                }
                catch (Exception ex)
                {
                    //LogSession.LogException(ex);
                    throw ex;
                }
                finally
                {
                    //LogSession.LeaveMethod("Range.ByLengthInRange");
                }

            };
        }

    }
}
