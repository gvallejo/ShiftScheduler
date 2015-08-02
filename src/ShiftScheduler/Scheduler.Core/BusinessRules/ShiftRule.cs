using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.DataAccess;
using Scheduler.Core.Entities;
using Scheduler.Core.Log;
using Scheduler.Core.Persistance;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public abstract class ShiftRule : IShiftRule
    {
        public int Id { get; set; }
        public int EmployeeId { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object Value { get; set; }
        public RuleType RuleType { get; private set; }
        public ConstraintType ConstraintType { get; private set; }
        public IAssessor Assessor { get; private set; }
        public bool Enabled { get; set; }

        public ShiftRule(int id, int employeeId, string ruleName, string desc, IAssessor assessor, RuleType ruleType, ConstraintType constraintType, object refObject)
        {
            this.Id = id;
            this.EmployeeId = employeeId;
            this.Name = ruleName;
            this.Assessor = assessor;                       
            this.Description = desc;            
            this.ConstraintType = constraintType;
            this.RuleType = ruleType;
            this.Value = refObject;
        }


        public virtual bool Assess(AssessorArgs args, out List<string> errors) 
        {
            bool result;
            LogSession.Main.EnterMethod(this, "Assess");
            try
            {
                /*--------- Your code goes here-------*/
                errors = null;
                LogSession.Main.LogBool("Enabled", Enabled);

                if (Enabled)
                    result = this.Assessor.Assess(args, out errors);
                else
                    result = true;

                if (result)
                    LogSession.Main.LogColored(Color.Green, "PASSED");
                else if(this.RuleType == BusinessRules.RuleType.HardRule)
                    LogSession.Main.LogColored(Color.Red, "FAILED");
                else
                    LogSession.Main.LogColored(Color.Yellow, "FAILED");
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
