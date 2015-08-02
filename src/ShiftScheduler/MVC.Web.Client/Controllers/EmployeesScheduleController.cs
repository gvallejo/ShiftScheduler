using Scheduler.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Web.Client.Controllers
{
    public class EmployeesScheduleController : Controller
    {
        // GET: EmployeesSchedule
        public ActionResult Index()
        {
            IDataAccess dataAccess = new Scheduler.Core.DataAccess.Mocked.MockedDataAccess(null, null);
            return View(dataAccess.GetEmployees());
        }

        // GET: EmployeesSchedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeesSchedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeesSchedule/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesSchedule/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeesSchedule/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeesSchedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeesSchedule/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
