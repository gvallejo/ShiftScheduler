using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using Scheduler.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules.Assessors
{
    public class EmployeeTimeOffAssessor: IAssessor
    {

        public object ReferenceValue { get; set; }

        public EmployeeTimeOffAssessor(object refValue)
        {
            this.ReferenceValue = refValue;
        }

        public virtual bool Assess(AssessorArgs args, out List<string> errors)
        {
            bool result = true;
            ShiftSchedule schedule = null;            
            Week weekToValidate = null;
            errors = new List<string>();
           
            LogSession.Main.EnterMethod(this, "Assess");
            try
            {
                /*--------- Your code goes here-------*/

                if (args.ItemToAssess == null)
                {
                    errors.Add(string.Format("Shift Schedule passed as argument is null"));
                    return false;
                }

                schedule = (ShiftSchedule)args.ItemToAssess;

                DTimeOffRequest TimeOffRequest = (DTimeOffRequest)this.ReferenceValue;

                //If week number used by this assessor cannot be found in schedule instance, then it passes the assessment
                if (!schedule.Weeks.TryGetValue(TimeOffRequest.week, out weekToValidate))
                {
                    LogSession.Main.LogWarning("Week number: {0} used by this assessor cannot be found in schedule instance. This assessor will automatically return a TRUE value for the assessment", TimeOffRequest.week);
                    return true;
                }
     
                if(args.EmployeeId != TimeOffRequest.employee_id)
                {
                    LogSession.Main.LogWarning("Employee ID: {0} passed as argument to this assessor is different from the employee id {1} for whom this assesor is exclusively for . This assessor will automatically return a TRUE value for the assessment", args.EmployeeId, TimeOffRequest.employee_id);
                    return true;
                }
                 
                //if time off request applies to the week under evaluation
                if (weekToValidate.Id == TimeOffRequest.week)
                {
                    //foreach day off requested by employee
                    foreach (int dayOff in TimeOffRequest.days)
                    {

                        if (weekToValidate.WorkingDays[dayOff].Shifts[1].AssignedEmployees.ContainsKey(args.EmployeeId))
                        {
                            string error;
                            
                            result = false;
                            error = string.Format("Time off request for day {0} of week {1} made by employee {2} was not met.",
                                                    dayOff, TimeOffRequest.week, TimeOffRequest.employee_id);
                            LogSession.Main.LogError(error);
                            errors.Add(error);
                        }
                    }
                }
                /*------------------------------------*/
            }
            catch (Exception ex)
            {
                LogSession.Main.LogException(ex);
                throw ex;
            }
            finally
            {
                LogSession.Main.LeaveMethod(this, "Assess");
            }


            return result;
        }
    }
}
