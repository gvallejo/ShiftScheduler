using Scheduler.Core.BusinessRules;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public class BruteForceAlgorithm: ISchedulingAlgorithm
    {
        public string Name {get;set;}
        public IShiftScheduleValidator ScheduleValidator { get; private set; }        
        public IShiftRuleFactory ShiftRuleFactory { get; set; }
        private Weeks WeekInfo { get; set; }
        private List<Employee> EmployeeList { get; set; }
        private List<DTimeOffRequest> timeOffRequests { get; set; }
        private List<ShiftRuleValue> RuleValues { get; set; }
        private const int MIN_EMPLOYEES_PER_SHIFT_RULE_ID = 7;


        public BruteForceAlgorithm(IShiftRuleFactory shiftRuleFactory, IShiftScheduleValidator scheduleValidator)
        {
            this.ShiftRuleFactory = shiftRuleFactory;
            this.Name = "Brute Force";
            WeekInfo = shiftRuleFactory.DataAccess.GetWeeks();
            this.EmployeeList = this.ShiftRuleFactory.DataAccess.GetEmployees();
            this.timeOffRequests = this.ShiftRuleFactory.DataAccess.GetTimeOffRequests();
            this.RuleValues = this.ShiftRuleFactory.DataAccess.GetShiftRulesValues();
            this.ScheduleValidator = scheduleValidator;
        }

        public virtual ShiftSchedule CalculateShiftSchedule(List<int> weeks)
        {
            ShiftSchedule shiftSchedule = null;

            shiftSchedule = new ShiftSchedule();
            foreach (int weekId in weeks)
            {
                Week week = GenerateAllEmployeesWorkInWeek(weekId);
                ApplyTimeOffRequestsRule(ref week);
                ApplyMinEmployeesPerShiftRule(ref week);
                shiftSchedule.Weeks.Add(week.Id, week);
            }          
            

            return shiftSchedule;
        }

        
        protected virtual Week GenerateAllEmployeesWorkInWeek(int weekId)
        {

            Week week = new Week();
            week.Id = weekId;
            week.Start_Date = this.WeekInfo[weekId].Start_Date;
            
            for (int i = 1; i < 8; i++)
            {
                WorkingDay wDay = new WorkingDay();
                wDay.Id = i;
                wDay.Week = week.Id;
                Shift shift = new Shift();
                shift.Id = 1;
                shift.Week = week.Id;

                foreach (Employee employee in this.EmployeeList)                
                    shift.AssignedEmployees.Add(employee.id, employee);                
                 
                wDay.Shifts.Add(shift.Id, shift);
                week.WorkingDays.Add(wDay.Id, wDay);
               
            }
            return week;
        }

        protected virtual void ApplyTimeOffRequestsRule( ref Week week)
        {
            int weekId = week.Id;
            if (Properties.Settings.Default.IgnoreTimeOffRequests == false)
            foreach (var timeOffRequest in this.timeOffRequests.Where(tr => tr.week == weekId))
            {
                foreach (var day in timeOffRequest.days)
                {
                    week.WorkingDays[day].Shifts[1].AssignedEmployees.Remove(timeOffRequest.employee_id);
                }
            }
        }

        protected virtual void ApplyMinEmployeesPerShiftRule(ref Week week)
        {
            int weekId = week.Id;
            RandomGenerator rand = new RandomGenerator();
            Stack<int> randStack;


            int ruleValue = this.RuleValues.Single(rule => rule.rule_id == MIN_EMPLOYEES_PER_SHIFT_RULE_ID).value;

            if (Properties.Settings.Default.RULE_EMPLOYEES_PER_SHIFT_Enabled == true)
                for (int i = 1; i < 8; i++)               
                {
                    randStack = new Stack<int>(rand.GetRandomSequence(this.EmployeeList.Count, 0, this.EmployeeList.Count - 1));
                    while (week.WorkingDays[i].Shifts[1].AssignedEmployees.Count > ruleValue)
                    {      
                        Employee employeeToRemove = this.EmployeeList[randStack.Pop()];
                        if (week.WorkingDays[i].Shifts[1].AssignedEmployees.ContainsKey(employeeToRemove.id))
                            week.WorkingDays[i].Shifts[1].AssignedEmployees.Remove(employeeToRemove.id);                                                
                    }
                 
                    while (week.WorkingDays[i].Shifts[1].AssignedEmployees.Count < ruleValue)
                    {
                        Employee employeeToAdd = this.EmployeeList[randStack.Pop()];
                        if (!week.WorkingDays[i].Shifts[1].AssignedEmployees.ContainsKey(employeeToAdd.id))
                            week.WorkingDays[i].Shifts[1].AssignedEmployees.Add(employeeToAdd.id, employeeToAdd);
                    }
                }
        }

        protected virtual IEnumerable<IEnumerable<T>> GetCombinationsNoRepetitions<T>(IEnumerable<T> items, int chosen)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (chosen == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetCombinationsNoRepetitions(items.Skip(i + 1), chosen - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }

        protected virtual IEnumerable<IEnumerable<T>> GetCombinationsWithRepetitions<T>(IEnumerable<T> items, int chosen)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (chosen == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetCombinationsWithRepetitions(items.Skip(i), chosen - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }
    }

    
}
