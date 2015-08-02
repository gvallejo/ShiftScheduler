
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using Scheduler.Core.BusinessRules;
using Scheduler.Core.BusinessRules.Assessors;
using Scheduler.Core.BusinessRules.Rules;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.UnitTests.Scheduler.Core.BusinessRules
{
    public abstract class When_working_with_TIME_OFF_REQUEST_Set_this_Up : Specification
    {
        protected IShiftRule _shiftRule;
        protected Week _weekToTest;
        protected bool _result;
        protected DTimeOffRequest _timeOffRequest;
        protected List<string> _errors = null;
        protected IAssessor _assessor = null;



        protected override void Establish_context()
        {
            base.Establish_context();
            
            _timeOffRequest = MockTimeOffRequest();
            _weekToTest = MockWeekTotest(_timeOffRequest.week);
            _assessor = new EmployeeTimeOffAssessor(_timeOffRequest);
            _shiftRule = new TimeOffRequestRule("TIME_OFF_REQUEST", _timeOffRequest, _assessor, RuleType.SoftRule);
            _shiftRule.Enabled = true;
        }


        protected abstract DTimeOffRequest MockTimeOffRequest();      
        protected abstract Employees MockEmployeesInShift();
        protected abstract Week MockWeekTotest(int WeekId);
      


        protected override void Because_of()
        {
            ShiftSchedule shiftSchedule = new ShiftSchedule();
            shiftSchedule.Weeks.Add(_weekToTest.Id, _weekToTest);
            AssessorArgs args = new AssessorArgs() { EmployeeId = _shiftRule.EmployeeId, ItemToAssess = shiftSchedule };
            _result = _shiftRule.Assess(args, out _errors);
            
        }

    }

    [TestClass]
    public class When_working_with_TIME_OFF_REQUEST_Rule_AND_Assessing_a_non_compliant_Week : When_working_with_TIME_OFF_REQUEST_Set_this_Up
    {

        protected override void Establish_context()
        {
            base.Establish_context();            

        }

        protected override DTimeOffRequest MockTimeOffRequest()
        {
            DTimeOffRequest timeOffRequest = new DTimeOffRequest();          

            timeOffRequest.employee_id = 1;
            timeOffRequest.week = 23;
            timeOffRequest.days = new List<int>() { 1, 2, 3 };

            return timeOffRequest;
        }
        protected override Employees MockEmployeesInShift()
        {
            Employees employees = new Employees();
            employees.Add(1, new Employee() { id = 1, name = "Lanny McDonald" });
            employees.Add(2, new Employee() { id = 2, name = "Allen Pitts" });
            return employees;
        }
        protected override Week MockWeekTotest(int WeekId)
        {

            Week week = new Week();
            week.Id = WeekId;
            week.Start_Date = DateTime.Now;
            week.WorkingDays = new WorkingDays();
            for (int i = 1; i < 8; i++)
            {
                week.WorkingDays.Add(i, new WorkingDay() { Id = i, Week = WeekId });
                Shift shift = new Shift() { Id = 1, Week = WeekId, WorkingDay = i, AssignedEmployees = MockEmployeesInShift() };
                week.WorkingDays[i].Shifts.Add(1, shift);
            }

            return week;
        }


        protected override void Because_of()
        {
            base.Because_of();
        }

        [TestMethod]
        public void then_a_FALSE_result_must_obtained()
        {
            _result.ShouldBeFalse();

            if (_errors != null)
            {                
                SiAuto.Main.LogArray("errors", _errors.ToArray());
            }
                

        }
    }

    [TestClass]
    public class When_working_with_TIME_OFF_REQUEST_Rule_AND_Assessing_a_compliant_Week : When_working_with_TIME_OFF_REQUEST_Set_this_Up
    {

        protected override void Establish_context()
        {
            base.Establish_context();

        }

        protected override DTimeOffRequest MockTimeOffRequest()
        {
            DTimeOffRequest timeOffRequest = new DTimeOffRequest();

            // {
            //    "employee_id": 1,
            //    "week": 23,
            //    "days": [
            //              1,
            //              2,
            //              3
            //            ]
            //}

            timeOffRequest.employee_id = 1;
            timeOffRequest.week = 23;
            timeOffRequest.days = new List<int>() { 1, 2, 3 };

            return timeOffRequest;
        }
        protected override Employees MockEmployeesInShift()
        {
            Employees employees = new Employees();
            //employees.Add(1, new Employee() { id = 1, name = "Lanny McDonald" });
            employees.Add(2, new Employee() { id = 2, name = "Allen Pitts" });
            employees.Add(4, new Employee() { id = 4, name = "Dave Sapunjis" });
            return employees;
        }
        protected override Week MockWeekTotest(int WeekId)
        {
            Week week = new Week();
            week.Id = WeekId;
            week.Start_Date = DateTime.Now;
            week.WorkingDays = new WorkingDays();
            for (int i = 1; i < 8; i++)
            {
                week.WorkingDays.Add(i, new WorkingDay() { Id = i, Week = WeekId });
                Shift shift = new Shift() { Id = 1, Week = WeekId, WorkingDay = i, AssignedEmployees = MockEmployeesInShift() };
                week.WorkingDays[i].Shifts.Add(1, shift);
            }

            return week;
        }


        protected override void Because_of()
        {
            base.Because_of();
        }

        [TestMethod]
        public void then_a_TRUE_result_must_obtained()
        {
            _result.ShouldBeTrue();

            if (_errors != null)
                SiAuto.Main.LogArray("errors", _errors.ToArray());

        }
    }

}
