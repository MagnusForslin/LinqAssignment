using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using LinqAssignment.Models;
using LinqAssignment.Models.ViewModels;
using System.Diagnostics;
using LinqAssignment.Data;

namespace LinqAssignment.Controllers
{
    public class CarInfoController : Controller
    {
        LinqAssigContext _db = new LinqAssigContext();

        public ActionResult GetCarInfo()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            
            var cars = Car.ProcessCars("Files/fuel.csv");
            Session["carList"] = cars;

            
            var manufacturers = Manufacturer.ProcessManufacturers("Files/manufacturers.csv");
            Session["manufacturersList"] = manufacturers;

            return RedirectToAction("Queries");
        }

        public ActionResult Queries()
        {
            return View();
        }

        // Queries

        // Most fuel efficient car - (extension) method syntax
        public ActionResult MostEfficient()
        {
            List<Car> cars = (List<Car>)Session["carList"];

            //_db.Database.Log = sql => Debug.Write(sql);

            var query = _db.Cars
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .Take(10);

            return View(query);
        }

        // Most fuel efficient car - query syntax
        public ActionResult MostEfficientQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            
            var query = (from c in _db.Cars
                         orderby c.Combined descending
                         select c
                        );

            return View(query);
        }

        // Filtering with Where and FirstOrDefault - (extension) method syntax
        public ActionResult WhereAndFirst()
        {
            List<Car> cars = (List<Car>)Session["carList"];

            var query = _db.Cars
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Manufacturer == "Mercedez-Benz")
                .Where(c => c.Year == 2016)
                .FirstOrDefault();
           

            return View(query);
        }

        // Filtering with Where and FirstOrDefault - query syntax
        public ActionResult WhereAndFirstQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];

            var query = (from c in _db.Cars
                         orderby c.Combined descending
                         where (c.Manufacturer == "BMW")
                         select c
                       );

            return View(query);
        }

       

        // Where condition with projected 'Select' - new objects with fewer properties
        // Method syntax
        public ActionResult ProjectedSelect()
        {
            List<Car> cars = (List<Car>)Session["carList"];

            var query = _db.Cars
               .OrderByDescending(c => c.Combined)
               .Select(c => new ProjectedCarsVM()
               {
                   Manufacturer = c.Manufacturer,
                   Name = c.Name,
                   Combined = c.Combined
               });


            return View(query);
        }

        // Where condition with projected 'Select' - new objects with fewer properties
        // Query syntax
        public ActionResult ProjectedSelectQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            var query = (from c in _db.Cars
                         orderby c.Combined descending, c.Manufacturer
                         select new ProjectedCarsVM()
                         {
                             Manufacturer = c.Manufacturer,
                             Name = c.Name,
                             Combined = c.Combined
                         });           


            return View(query);
        }

        // SelectMany in method syntax
        // Flattening of sequences in a sequence to a single collection
        // E.g. - producing a list of all characters in all car names
        // IEnumerable<char> characters is the same as a string
        public ActionResult SelectMany()
        {
            List<Car> cars = (List<Car>)Session["carList"];

            var query = _db.Cars
                .Select(c => c.Name).ToList();

            foreach(var car in query)
            {
                Car car2 = new Car();
            }  
                
            return View(query);
        }

        // Join tables - query syntax
        public ActionResult JoinTablesQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

            
            return View();
        }

        // Join tables - method syntax
        public ActionResult JoinTables()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

           
            return View();
        }

        // Group by - query syntax
        public ActionResult GroupingQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

            
            return View();
        }

        // Group by - method syntax
        public ActionResult Grouping()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

            

            return View();
        }

        // combined group and join (get 2 properties from manufacturer) - query syntax
        public ActionResult GroupJoinQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

            
            return View();
        }

        // Combined group and join (get 2 properties from manufacturer) - method syntax
        // Equality of keys
        public ActionResult GroupJoin()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

           
            return View();

        }

        // Top 3 fuel efficient cars by country (advanced)
        // GroupJoin + SelectMany (flattening sequence) - query syntax
        public ActionResult GroupJoinSelectManyQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

            

            return View();
        }

        // Top 3 fuel least efficient cars by country (advanced)
        // GroupJoin + SelectMany (flattening sequence) - method syntax
        public ActionResult GroupJoinSelectMany()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];


            return View();
        }

        // Aggregating data (order by most efficient car) - query syntax
        public ActionResult AggregatingDataQ()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];
        
            
            return View();
        }

        // Aggregating data (order by most efficient car) - method syntax
        // Use class 'CarStatistics' to avoid iterating through list 3 times
        public ActionResult AggregatingData()
        {
            List<Car> cars = (List<Car>)Session["carList"];
            List<Manufacturer> manufacturers
                = (List<Manufacturer>)Session["manufacturersList"];

            return View();
        }
    }
}