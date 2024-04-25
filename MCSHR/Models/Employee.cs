using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCSHR.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Employee Name Is Required Field"), MaxLength(250, ErrorMessage = "Maximum length for the Name is 250 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Employee Address Is Required Field"), MaxLength(500, ErrorMessage = "Maximum length for the Address is 500 characters")]
        public string Address { get; set; }

        [Required, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Employee Graduation Is Required Field"), MaxLength(250, ErrorMessage = "Maximum length for the Graduation is 250 characters")]
        public string Graduation { get; set; }

        [Required(ErrorMessage = "Employee Payroll Type Is Required Field")]
        public EmployeeTypes Emp_Type { get; set; }

        public bool Assurance { get; set; }

        [Required, Column(TypeName = "Money"), Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Salary { get; set; } = 0;

        [NotMapped]
        public decimal OvertimeHourRate { 
            get { 
                switch (Emp_Type)
                {
                    case EmployeeTypes.Monthly:
                        return Salary / 160;

                    case EmployeeTypes.Hourly:
                        return Salary * 3 / 16;

                    case EmployeeTypes.FreeLancer:
                        return Salary * 1.5M;

                    default: return 0;
                }
            } 
        }
    }
}
