using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using Scheduler.Core.DataAccess.Json;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using Scheduler.Core.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.App_Code;
using WebClient.Helper;
using WebClient.Log;


namespace WebClient 
{
    public partial class _Default : System.Web.UI.Page 
    {
        IScheduler _scheduler = IoC.IoC.Resolve<IScheduler>();
        ShiftSchedule _schedule = null;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
        }

        ASPxSchedulerStorage Storage
        {
            get { return ASPxScheduler1.Storage; }
        }

        private ShiftSchedule ShiftSchedule 
        {
            get { return Session["CalculatedShiftSchedule"] as ShiftSchedule;}
            set {Session["CalculatedShiftSchedule"] = value;}
        } 

        protected void Page_Load(object sender, EventArgs e) 
        {
           

            
            List<Employee> employeeList = GetEmployeesList();
            UIHelper.SetUpEmployeesView(this.ASPxNavBar1, "Employees");
            UIHelper.PopulateEmployeesView(this.ASPxNavBar1, employeeList);
           
            this.ASPxScheduler1.ResourceNavigator.Visibility = ResourceNavigatorVisibility.Never;
            SchedulerDemoUtils.ApplyDefaults(this, ASPxScheduler1);
            SetupMappings();
            SchedulerDemoUtils.FillResources(Storage, _scheduler.AlgorithmFactory.ShiftRuleFactory.DataAccess.GetEmployees());

            if (!string.IsNullOrEmpty(Request.Params["Image"]))
                PostImage(Request.Params["Image"]);

            if (!IsPostBack)
            {
                this.ASPxDateEditFrom.Date = new DateTime(2015, 06, 01);
                this.ASPxDateEditTo.Date = new DateTime(2015, 06, 22);
            }
        }

        private void CalculateSchedule()
        {           
            _scheduler.SetSchedulingAlgorithm("brute force");
            //_scheduler.SetSchedulingAlgorithm("dummy");
            int weekFrom =  this._scheduler.AlgorithmFactory.ShiftRuleFactory.DataAccess.GetWeeks().FirstOrDefault(w => w.Value.Start_Date.Date.Equals(this.ASPxDateEditFrom.Date.Date)).Key;
            int weekTo =  this._scheduler.AlgorithmFactory.ShiftRuleFactory.DataAccess.GetWeeks().FirstOrDefault(w => w.Value.Start_Date.Date.Equals(this.ASPxDateEditTo.Date.Date)).Key;


            this.ShiftSchedule = _scheduler.GenerateSchedule(new List<int>(Enumerable.Range(weekFrom, Math.Abs(weekTo - weekFrom)+1)));

            if (this.ShiftSchedule != null)
            {                
                this.ASPxScheduler1.Start = this.ShiftSchedule.Weeks.First().Value.Start_Date;
                this.ASPxScheduler1.GoToDate(this.ASPxScheduler1.Start);                
            }
          
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            CalculateSchedule();
            
        }

        public List<Employee> GetEmployeesList()
        {
           return  this._scheduler.AlgorithmFactory.ShiftRuleFactory.DataAccess.GetEmployees();
        }

        void SetupMappings()
        {
            ASPxAppointmentMappingInfo mappings = Storage.Appointments.Mappings;
            Storage.BeginUpdate();
            try
            {
                mappings.AppointmentId = "Id";
                mappings.Start = "StartTime";
                mappings.End = "EndTime";
                mappings.Subject = "Subject";
                mappings.AllDay = "AllDay";
                mappings.Description = "Description";
                mappings.Label = "Label";
                mappings.Location = "Location";
                mappings.RecurrenceInfo = "RecurrenceInfo";
                mappings.ReminderInfo = "ReminderInfo";
                mappings.ResourceId = "OwnerId";
                mappings.Status = "Status";
                mappings.Type = "EventType";
            }
            finally
            {
                Storage.EndUpdate();
            }
        }

