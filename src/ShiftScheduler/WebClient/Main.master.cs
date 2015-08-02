using DevExpress.Web.ASPxNavBar;
using Scheduler.Core.DataAccess;
using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.Helper;


namespace WebClient 
{
    public partial class MainMaster : System.Web.UI.MasterPage 
    {
       

        protected void Page_Load(object sender, EventArgs e) 
        {
            //UIHelper.SetUpEmployeesView(this.ASPxNavBar1, "Employees");
            //UIHelper.PopulateEmployeesView(this.ASPxNavBar1, this.schedulerDataAccess.GetEmployees());
            
        }

       

        
    }
}