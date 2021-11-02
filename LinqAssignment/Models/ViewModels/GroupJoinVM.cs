using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqAssignment.Models;

namespace LinqAssignment.Models.ViewModels
{
    public class GroupJoinVM
    {
        public Manufacturer CarManufacturer { get; set; }
        public List<Car> CarBrands { get; set; }
    }
}