using Scheduler.Core.BusinessRules;
using Scheduler.Core.DataAccess.Json;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using Scheduler.Core.Log;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Scheduling
{
    public class ShiftScheduleMock : ShiftSchedule
    {
        public ShiftScheduleMock()
            : base()
        {
            PopulateMock(this);
        }

        private List<JsonWeek> GetJsonSchedule()
        {
           
            //                        day 1          day 2         day 3        day 4         day 5         day 6          day 7
            string jsonWeek1 = @"[{'AE':[1,2]}, {'AE':[3,4]}, {'AE':[5,1]}, {'AE':[2,3]}, {'AE':[4,5]}, {'AE':[1,2]}, {'AE':[3,4]}]";
            string jsonWeek2 = @"[{'AE':[5,1]}, {'AE':[2,3]}, {'AE':[4,5]}, {'AE':[1,2]}, {'AE':[3,4]}, {'AE':[5,1]}, {'AE':[2,3]}]";
            string jsonWeek3 = @"[{'AE':[4,5]}, {'AE':[1,2]}, {'AE':[3,4]}, {'AE':[5,1]}, {'AE':[2,3]}, {'AE':[4,5]}, {'AE':[1,2]}]";
            string jsonWeek4 = @"[{'AE':[3,4]}, {'AE':[5,1]}, {'AE':[2,3]}, {'AE':[4,5]}, {'AE':[1,2]}, {'AE':[3,4]}, {'AE':[5,1]}]";

            JsonNetSerializer serializer = new JsonNetSerializer();
            List<JsonWeek> JsonSchedule = new List<JsonWeek>();
            JsonSchedule.Add(new JsonWeek(serializer.Deserialize<List<JsonDay>>(jsonWeek1)));
            JsonSchedule.Add(new JsonWeek(serializer.Deserialize<List<JsonDay>>(jsonWeek2)));
            JsonSchedule.Add(new JsonWeek(serializer.Deserialize<List<JsonDay>>(jsonWeek3)));
            JsonSchedule.Add(new JsonWeek(serializer.Deserialize<List<JsonDay>>(jsonWeek4)));

            return JsonSchedule;
        }

        private void PopulateMock(ShiftSchedule schedule)
        {
            List<JsonWeek> jsonSchedule = GetJsonSchedule();
            DateTime initDate = new DateTime(2015, 06, 01);

            int w = 23;
            foreach (var jweek in jsonSchedule)
            {
                Week newWeek = new Week() { Id = w, Start_Date = initDate };

                for (int wday = 1; wday < 8; wday++)
                {
                    WorkingDay workingDay = new WorkingDay() { Id = wday, Week = w };
                    workingDay.Shifts.Add(1, new Shift() { Id = 1, Week = w, WorkingDay = wday });
                    foreach (int employeeId in jsonSchedule[w - 23].Week[wday - 1].AE)
                    {
                        workingDay.Shifts[1].AssignedEmployees.Add(employeeId, new Employee() { id = employeeId, name = "John Doe" });
                    }
                    newWeek.WorkingDays.Add(workingDay.Id, workingDay);
                }
                schedule.Weeks.Add(newWeek.Id, newWeek);
                newWeek.Start_Date = initDate;
                initDate = initDate.AddDays(7);                
                LogSession.Main.LogMessage("Week[{0}]: Start date: {1}", newWeek.Id, newWeek.Start_Date);
                w++;
            }
        }
    }

    public class JsonDay
    {
        public List<int> AE { get; set; }
    }
    public class JsonWeek
    {
        public JsonWeek(List<JsonDay> week)
        {
            this.Week = week;
        }
        public List<JsonDay> Week { get; set; }
    }

    public class DummyAlgorithm: ISchedulingAlgorithm
    {
        public string Name {get;set;}
        public IShiftScheduleValidator ScheduleValidator {get; private set;}        
        public IShiftRuleFactory ShiftRuleFactory { get; set; }

        public DummyAlgorithm()
        {
            this.Name = "Dummy";
        }

        public ShiftSchedule CalculateShiftSchedule(List<int> weeks)
        {
            ShiftScheduleMock schedule = new ShiftScheduleMock();
            return schedule;
        }
    }
}