        // Populating ObjectDataSource
        protected void appointmentsDataSource_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = new CustomEventDataSource(GetCustomEvents());
        }
        CustomEventList GetCustomEvents()
        {           
            CustomEventList events = Session["ListBoundModeObjects"] as CustomEventList;
            if ((this.ShiftSchedule != null) | (events == null))
            {
                events = GenerateCustomEventList();
                Session["ListBoundModeObjects"] = events;
                this.ShiftSchedule = null;
            }
              
            return events;
        }

        #region Events generation
        CustomEventList GenerateCustomEventList()
        {
            
            ShiftSchedule shiftSchedule = this.ShiftSchedule;
            CustomEventList eventList = new CustomEventList();           


            if (shiftSchedule != null)
            {
                if (this.ASPxScheduler1.Storage.Resources.Count == 0)
                {
                    SchedulerDemoUtils.ApplyDefaults(this, ASPxScheduler1);
                    SetupMappings();
                    SchedulerDemoUtils.FillResources(this.ASPxScheduler1.Storage, _scheduler.AlgorithmFactory.ShiftRuleFactory.DataAccess.GetEmployees());
                }

                foreach (var weekItem in shiftSchedule.Weeks)
                {

                    foreach (var wday in weekItem.Value.WorkingDays)
                    {
                        LogSession.Main.LogMessage("Week[{0}]: Start date: {1} wDay:{2} Date: {3}", weekItem.Value.Id, weekItem.Value.Start_Date, wday.Value.Id, weekItem.Value.Start_Date.AddDays(wday.Value.Id - 1));
                        foreach (var assignedEmployee in wday.Value.Shifts[1].AssignedEmployees)
                        {

                            Resource resource = Storage.Resources.GetResourceById(assignedEmployee.Value.id);

                            eventList.Add(CreateEvent(resource.Id, string.Format("{0} {1}", resource.Id.ToString(), resource.Caption), 0, assignedEmployee.Value.id, weekItem.Value.Start_Date.AddDays(wday.Value.Id - 1)));

                        }
                    }
                }
            }

            return eventList;
        }
        CustomEvent CreateEvent(object resourceId, string subject, int status, int label, DateTime date)
        {
            int id = (int)resourceId;

            CustomEvent customEvent = new CustomEvent();
            customEvent.Subject = subject;
            customEvent.OwnerId = resourceId;
            
            int rangeInHours = 1;
            customEvent.StartTime = date + TimeSpan.FromHours(rangeInHours*id);
            customEvent.EndTime = customEvent.StartTime + TimeSpan.FromHours(rangeInHours);
            customEvent.Status = status;
            customEvent.Label = label;
            customEvent.Id = "ev" + customEvent.GetHashCode();
            return customEvent;
        }
        #endregion

        protected void ASPxScheduler1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {           
            {
                e.Menu.Items.Clear();
                e.Menu.Visible = false;
            }
        }

        protected void ASPxDateEditFrom_CalendarCustomDisabledDate(object sender, DevExpress.Web.ASPxEditors.CalendarCustomDisabledDateEventArgs e)
        {
            JusPaintMondays(e);
        }

        protected void ASPxDateEditTo_CalendarCustomDisabledDate(object sender, DevExpress.Web.ASPxEditors.CalendarCustomDisabledDateEventArgs e)
        {
            JusPaintMondays(e);
        }
        
        protected void JusPaintMondays(DevExpress.Web.ASPxEditors.CalendarCustomDisabledDateEventArgs e)
        {
            if (e.Date.DayOfWeek != DayOfWeek.Monday)
                e.IsDisabled = true;
        }

        protected void Unnamed1_Command(object sender, CommandEventArgs e)
        {

        }

        protected void ASPxScheduler1_BeforeExecuteCallbackCommand(object sender, SchedulerCallbackCommandEventArgs e)
        {
           

        }

        protected void ASPxScheduler1_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            if (e.Parameter.Equals("CALCULATESCHED"))
            {
                CalculateSchedule();
            }

        }

       
    }
}