using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scheduler.Core.BusinessRules;
using Scheduler.Core.DataAccess;
using Scheduler.Core.DataAccess.Json;
using Scheduler.Core.DataAccess.Mocked;
using Scheduler.Core.Validation;
using Scheduler.Core.Scheduling;
using IoC;
using Ninject;
using Ninject.Parameters;
using Ninject.Activation;
using Ninject.Modules;


namespace WebClient.DependencyInjection
{
    public class DependencyResolver : NinjectModule, IResolver
    {

        private readonly StandardKernel _kernel;
        public DependencyResolver()
        {
            _kernel = new StandardKernel(this);
        }

        public T Resolve<T>()
        {
            return _kernel.Get<T>();
        }

        public override void Load()
        {
            Bind<IJsonSerializer>().To<JsonNetSerializer>();
            Bind<IJsonInputLoader>().To<HttpWebRequestJsonInputLoader>();
            Bind<IDataAccess>().ToProvider(new JsonDataAccessProvider());
            Bind<IShiftRuleFactory>().ToProvider(new ShiftRuleFactoryProvider());
            Bind<IShiftScheduleValidator>().ToProvider(new ShiftScheduleValidatorProvider());
            Bind<ISchedulingAlgorithmFactory>().ToProvider(new SchedulingAlgorithmFactoryProvider());
            Bind<IScheduler>().ToProvider(new SchedulerProvider());


        }

        class SchedulerProvider : Provider<Scheduler.Core.Scheduling.Scheduler>
        {
            protected override Scheduler.Core.Scheduling.Scheduler CreateInstance(IContext context)
            {
                return new Scheduler.Core.Scheduling.Scheduler(context.Kernel.Get<ISchedulingAlgorithmFactory>());
            }
        }
        class SchedulingAlgorithmFactoryProvider : Provider<SchedulingAlgorithmFactory>
        {
            protected override SchedulingAlgorithmFactory CreateInstance(IContext context)
            {
                return new SchedulingAlgorithmFactory(context.Kernel.Get<IShiftScheduleValidator>(), context.Kernel.Get<IShiftRuleFactory>());
            }
        }

        class JsonDataAccessProvider : Provider<JsonDataAccess>
        {
            protected override JsonDataAccess CreateInstance(IContext context)
            {
                //return new MockedDataAccess(context.Kernel.Get<IJsonInputLoader>(), context.Kernel.Get<IJsonSerializer>());
                return new JsonDataAccess(context.Kernel.Get<IJsonInputLoader>(), context.Kernel.Get<IJsonSerializer>());
            }
        }

        class ShiftRuleFactoryProvider : Provider<ShiftRuleFactory>
        {
            protected override ShiftRuleFactory CreateInstance(IContext context)
            {
                return new ShiftRuleFactory(context.Kernel.Get<IDataAccess>());
            }
        }

        class ShiftScheduleValidatorProvider : Provider<ShiftScheduleValidator>
        {
            protected override ShiftScheduleValidator CreateInstance(IContext context)
            {
                return new ShiftScheduleValidator(context.Kernel.Get<IShiftRuleFactory>());
            }
        }

    }
}