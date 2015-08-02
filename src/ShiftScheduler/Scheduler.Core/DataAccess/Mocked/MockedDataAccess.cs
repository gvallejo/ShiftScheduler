using Scheduler.Core.DataAccess.Json;
using Scheduler.Core.Entities;
using Scheduler.Core.Entities.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.DataAccess.Mocked
{
    public class MockedDataAccess: JsonDataAccess
    {
        private List<ShiftRuleDefinition> _shiftRuleDefinition;
        private List<DTimeOffRequest> _TimeOffRequests;
        private List<ShiftRuleValue> _shiftRulesValues;
        private List<Employee> _employees;


        public MockedDataAccess(IJsonInputLoader jsonInputLoader, IJsonSerializer jsonSerializer):
            base(jsonInputLoader, jsonSerializer)
        {
            _shiftRuleDefinition = CreateMockedRulesDefinitions();
            _TimeOffRequests = MockTimeOffRequests();
            _shiftRulesValues = CreateMockedRulesValues();
            _employees = MockEmployees();
        }

        public override List<Employee> GetEmployees()
        {
            return _employees;
        }

        public override List<ShiftRuleValue> GetShiftRulesValues()
        {
            return _shiftRulesValues;
        }

        public override List<DTimeOffRequest> GetTimeOffRequests()
        {
            return _TimeOffRequests;
        }

        public override List<ShiftRuleDefinition> GetShiftRulesDefinitions()
        {
            return this._shiftRuleDefinition;
        }

        public override ShiftRuleDefinition GetRuleDefinition(int ruleId)
        {
            return _shiftRuleDefinition.SingleOrDefault(ruleDef => ruleDef.id == ruleId);
        }

       

        protected override string GetJsonStream(string url)
        {
            if (url.Equals(@"http://interviewtest.replicon.com/weeks"))
                return @"[{'id':1,'start_date':'2014/12/29'},{'id':2,'start_date':'2015/01/05'},{'id':3,'start_date':'2015/01/12'},{'id':4,'start_date':'2015/01/19'},{'id':5,'start_date':'2015/01/26'},{'id':6,'start_date':'2015/02/02'},{'id':7,'start_date':'2015/02/09'},{'id':8,'start_date':'2015/02/16'},{'id':9,'start_date':'2015/02/23'},{'id':10,'start_date':'2015/03/02'},{'id':11,'start_date':'2015/03/09'},{'id':12,'start_date':'2015/03/16'},{'id':13,'start_date':'2015/03/23'},{'id':14,'start_date':'2015/03/30'},{'id':15,'start_date':'2015/04/6'},{'id':16,'start_date':'2015/04/13'},{'id':17,'start_date':'2015/04/20'},{'id':18,'start_date':'2015/04/27'},{'id':19,'start_date':'2015/05/04'},{'id':20,'start_date':'2015/05/11'},{'id':21,'start_date':'2015/05/18'},{'id':22,'start_date':'2015/05/25'},{'id':23,'start_date':'2015/06/01'},{'id':24,'start_date':'2015/06/08'},{'id':25,'start_date':'2015/06/15'},{'id':26,'start_date':'2015/06/22'},{'id':27,'start_date':'2015/06/29'},{'id':28,'start_date':'2015/07/06'},{'id':29,'start_date':'2015/07/13'},{'id':30,'start_date':'2015/07/20'},{'id':31,'start_date':'2015/07/27'},{'id':32,'start_date':'2015/08/03'},{'id':33,'start_date':'2015/08/10'},{'id':34,'start_date':'2015/08/17'},{'id':35,'start_date':'2015/08/24'},{'id':36,'start_date':'2015/08/31'},{'id':37,'start_date':'2015/09/07'},{'id':38,'start_date':'2015/09/14'},{'id':39,'start_date':'2015/09/21'},{'id':40,'start_date':'2015/09/28'},{'id':41,'start_date':'2015/10/05'},{'id':42,'start_date':'2015/10/12'},{'id':43,'start_date':'2015/10/19'},{'id':44,'start_date':'2015/10/26'},{'id':45,'start_date':'2015/11/02'},{'id':46,'start_date':'2015/11/09'},{'id':47,'start_date':'2015/11/16'},{'id':48,'start_date':'2015/11/23'},{'id':49,'start_date':'2015/11/30'},{'id':50,'start_date':'2015/12/07'},{'id':51,'start_date':'2015/12/14'},{'id':52,'start_date':'2015/12/21'},{'id':53,'start_date':'2015/12/28'}]";
            else
                return string.Empty;
        }
       

        private List<ShiftRuleValue> CreateMockedRulesValues()
        {
            List<ShiftRuleValue> rules = new List<ShiftRuleValue>();
            int NULL_EMPLOYEE_ID = global::Scheduler.Core.Properties.Settings.Default.NULL_EMPLOYEE_ID;


            rules.Add(new ShiftRuleValue() { rule_id = 4, employee_id = 1, value = 3 });
            rules.Add(new ShiftRuleValue() { rule_id = 4, employee_id = 2, value = 5 });
            rules.Add(new ShiftRuleValue() { rule_id = 2, employee_id = 1, value = 5 });
            rules.Add(new ShiftRuleValue() { rule_id = 2, employee_id = 2, value = 5 });
            rules.Add(new ShiftRuleValue() { rule_id = 2, employee_id = NULL_EMPLOYEE_ID, value = 6 });
            rules.Add(new ShiftRuleValue() { rule_id = 4, employee_id = NULL_EMPLOYEE_ID, value = 2 });
            rules.Add(new ShiftRuleValue() { rule_id = 7, employee_id = NULL_EMPLOYEE_ID, value = 2 });

            return rules;
        }
        private List<ShiftRuleDefinition> CreateMockedRulesDefinitions()
        {
            List<ShiftRuleDefinition> rules = new List<ShiftRuleDefinition>();

            rules.Add(new ShiftRuleDefinition() { id = 2, description = "Maximum number of shifts an employee may work per week. If employee_id is included then this applies to that employee only.", value = "MAX_SHIFTS" });
            rules.Add(new ShiftRuleDefinition() { id = 4, description = "Minimum number of shifts an employee must work per week. If employee_id is included then this applies to that employee only.", value = "MIN_SHIFTS" });
            rules.Add(new ShiftRuleDefinition() { id = 7, description = "Number of employees required per shift", value = "EMPLOYEES_PER_SHIFT" });

            return rules;
        }
        protected virtual List<DTimeOffRequest> MockTimeOffRequests()
        {
            List<DTimeOffRequest> items = new List<DTimeOffRequest>();

            items.Add(new DTimeOffRequest() { employee_id = 1, week = 23, days = new List<int>() { 1, 2, 3 } });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 2,
                week = 23,
                days = new List<int>() {
      5,
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 3,
                week = 23,
                days = new List<int>() {
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 24,
                days = new List<int>() {
      3,
      4
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 5,
                week = 24,
                days = new List<int>() {
      5,
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 24,
                days = new List<int>() {
      1
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 1,
                week = 25,
                days = new List<int>() {
      1,
      2,
      3
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 1,
                week = 25,
                days = new List<int>() {
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 25,
                days = new List<int>() {
      5,
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 3,
                week = 25,
                days = new List<int>() {
      6,
      7
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 5,
                week = 26,
                days = new List<int>() {
      1,
      2,
      3
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 2,
                week = 26,
                days = new List<int>() {
      3,
      4
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 4,
                week = 26,
                days = new List<int>() {
      1,
      2,
      3,
      4
    }
            });
            items.Add(new DTimeOffRequest()
            {
                employee_id = 2,
                week = 26,
                days = new List<int>() {
      1
    }
            });
            return items;
        }
        protected  List<Employee> MockEmployees()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { id = 1, name = "Lanny McDonald" });
            employees.Add(new Employee() { id = 2, name = "Allen Pitts" });
            employees.Add(new Employee() { id = 3, name = "Gary Roberts" });
            employees.Add(new Employee() { id = 4, name = "Dave Sapunjis" });
            employees.Add(new Employee() { id = 5, name = "Mike Vernon" });
            
            return employees;
        }

    }
}
