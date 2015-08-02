using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using Scheduler.Core.BusinessRules;
using Scheduler.Core.DataAccess;
using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.UnitTests.Scheduler.Core.BusinessRules
{    
   public abstract class When_working_with_ShiftRuleFactory_Set_this_Up : Specification
    {
        protected Mock<IDataAccess> _dataAccess;
        protected IShiftRuleFactory _shiftRuleFactory;
        protected List<ShiftRuleDefinition> _ruleDefinitions;
        protected List<ShiftRuleValue> _ruleValues;
        protected List<DTimeOffRequest> _timeOffRequests;
        protected ShiftRuleCollection _HardRulesResult;
        protected ShiftRuleCollection _SoftRulesResult;


        protected List<int> _ExpectedHardRuleIds;
        protected List<int> _ExpectedSoftRuleIds;
       



        protected override void Establish_context()
        {
            base.Establish_context();

            _ruleDefinitions = CreateMockedRulesDefinitions();
            _ruleValues = CreateMockedRulesValues();
            _timeOffRequests = MockTimeOffRequests();

            _dataAccess = new Mock<IDataAccess>();
            _dataAccess.Setup(x => x.GetRuleDefinition(It.IsAny<int>())).Returns((int x) => { return MockShiftRuleDefinition(x); });
            _dataAccess.Setup(x => x.GetShiftRulesValues()).Returns(_ruleValues);
            _dataAccess.Setup(x => x.GetTimeOffRequests()).Returns(_timeOffRequests);

            _shiftRuleFactory = new ShiftRuleFactory(_dataAccess.Object);
        }

        private List<ShiftRuleValue> CreateMockedRulesValues()
        {
            List<ShiftRuleValue> rules = new List<ShiftRuleValue>();
            int NULL_EMPLOYEE_ID = global::Scheduler.Core.Properties.Settings.Default.NULL_EMPLOYEE_ID;


            rules.Add(new ShiftRuleValue() {rule_id= 4, employee_id= 1, value= 3});
            rules.Add(new ShiftRuleValue() {rule_id= 4, employee_id= 2, value= 5});
            rules.Add(new ShiftRuleValue() {rule_id= 2, employee_id= 1, value= 5});
            rules.Add(new ShiftRuleValue() {rule_id= 2, employee_id= 2, value= 5});
            rules.Add(new ShiftRuleValue() { rule_id = 2, employee_id = NULL_EMPLOYEE_ID, value = 6 });
            rules.Add(new ShiftRuleValue() { rule_id = 4, employee_id = NULL_EMPLOYEE_ID, value = 2 });
            rules.Add(new ShiftRuleValue() { rule_id = 7, employee_id = NULL_EMPLOYEE_ID, value = 2 });
          
            return rules;
        }
        private List<ShiftRuleDefinition> CreateMockedRulesDefinitions()
        {
            List<ShiftRuleDefinition> rules = new List<ShiftRuleDefinition>();

            rules.Add(new ShiftRuleDefinition() { id = 2, description = "Maximum number of shifts an employee may work per week. If employee_id is included then this applies to that employee only.", value = "MAX_SHIFTS" });
            rules.Add(new ShiftRuleDefinition() { id = 4, description = "Minimum number of shifts an employee must work per week. If employee_id is included then this applies to that employee only.", value = "MIN_SHIFTS" });
            rules.Add(new ShiftRuleDefinition() { id = 7, description = "Number of employees required per shift", value = "EMPLOYEES_PER_SHIFT" });

            return rules;
        }
        protected virtual List<DTimeOffRequest> MockTimeOffRequests()
        {
            List<DTimeOffRequest> items = new List<DTimeOffRequest>();

            items.Add(new DTimeOffRequest() { employee_id = 1, week = 23, days = new List<int>() { 1, 2, 3 } });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 2,
                week = 23,
                days = new List<int>() {
      5,
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 3,
                week = 23,
                days = new List<int>() {
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 24,
                days = new List<int>() {
      3,
      4
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 5,
                week = 24,
                days = new List<int>() {
      5,
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 24,
                days = new List<int>() {
      1
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 1,
                week = 25,
                days = new List<int>() {
      1,
      2,
      3
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 1,
                week = 25,
                days = new List<int>() {
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 25,
                days = new List<int>() {
      5,
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 3,
                week = 25,
                days = new List<int>() {
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 5,
                week = 26,
                days = new List<int>() {
      1,
      2,
      3
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 2,
                week = 26,
                days = new List<int>() {
      3,
      4
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 26,
                days = new List<int>() {
      1,
      2,
      3,
      4
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 2,
                week = 26,
                days = new List<int>() {
      1
    }
            });
            return items;
        }
        protected virtual ShiftRuleDefinition MockShiftRuleDefinition(int ruleId)
        {            
            return _ruleDefinitions.SingleOrDefault(ruleDef => ruleDef.id == ruleId);
        }

        
      


        protected override void Because_of()
        {
           
            
        }

    }

   [TestClass]
   public class When_working_with_ShiftRuleFactory_AND_invoking_GetHardRules_for_a_valid_employee_AND_using_override_employee_settings: When_working_with_ShiftRuleFactory_Set_this_Up
   {

       protected override void Establish_context()
       {
           base.Establish_context();

           _ExpectedHardRuleIds = new List<int>() { 2, 7};
           _ExpectedSoftRuleIds = new List<int>() { 5245, 5261 };
           
       }

      
       protected override void Because_of()
       {
           base.Because_of();

           int employeeId = 5;
           
           this._HardRulesResult = this._shiftRuleFactory.GetHardRules(employeeId);
           
       }

       [TestMethod]
       public void then_a_valid_list_of_HARD_and_SOFT_rules_must_be_obtained()
       {
           

           if (_HardRulesResult != null)
           {
               foreach (var item in _HardRulesResult)
               {
                   SiAuto.Main.LogObject("hard rule", item);
                   
               }                              
           }
        
           _ExpectedHardRuleIds.All((int ruleId) => (_HardRulesResult.SingleOrDefault(resultingRule => resultingRule.Id == ruleId) != null)).ShouldBeTrue();

       }
   }

}
