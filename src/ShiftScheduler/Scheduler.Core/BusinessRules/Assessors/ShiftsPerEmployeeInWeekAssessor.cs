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
    public abstract class ShiftsPerEmployeeInWeekAssessor : IAssessor
    {
        public object ReferenceValue { get; set; }

        public ShiftsPerEmployeeInWeekAssessor(object refValue)
        {
            this.ReferenceValue = refValue;
        }

        protected abstract bool IsShiftsAssignedToEmployeeCompliantWithRule(int shiftsAssignedToEmployee);
        protected abstract string GetNotCompliantFormatMessage(int shiftsAssignedToEmployee, int employeeId, int weekId, int ruleValue);

       
       

        public virtual bool Assess(AssessorArgs args, out List<string> errors)
        {
            bool result = true;
            errors = new List<string>();
            ShiftSchedule schedule = null;
            ShiftRuleValue ruleValue = null;

            LogSession.Main.EnterMethod(this, "Assess");
            try
            {
                /*--------- Your code goes here-------*/
                int employeeId = args.EmployeeId;
                ruleValue = (ShiftRuleValue)this.ReferenceValue;
        
                if (args.ItemToAssess == null)
                {
                    errors.Add(string.Format("Shift Schedule passed as argument is null"));
                    return false;
                }

                schedule = (ShiftSchedule)args.ItemToAssess;

                foreach (var weekItem in schedule.Weeks)
                {
                    Week weekToValidate = weekItem.Value;
                    int shiftsAssignedToEmployee = 0;
                               
                    //foreach day of the week under evaluation
                    foreach (var wDay in weekToValidate.WorkingDays)
                    {
                        if (wDay.Value.Shifts[1].AssignedEmployees.ContainsKey(employeeId))                        
                            shiftsAssignedToEmployee++;                                                    
                    }

                    if (!IsShiftsAssignedToEmployeeCompliantWithRule(shiftsAssignedToEmployee))
                    {
                        string error;

                        result = result & false;
                        error = GetNotCompliantFormatMessage(shiftsAssignedToEmployee, employeeId, weekToValidate.Id, ruleValue.value);                            
                        LogSession.Main.LogError(error);
                        errors.Add(error);
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
