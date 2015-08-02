
using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.BusinessRules.Rules;
using Scheduler.Core.Config;
using Scheduler.Core.DataAccess;
using Scheduler.Core.Entities;
using Scheduler.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.BusinessRules
{
    public class ShiftRuleFactory : IShiftRuleFactory 
    {
        public IDataAccess DataAccess {get;  set;}

        protected ShiftRuleCollection m_HardRulesRepository;
        protected ShiftRuleCollection m_SoftRulesRepository; 
        
        public ShiftRuleFactory(IDataAccess dataAccess)
        {

            LogSession.Main.EnterMethod(this, "ShiftRuleFactory");
            try
            {
                /*--------- Your code goes here-------*/
                this.DataAccess = dataAccess;

                this.m_HardRulesRepository = new ShiftRuleCollection();
                this.m_SoftRulesRepository = new ShiftRuleCollection();

                CreateRulesInstances(ref m_HardRulesRepository, ref m_SoftRulesRepository);

                foreach (var hardRule in this.m_HardRulesRepository)
                {
                    LogSession.Main.LogObject("Hard Rules Repository item", hardRule);
                }
                foreach (var softRule in this.m_SoftRulesRepository)
                {
                    LogSession.Main.LogObject("Soft Rules Repository item", softRule);
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
                LogSession.Main.LeaveMethod(this, "ShiftRuleFactory");
            }

        }

        public virtual ShiftRuleCollection GetHardRules(int employeeId)
        {
            return GetEmployeeRules(employeeId, this.m_HardRulesRepository);
        }

        public virtual ShiftRuleCollection GetSoftRules(int employeeId)
        {
            return GetEmployeeRules(employeeId, this.m_SoftRulesRepository);
        }

        protected virtual ShiftRuleCollection GetEmployeeRules(int employeeId, ShiftRuleCollection rulesRepository)
        {
            ShiftRuleCollection ruleSet = new ShiftRuleCollection();
            
            foreach (var rule in rulesRepository)
            {
               
                //if not previously added
                if(ruleSet.SingleOrDefault(shiftrule => ((shiftrule.Id == rule.Id))) == null)
                {
                    IShiftRule employeeSpecificRule = null;

                    if(MustUseEmployeeSpecificRule(rule.Id))
                    {
                        employeeSpecificRule = rulesRepository.SingleOrDefault(shiftrule => ((shiftrule.Id == rule.Id) && (shiftrule.EmployeeId == employeeId)));
                        if(employeeSpecificRule != null)
                        {
                            if(employeeSpecificRule.SetEnabledProperty().Enabled)
                                ruleSet.Add(employeeSpecificRule);
                        }
                            
                    }

                    if (!MustUseEmployeeSpecificRule(rule.Id) || (employeeSpecificRule == null)) //try base rule
                    {
                        IShiftRule baseRule = rulesRepository.SingleOrDefault(shiftrule => ((shiftrule.Id == rule.Id) && (shiftrule.EmployeeId == Scheduler.Core.Properties.Settings.Default.NULL_EMPLOYEE_ID)));
                        if (baseRule != null)
                        {
                            if(baseRule.SetEnabledProperty().Enabled)
                                ruleSet.Add(baseRule);
                        }
                            
                    }                    
                }
            }

            return ruleSet;
        }

       

        protected virtual bool MustUseEmployeeSpecificRule(int ruleId)
        {
            bool result = false;
            switch (ruleId)
            {
                case 2:
                    if (Properties.Settings.Default.Employee_Override_MAX_SHIFTS_Enabled)
                        result = true;
                    break;
                case 4:
                    if (Properties.Settings.Default.Employee_Override_MIN_SHIFTS_Enabled)
                        result = true;
                    break;
                case 7:
                    result = false;
                    break;
                default:
                    result = true;
                    break;
            }

            return result;
        }
     

        protected virtual IShiftRule GetShiftRuleInstance(DTimeOffRequest request, RuleType type, ConstraintType constraintType)
        {
            IShiftRule ruleInstance = null;
            IAssessor assessor = null;

            assessor = new EmployeeTimeOffAssessor(request);
            ruleInstance = new TimeOffRequestRule("TIME_OFF_REQUEST", request, assessor, type);

            if (ruleInstance != null)
                ruleInstance.Enabled = ruleInstance.IsRuleEnabled();

            return ruleInstance;
        }

        protected virtual IShiftRule GetShiftRuleInstance(ShiftRuleValue shiftRuleValue, RuleType type, ConstraintType constraintType)
        {
            IShiftRule ruleInstance = null;
            IAssessor assessor = null;
            ShiftRuleDefinition ruleDefinition;

            ruleDefinition = this.DataAccess.GetRuleDefinition(shiftRuleValue.rule_id);

            switch (shiftRuleValue.rule_id)
            {
                case 2:
                    assessor = new MaxShiftsPerEmployeeInWeekAssessor(shiftRuleValue);
                    ruleInstance = new MaxEmployeeShiftsPerWeekRule(ruleDefinition, shiftRuleValue, assessor, type);
                    break;
                case 4:
                    assessor = new MinShiftsPerEmployeeInWeekAssessor(shiftRuleValue);
                    ruleInstance = new MinEmployeeShiftsPerWeekRule(ruleDefinition, shiftRuleValue, assessor, type);
                    break;
                case 7:
                    assessor = new EmployeesInShiftAssessor(shiftRuleValue.value);
                    ruleInstance = new MinEmployeesPerShiftRule(ruleDefinition, shiftRuleValue, assessor, type);
                    break;
                default:
                    break;
            }

            if (ruleInstance != null)
                ruleInstance.Enabled = ruleInstance.IsRuleEnabled();
            
            return ruleInstance;
        }

        protected virtual void CreateRulesInstances(ref ShiftRuleCollection hardRules, ref ShiftRuleCollection softRules)
        {                       
            //Get the list of rules from data repository.
            List<ShiftRuleValue> shiftRulesValues = this.DataAccess.GetShiftRulesValues();
            List<DTimeOffRequest> timeOffRequests = this.DataAccess.GetTimeOffRequests();

            foreach (var shiftRuleValue in shiftRulesValues)
            {
                RuleType ruleType;
                ConstraintType constraintType = ConstraintType.ShiftRuleValue;

                /**************************  SHIFT RULE VALUES  ****************************************************/
                // If true it means that the rule has been classified and RuleType found.
                if (RuleIdToRuleTypeMapper.Instance.TryGetRuleType(mapItem => mapItem.RuleId == shiftRuleValue.rule_id, out ruleType))
                {
                    IShiftRule shiftRule = null;
                    shiftRule = GetShiftRuleInstance(shiftRuleValue, ruleType, constraintType);
                    if (shiftRule != null)
                    {
                        switch (ruleType)
                        {
                            case RuleType.HardRule:
                                hardRules.Add(shiftRule);
                                break;
                            case RuleType.SoftRule:
                                softRules.Add(shiftRule);
                                break;
                        }
                    }                                                                                       
                }
            }

            /**************************  TIME OFF REQUESTS  ****************************************************/
            foreach (var timeOffRequest in timeOffRequests)
            {
                RuleType ruleType;
                ConstraintType constraintType = ConstraintType.TimeOffRequest;

                // If true it means that the rule has been classified and RuleType found.
                if (RuleIdToRuleTypeMapper.Instance.TryGetRuleType(mapItem => mapItem.ConstraintType == constraintType, out ruleType))
                {
                    IShiftRule shiftRule = null;
                    shiftRule = GetShiftRuleInstance(timeOffRequest, ruleType, constraintType);
                    if (shiftRule != null)
                    {
                        switch (ruleType)
                        {
                            case RuleType.HardRule:
                                hardRules.Add(shiftRule);
                                break;
                            case RuleType.SoftRule:
                                softRules.Add(shiftRule);
                                break;
                        }
                    }
                }
            }
        }
      
        
    }
}
