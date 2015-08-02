using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using DevExpress.Schedule;
using DevExpress.Schedule.Serializing;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Xml;
using System.Web.UI;
using Scheduler.Core.Entities;


namespace WebClient.App_Code
{
    public class SchedulerDemoUtils
    {
        static DateTime baseDate = new DateTime(2014, 5, 23);
        public static Random RandomInstance = new Random();
        //public static string[] Users = new string[] { 
        //"Peter Dolan", "Ryan Fischer", "Andrew Miller", "Tom Hamlett",
        //"Jerry Campbell", "Carl Lucas", "Mark Hamilton", "Steve Lee" 
        //};

        public static DateTime BaseDate { get { return baseDate; } }
        public static void FillResources(ASPxSchedulerStorage storage, List<Employee> Users)
        {
            ResourceCollection resources = storage.Resources.Items;
           
            storage.BeginUpdate();
            try
            {
                foreach (var employee in Users)
                {
                    Resource resource = storage.CreateResource(employee.id);
                    resource.Caption = employee.name;
                    resources.Add(resource);
                }                                    
            }
            finally
            {
                storage.EndUpdate();
            }
        }
        public static void ApplyDefaults(UserControl page, ASPxScheduler scheduler)
        {
            if (page.IsPostBack)
                return;
            ApplyDefaultsCore(scheduler);
        }
        public static void ApplyDefaults(Page page, ASPxScheduler scheduler)
        {
            if (page.IsPostBack)
                return;
            ApplyDefaultsCore(scheduler);
        }
        static void ApplyDefaultsCore(ASPxScheduler scheduler)
        {
            scheduler.Start = BaseDate;
            scheduler.ResourceNavigator.Visibility = ResourceNavigatorVisibility.Never;
            //scheduler.ResourceNavigator.EnableIncreaseDecrease = false;
            scheduler.ActiveView.GroupType = SchedulerGroupType.None;
        }
        public static HolidayBaseCollection LoadHolidaysFromXml(string uri, string location)
        {
            XPathDocument document = new XPathDocument(uri);
            XPathNavigator navigator = document.CreateNavigator();

            string xPath = String.Format("/Holidays/Holiday[@Location='{0}']", location);
            XPathNodeIterator nodes = navigator.Select(xPath);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Holidays>");
            while (nodes.MoveNext())
            {
                sb.AppendLine(nodes.Current.OuterXml);
            }
            sb.AppendLine("</Holidays>");

            return HolidayCollectionXmlPersistenceHelper.ObjectFromXml(sb.ToString());
        }


