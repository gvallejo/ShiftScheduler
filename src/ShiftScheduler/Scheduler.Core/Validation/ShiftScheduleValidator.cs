using Scheduler.Core.BusinessRules;
using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.Entities;
using Scheduler.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;   

namespace Scheduler.Core.Validation
{
    public class ShiftScheduleValidator : IShiftScheduleValidator
    {
        public IShiftRuleFactory ShiftRuleFactory { get; set; }

        public ShiftScheduleValidator(IShiftRuleFactory shiftRuleFactory)
        {
            this.ShiftRuleFactory = shiftRuleFactory;
        }
        
        virtual public bool IsValid(ShiftSchedule item, out List<string> hardErrors, out List<string> softErrors)
        {
            bool result = true;
            bool softResult = true;

            hardErrors = new List<string>();
            softErrors = new List<string>();

            ShiftRuleCollection hardRules = null;
            ShiftRuleCollection softRules = null;


            LogSession.Main.EnterMethod(this, "IsValid");
            try
            {
                /*--------- Your code goes here-------*/
                List<Employee> employeeList = this.ShiftRuleFactory.DataAccess.GetEmployees();

                foreach (var employee in employeeList)
                {
                    LogSession.Main.LogMessage(string.Format("Assessing schedule for employee {0}", employee.id));
                    hardRules = this.ShiftRuleFactory.GetHardRules(employee.id);
                    softRules = this.ShiftRuleFactory.GetSoftRules(employee.id);

                    #region LogRules
                    foreach (var hardRule in hardRules)
                    {
                        LogSession.Main.LogObject(string.Format("Hard Rule {0} for employee {1}", hardRule.Name, employee.id), hardRule);
                    }
                    foreach (var softRule in softRules)
                    {
                        LogSession.Main.LogObject(string.Format("Soft Rule {0} for employee {1}", softRule.Name, employee.id), softRule);
                    }
                    #endregion

                    AssessorArgs args = new AssessorArgs() { EmployeeId = employee.id, ItemToAssess = item };

                    //Assess hard rules first 
                    List<string> hardRulesErrors = null;
                    result = result & AssessRules(args, hardRules, out hardRulesErrors);
                    if (hardRulesErrors != null)
                        hardErrors.AddRange(hardRulesErrors);

                    //Assess soft rules                    
                    List<string> softRulesErrors = null;
                    softResult = softResult & AssessRules(args, softRules, out softRulesErrors);
                    if (softRulesErrors != null)
                        softErrors.AddRange(softRulesErrors);

                    LogSession.Main.LogSeparator();
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
                LogSession.Main.LeaveMethod(this, "IsValid");
            }

            
            return result;
        }

        protected virtual  bool AssessRules(AssessorArgs args, ShiftRuleCollection rules, out List<string> errors)
        {
            bool result = true;            
            errors = new List<string>();

            foreach (var rule in rules)
            {
                List<string> ruleErrors = null;
                result = result & rule.Assess(args, out ruleErrors);
                if (ruleErrors != null)
                    errors.AddRange(ruleErrors);
            }

            return result;
        }

        
    }
}
