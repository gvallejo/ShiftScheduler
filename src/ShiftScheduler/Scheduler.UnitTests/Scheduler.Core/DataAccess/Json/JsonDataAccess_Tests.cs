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
using Scheduler.Core.DataAccess.Json;
using Newtonsoft.Json;
using Rhino.Mocks;


namespace Scheduler.UnitTests.Scheduler.Core.DataAccess.Json
{
    
    public class When_working_with_the_JsonDataAccess_component_Set_this_Up : Specification
    {
        protected IDataAccess _dataAccess;
        protected IJsonInputLoader _jsonInputLoader;
        protected Mock<IJsonSerializer> _jsonSerializer;

        protected override void Establish_context()
        {
            base.Establish_context();

            Rhino.Mocks.MockRepository mockRepository = new Rhino.Mocks.MockRepository();

            this._jsonInputLoader = mockRepository.Stub<IJsonInputLoader>();
            this._jsonSerializer = new Mock<IJsonSerializer>();
            
        }
    }

    [TestClass]
    public class When_working_with_the_JsonDataAccess_component_AND_requesting : When_working_with_the_JsonDataAccess_component_Set_this_Up
    {        
        private object _result;
      //  private string _jsonStream;

        protected override void Establish_context()
        {
            base.Establish_context();

            string json = "";
            string url = @"http://interviewtest.replicon.com/employees";
            int OK = 0;
            List<Employee> mockedList = GetMockedEmployeeList();

            this._jsonInputLoader.Expect(sf => sf.Load(url, out OK)).Return(json).OutRef(200);
            this._jsonInputLoader.Replay();
            this._jsonSerializer.Setup(sf => sf.Deserialize<List<Employee>>(json)).Returns(mockedList);
            this._dataAccess = new JsonDataAccess(this._jsonInputLoader, this._jsonSerializer.Object);
        }

        private List<Employee> GetMockedEmployeeList()
        {
            List<Employee> list = null;
            string json;
            json = @"[{'id':1,'name':'Lanny McDonald'},{'id':2,'name':'Allen Pitts'},{'id':3,'name':'Gary Roberts'},{'id':4,'name':'Dave Sapunjis'},{'id':5,'name':'Mike Vernon'}]";

            list = JsonConvert.DeserializeObject<List<Employee>>(json);
            return list;
        }
     
        protected override void Because_of()
        {
            this._result = this._dataAccess.GetEmployees();
        }

        [TestMethod]
        public void List_of_Employees_then_a_list_of_Employees_should_be_obtained()
        {
            _result.ShouldBeInstanceOfType(typeof(List<Employee>));
           
        }
    }

}
