using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using Scheduler.Core.BusinessRules;
using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.BusinessRules.Rules;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using Scheduler.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.UnitTests.Scheduler.Core.BusinessRules
{
    public class When_working_with_EMPLOYEES_PER_SHIFT_Set_this_Up : Specification
    {
        protected IShiftRule _shiftRule;
        protected Shift _shift;
        protected bool _result;
        protected ShiftRuleDefinition _shiftRuleDefinition;
        protected ShiftRuleValue _shiftRuleValue;
        protected IAssessor _assessor;


        protected override void Establish_context()
        {
            base.Establish_context();

            _shift = new Shift();
            _shiftRuleDefinition = MockShiftRuleDefinition();          
            _shiftRuleValue = MockShiftRuleValue();
            _assessor = new EmployeesInShiftAssessor(_shiftRuleValue.value);
            _shiftRule = new MinEmployeesPerShiftRule(_shiftRuleDefinition, _shiftRuleValue, _assessor, RuleType.HardRule);
            _shiftRule.Enabled = true;
        }

        protected virtual ShiftRuleDefinition MockShiftRuleDefinition()
        {
            ShiftRuleDefinition ruleDefinition = new ShiftRuleDefinition();
            ruleDefinition.id = 7;
            ruleDefinition.value = "EMPLOYEES_PER_SHIFT";
            ruleDefinition.description = "Number of employees required per shift";

            return ruleDefinition;
        }

        protected virtual ShiftRuleValue MockShiftRuleValue()
        {
            ShiftRuleValue shiftRuleValue = new ShiftRuleValue();
            
            shiftRuleValue.rule_id = 7;
            shiftRuleValue.value = 2;

            return shiftRuleValue;
        }

       

        protected override void Because_of()
        {
            List<string> errors = null;
            AssessorArgs args = null; 
            ShiftSchedule shiftSchedule = new ShiftSchedule();
            Week week = new Week();
            week.Id = 50;
            week.Start_Date = new DateTime(2015,07,12);            
            WorkingDay wDay = new WorkingDay();
            wDay.Id = 1;
            wDay.Week = week.Id;
            wDay.Shifts.Add(1,_shift);
            week.WorkingDays.Add(wDay.Id, wDay);


            shiftSchedule.Weeks.Add(week.Id, week);
            args = new AssessorArgs() { EmployeeId = _shiftRule.EmployeeId, ItemToAssess = shiftSchedule };
            _result = _shiftRule.Assess(args, out errors);

        }

    }

    [TestClass]
    public class When_working_with_EMPLOYEES_PER_SHIFT_Rule_AND_Assessing_a_non_compliant_Shift : When_working_with_EMPLOYEES_PER_SHIFT_Set_this_Up
    {

        protected override void Establish_context()
        {
            base.Establish_context();
            _shift.AssignedEmployees = MockEmployeesInShift();

        }

        protected virtual Employees MockEmployeesInShift()
        {
            Employees employees = new Employees();
            employees.Add(1, new Employee(){ id = 1, name = "Lanny McDonald"});
            //employees.Add(2, new Employee() { id = 2, name = "Allen Pitts" });
            return employees;
        }

        protected override void Because_of()
        {
            base.Because_of();
        }

        [TestMethod]
        public void then_a_FALSE_result_must_obtained()
        {
            _result.ShouldBeFalse();

        }
    }


    [TestClass]
    public class When_working_with_EMPLOYEES_PER_SHIFT_Rule_AND_Assessing_a_compliant_Shift : When_working_with_EMPLOYEES_PER_SHIFT_Set_this_Up
    {

        protected override void Establish_context()
        {
            base.Establish_context();
            _shift.AssignedEmployees = MockEmployeesInShift();

        }

        protected virtual Employees MockEmployeesInShift()
        {
            Employees employees = new Employees();
            employees.Add(1, new Employee() { id = 1, name = "Lanny McDonald" });
            employees.Add(2, new Employee() { id = 2, name = "Allen Pitts" });
            return employees;
        }

        protected override void Because_of()
        {
            base.Because_of();
        }

        [TestMethod]
        public void then_a_TRUE_result_must_obtained()
        {
            _result.ShouldBeTrue();

        }
    }

}
