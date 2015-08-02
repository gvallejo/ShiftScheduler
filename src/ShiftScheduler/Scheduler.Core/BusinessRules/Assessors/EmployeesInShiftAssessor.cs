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
    public class EmployeesInShiftAssessor: IAssessor
    {

        public object ReferenceValue { get; set; }

        public EmployeesInShiftAssessor(object refValue )
        {
            this.ReferenceValue = refValue;
        }

        public virtual bool Assess(AssessorArgs args, out List<string> errors)
        {
            bool result = true;
            ShiftSchedule schedule = null;                
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
                else
                    schedule = (ShiftSchedule)args.ItemToAssess;

                foreach (var weekItem in schedule.Weeks)
                {
                    Week week = weekItem.Value;
                    foreach (var wDay in week.WorkingDays)
                    {
                        Shift itemToAssess = wDay.Value.Shifts[1];
                        if (itemToAssess.AssignedEmployees.Count == (int)this.ReferenceValue)
                            result = result & true;
                        else
                        {
                            string error;
                            result = result & false;
                            error = string.Format("Shift:{0} of WorkingDay:{1} - Week:{4} has {2} employees and does not comply with the Number of employees required per shift which is {3}",
                                itemToAssess.Id, itemToAssess.WorkingDay, itemToAssess.AssignedEmployees.Count, (int)this.ReferenceValue, week.Id);
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
