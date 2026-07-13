using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataTableForPagingSortingSearchingDemo.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Employee ID is required")]
       
        public int Id { get; set; }


        [Required(ErrorMessage = "First Name is required")]
        [StringLength(10, ErrorMessage = "First Name must be less than 10 characters")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, ErrorMessage = "Last Name must be less than 20 characters")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Title is required")]
        [StringLength(30, ErrorMessage = "Title must be less than 30 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "Hire Date is required")]
        public DateTime HireDate { get; set; }


        public int CountryID { get; set; }

        [ForeignKey("CountryID")]
        public virtual Country Countries { get; set; }


        public int DeptID { get; set; }

        [ForeignKey("DeptID")]
        public virtual Department Departments { get; set; }


        [StringLength(500, ErrorMessage = "Notes must be less than 500 characters")]
        public string Notes { get; set; }
    }
}
