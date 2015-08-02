using Ninject.Activation;
using Ninject.Modules;
using Scheduler.Core.BusinessRules;
using Scheduler.Core.DataAccess;
using Scheduler.Core.DataAccess.Json;
using Scheduler.Core.DataAccess.Mocked;
using Scheduler.Core.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.IntegrationTests
{
    public class IntegrationTestModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IJsonSerializer>().To<JsonNetSerializer>();
            Bind<IJsonInputLoader>().To<HttpWebRequestJsonInputLoader>();
            //Bind<IDataAccess>().To<JsonDataAccess>();
            Bind<IDataAccess>().To<MockedDataAccess>();
            
            Bind<IShiftRuleFactory>().To<ShiftRuleFactory>();
            Bind<IShiftScheduleValidator>().To<ShiftScheduleValidator>();

        }
    }

    //public class IntegrationTest_DataAccess_Provider: Provider<IDataAccess>
    //{

    //    protected override IDataAccess CreateInstance(IContext context)
    //    {
    //        IJsonSerializer
    //    }
    //}
}