        #region Custom Events
        public static CustomEventList CreateCustomEvents()
        {
            return CreateCustomEvents(false);
        }
        public static CustomEventList CreateCustomEvents(bool resourceSharing)
        {
            CustomEventList events = new CustomEventList();
            PopulateRentals(events, resourceSharing);
            PopulateMaintenance(events, resourceSharing);
            return events;
        }
        static void PopulateRentals(CustomEventList events, bool resourceSharing)
        {
            string[] customers = new string[] { "Mr.Brown", "Mr.White", "Mrs.Black", "Mr.Green" };
            string[] locations = new string[] { "city", "out of town" };
            int count = 20;
            for (int i = 0; i < count; i++)
                events.Add(CreateRental(customers, locations, resourceSharing));
            CreatePriorityRentals(events, resourceSharing, customers, locations);
        }
        static void CreatePriorityRentals(CustomEventList events, bool resourceSharing, string[] customers, string[] locations)
        {
            List<DateTime> priorityDates = new List<DateTime>();
            for (int i = -5; i < 3; i++)
                priorityDates.Add(baseDate.AddDays(i));

            int[] startHourSectors = new int[] { 11 };
            for (int resourceId = 0; resourceId < ResourceCount; resourceId++)
            {
                foreach (DateTime priorityDate in priorityDates)
                    foreach (int startHourSector in startHourSectors)
                        events.Add(CreateRental(resourceId, customers, locations, resourceSharing, priorityDate, startHourSector));
            }
        }
        static CustomEvent CreateRental(int resourceId, string[] customers, string[] locations, bool resourceSharing, DateTime baseDate, int startHourSector)
        {
            Random rnd = SchedulerDemoUtils.RandomInstance;

            CustomEvent result = new CustomEvent();
            result.Subject = customers[rnd.Next(0, customers.Length)];
            result.Location = locations[rnd.Next(0, locations.Length)];
            result.Description = "Rent this car";

            TimeSpan offset = TimeSpan.FromHours(rnd.Next(startHourSector, startHourSector + 4));
            result.StartTime = baseDate + offset;
            result.EndTime = result.StartTime + TimeSpan.FromHours(rnd.Next(1, 4));
            result.OwnerId = CalculateResourceIdOrIds(resourceSharing, resourceId);
            result.Status = 2;
            result.Label = rnd.Next(0, 7);
            result.Id = Guid.NewGuid();
            AddEventAdditionalInfo(result);
            return result;
        }
        static CustomEvent CreateRental(string[] customers, string[] locations, bool resourceSharing)
        {
            Random rnd = SchedulerDemoUtils.RandomInstance;

            CustomEvent result = new CustomEvent();
            result.Subject = customers[rnd.Next(0, customers.Length)];
            result.Location = locations[rnd.Next(0, locations.Length)];
            result.Description = "Rent this car";

            int rangeInDays = 7;
            int rangeInHours = 18;
            TimeSpan offset = TimeSpan.FromDays(rnd.Next(0, rangeInDays)) + TimeSpan.FromHours(rnd.Next(0, rangeInHours));
            result.StartTime = baseDate + offset;
            result.EndTime = result.StartTime + TimeSpan.FromHours(rnd.Next(1, rangeInHours));
            result.OwnerId = CalculateResourceIdOrIds(resourceSharing);
            result.Status = 2;
            result.Label = rnd.Next(0, 7);
            result.Id = Guid.NewGuid();
            AddEventAdditionalInfo(result);
            return result;
        }
        static void PopulateMaintenance(CustomEventList events, bool resourceSharing)
        {
            CustomEvent wash = new CustomEvent();
            wash.Subject = "Wash";
            wash.Description = "Wash this car in the garage";
            wash.Location = "Garage";
            wash.StartTime = baseDate + TimeSpan.FromHours(7);
            wash.EndTime = baseDate + TimeSpan.FromHours(8);
            wash.OwnerId = CalculateResourceIdOrIds(resourceSharing);
            wash.Status = 1;
            wash.Label = 2;
            wash.EventType = 1;
            wash.RecurrenceInfo = @"<RecurrenceInfo AllDay=""False"" DayNumber=""1"" DayOfMonth=""0"" WeekDays=""42"" Id=""51c81018-53fa-4d10-925f-2ed7f8408c75"" Month=""12"" OccurenceCount=""19"" Periodicity=""1"" Range=""2"" Start=""7/11/2008 7:00:00"" End=""8/24/2008 1:00:00"" Type=""1"" />";
            wash.Id = Guid.NewGuid();
            AddEventAdditionalInfo(wash);
            events.Add(wash);
        }
        static void AddEventAdditionalInfo(CustomEvent ev)
        {
            string[] info = new string[] {
                "Email: info{0}@wash_garage.com",
                "cellular: +530145961{0}",
                "Address: WA Seattle {0} - 24th Ave. S.Suite 4B phone: (206) 555-4{0}",
                "Contact: Address: OR Elgin City Center Plaza {0} Main St.",
                "Phone: (262) 946-9{0}; w ({0}) 723-2678 x22, cell (253) 713-0{0}, fax (361) 733-2{0}" 
        };
            Random rnd = SchedulerDemoUtils.RandomInstance;

            ev.Price = rnd.Next(5, 100);

            string infoTemplate = info[rnd.Next(0, info.Length)];
            Char ch = Char.Parse(rnd.Next(1, 9).ToString());
            ev.ContactInfo = string.Format(infoTemplate, new String(ch, 3));
        }
        static object CalculateResourceIdOrIds(bool resourceSharing)
        {
            if (resourceSharing)
            {
                List<int> resourceIdList = GetRandomResourceIdSequence();
                return GenerateResourceIdsString(resourceIdList.ToArray());
            }
            else
                return GetRandomResourceId();
        }
        static object CalculateResourceIdOrIds(bool resourceSharing, int resourceId)
        {
            if (resourceSharing)
            {
                List<int> resourceIdList = new List<int>();
                resourceIdList.Add(resourceId);
                return GenerateResourceIdsString(resourceIdList.ToArray());
            }
            else
                return resourceId;
        }
        private static List<int> GetRandomResourceIdSequence()
        {
            int expectedCount = GetRandomResourceId();
            List<int> result = new List<int>();
            for (int i = 0; i < expectedCount; i++)
            {
                int tryCounter = 0;
                while (true)
                {
                    int id = GetRandomResourceId();
                    if (result.IndexOf(id) == -1)
                    {
                        result.Add(id);
                        break;
                    }
                    if (tryCounter > 10)
                        break;
                    tryCounter++;
                }
            }
            return result;
        }
        static int GetRandomResourceId()
        {
            return SchedulerDemoUtils.RandomInstance.Next(1, 6);
        }
        static string GenerateResourceIdsString(int[] resourceIds)
        {
            ResourceIdCollection resourceIdCollection = new ResourceIdCollection();
            int count = resourceIds.Length;
            for (int i = 0; i < count; i++)
                resourceIdCollection.Add(resourceIds[i]);
            AppointmentResourceIdCollectionXmlPersistenceHelper helper = new AppointmentResourceIdCollectionXmlPersistenceHelper(resourceIdCollection);
            return helper.ToXml();
        }
        #endregion

        #region Custom Resources
        const int ResourceCount = 5;
        public static CustomResourceList CreateCustomResurces()
        {
            CustomResourceList resources = new CustomResourceList();
            resources.Add(CreateCustomResource(1, "SL500 Roadster"));
            resources.Add(CreateCustomResource(2, "CLK55 AMG Cabriolet"));
            resources.Add(CreateCustomResource(3, "C230 Kompressor Sport Coupe"));
            resources.Add(CreateCustomResource(4, "530i"));
            resources.Add(CreateCustomResource(5, "Corniche"));
            return resources;
        }
        static CustomResource CreateCustomResource(int id, string caption)
        {
            CustomResource result = new CustomResource();
            result.Id = id;
            result.Caption = caption;
            return result;
        }
        #endregion
    }

}