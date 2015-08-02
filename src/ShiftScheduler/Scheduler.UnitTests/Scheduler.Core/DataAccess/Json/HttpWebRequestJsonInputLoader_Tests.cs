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
    public class when_working_with_the_HttpWebRequestJsonInputLoader : Specification
    {
        protected IJsonInputLoader _jasonInputLoader;
        protected string _URL;


        protected override void Establish_context()
        {
            base.Establish_context();
            this._URL = @"http://jsonplaceholder.typicode.com/users";
            this._jasonInputLoader = new HttpWebRequestJsonInputLoader();
        }
    }

    [TestClass]
    public class When_working_with_the_HttpWebRequestJsonInputLoader_AND_loading_an_invalid_url : when_working_with_the_HttpWebRequestJsonInputLoader
    {
        private Exception _result;

        protected override void Establish_context()
        {
            base.Establish_context();
            this._URL = @"http://ww.gglwebhsjksdsdjasdjksd.cow";
        }

        protected override void Because_of()
        {
            try
            {
                int status;
                this._jasonInputLoader.Load(this._URL, out status);
            }
            catch (Exception exception)
            {
                _result = exception;
            }
        }

        [TestMethod]
        public void then_a_web_exception_should_be_raised()
        {
            _result.ShouldBeInstanceOfType(typeof(WebException));
        }
    }

    [TestClass]
    public class When_working_with_the_HttpWebRequestJsonInputLoader_AND_successfully_connected_to_a_valid_url : when_working_with_the_HttpWebRequestJsonInputLoader
    {
        private string _result;
        private int _status;

        protected override void Establish_context()
        {
            base.Establish_context();
            
        }

        protected override void Because_of()
        {           
            _result = this._jasonInputLoader.Load(this._URL, out this._status);                           
        }

        [TestMethod]
        public void then_a_200_status_code_should_be_acquired()
        {
            _status.ShouldEqual((int)HttpStatusCode.OK);
        }
    }
}
