using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.DataAccess.Json
{
    public class JsonDataAccess : IDataAccess
    {
        public IJsonInputLoader JsonInputLoader { get; private set; }
        public IJsonSerializer JsonSerializer { get; private set; }
        


        public JsonDataAccess(IJsonInputLoader jsonInputLoader, IJsonSerializer jsonSerializer)
        {
            this.JsonInputLoader = jsonInputLoader;
            this.JsonSerializer = jsonSerializer;
        }

        protected virtual  T GetObject<T>(string url)
        {
            T resp = default(T);

          
            string json = GetJsonStream(url);
            resp = this.JsonSerializer.Deserialize<T>(json);

            return resp; 
        }





        protected virtual  string GetJsonStream(string url)
        {
            int status;
            string json;

            json = this.JsonInputLoader.Load(url, out status);
            if (status != 200)
                throw new Exception(string.Format("Json service returned code {0}", status));

            return json;
        }

        public virtual List<Employee> GetEmployees()
        {
             string url = @"http://interviewtest.replicon.com/employees";
             return GetObject<List<Employee>>(url);
        }

        public virtual List<ShiftRuleValue> GetShiftRulesValues()
        {
            string url = @"http://interviewtest.replicon.com/shift-rules";
            return GetObject<List<ShiftRuleValue>>(url);
        }

        public virtual List<DTimeOffRequest> GetTimeOffRequests()
        {
            string url = @"http://interviewtest.replicon.com/time-off/requests";
            return GetObject<List<DTimeOffRequest>>(url);
        }

        public virtual List<ShiftRuleDefinition> GetShiftRulesDefinitions()
        {
            string url = @"http://interviewtest.replicon.com/rule-definitions";
            return GetObject<List<ShiftRuleDefinition>>(url);
        }

        public virtual ShiftRuleDefinition GetRuleDefinition(int ruleId)
        {
            return GetShiftRulesDefinitions().SingleOrDefault(item => item.id == ruleId);
        }


        public virtual Weeks GetWeeks()
        {
            Weeks weeks = new Weeks();
            List<Week> weeksList;
            string url = @"http://interviewtest.replicon.com/weeks";
            weeksList =  GetObject<List<Week>>(url);

            foreach (Week week in weeksList)            
                weeks.Add(week.Id, week);
            return weeks;
        }
    }
}
