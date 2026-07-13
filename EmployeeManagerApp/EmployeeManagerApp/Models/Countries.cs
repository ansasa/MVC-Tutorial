using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagerApp.Models
{
    public class Countries
    {
        [Key]
        public int CountryID { get; set; }
        public string Name { get; set; }
        
    }
}
