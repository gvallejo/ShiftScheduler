using Scheduler.Core.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Entities
{
    public class ShiftSchedule
    {
        public Dictionary<int, ShiftScheduleItem> ScheduleItems { get; set; }
        public Weeks Weeks { get; set; }

        public ShiftSchedule()
        {
            this.ScheduleItems = new Dictionary<int, ShiftScheduleItem>();
            this.Weeks = new Weeks();
        }
        
        virtual public List<ShiftScheduleItem> ToScheduleItems()
        {
            List<ShiftScheduleItem> scheduleItemList = new List<ShiftScheduleItem>();

            foreach (var week in Weeks)
            {
                ShiftScheduleItem scheduleItem = new ShiftScheduleItem();
                scheduleItem.week = week.Value.Id;
                

                foreach (var wDay in week.Value.WorkingDays)
                {
                    foreach (var employeeItem in wDay.Value.Shifts[1].AssignedEmployees)
                    {
                        Employee employee = employeeItem.Value;
                        EmployeeSchedule eSchedule;

                        if(scheduleItem.schedules.Exists(x => x.employee_id == employee.id))
                        {
                            eSchedule = scheduleItem.schedules.SingleOrDefault(x => x.employee_id == employee.id);
                            if (!eSchedule.schedule.Exists(day => day == wDay.Value.Id))
                                eSchedule.schedule.Add(wDay.Value.Id);
                        }
                        else
                        {
                            eSchedule = new EmployeeSchedule();
                            eSchedule.employee_id = employee.id;
                            eSchedule.schedule.Add(wDay.Value.Id);
                            scheduleItem.schedules.Add(eSchedule);
                            scheduleItem.schedules.Sort((es1, es2) => es1.employee_id.CompareTo(es2.employee_id));
                        }                    
                                             
                    }
                }
                scheduleItemList.Add(scheduleItem);
            }

            return scheduleItemList;

        }
    }
}
