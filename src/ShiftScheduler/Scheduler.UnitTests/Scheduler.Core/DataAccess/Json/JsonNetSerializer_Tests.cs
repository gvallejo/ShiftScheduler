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
using System.Net;


namespace Scheduler.UnitTests.Scheduler.Core.DataAccess.Json
{
    public class SampleClass
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class When_working_with_the_JsonNetSerializer_SetUp : Specification
    {
        protected IJsonSerializer _jasonSerializer;        

        protected override void Establish_context()
        {
            base.Establish_context();
            
            this._jasonSerializer = new JsonNetSerializer();
        }
    }

    [TestClass]
    public class When_working_with_the_JsonNetSerializer_AND_Deserializing_a_valid_KNOWN_Json_stream : When_working_with_the_JsonNetSerializer_SetUp
    {
        private SampleClass _expected;
        private object _result;
        private string _jsonStream;

        protected override void Establish_context()
        {
            base.Establish_context();

            MockJsonStream();
            CreateExpectedInstance();
        }
        private void MockJsonStream()
        {
            this._jsonStream = @"{
                               'Email': 'james@example.com',
                               'Active': true,
                               'CreatedDate': '2013-01-20T00:00:00Z',
                               'Roles': [
                                 'User',
                                 'Admin'
                               ]
                             }";
        }
        private void CreateExpectedInstance()
        {
            this._expected = new SampleClass
                                 {
                                     Email = "james@example.com",
                                     Active = true,
                                     CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                                     Roles = new List<string>
                                     {
                                         "User",
                                         "Admin"
                                    }
                                };
        }

        protected override void Because_of()
        {                           
                this._result = this._jasonSerializer.Deserialize<SampleClass>(this._jsonStream);            
        }

        [TestMethod]
        public void then_the_recovered_object_should_be_the_same_as_the_expected_one()
        {
            _result.ShouldBeInstanceOfType(typeof(SampleClass));
            (_result as SampleClass).Email.ShouldEqual(_expected.Email);
            (_result as SampleClass).Active.ShouldEqual(_expected.Active);
            (_result as SampleClass).CreatedDate.ShouldEqual(_expected.CreatedDate);

            for (int i = 0; i < (_result as SampleClass).Roles.Count; i++)
            {
                (_result as SampleClass).Roles[i].ShouldEqual(_expected.Roles[i]);
            }
        }
    }

}
