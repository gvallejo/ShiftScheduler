using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NBehave.Spec.MSTest;
using Ninject;
using Ninject.Parameters;
using Scheduler.Core.BusinessRules;
using Scheduler.Core.DataAccess;
using Scheduler.Core.DataAccess.Json;
using Scheduler.Core.Entities;
using Scheduler.Core.Validation;
using Scheduler.IntegrationTests.Log;
using Scheduler.IntegrationTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.IntegrationTests.Scheduler.Core
{
    public abstract class When_using_ShiftRuleFactory_Set_this_Up : Specification
    {
        protected IDataAccess _dataAccess;
        protected IJsonSerializer _jsonSerializer;
        protected IJsonInputLoader _jsonInputLoader;
        protected IShiftRuleFactory _shiftRuleFactory;
        protected ShiftSchedule _shiftSchedule;
        protected bool _result;
        protected IShiftScheduleValidator _shiftScheduleValidator;

        private StandardKernel _kernel;


        protected override void Establish_context()
        {
            base.Establish_context();

            _kernel = new StandardKernel(new IntegrationTestModule());
            _jsonSerializer = _kernel.Get<IJsonSerializer>();
            _jsonInputLoader = _kernel.Get<IJsonInputLoader>();
            _dataAccess = _kernel.Get<IDataAccess>(new ConstructorArgument("jsonInputLoader", _jsonInputLoader), new ConstructorArgument("jsonSerializer", _jsonSerializer));
            _shiftRuleFactory = _kernel.Get<IShiftRuleFactory>(new ConstructorArgument("dataAccess", _dataAccess));
            _shiftScheduleValidator = _kernel.Get<IShiftScheduleValidator>(new ConstructorArgument("shiftRuleFactory", _shiftRuleFactory));
        }       

        protected override void Because_of()
        {           

        }

    }

    [TestClass]
    public class When_using_ShiftRuleFactory_AND_using_EMPLOYEES_PER_SHIFT_rule_to_Assess_a_compliant_Week : When_using_ShiftRuleFactory_Set_this_Up
    {

        protected override void Establish_context()
        {
            base.Establish_context();

            List<string> _jsonWeeks = new List<string>()
                {
                //        day 1          day 2         day 3        day 4         day 5         day 6          day 7
                    @"[{'AE':[2,3]}, {'AE':[3,5]}, {'AE':[5,2]}, {'AE':[4,3]}, {'AE':[1,5]}, {'AE':[1,4]}, {'AE':[1,4]}]",
                    @"[{'AE':[5,1]}, {'AE':[2,4]}, {'AE':[3,5]}, {'AE':[1,2]}, {'AE':[3,4]}, {'AE':[4,1]}, {'AE':[2,3]}]",
                    @"[{'AE':[4,5]}, {'AE':[2,4]}, {'AE':[3,4]}, {'AE':[3,1]}, {'AE':[2,3]}, {'AE':[1,5]}, {'AE':[5,2]}]",
                    @"[{'AE':[3,1]}, {'AE':[2,1]}, {'AE':[1,3]}, {'AE':[1,5]}, {'AE':[2,4]}, {'AE':[2,4]}, {'AE':[5,4]}]",
                };
            _shiftSchedule = new ShiftScheduleMock(_jsonWeeks);
        }

     
        protected override void Because_of()
        {
            base.Because_of();

           var randomNumberGenerator = new Random();                 
           int employeeId = randomNumberGenerator.Next(32000);
            List<string> hardErrors = null;
            List<string> softErrors  = null;

           

           _result = _shiftScheduleValidator.IsValid(_shiftSchedule, out hardErrors,  out softErrors);
          
        }

        [TestMethod]
        public void then_a_TRUE_result_must_obtained()
        {
            _result.ShouldBeTrue();
            LogSession.Main.LogObject("result", _result);
           

        }
    }

}
