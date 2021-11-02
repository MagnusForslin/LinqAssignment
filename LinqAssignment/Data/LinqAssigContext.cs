using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LinqAssignment.Models;


namespace LinqAssignment.Data
{
    public class LinqAssigContext: DbContext
    {
        public LinqAssigContext() : base("LinqAssigConnection")
        { }

        public DbSet<Manufacturer> Manufacturers { get; set; }  
        public DbSet<Car> Cars { get; set; }    
    }
}