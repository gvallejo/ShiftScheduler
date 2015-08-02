using Moq;
using NBehave.Spec.MSTest;
using Scheduler.Core.DataAccess;
using Scheduler.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scheduler.UnitTests.Scheduler.Core
{
    
    public class when_working_with_the_Employee_type_repository : Specification
    {
        protected IDataAccess _DataAccess;
        

        protected override void Establish_context()
        {
            base.Establish_context();

            
        }
    }

    [TestClass]
    public class and_reading_the_Employees_list : when_working_with_the_Employee_type_repository
    {
        private List<Employee> _expected;
        private List<Employee> _result;
        
        protected override void Establish_context()
        {
            base.Establish_context();

            PopulateExpectedList();
        }

        private void PopulateExpectedList()
        {
            this._expected = new List<Employee>();
            /*---- Lanny McDonald---*/
            Employee e0 = new Employee();
            e0.Id = 1;
            e0.FullName = "Lanny McDonald";            
            this._expected.Add(e0);
            /*---- Allen Pitts---*/
            Employee e1 = new Employee();
            e1.Id = 2;
            e1.FullName = "Allen Pitts";
            this._expected.Add(e1);
            /*---- Gary Roberts---*/
            Employee e2 = new Employee();
            e2.Id = 3;
            e2.FullName = "Gary Roberts";
            this._expected.Add(e2);
            /*---- Dave Sapunjis---*/
            Employee e3 = new Employee();
            e3.Id = 4;
            e3.FullName = "Dave Sapunjis";
            this._expected.Add(e3);
            /*---- Mike Vernon---*/
            Employee e4 = new Employee();
            e4.Id = 5;
            e4.FullName = "Mike Vernon";
            this._expected.Add(e4);
        }

        protected override void Because_of()
        {
            _result = _DataAccess.GetEmployees();
        }

        [TestMethod]
        public void then_a_known_Employee_list_should_be_returned()
        {
            for (int i = 0; i < _expected.Count; i++)
            {
                _result[i].Id.ShouldEqual(_expected[i].Id);
                _result[i].FullName.ShouldEqual(_expected[i].FullName);
            }
        }
    }
}
