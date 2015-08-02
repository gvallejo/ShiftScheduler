using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Web.Client.Models
{
    public class EmployeesScheduleViewModel
    {
        public int ID { get; set; }
        public List<Employee> Employees { get; set; }

    }
}