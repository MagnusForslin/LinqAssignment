using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LinqAssignment.Data;
using LinqAssignment.Models;

namespace LinqAssignment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        LinqAssigContext _db = new LinqAssigContext();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _db.Database.Log = sql => Debug.Write(sql);

            Database.SetInitializer
                (new DropCreateDatabaseIfModelChanges<LinqAssigContext>());

            if (!_db.Cars.Any())
            {
                InsertCars();
            }
            if (!_db.Manufacturers.Any())
            {
                InsertManufacturers();
            }

        }

        private void InsertCars()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            List<Car> cars = Car.ProcessCars("Files/fuel.csv");

            _db.Cars.AddRange(cars);  
            _db.SaveChanges();  
        }

        private void InsertManufacturers()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            List<Manufacturer> manufacturer = Manufacturer.ProcessManufacturers("Files/manufacturers.csv");

            _db.Manufacturers.AddRange(manufacturer);
            _db.SaveChanges();  
        }
    }
}
