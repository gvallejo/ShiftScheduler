using DevExpress.Web.ASPxNavBar;
using Scheduler.Core.DataAccess;
using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebClient.Helper
{
    public static class UIHelper
    {
        public static void SetUpEmployeesView(WebControl control, string title)
        {
            ASPxNavBar navBar = (ASPxNavBar)control;
            NavBarGroup employeesGroup = navBar.Groups.First();
            employeesGroup.Items.Clear();
            employeesGroup.Text = title;
        }

        public static void PopulateEmployeesView(WebControl control, IEnumerable<Employee> employees)
        {
            ASPxNavBar navBar = (ASPxNavBar)control;
            NavBarGroup employeesGroup = navBar.Groups.First();
            
            foreach (var employee in employees)
            {
                employeesGroup.Items.Add(new NavBarItem() { Name = string.Format("item_{0}", employee.id), Text = employee.name });                
            }
            
        }
    }
}